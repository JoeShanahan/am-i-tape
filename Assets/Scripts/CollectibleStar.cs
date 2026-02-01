using UnityEngine;

public class CollectibleStar : MonoBehaviour
{
    // Optional: assign a sound or particle effect in the Inspector
    [SerializeField] private AudioClip collectSound;
    [SerializeField] private ParticleSystem collectEffect;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the thing touching the star is the player
        if (other.CompareTag("Player"))
        {
            // Optional: play sound
            if (collectSound != null)
                AudioSource.PlayClipAtPoint(collectSound, transform.position);

            // Optional: spawn particle effect
            if (collectEffect != null)
                Instantiate(collectEffect, transform.position, Quaternion.identity);

            // Destroy the star
            Destroy(gameObject);
        }
    }
}