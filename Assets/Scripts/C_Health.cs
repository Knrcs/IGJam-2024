using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class C_Health : MonoBehaviour
{
    public TMP_Text healthText;
    public int currentHealth;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
        healthText.text = currentHealth.ToString();
    }

    public void AddHealth(int health)
    {
        currentHealth = currentHealth + health;
        healthText.text = currentHealth.ToString();
        Debug.Log("[Health] - Set Health to " + currentHealth + " Total Healing: " + health);
    }

    public void RemoveHealth(int health)
    {
        currentHealth = currentHealth - health;
        healthText.text = currentHealth.ToString();
        Debug.Log("[Health] - Set Health to " + currentHealth + " Total Damange: " + health);
    }
}
