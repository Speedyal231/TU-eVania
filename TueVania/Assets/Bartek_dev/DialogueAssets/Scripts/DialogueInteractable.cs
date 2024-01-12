using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueInteractable : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;
    [SerializeField] private TextAsset inkJSON;
    [SerializeField] private TMP_Text cueText;

    private bool activateCue;



    // Start is called before the first frame update
    void Start()
    {
        activateCue = false;
        visualCue.SetActive(false);
    }

    void Update()
    {
        VisualCueHandling();
    }

    void VisualCueHandling()
    {
        if (activateCue)
        {
            visualCue.SetActive(true);
        }
        else
        {
            visualCue.SetActive(false);
        }
        activateCue = false;
    }

    public void TriggerVisualCue(GameObject player)
    {
        activateCue = true;
    }

    public void EnterDialogue(GameObject player)
    {
        Debug.Log("Dialogue sequence entered");
        DialogueManager.instance.EnterDialogue(inkJSON);
    }
}
