using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;

    private void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0f, vertical).normalized;

        if (movement != Vector3.zero)
        {
            // Menghitung rotasi berdasarkan vektor pergerakan
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);

            // Memasukkan rotasi ke pemain
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, Time.deltaTime * 10f);
        }

        // Menggunakan CharacterController untuk mendukung pergerakan
        CharacterController characterController = GetComponent<CharacterController>();
        characterController.Move(movement * speed * Time.deltaTime);
    }
}
