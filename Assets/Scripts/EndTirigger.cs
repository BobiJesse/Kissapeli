using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTirigger : MonoBehaviour
{
    public CanvasGroup sceneTransition; // Reference to the CanvasGroup for fading out the scene

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine(SceneFadeOut());
            PlayerScript.instance.playerHasControl = false; // Disable player control
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.LogWarning("End Trigger Left!");
        }
    }

    IEnumerator SceneFadeOut()
    {
        while (sceneTransition.alpha < 1)
        {
            sceneTransition.alpha += Time.deltaTime / 2;
            yield return null;
        }
        SceneManager.LoadScene(7); // <-- Replace with the scene index or name with ending
        yield return new WaitForSeconds(0.5f);
        Destroy(PlayerScript.instance.gameObject); // Destroy the player object
        sceneTransition.gameObject.SetActive(false); // Disable the CanvasGroup after fading out
        yield return null;
    }

    IEnumerator SceneFadeIn()
    {
        while (sceneTransition.alpha > 0)
        {
            sceneTransition.alpha -= Time.deltaTime / 2;
            yield return null;
        }
        PlayerScript.instance.playerHasControl = true; // Re-enable player control
    }

}
