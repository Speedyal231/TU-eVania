using UnityEngine;
using TMPro;

public class scoreUpdated : MonoBehaviour
{
    public TMP_Text textScore;
    public playerData pdata; // Reference to PlayerData script

    // Start is called before the first frame update
    void Start()
    {
        if (pdata == null)
        {
            // Find PlayerData script if not assigned in the Inspector
            pdata = FindObjectOfType<playerData>();
        }

        UpdateScoreText();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        if (pdata != null)
        {
            // Use the instance to access the non-static property
            textScore.text = "Final Score: " + pdata.PlayerScore.ToString();
        }
        else
        {
            Debug.LogWarning("PlayerData script not found!");
        }
    }
}
