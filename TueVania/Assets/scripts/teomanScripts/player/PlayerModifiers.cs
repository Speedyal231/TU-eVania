using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModifiers : MonoBehaviour
{
    public float speedIncreaseAmount = 2f;
    public int healthRestoreAmount = 20;
    public int maxHealthIncreaseAmount = 20;

    // Reference to playerData
    public playerData PlayerData;

    private void Start()
    {
        // Ensure there's only one instance of PlayerModifiers in the scene
        PlayerModifiers[] modifiers = FindObjectsOfType<PlayerModifiers>();
        if (modifiers.Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        // Other initialization...
    }

    // Method to increase player's speed
    public void IncreaseSpeed()
    {
        // Modify the speed property in PlayerSpeedManager
        PlayerSpeedManager.Instance.ModifySpeed(1.5f); // You can adjust the speed increase value as needed

        // Update the modified speed in playerData
        PlayerData.ModifiedPlayerSpeed = PlayerSpeedManager.Instance.GetPlayerSpeed();
    }

    // Method to restore player's health
    public void RestoreHealth()
    {
        // Assuming FullHealth is a public method in PlayerHealthManager
        PlayerHealthManager healthManager = FindObjectOfType<PlayerHealthManager>();
        if (healthManager != null)
        {
            healthManager.FullHealth();
            Debug.Log("Player health restored!");
        }
        else
        {
            Debug.LogWarning("PlayerHealthManager not found!");
        }
    }

    // Method to increase player's max health
    public void IncreaseMaxHealth()
    {
        // Assuming maxPlayerHealth is a public property in PlayerHealthManager
        PlayerHealthManager healthManager = FindObjectOfType<PlayerHealthManager>();
        if (healthManager != null)
        {
            healthManager.maxPlayerHealth += maxHealthIncreaseAmount;
            Debug.Log("Player max health increased!");
        }
        else
        {
            Debug.LogWarning("PlayerHealthManager not found!");
        }
    }
}
