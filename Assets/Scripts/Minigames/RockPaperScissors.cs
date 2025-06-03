using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RockPaperScissors : MonoBehaviour
{
    public int playerChoice; // Player's choice (0: Rock, 1: Paper, 2: Scissors)
    public int aiChoice; // AI's choice (0: Rock, 1: Paper, 2: Scissors)
    public bool isGameActive = false; // Flag to check if the game is active
    public bool isGameCompleted = false; // Flag to check if the game is completed

    public GameObject buttonCanvas; // Reference to the button canvas
    public GameObject resultCanvas; // Reference to the result canvas
    public GameObject winCanvas; // Reference to the win canvas

    public TMPro.TextMeshProUGUI resultText; // Reference to the result text UI element

    public Button rockButton; // Reference to the Rock button
    public Button paperButton; // Reference to the Paper button
    public Button scissorsButton; // Reference to the Scissors button

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        aiChoice = Random.Range(0, 3); // Randomly generate AI's choice (0, 1, or 2)
        isGameActive= true; // Set the game to active
        buttonCanvas.SetActive(true); // Show the button canvas
        resultCanvas.SetActive(false); // Hide the result canvas
        winCanvas.SetActive(false); // Hide the win canvas
    }

    public void Reset()
    {
        aiChoice = Random.Range(0, 3); // Randomly generate AI's choice (0, 1, or 2)
        playerChoice = -1; // Reset player's choice
        isGameActive = true; // Reset game active flag
        resultCanvas.SetActive(false); // Hide result canvas
        buttonCanvas.SetActive(true); // Show button canvas
        resultText.text = ""; // Clear result text
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayerChoice(int choice)
    {
        playerChoice = choice; // Set player's choice based on button clicked
        CheckResult(); // Check the result of the game
    }

    public void CheckResult()
    {
        buttonCanvas.SetActive(false); // Hide the button canvas
        if (isGameActive && !isGameCompleted)
        {
            if (playerChoice == aiChoice)
            {
                Debug.Log("Draw!"); // Log draw result
                resultText.text = "Draw!"; // Update result text
                resultCanvas.SetActive(true); // Show result canvas
            }
            else if ((playerChoice == 0 && aiChoice == 2) || (playerChoice == 1 && aiChoice == 0) || (playerChoice == 2 && aiChoice == 1))
            {
                Debug.Log("Player wins!"); // Log player win
                resultText.text = "You win!"; // Update result text
                isGameActive = false; // Deactivate the game
                isGameCompleted = true; // Mark the game as completed
                winCanvas.SetActive(true); // Show win canvas
            }
            else
            {
                Debug.Log("AI wins!"); // Log AI win
                resultText.text = "You lose!"; // Update result text
                resultCanvas.SetActive(true); // Show result canvas
            }
        }
    }

    public void WinTrigger()
    {
        // do stuff
        SceneManager.UnloadSceneAsync("RPS");

        if (GameEvents.OnMinigameExit != null)
        {
            GameEvents.OnMinigameExit();
        }

    }
}
