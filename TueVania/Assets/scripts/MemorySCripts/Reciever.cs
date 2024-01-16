using UnityEngine;

public class Receiver : MonoBehaviour
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
        // Set values to variables in VariableManager
        healthManager.setHealth(VariableManager.hTransfer);
        PlayerScoreManager.playerScore = VariableManager.sTransfer;
        if (VariableManager.p1) { PlayerClingScript.changeClingStatus(); }
        if (VariableManager.p2) { PlayerDashScript.changeDashStatus(); }
        if (VariableManager.p3) { PlayerShootScript.changeGunStatus(); }
        if (VariableManager.p4) { PlayerShootScript.changeBigGunStatus(); }
    }
}