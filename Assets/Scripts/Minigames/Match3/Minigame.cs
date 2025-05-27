using JetBrains.Annotations;
using Unity.Mathematics;
using UnityEngine;

public class Minigame : MonoBehaviour
{
    [SerializeField]
    Match3Skin match3;
    [SerializeField]
    bool automaticPlay;

    Vector3 dragStart;
    bool isDragging;

    private void Awake() => match3.StartGame();

    // Update is called once per frame
    void Update()
    {
        if (match3.IsGameActive)
        {
            if (!match3.IsBusy)
            {
                HandleInput();
            }
            match3.DoStuff();
        }
    }

    void HandleInput()
    { 
        if(automaticPlay && match3.game.currentMoves > 0)
        {
            match3.DoAutomaticMove();
        }
        else if (!isDragging && Input.GetMouseButtonDown(0))
        {
            dragStart = Input.mousePosition;
            isDragging = true;
        }
        else if (isDragging && Input.GetMouseButton(0))
        {
            isDragging = match3.EvaluateDrag(dragStart, Input.mousePosition);
        }
        else
        {
            isDragging = false;
        }
    }

}
