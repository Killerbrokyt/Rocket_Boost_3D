using UnityEngine;
using System.Collections;
public class CollisionManager : MonoBehaviour
{
    [Header("Audio Settings")]
    [SerializeField] private AudioClip landingAudio;
    //[SerializeField] private AudioClip explosionAudio;

    [Header("Particles Settings")]
    [SerializeField] private ParticleSystem landingParticleSystem;
    [SerializeField] private ParticleSystem explosionParticleSystem;
    bool isControllable = true;
    AudioSource audioSource;
    PlayerMovement playerMovement;
    PlayerHealthManager playerHealthManager;
    PlayerShooting playerShooting;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
        playerHealthManager = GetComponent<PlayerHealthManager>();
        playerShooting = GetComponent<PlayerShooting>();
    }

    private void Update()
    {
        if (!isControllable) {
            playerMovement.StopControlling();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null) 
        {
            switch (collision.gameObject.tag)
            {
                case "LaunchPad":
                    Debug.Log("Empezo");
                    break;
                case "LandingPad":
                    isControllable = false;
                    audioSource.Stop();
                    audioSource.PlayOneShot(landingAudio);
                    landingParticleSystem.Play();
                    break;
                case "Explosion":
                    playerHealthManager.Impacted();
                    // isControllable = false;
                    // audioSource.Stop();
                    // audioSource.PlayOneShot(explosionAudio);
                    explosionParticleSystem.Play();
                    break;
                case "Bomb":
                    if (collision.gameObject.TryGetComponent(out BombController bomb) && bomb.Explode())
                    {
                        playerHealthManager.Impacted();
                    }
                    break;
                default:
                    break;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Recharge":
                if (!playerShooting.HaveMaxAmmo())
                {
                    playerShooting.RechargeBullets();
                    Destroy(other.gameObject);
                }
                break;

            case "Heal":
                if (!playerHealthManager.HaveMaxHealth())
                {
                    playerHealthManager.HealPlayer();
                    Destroy(other.gameObject);
                    // Podria hacer sonido
                }
                break;
            default:
                break;
        }
    }
}
