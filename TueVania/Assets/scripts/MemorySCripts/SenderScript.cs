using UnityEngine;
using UnityEngine.SceneManagement;

public class SenderScript : MonoBehaviour
{
    private healthManager healthManager;
    private PlayerScoreManager PlayerScoreManager;
    private PlayerClingScript PlayerClingScript;
    private void Start()
    {
        healthManager = FindObjectOfType<healthManager>();
        PlayerScoreManager = FindObjectOfType<PlayerScoreManager>();
        PlayerClingScript = FindObjectOfType<PlayerClingScript>();
    }

    public void Send()
    {
        // Set values to variables in VariableManager
        VariableManager.hTransfer = healthManager.getHealth();
        VariableManager.sTransfer = PlayerScoreManager.playerScore;
        VariableManager.p1 = PlayerClingScript.clingStatus();
    }
}