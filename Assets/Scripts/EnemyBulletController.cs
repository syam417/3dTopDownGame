using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    public int damageAmount = 1;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Memanggil fungsi TakeDamage dari komponen PlayerHealthController
            other.GetComponent<PlayerHealthController>().TakeDamage(damageAmount);

            // Menghancurkan peluru setelah menyentuh pemain
            Destroy(gameObject);
        }
    }
}
