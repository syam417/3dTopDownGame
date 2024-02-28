using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    // Event untuk memberitahukan perubahan kesehatan kepada pihak lain
    public UnityAction<int> OnHealthChanged;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        
        // Pemberitahuan bahwa kesehatan telah berubah
        OnHealthChanged?.Invoke(currentHealth);

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
