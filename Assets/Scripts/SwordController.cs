using UnityEngine;
using System.Collections;

public class SwordController : MonoBehaviour
{
    public int damage = 1;
    public TrailRenderer swordTrail;

    void Start()
    {
        // Jika TrailRenderer tidak ditetapkan, coba dapatkan dari komponen pada objek ini
        if (swordTrail == null)
        {
            swordTrail = GetComponent<TrailRenderer>();
        }

        // Nonaktifkan efek trail pada awal permainan
        DeactivateTrailWithDelay(0f); // Mengatur delay menjadi 0 agar segera nonaktif saat permainan dimulai
    }

    void Update()
    {
        // Cek saat tombol kiri mouse ditekan
        if (Input.GetMouseButtonDown(0))
        {
            // Aktifkan efek trail saat tombol kiri ditekan
            ActivateSwordTrail();
        }

        // Cek saat tombol kiri mouse dilepaskan
        if (Input.GetMouseButtonUp(0))
        {
            // Mulai coroutine untuk menunggu beberapa detik sebelum menonaktifkan efek trail
            StartCoroutine(DeactivateTrailWithDelay(0.5f)); // Ganti 2f dengan jumlah detik yang diinginkan
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Pastikan objek lain memiliki tag "Enemy"
        if (other.CompareTag("Enemy"))
        {
            // Lakukan sesuatu, misalnya, kurangi health enemy
            HealthController enemyHealth = other.GetComponent<HealthController>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
        }
    }

    void ActivateSwordTrail()
    {
        // Memastikan TrailRenderer telah ditetapkan
        if (swordTrail != null)
        {
            // Mengaktifkan efek trail pada pedang
            swordTrail.enabled = true;
        }
        else
        {
            Debug.LogError("TrailRenderer is not assigned.");
        }
    }

    IEnumerator DeactivateTrailWithDelay(float delay)
    {
        // Menunggu beberapa detik sebelum menonaktifkan efek trail
        yield return new WaitForSeconds(delay);

        // Memastikan TrailRenderer telah ditetapkan
        if (swordTrail != null)
        {
            // Menonaktifkan efek trail pada pedang
            swordTrail.enabled = false;
        }
        else
        {
            Debug.LogError("TrailRenderer is not assigned.");
        }
    }
}
