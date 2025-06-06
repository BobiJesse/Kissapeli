using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class Timer : MonoBehaviour
{
    public static Timer instance;
    [SerializeField] 
    private float totalTime;  // Total time in seconds

    [SerializeField] 
    private TMP_Text timeDisplay; // UI Text to display the time

    public float remainingTime; // Track the remaining time

    private bool timeIsUp = false; // Flag to check if time is up
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
            timeIsUp = true;
        }
        if (timeIsUp)
        {
            StartCoroutine(SceneFadeOut());
            PlayerScript.instance.playerHasControl = false; // Disable player control
            timeIsUp = false; // Reset the flag
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

    IEnumerator SceneFadeOut()
    {
        while (GameManager.instance.sceneTransition.alpha < 1)
        {
            GameManager.instance.sceneTransition.alpha += Time.deltaTime / 2;
            yield return null;
        }
        GameManager.instance.EndGame(); // Call the EndGame method to handle game ending logic
        if (Clock.instance != null)
        {
            Destroy(Clock.instance.gameObject); // Destroy the clock object

        }
        SceneManager.LoadScene("Ending"); // <-- Replace with the scene index or name with ending
        Destroy(gameObject); // Destroy the timer object
        yield return null;
    }
}
