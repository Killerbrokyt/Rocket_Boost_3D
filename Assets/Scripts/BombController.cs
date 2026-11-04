using UnityEngine;

public class BombController : MonoBehaviour
{
    [SerializeField] private ParticleSystem explosionParticles;
    [SerializeField] private SphereCollider explosionArea;
    [SerializeField] private float destroyDelay = 3f;

    private bool exploded;

    public bool Explode()
    {
        if (exploded) return false;
        exploded = true;

        explosionArea.enabled = true;
        explosionParticles.Play();
        Destroy(gameObject, destroyDelay);
        return true;
    }
}
