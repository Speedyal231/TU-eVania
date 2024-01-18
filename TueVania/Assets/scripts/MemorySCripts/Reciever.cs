using UnityEngine;

public class Receiver : MonoBehaviour
{
    private healthManager healthManager;
    private PlayerScoreManager PlayerScoreManager;
    private PlayerClingScript PlayerClingScript;
    private PlayerDashScript PlayerDashScript;
    private PlayerShootScript PlayerShootScript;
    private Elevator elevator;
    private LevelEventScript levelEventScript;
    private LevelEvent3Script levelEvent3Script;
    private LevelEvent2Script levelEvent2Script;


    private void Awake()
    {
        healthManager = FindObjectOfType<healthManager>();
        PlayerScoreManager = FindObjectOfType<PlayerScoreManager>();
        PlayerClingScript = FindObjectOfType<PlayerClingScript>();
        PlayerShootScript = FindObjectOfType<PlayerShootScript>();
        PlayerDashScript = FindObjectOfType<PlayerDashScript>();
        elevator = FindAnyObjectByType<Elevator>();
        levelEvent3Script = FindObjectOfType<LevelEvent3Script>();
        levelEvent2Script = FindObjectOfType<LevelEvent2Script>();
        levelEventScript = FindObjectOfType<LevelEventScript>();

        // Set values to variables in VariableManager
        healthManager.setHealth(VariableManager.hTransfer);
        PlayerScoreManager.playerScore = VariableManager.sTransfer;
        if (VariableManager.p1) { PlayerClingScript.changeClingStatus(); }
        if (VariableManager.p2) { PlayerDashScript.changeDashStatus(); }
        if (VariableManager.p3) { PlayerShootScript.changeGunStatus(); }
        if (VariableManager.p4) { PlayerShootScript.changeBigGunStatus(); }
        if (VariableManager.L1e) { 
            elevator.SetActive(true);
        } 
        else 
        {
            healthManager.setHealth(20);
        }
        if (levelEventScript != null && VariableManager.L2e) { 
            levelEventScript.SetPressed(); 
        }
        if (levelEvent3Script != null && VariableManager.L3e) { 
            levelEvent3Script.SetPressed(); 
        }
        if (levelEvent2Script != null && VariableManager.L4e){
            levelEvent2Script.SetPressed();
        }
    }
}