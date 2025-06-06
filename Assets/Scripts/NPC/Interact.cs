using UnityEngine;
using UnityEngine.SceneManagement;

public class Interact : MonoBehaviour
{
    public static Interact closestCat;
    public static List<Interact> overlappingCats = new List<Interact>();
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
            overlappingCats.Add(this);
            closestCat = this;
            PlayerScript.instance.closeToCat = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            toolTip.SetActive(false);
            overlappingCats.Remove(this);

            // Update closestCat to another overlapping cat (if any)
            if (overlappingCats.Count > 0)
            {
                closestCat = overlappingCats[overlappingCats.Count - 1];
            }
            else
            {
                closestCat = null;
                PlayerScript.instance.closeToCat = false;
            }
        }
    }

    public void StartMinigame()
    {
        mainCam.SetActive(false);
        SceneManager.LoadScene(minigameSceneName, LoadSceneMode.Additive);
        gameObject.transform.parent.gameObject.SetActive(false);
        Debug.Log("Trigger poistettu");
    }
    public void StartDialogue()
    {
        mainCam.SetActive(false);
        GameObject.Find("DialogueSystem").gameObject.transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("DialogueSystem").gameObject.transform.GetChild(0).GetComponent<BasicInkExample>().StartStory();
    }
}
