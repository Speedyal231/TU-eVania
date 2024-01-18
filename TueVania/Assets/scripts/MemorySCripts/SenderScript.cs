using UnityEngine;
using UnityEngine.SceneManagement;

public class SenderScript : MonoBehaviour
{
    private healthManager healthManager;
    private PlayerScoreManager PlayerScoreManager;
    private PlayerClingScript PlayerClingScript;
    private PlayerDashScript PlayerDashScript;
    private PlayerShootScript PlayerShootScript;
    private Elevator elevator;
    private LevelEventScript levelEventScript;
    private LevelEvent2Script levelEvent2Script;
    private LevelEvent3Script levelEvent3Script;
    private void Start()
    {
        healthManager = FindObjectOfType<healthManager>();
        PlayerScoreManager = FindObjectOfType<PlayerScoreManager>();
        PlayerClingScript = FindObjectOfType<PlayerClingScript>();
        PlayerShootScript = FindObjectOfType<PlayerShootScript>();
        PlayerDashScript = FindObjectOfType<PlayerDashScript>();
        elevator = FindAnyObjectByType<Elevator>();
        levelEvent3Script = FindObjectOfType<LevelEvent3Script>();
        levelEventScript = FindObjectOfType<LevelEventScript>();
        levelEvent2Script = FindObjectOfType<LevelEvent2Script>();
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
        VariableManager.L1e = elevator.active;
        if (levelEventScript != null) { VariableManager.L2e = levelEventScript.getInteracted(); }
        if (levelEvent3Script != null) { VariableManager.L3e = levelEvent3Script.getInteracted(); }
        if (levelEvent2Script != null) { VariableManager.L4e = levelEvent2Script.getInteracted(); }
    }
}