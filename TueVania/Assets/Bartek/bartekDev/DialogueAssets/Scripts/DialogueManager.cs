using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{
    // declare the DialogueManager class as a singleton class
    public static DialogueManager instance { get; private set; }

    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    public bool DialogueIsPlaying { get; private set; }

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    [Header("Player Reference")]
    [SerializeField] private GameObject player;
    private CharacterController playerController;
    private PlayerInputActions playerInputActions;

    private Story currentStory;

    private void Awake()
    {
        // safety check to make sure there is only one DialogueManager in a scene
        if (instance != null && instance != this)
        {
            Debug.Log("More than one instance of the singleton class DialogueManager found in the scene. " +
                      "The new instance has been terminated. ");
            Destroy(this);
            return;
        }
        instance = this;

        // enable player input script.
        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();
    }

    private void Start()
    {
        DialogueIsPlaying = false;
        dialoguePanel.SetActive(false);

        playerController = player.GetComponent<CharacterController>();
        if (playerController == null)
        {
            Debug.Log("Dialogue Manager couldn't get the Player Controller script of the Player.");
        }

        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    public void EnterDialogue(TextAsset inkJSON)
    {

        // manage inky dialogue
        currentStory = new Story(inkJSON.text);
        DialogueIsPlaying = true;
        dialoguePanel.SetActive(true);  
        ContinueStory();

        FreezePlayerPosition();
    }

    private void FreezePlayerPosition()
    {
        playerController.enabled = false;
    }

    private void UnFreezePlayerPosition()
    {
        playerController.enabled = true;
    }

    private IEnumerator ExitDialogue()
    {
        Debug.Log("ExitDialogue() called");
        yield return new WaitForSeconds(0.2f);
        DialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";

        UnFreezePlayerPosition();
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            // set the dialogue line
            dialogueText.text = currentStory.Continue();

            // set the player choices for the dialogue line
            DisplayDialogueChoices();
        }
        else
        {
            StartCoroutine(ExitDialogue());
        }
    }

    private void Update()
    {
        if (!DialogueIsPlaying)
        {
            return;
        }

        Debug.Log("Update called");

        // When player presses space (jump), next dialogue line is read
        /*
        if(playerInputActions.Keyboard.Submit.WasPressedThisFrame())
        {
            Debug.Log("ContinueStory() called");
            ContinueStory();
        }*/

    }

    private void DisplayDialogueChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        // send warning if at some point the ink file passes more choices than the set UI limit
        if (currentChoices.Count > choices.Length)
        {
            Debug.Log("The number of dialogue choices provided in the INK file exceeds the UI limit. " +
                "Number of choices given: " + currentChoices.Count);
        }

        int index = 0;
        // set all UI choices being used to active and set their text according to the INK file
        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }
        // disable the visibility of unused choice boxes
        for (int i = index; i < choices.Length; i++)
        {
            choices[i].SetActive(false);
        }

        // Unity's Event System requires us to select the first choice | we clear it first and then set it in the next frame
        StartCoroutine(SelectFirstChoice());
    }
    
    private IEnumerator SelectFirstChoice()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    public void MakeChoice(int choiceIndex)
    {
        List<string> tags = currentStory.currentTags;
        foreach (string tag in tags)
        {
            if (tag == "win")
            {
                Debug.Log("Rizz increased");
            }
        }
        currentStory.ChooseChoiceIndex(choiceIndex);

    }
}
