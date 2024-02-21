using UnityEngine;

public class EnemyTurretTower : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab peluru
    public float shootingRange = 10f; // Jarak deteksi pemain dan tembak
    public float rotationSpeed = 5f; // Kecepatan rotasi turret
    public float shootInterval = 2f; // Interval antara tembakan
    public float bulletSpeed = 5f; // Kecepatan peluru
    public Transform bulletSpawnPoint; // Posisi spawn peluru

    private Transform player; // Referensi ke pemain
    private float shootTimer = 0f; // Timer untuk menembak

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (player == null)
        {
            Debug.LogError("Player not found or does not have the 'Player' tag.");
        }
    }

    void Update()
    {
        // Cek apakah pemain ada dan dalam jarak deteksi
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            // Jika pemain dalam jarak deteksi, turret mendeteksi pemain
            if (distanceToPlayer <= shootingRange)
            {
                // Memanggil fungsi untuk menghadap ke arah pemain
                LookAtPlayer();

                // Memulai timer untuk menembak
                shootTimer += Time.deltaTime;

                // Jika timer mencapai interval, tembak pemain
                if (shootTimer >= shootInterval)
                {
                    ShootPlayer();
                    // Reset timer
                    shootTimer = 0f;
                }
            }
            else
            {
                // Reset timer jika pemain keluar dari jarak deteksi
                shootTimer = 0f;
            }
        }
    }

    void LookAtPlayer()
    {
        // Menghitung arah pandang ke pemain
        Vector3 direction = player.position - transform.position;

        // Menghitung rotasi untuk menghadap ke pemain
        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);

        // Memasukkan rotasi ke turret dengan interpolasi
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }

    void ShootPlayer()
    {
        // Mengecek apakah spawn point peluru sudah di-set
        if (bulletSpawnPoint != null)
        {
            // Membuat instance peluru di posisi spawn point peluru
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);

            // Mendapatkan arah ke pemain
            Vector3 shootDirection = (player.position - bulletSpawnPoint.position).normalized;

            // Menetapkan rotasi peluru ke arah pemain
            bullet.transform.rotation = Quaternion.LookRotation(shootDirection);

            // Memberikan kecepatan pada peluru
            bullet.GetComponent<Rigidbody>().velocity = shootDirection * bulletSpeed;

            // Hancurkan peluru setelah beberapa detik untuk mencegah penumpukan
            Destroy(bullet, 5f);
        }
        else
        {
            Debug.LogError("Bullet spawn point not assigned!");
        }
    }
}
