using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class C_Health : MonoBehaviour
{
    public TMP_Text healthText;
    public int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        healthText.text = currentHealth.ToString();
    }

    public void AddHealth(int health)
    {
        currentHealth = currentHealth + health;

        healthText.text = currentHealth.ToString();
    }

    public void RemoveHealth(int health)
    {
        currentHealth = currentHealth - health;
        healthText.text = currentHealth.ToString();
    }
}
