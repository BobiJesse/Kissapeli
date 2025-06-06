using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Unity.Mathematics.math;
public class Match3Skin : MonoBehaviour
{
    public bool IsGameActive => IsBusy || game.PossibleMove.IsValid;
    public bool IsBusy => busyDuration > 0f;

    public bool isGameCompleted = false; // Flag to check if the game is completed

    public void DoAutomaticMove() => DoMove(game.PossibleMove);

    [SerializeField]
    Cat[] catPrefabs;

    [SerializeField]
    public Match3Game game;

    Grid2D<Cat> tiles;
    [SerializeField]
    SwapTiles tileSwapper;

    float busyDuration;
    float2 tileOffset;

    [SerializeField, Range(0.1f, 1f)]
    float dragThreshold = 0.5f;

    [SerializeField, Range(0.1f, 20f)]
    float dropSpeed = 8f;

    [SerializeField, Range(0f, 10f)]
    float newDropOffset = 2f;

    [SerializeField]
    TMP_Text movesText, gameOverText, totalScoreText;
    [SerializeField]
    GameObject gameOverPanel;

    private bool endedGame = false;

    //[SerializeField]
    //FloatingScore floatingScorePrefab;
    //float floatingScoreZ;

    public void StartGame()
    {
        busyDuration = 0f;
        totalScoreText.SetText("0");
        gameOverPanel.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        endedGame = false;
        game.StartNewGame();
        tileOffset = -0.5f * (float2)(game.Size - 1);
        if (tiles.IsInvalid)
        {
            tiles = new(game.Size);
        }
        else
        {
            for (int y = 0; y < tiles.Height; y++)
            {
                for (int x = 0; x < tiles.Width; x++)
                {
                    tiles[x, y].Despawn();
                    tiles[x, y] = null;
                }
            }
        }
        for (int y = 0; y < tiles.Height; y++)
        {
            for (int x = 0; x < tiles.Width; x++)
            {
                tiles[x, y] = SpawnTile(game[x, y], x, y);
            }
        }
        movesText.SetText("" + game.currentMoves);
    }

    Cat SpawnTile(TileState t, float x, float y) => catPrefabs[(int)t - 1].Spawn(new Vector3(x + tileOffset.x, y + tileOffset.y));

    public void DoStuff()
    {
        if (!isGameCompleted)
        {
            if (busyDuration > 0f)
            {
                tileSwapper.Update();
                busyDuration -= Time.deltaTime;
                if (busyDuration > 0f)
                {
                    return;
                }
            }
            if (game.HasMatches)
            {
                ProcessMatches();
            }
            else if (game.NeedsFilling)
            {
                DropTiles();
            }
            else if (!IsGameActive)
            {
                endedGame = true;
                EndGame();
            }
        }
    }

    void DoMove(Move move)
    {
        if (!isGameCompleted)
        {
            bool success = game.TryMove(move);
            Cat a = tiles[move.From], b = tiles[move.To];
            busyDuration = tileSwapper.Swap(a, b, !success);
            if (success)
            {
                tiles[move.From] = b;
                tiles[move.To] = a;
            }
        }
    }

    float2 ScreenToTileSpace(Vector3 screenPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        Vector3 p = ray.origin - ray.direction * (ray.origin.z / ray.direction.z);
        return float2(p.x - tileOffset.x + 0.5f, p.y - tileOffset.y + 0.5f);
    }

    public bool EvaluateDrag(Vector3 start, Vector3 end)
    {
        float2 a = ScreenToTileSpace(start), b = ScreenToTileSpace(end);
        var move = new Move((int2)floor(a), (b - a) switch
            {
                var d when d.x > dragThreshold => MoveDirection.Right,
                var d when d.x < -dragThreshold => MoveDirection.Left,
                var d when d.y > dragThreshold => MoveDirection.Up,
                var d when d.y < -dragThreshold => MoveDirection.Down,
                _ => MoveDirection.None
            }
        );
        if (move.IsValid && tiles.AreValidCoordinates(move.From) && tiles.AreValidCoordinates(move.To))
        {
            DoMove(move);
            return false;
        }
        return true;
    }
    void ProcessMatches()
    {
        game.ProcessMatches();

        for (int i = 0; i < game.ClearedTileCoordinates.Count; i++)
        {
            int2 c = game.ClearedTileCoordinates[i];
            busyDuration = Mathf.Max(tiles[c].Disappear(), busyDuration);
            tiles[c] = null;
        }
        for (int i = 0; i < game.Scores.Count; i++)
        {
            SingleScore score = game.Scores[i];
            //floatingScorePrefab.Show(
            //    new Vector3(
            //        score.position.x + tileOffset.x,
            //        score.position.y + tileOffset.y,
            //        floatingScoreZ
            //    ),
            //    score.value
            //);
            //floatingScoreZ = floatingScoreZ <= -0.02 ? 0f : floatingScoreZ - 0.001f;
        }
        totalScoreText.SetText("{0}", game.TotalScore);
        movesText.SetText("{0}", game.currentMoves);
    }

    void DropTiles()
    {
        game.DropTiles();

        for (int i = 0; i < game.DroppedTiles.Count; i++)
        {
            DropTiles drop = game.DroppedTiles[i];
            Cat tile;

            if (drop.fromY < tiles.Height)
            {
                tile = tiles[drop.coordinates.x, drop.fromY];
                //tile.transform.localPosition = new Vector3(drop.coordinates.x + tileOffset.x, drop.coordinates.y + tileOffset.y);
            }
            else
            {
                tile = SpawnTile(
                    game[drop.coordinates], drop.coordinates.x, drop.fromY + newDropOffset);
            }
            tiles[drop.coordinates] = tile;
            busyDuration = Mathf.Max(tile.Fall(drop.coordinates.y + tileOffset.y, dropSpeed), busyDuration);
        }
    }

    private void FixedUpdate()
    {
        if (game.currentMoves <= 0 && !IsBusy && !endedGame)
        {
            endedGame = true;
            EndGame();
        }
    }

    void EndGame()
    {
        isGameCompleted = true;
        gameOverPanel.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        gameOverText.SetText("Game Over! Your score: {0}", game.TotalScore);
    }

    public void ExitMinigame()
    {
        //exit button call
        SceneManager.UnloadSceneAsync("Match3");
        Clock.instance.CatDone(); // Notify clock that the minigame is done
        if (GameEvents.OnMinigameExit != null)
        {
            GameEvents.OnMinigameExit();
        }
    }
}

public enum TileState
{
    None, A, B, C, D, E, F, G
}

