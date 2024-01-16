using UnityEngine;
using UnityEngine.SceneManagement;

public class SenderScript : MonoBehaviour
{
    private healthManager healthManager;
    private PlayerScoreManager PlayerScoreManager;
    private PlayerClingScript PlayerClingScript;
    private PlayerDashScript PlayerDashScript;
    private PlayerShootScript PlayerShootScript;
    private void Start()
    {
        healthManager = FindObjectOfType<healthManager>();
        PlayerScoreManager = FindObjectOfType<PlayerScoreManager>();
        PlayerClingScript = FindObjectOfType<PlayerClingScript>();
        PlayerShootScript = FindObjectOfType<PlayerShootScript>();
        PlayerDashScript = FindObjectOfType<PlayerDashScript>();
    }

    public void Send()
    {
        // Set values to variables in VariableManager
        VariableManager.hTransfer = healthManager.getHealth();
        VariableManager.sTransfer = PlayerScoreManager.playerScore;
        VariableManager.p1 = PlayerClingScript.clingStatus();
        VariableManager.p2 = PlayerDashScript.dashStatus();
        VariableManager.p3 = PlayerShootScript.shootStatus();
        VariableManager.p4 = PlayerShootScript.BlastStatus();
    }
}