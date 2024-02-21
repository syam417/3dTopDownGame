using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public Transform gunBarrel;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, gunBarrel.position, gunBarrel.rotation);
        bullet.GetComponent<Rigidbody>().velocity = gunBarrel.forward * bulletSpeed;
        Destroy(bullet, 3f);
    }
}
