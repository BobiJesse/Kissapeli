using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Singleton instance
    public bool paused = false; // Boolean to check if the game is paused

    public int catsHelped = 1;
    public int catsTalkedTo = 1;

    public PlayerScript playerScript;
    public GameObject pausePanel;

    public GameObject clock;
    public GameObject timer;

    public GameObject back1;
    public GameObject back2;

    public bool speedrunMode = false; // Boolean to check if the game is in speedrun mode

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Ensure this GameManager persists across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (speedrunMode)
        {
            clock.SetActive(false);
            timer.SetActive(true);
            back1.SetActive(false);
            back2.SetActive(true);
        }
        else
        {
            clock.SetActive(true);
            timer.SetActive(false);
            back1.SetActive(true);
            back2.SetActive(false);
        }
    }

    //Update is called once per frame
    void Update()
    {
        if (speedrunMode || Clock.instance.currentTime >= 20 * 60)
        {
            if (back1 != null)
            {
                back1.SetActive(false);
            }
            if (back2 != null)
            {
                back2.SetActive(true);
            }
        }
    }

}
