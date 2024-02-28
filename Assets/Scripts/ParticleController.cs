using UnityEngine;

public class ParticleController : MonoBehaviour
{
    public ParticleSystem customParticleSystem;

    void Start()
    {
        // Jika partikel sistem tidak ditetapkan, coba dapatkan dari komponen pada objek ini
        if (customParticleSystem == null)
        {
            customParticleSystem = GetComponent<ParticleSystem>();
        }
    }

    // Fungsi untuk menyalakan partikel
    public void TurnOnParticles()
    {
        if (customParticleSystem != null)
        {
            customParticleSystem.Play();
        }
        else
        {
            Debug.LogError("Particle system is not assigned.");
        }
    }

    // Fungsi untuk mematikan partikel
    public void TurnOffParticles()
    {
        if (customParticleSystem != null)
        {
            customParticleSystem.Stop();
        }
        else
        {
            Debug.LogError("Particle system is not assigned.");
        }
    }
}
