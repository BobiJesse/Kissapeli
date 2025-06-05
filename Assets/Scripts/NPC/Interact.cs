using UnityEngine;
using UnityEngine.SceneManagement;

public class Interact : MonoBehaviour
{
    public static Interact closestCat;
    public string minigameSceneName;
    public GameObject toolTip;
    public GameObject mainCam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        toolTip.SetActive(false);
        mainCam = GameObject.Find("Main Camera");
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
            PlayerScript.instance.closeToCat = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            toolTip.SetActive(false);
            closestCat = null;
            PlayerScript.instance.closeToCat = false;
        }
    }

    public void StartMinigame()
    {
        mainCam.SetActive(false);
        SceneManager.LoadScene(minigameSceneName, LoadSceneMode.Additive);
        gameObject.SetActive(false);
    }
    public void StartDialogue()
    {
        mainCam.SetActive(false);
        GameObject.Find("DialogueSystem").gameObject.transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("DialogueSystem").gameObject.transform.GetChild(0).GetComponent<BasicInkExample>().StartStory();
    }
}
