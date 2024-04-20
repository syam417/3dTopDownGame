using UnityEngine;

public class BulletController : MonoBehaviour
{
    public int damage = 1;

    private void OnTriggerEnter(Collider other)
    {
        // Pastikan objek lain memiliki tag "Enemy"
        if (other.CompareTag("Enemy"))
        {
            // Lakukan sesuatu, misalnya, kurangi health player
            HealthController playerHealth = other.GetComponent<HealthController>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }

            // Hancurkan peluru setelah menabrak
            Destroy(gameObject);

            
        }
    }
}
