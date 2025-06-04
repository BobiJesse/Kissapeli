using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BoxShuffleMinigame : MonoBehaviour
{
    [Header("References")]
    public Button[] boxButtons;         // UI buttons to shuffle
    public Transform[] slotPositions;   // Fixed slot positions to move buttons to
    public Image catIcon;

    [Header("Settings")]
    public float moveDuration = 0.5f;
    public int shuffleCount = 5;

    private int catIndex;
    private bool playerCanClick = false;

    public GameObject winScreen;
    public GameObject gameScreen;
    public GameObject loseScreen;

    void Start()
    {
        AssignButtonListeners();
        StartCoroutine(StartGame());
    }

    private void AssignButtonListeners()
    {
        for (int i = 0; i < boxButtons.Length; i++)
        {
            Button btn = boxButtons[i]; // capture the actual reference!
            btn.onClick.AddListener(() => OnBoxClicked(btn));
        }
    }

    private IEnumerator StartGame()
    {
        // Initially place buttons in order
        for (int i = 0; i < boxButtons.Length; i++)
        {
            boxButtons[i].transform.position = slotPositions[i].position;
        }

        // Randomly select a cat-hiding box
        catIndex = Random.Range(0, boxButtons.Length);
        catIcon.transform.position = boxButtons[catIndex].transform.position;
        catIcon.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);
        catIcon.gameObject.SetActive(false);

        yield return StartCoroutine(ShuffleBoxes());

        playerCanClick = true;
    }

    private IEnumerator ShuffleBoxes()
    {
        for (int i = 0; i < shuffleCount; i++)
        {
            int a = Random.Range(0, boxButtons.Length);
            int b;
            do
            {
                b = Random.Range(0, boxButtons.Length);
            } while (b == a);

            // Swap slot targets (just visual positions)
            yield return StartCoroutine(SwapButtons(a, b));

            // Swap button references
            Button temp = boxButtons[a];
            boxButtons[a] = boxButtons[b];
            boxButtons[b] = temp;

            // Update cat index
            if (catIndex == a) catIndex = b;
            else if (catIndex == b) catIndex = a;
        }
    }

    private IEnumerator SwapButtons(int i, int j)
    {
        Transform t1 = boxButtons[i].transform;
        Transform t2 = boxButtons[j].transform;

        Vector3 p1 = t1.position;
        Vector3 p2 = t2.position;

        float elapsed = 0f;
        while (elapsed < moveDuration)
        {
            float t = elapsed / moveDuration;
            t1.position = Vector3.Lerp(p1, p2, t);
            t2.position = Vector3.Lerp(p2, p1, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        t1.position = p2;
        t2.position = p1;
    }

    private void OnBoxClicked(Button clickedButton)
    {
        if (!playerCanClick) return;

        int clickedIndex = System.Array.IndexOf(boxButtons, clickedButton);

        if (clickedIndex == catIndex)
        {
            gameScreen.SetActive(false);
            winScreen.SetActive(true);
        }
        else
        {
            gameScreen.SetActive(false);
            loseScreen.SetActive(true);
        }

        playerCanClick = false;
    }

    public void ResetGame()
    {
        SceneManager.UnloadSceneAsync("BoxGame");
        SceneManager.LoadScene("BoxGame", LoadSceneMode.Additive);
    }

    public void EndMinigame()
    {
        SceneManager.UnloadSceneAsync("BoxGame");

        if (GameEvents.OnMinigameExit != null)
        {
            GameEvents.OnMinigameExit();
        }
    }
}
