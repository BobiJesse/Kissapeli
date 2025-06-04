using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject optionsPanel;
    public GameObject exitPanel;
    private GameManager gameManager;

    public Button loadButton;

    private void Start()
    {
        gameManager = GameManager.instance;
    }

    private void FixedUpdate()
    {

    }

    public void Continue()
    {
        //SoundManager.PlaySound("menuSelect");
        gameManager.paused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1;
        PlayerScript.instance.playerHasControl = true;
    }

    public void OpenOptions()
    {
        //SoundManager.PlaySound("menuHover");
        pausePanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        //SoundManager.PlaySound("menuHover");
        optionsPanel.SetActive(false);
        pausePanel.SetActive(true);
    }

    public void ExitGame()
    {
        //SoundManager.PlaySound("menuHover");
        pausePanel.SetActive(false);
        exitPanel.SetActive(true);
    }

    public void CancelExit()
    {
        SoundManager.PlaySound("menuHover");
        exitPanel.SetActive(false);
        pausePanel.SetActive(true);
    }

    public void ConfirmExit()
    {
        //SoundManager.PlaySound("menuSelect");
        gameManager.paused = false;
        pausePanel.SetActive(false);
        Destroy(PlayerScript.instance.gameObject);
        Destroy(GameManager.instance.gameObject);
        Destroy(SoundManager.instance.gameObject);
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

}
