using UnityEngine;
using UnityEngine.SceneManagement;

public class DoNotTheCat : MonoBehaviour
{
    public GameObject gameScreen;
    public GameObject loseScreen;
    public GameObject winScreen;

    public float timer;
    private bool gameIsOn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameScreen.SetActive(true);
        loseScreen.SetActive(false);
        winScreen.SetActive(false);
        timer = 10;
        gameIsOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameIsOn)
        {
            timer -= Time.deltaTime;
        }

        if (timer < 0)
        {
            gameScreen.SetActive(false);
            winScreen.SetActive(true);
        }
    }

    public void PetTheCat()
    {
        gameIsOn = false;
        timer = 10;
        gameScreen.SetActive(false);
        loseScreen.SetActive(true);
    }

    public void ResetGame()
    {
        gameIsOn = true;
        loseScreen.SetActive(false);
        gameScreen.SetActive(true);
    }

    public void StopPlaying()
    {
        SceneManager.UnloadSceneAsync("DoNotTheCat");

        if (GameEvents.OnMinigameExit != null)
        {
            GameEvents.OnMinigameExit();
        }
    }
}
