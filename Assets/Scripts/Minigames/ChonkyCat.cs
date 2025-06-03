using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChonkyCat : MonoBehaviour
{
    public float liftMeter;
    public Slider pickUpSlider;
    public GameObject gameScreen;
    public GameObject winScreen;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameScreen.SetActive(true);
        winScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            liftMeter++;
        }

        if(liftMeter > 0)
        {
            liftMeter -= Time.deltaTime * 3;
        }

        pickUpSlider.value = liftMeter;

        if(pickUpSlider.value >= 20)
        {
            gameScreen.SetActive(false);
            winScreen.SetActive(true);
        }
    }

    public void StopPlaying()
    {
        SceneManager.UnloadSceneAsync("ChonkyCat");

        if (GameEvents.OnMinigameExit != null)
        {
            GameEvents.OnMinigameExit();
        }
    }
}
