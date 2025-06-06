using System.Collections;
using Ink.Parsed;
using TMPro;
using UnityEngine;
using UnityEngine.Windows;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed;
    public bool isMoving;
    public float typeSpeed;
    public GameObject nextButton;

    public GameObject[] listOfCats;

    public TextMeshProUGUI goodEnding;
    public TextMeshProUGUI neutralEnding;
    public TextMeshProUGUI badEnding;
    public TextMeshProUGUI nextButtonText;

    [Header("Ending Text Content")]
    [TextArea(2, 4)] public string[] goodEndingPages;
    [TextArea(2, 4)] public string[] neutralEndingPages;
    [TextArea(2, 4)] public string[] badEndingPages;

    private string[] currentPages;
    private int currentPageIndex = 0;
    private TextMeshProUGUI currentTextBox;

    public Animator animator;
    public bool facingRight = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        goodEnding.gameObject.SetActive(false);
        neutralEnding.gameObject.SetActive(false);
        badEnding.gameObject.SetActive(false);
        nextButton.SetActive(false);

        int catsHelped = GameManager.instance.catsHelped;
        //int catsHelped = 20;

        for (int i = 0; i < listOfCats.Length; i++)
        {
            listOfCats[i].SetActive(i < catsHelped - 1);
        }

        Invoke(nameof(MovePlayer), 0.5f);
        Invoke(nameof(StopMoving), 16.5f);

        // Choose ending text and box based on how many cats were helped
        if (catsHelped >= 0 && catsHelped < 10)
        {
            currentTextBox = badEnding;
            currentPages = badEndingPages;
        }
        else if (catsHelped >= 10 && catsHelped < 20)
        {
            currentTextBox = neutralEnding;
            currentPages = neutralEndingPages;
        }
        else
        {
            currentTextBox = goodEnding;
            currentPages = goodEndingPages;
        }

        currentTextBox.gameObject.SetActive(true);
        StartCoroutine(TypeText(currentPages[currentPageIndex]));

    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            rb.linearVelocity = new Vector2(-moveSpeed, rb.linearVelocity.y);
            FlipCharacter();
        }
        SetAnimation();

    }

    private void StopMoving()
    {
        isMoving = false;
    }

    private void MovePlayer()
    {
        isMoving = true;
    }

    private IEnumerator TypeText(string fullText)
    {
        currentTextBox.text = "";
        nextButton.SetActive(false);

        
        int catsHelped = GameManager.instance.catsHelped;
        //int catsHelped = 20;

        string processedText = fullText.Replace("{CAT_COUNT}", (catsHelped - 1).ToString());

        foreach (char c in processedText)
        {
            currentTextBox.text += c;
            yield return new WaitForSeconds(typeSpeed);
        }

        // Change button text if it's the last page
        if (currentPageIndex == currentPages.Length - 1)
        {
            nextButtonText.text = "Main Menu";
        }
        else
        {
            nextButtonText.text = "Next";
        }

        nextButton.SetActive(true);
    }

    public void OnNextPage()
    {
        currentPageIndex++;
        SoundManager.PlaySound("menuSelect");
        if (currentPageIndex < currentPages.Length)
        {
            StartCoroutine(TypeText(currentPages[currentPageIndex]));
        }
        else
        {
            nextButton.SetActive(false);
            Destroy(GameManager.instance.gameObject);
            Destroy(SoundManager.instance.gameObject);
            SceneManager.LoadScene("MainMenu");
        }
    }

    private void SetAnimation()
    {
        if (rb.linearVelocity.x != 0)
        {
            animator.Play("player_walk");
        }
        else
        {
            animator.Play("player_idle");
        }

    }
    private void FlipCharacter()
    {
        // Rotate player depending where players is trying to go.
        if (facingRight && rb.linearVelocity.x <= -0.1)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            Debug.Log("Flipping character to the left");
            facingRight = false;
        }
        else if (!facingRight && rb.linearVelocity.x >= 0.1)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            Debug.Log("Flipping character to the right");
            facingRight = true;
        }
    }
}
