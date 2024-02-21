using UnityEngine;

public class MouseControl : MonoBehaviour
{
    private bool isCursorLocked = true;

    void Start()
    {
        // Mengunci kursor mouse saat permainan dimulai
        LockCursor();
    }

    void Update()
    {
        // Mengembalikan atau mengunci kursor mouse saat tombol Escape ditekan
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleCursorLock();
        }
    }

    void ToggleCursorLock()
    {
        // Mengubah status kunci kursor dan memperbarui tampilan kursor
        isCursorLocked = !isCursorLocked;

        if (isCursorLocked)
        {
            LockCursor();
        }
        else
        {
            UnlockCursor();
        }
    }

    void LockCursor()
    {
        // Mengunci dan menyembunyikan kursor mouse
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    void UnlockCursor()
    {
        // Melepaskan kunci kursor dan menampilkan kembali kursor mouse
        Cursor.lockState = CursorLockMode.None;
       
    }
}
