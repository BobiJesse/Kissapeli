using UnityEngine;
using UnityEngine.UI;

public class ProgressTracker : MonoBehaviour
{
    public Transform player;
    public Transform endPoint;
    public float startX;      // starting x position
    public float endX;        // ending x position
    public Slider progressBar; // reference to UI slider

    void Start()
    {
        startX = player.position.x;
        endX = endPoint.position.x;
    }

    void Update()
    {
        float currentX = player.position.x;
        float progress = Mathf.InverseLerp(startX, endX, currentX); // normalizes to 0-1
        progressBar.value = progress;
    }
}