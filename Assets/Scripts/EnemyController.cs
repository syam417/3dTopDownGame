using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float rotationSpeed = 5f;
    public GameObject bulletPrefab;
    public float bulletSpeed = 5f;
    public float shootInterval = 2f;
    public float detectionRange = 10f;
    public float shootingRange = 5f;

    public Transform bulletSpawnPoint; // Penambahan spawn point peluru

    private float shootTimer = 0f;
    private Transform player;

    private enum EnemyState
    {
        Idle,
        Chasing,
        Shooting
    }

    private EnemyState currentState = EnemyState.Idle;

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
        if (player != null)
        {
            switch (currentState)
            {
                case EnemyState.Idle:
                    if (Vector3.Distance(transform.position, player.position) < detectionRange)
                    {
                        currentState = EnemyState.Chasing;
                    }
                    break;

                case EnemyState.Chasing:
                    ChasePlayer();

                    if ((player.position - transform.position).sqrMagnitude < shootingRange * shootingRange)
                    {
                        currentState = EnemyState.Shooting;
                    }
                    break;

                case EnemyState.Shooting:
                    ShootPlayer();

                    if ((player.position - transform.position).sqrMagnitude > shootingRange * shootingRange)
                    {
                        currentState = EnemyState.Chasing;
                    }
                    break;
            }
        }
    }

    void ChasePlayer()
    {
        Vector3 direction = player.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    void ShootPlayer()
    {
        shootTimer += Time.deltaTime;

        if (shootTimer >= shootInterval)
        {
            // Mengecek apakah spawn point peluru sudah di-set
            if (bulletSpawnPoint != null)
            {
                // Membuat instance peluru di posisi spawn point peluru
                GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);

                Vector3 shootDirection = (player.position - bulletSpawnPoint.position).normalized;
                bullet.transform.rotation = Quaternion.LookRotation(shootDirection);
                bullet.GetComponent<Rigidbody>().velocity = shootDirection * bulletSpeed;

                // Hancurkan peluru setelah beberapa detik
                Destroy(bullet, 5f);

                // Reset timer
                shootTimer = 0f;
            }
            else
            {
                Debug.LogError("Bullet spawn point not assigned!");
            }
        }
    }
}
