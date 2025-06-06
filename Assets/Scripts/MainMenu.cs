using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{

    public GameObject menuPanel;
    public GameObject levelPanel;
    public GameObject optionsPanel;
    public GameObject exitPanel;

    public Button continueButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayGame()
    {
        SoundManager.PlaySound("menuSelect");
        menuPanel.gameObject.SetActive(false);
        levelPanel.gameObject.SetActive(true);
    }
    public void CancelLoad()
    {
        SoundManager.PlaySound("menuHover");
        menuPanel.gameObject.SetActive(true);
        levelPanel.gameObject.SetActive(false);
    }
    public void StartGame()
    {
        SceneManager.LoadSceneAsync(1); // Game intro scene index here <<--------------------------------------
        Destroy(gameObject);
    }

    public void StartSpeedrunGame()
    {
        SceneManager.LoadSceneAsync(2); // Game intro scene index here <<--------------------------------------
        Destroy(gameObject);
    }

    public void OpenOptions()
    {
        SoundManager.PlaySound("menuSelect");
        menuPanel.gameObject.SetActive(false);
        optionsPanel.gameObject.SetActive(true);
    }
    public void OpenCredits()
    {
        SoundManager.PlaySound("menuSelect");
        menuPanel.gameObject.SetActive(false);
        SceneManager.LoadSceneAsync("Credits"); // Credits scene index here <<--------------------------------------
    }
    public void CloseOptions()
    {
        SoundManager.PlaySound("menuHover");
        menuPanel.gameObject.SetActive(true);
        optionsPanel.gameObject.SetActive(false);
    }
    
    public void QuitGame()
    {
        SoundManager.PlaySound("menuHover");
        menuPanel.gameObject.SetActive(false);
        exitPanel.gameObject.SetActive(true);
    }
    public void CancelExit()
    {
        SoundManager.PlaySound("menuHover");
        menuPanel.gameObject.SetActive(true);
        exitPanel.gameObject.SetActive(false);
    }
    public void ConfirmExit()
    {
        SoundManager.PlaySound("menuSelect");
        Application.Quit();
    }
}
