using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        UpdateAnimation();
    }

    void UpdateAnimation()
    {
        // Logika untuk menentukan animasi berdasarkan parameter InputMag
        float inputMag = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).magnitude;
        animator.SetFloat("InputMag", inputMag);

        // Pemain bergerak, tambahkan logika animasi berjalan di sini
        // Pemain tidak bergerak, tambahkan logika animasi berhenti di sini

        // Logika untuk menangani input serangan
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetBool("Attack", true);
        }
        else
        {
            animator.SetBool("Attack", false);
        }

        // Logika untuk menangani input roll
        if (Input.GetButtonDown("Fire2"))
        {
            // Atur parameter "Roll" di Animator menjadi true
            animator.SetBool("Roll", true);
        }
        else
        {
            // Atur parameter "Roll" di Animator menjadi false
            animator.SetBool("Roll", false);
        }
    }
}