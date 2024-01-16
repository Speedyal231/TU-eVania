using UnityEngine;

public class Receiver : MonoBehaviour
{
    private healthManager healthManager;
    private PlayerScoreManager PlayerScoreManager;
    private PlayerClingScript PlayerClingScript;

    private void Start()
    {
        healthManager = FindObjectOfType<healthManager>();
        PlayerScoreManager = FindObjectOfType<PlayerScoreManager>();
        PlayerClingScript = FindObjectOfType<PlayerClingScript>();
        // Set values to variables in VariableManager
        healthManager.setHealth(VariableManager.hTransfer);
        PlayerScoreManager.playerScore = VariableManager.sTransfer;
        if (VariableManager.p1) { PlayerClingScript.changeClingStatus(); }
    }
}