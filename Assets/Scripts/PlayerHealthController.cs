using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public int maxHealth = 5;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        // Contoh: Pemain mati jika health mencapai 0 atau kurang
        if (currentHealth <= 0)
        {
            // Implementasi lebih lanjut, misalnya: respawn pemain atau tampilkan layar kematian
            Debug.Log("Player has died.");

            // Menyebabkan objek pemain kehilangan tag setelah mati
            gameObject.tag = "Untagged";

            // Untuk sementara, kita hanya akan menonaktifkan GameObject pemain
            gameObject.SetActive(false);
        }
    }

    public void Heal(int healAmount)
    {
        // Fungsi untuk menyembuhkan pemain
        currentHealth = Mathf.Min(currentHealth + healAmount, maxHealth);
    }

    public float GetHealthPercentage()
    {
        // Fungsi untuk mendapatkan persentase kesehatan pemain (dalam bentuk nilai antara 0 dan 1)
        return (float)currentHealth / maxHealth;
    }
}
