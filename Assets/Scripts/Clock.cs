using UnityEngine;
using TMPro;

public class Clock : MonoBehaviour
{
    public static Clock instance;

    [SerializeField] 
    private TMP_Text timeDisplay; // UI Text to display the time

    public float currentTime; // Track the remaining time

    public float startingTime = 18 * 60; // Starting time in minutes (6 PM)

    public float oneCatDuration = 10; // Duration for one cat in minutes (10 minutes)

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject); // Ensure only one instance exists
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentTime = startingTime;
        timeDisplay.text = FormatTime(currentTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CatDone()
    {
        if (!GameManager.instance.speedrunMode)
        {
            currentTime += oneCatDuration; // Add the duration of one cat to the current time
            timeDisplay.text = FormatTime(currentTime);
        }
    }
    public void CatDone(float duration)
    {
        if (!GameManager.instance.speedrunMode)
        {
            currentTime += duration; // Add the specified duration to the current time
            timeDisplay.text = FormatTime(currentTime);
        }
    }

    private string FormatTime(float time)
    {
        int hours = Mathf.FloorToInt(time / 60);
        int minutes = Mathf.FloorToInt(time % 60);
        return string.Format("{0:00}:{1:00}", hours, minutes);
    }

    public float GetTimeLeft()
    {
        return (21 * 60 - currentTime) * 60; // Calculate the time left until 9 PM (21:00);
    }
}
