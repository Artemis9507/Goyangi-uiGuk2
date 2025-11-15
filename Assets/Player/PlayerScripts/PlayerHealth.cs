using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100f;
    public float currentHealth;

    [Header("UI Reference")]
    public Image healthFillImage;

    public void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        UpdateHealthUI();
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void UpdateHealthUI()
    {
        float fillAmount = currentHealth / maxHealth;
        healthFillImage.fillAmount = fillAmount;
        
        
    }

    private void Die()
    {
        Debug.Log("PLAYER DIED!");
        
        GameManegare.Instance.PlayerDied();
    }
}
