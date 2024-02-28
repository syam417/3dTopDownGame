using UnityEngine;

public class SlimeController : MonoBehaviour
{
    public float speed = 3f;
    public float jumpForce = 5f;
    public float stoppingDistance = 2f;
    public float jumpCooldown = 2f;

    private Transform player;
    private Rigidbody slimeRigidbody;
    private bool isJumping = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player == null)
        {
            Debug.LogError("Player not found!");
            return;
        }

        slimeRigidbody = GetComponent<Rigidbody>();
        InvokeRepeating("JumpRandomly", 0f, jumpCooldown);
    }

    void Update()
    {
        if (player != null)
        {
            Vector3 direction = player.position - transform.position;

            // Jarak antara slime dan pemain
            float distanceToPlayer = direction.magnitude;

            // Berhenti jika sudah dalam jarak tertentu dengan pemain
            if (distanceToPlayer <= stoppingDistance)
            {
                isJumping = false;
                return;
            }

            // Bergerak menuju pemain
            transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
            transform.LookAt(player);
        }
    }

    void JumpRandomly()
    {
        if (!isJumping)
        {
            Jump();
            Invoke("ResetJump", jumpCooldown);
        }
    }

    void Jump()
    {
        // Melompat ke atas
        slimeRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isJumping = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Mengecek apakah slime menyentuh tanah
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }

    void ResetJump()
    {
        isJumping = false;
    }
}
