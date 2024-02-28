using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public HealthController healthController;  // Referensi ke HealthController
    public Slider slider;  // Referensi ke Slider

    void Start()
    {
        if (healthController == null)
        {
            Debug.LogError("HealthController not assigned to HealthBar.");
            return;
        }

        if (slider == null)
        {
            Debug.LogError("Slider not assigned to HealthBar.");
            return;
        }

        // Subscribe ke event OnHealthChanged dari HealthController
        healthController.OnHealthChanged += UpdateHealthBar;

        // Inisialisasi tampilan awal health bar
        UpdateHealthBar(healthController.maxHealth);
    }

    void UpdateHealthBar(int currentHealth)
    {
        // Perbarui nilai slider sesuai dengan kesehatan saat ini
        slider.value = currentHealth;

        // Menghadap ke arah Z global tanpa mengikuti rotasi parent
        transform.rotation = Quaternion.identity;
    }

    void LateUpdate()
    {
        // Selalu menghadap ke arah Z global tanpa mengikuti rotasi parent
        transform.rotation = Quaternion.identity;
    }
}
