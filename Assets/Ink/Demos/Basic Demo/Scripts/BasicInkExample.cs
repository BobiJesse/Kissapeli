﻿using System;
using Ink.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// This is a super bare bones example of how to play and display a ink story in Unity.
public class BasicInkExample : MonoBehaviour {
    public string minigameSceneName;
    public static event Action<Story> OnCreateStory;
	string variableName = "Total";
	public int variableValue = 1;
	string timeLeft = "Time_Left";
	float SecondsLeft = 1200;
	

    public int secondsLeft = 1200;
    void Awake () {
		// Remove the default message
		RemoveChildren();
		//StartStory();
	}

	// Creates a new Story object with the compiled story which we can then play!
	public void StartStory () {
        story = new Story (inkJSONAsset.text);
		
        if (GameManager.instance.catsTalkedTo == 1)
		{
            variableValue = GameManager.instance.catsHelped;
        }
		else
		{
			variableValue = 2;
        }

		story.variablesState[variableName] = variableValue;
		if(OnCreateStory != null) OnCreateStory(story);
		RefreshView();
		GameManager.instance.catsTalkedTo++;
	}
	
	// This is the main function called every time the story changes. It does a few things:
	// Destroys all the old content and choices.
	// Continues over all the lines of text, then displays all the choices. If there are no choices, the story is finished!
	void RefreshView () {
		// Remove all the UI on screen
		RemoveChildren ();
		
		// Read all the content until we can't continue any more
		while (story.canContinue) {
			// Continue gets the next line of the story
			string text = story.Continue ();
			// This removes any white space from the text.
			text = text.Trim();
			// Display the text on screen!
			CreateContentView(text);
		}

		// Display all the choices, if there are any!
		if(story.currentChoices.Count > 0) {
			for (int i = 0; i < story.currentChoices.Count; i++) {
				Choice choice = story.currentChoices [i];
				Button button = CreateChoiceView (choice.text.Trim ());
				// Tell the button what to do when we press it
				button.onClick.AddListener (delegate {
					OnClickChoiceButton (choice);
				});
			}
		}
		// If we've read all the content and there's no choices, the story is finished!
		else {
			//Button choice = CreateChoiceView("Start minigame");
			//choice.onClick.AddListener(delegate{
				StopDialogue();
			//});
		}
	}

	// When we click the choice button, tell the story to choose that choice!
	void OnClickChoiceButton (Choice choice) {
        if (GameManager.instance.speedrunMode)
        {
            SecondsLeft = Timer.instance.remainingTime;
        }
        else
        {
			SecondsLeft = Clock.instance.GetTimeLeft();
        }
        story.variablesState[timeLeft] = SecondsLeft;
        story.ChooseChoiceIndex (choice.index);
		RefreshView();
	}

	// Creates a textbox showing the the line of text
	void CreateContentView (string text) {
		Text storyText = Instantiate (textPrefab) as Text;
		storyText.text = text;
		storyText.transform.SetParent (canvas.transform, false);
	}

	// Creates a button showing the choice text
	Button CreateChoiceView (string text) {
		// Creates the button from a prefab
		Button choice = Instantiate (buttonPrefab) as Button;
		choice.transform.SetParent (canvas.transform, false);
		
		// Gets the text from the button prefab
		Text choiceText = choice.GetComponentInChildren<Text> ();
		choiceText.text = text;

		// Make the button expand to fit the text
		HorizontalLayoutGroup layoutGroup = choice.GetComponent <HorizontalLayoutGroup> ();
		layoutGroup.childForceExpandHeight = false;

		return choice;
	}

	// Destroys all the children of this gameobject (all the UI)
	void RemoveChildren () {
		int childCount = canvas.transform.childCount;
		for (int i = childCount - 1; i >= 0; --i) {
			Destroy (canvas.transform.GetChild (i).gameObject);
		}
	}

	[SerializeField]
	private TextAsset inkJSONAsset = null;
	public Story story;

	[SerializeField]
	private Canvas canvas = null;

	// UI Prefabs
	[SerializeField]
	private Text textPrefab = null;
	[SerializeField]
	private Button buttonPrefab = null;

    public void StopDialogue()
    {
        int ChoiceValue = (int)story.variablesState["WhichChoice"];
        if (ChoiceValue == 0)
		{
			//ChoiceValue = story.variablesState[CheckChoice];
			//story.variablesState[variableName] = variableValue;
			GameManager.instance.catsTalkedTo = 1;
			GameManager.instance.catsHelped += 1;
			//SceneManager.LoadScene(Interact.closestCat.GetComponent<Interact>().minigameSceneName, LoadSceneMode.Additive);
			Interact.closestCat.StartMinigame();
            GameObject.Find("DialogueSystem").gameObject.transform.GetChild(0).gameObject.SetActive(false);

        }
		else
		{
            //SceneManager.LoadScene("Testing Ground", LoadSceneMode.Additive);
            GameObject.Find("DialogueSystem").gameObject.transform.GetChild(0).gameObject.SetActive(false);
            if (GameEvents.OnMinigameExit != null)
            {
                GameEvents.OnMinigameExit();
            }
        }
        
    }
}
