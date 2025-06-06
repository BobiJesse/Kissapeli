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
        GameManager.instance.EndGame(); // Call the EndGame method to handle game ending logic
        if (Clock.instance != null)
        {
            Destroy(Clock.instance.gameObject); // Destroy the clock object

        }
        if (Timer.instance != null)
        {
            Destroy(Timer.instance.gameObject); // Destroy the timer object
        }

        SceneManager.LoadScene("Ending"); // <-- Replace with the scene index or name with ending
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
