using UnityEngine;
using UnityEngine.SceneManagement;

public class Interact : MonoBehaviour
{
    public static Interact closestCat;
    public PlayerScript PlayerScript;
    public string minigameSceneName;
    public GameObject toolTip;
    public GameObject mainCam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        toolTip.SetActive(false);
        PlayerScript = GameObject.Find("Player").GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            toolTip.SetActive(true);
            closestCat = this;
            PlayerScript.closeToCat = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            toolTip.SetActive(false);
            closestCat = null;
            PlayerScript.closeToCat = false;
        }
    }

    public void StartMinigame()
    {
        mainCam.SetActive(false);
        SceneManager.LoadScene(minigameSceneName, LoadSceneMode.Additive);
    }
}
