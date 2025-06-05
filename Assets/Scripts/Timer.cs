using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public static Timer instance;
    [SerializeField] 
    private float totalTime;  // Total time in seconds

    [SerializeField] 
    private TMP_Text timeDisplay; // UI Text to display the time

    public float remainingTime; // Track the remaining time


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keep this instance alive across scenes
        }
        else
        {
            Destroy(gameObject); // Ensure only one instance exists
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        remainingTime = totalTime;
        timeDisplay.text = FormatTime(remainingTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            timeDisplay.text = FormatTime(remainingTime);
        }
        else
        {
            // Handle when the timer reaches zero (e.g., trigger an event)
            //timeDisplay.text = "Time's up!";
            Debug.Log("Timer finished!");
        }
    }
    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void ResetTimer()
    {
        remainingTime = totalTime;
        timeDisplay.text = FormatTime(remainingTime);
    }

    public void AddTime(int amount)
    {
        remainingTime += amount; // Add amount of seconds to the timer
        timeDisplay.text = FormatTime(remainingTime);
    }

    public void RemoveTime(int amount)
    {
        remainingTime -= amount; // Remove amount of seconds from the timer
        timeDisplay.text = FormatTime(remainingTime);
    }
    
}
