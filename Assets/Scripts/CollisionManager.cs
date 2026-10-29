using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    [Header("Audio Settings")]
    [SerializeField] private AudioClip landingAudio;
    [SerializeField] private AudioClip explosionAudio;

    [Header("Particles Settings")]
    [SerializeField] private ParticleSystem landingParticleSystem;
    [SerializeField] private ParticleSystem explosionParticleSystem;
    bool isControllable = true;
    AudioSource audioSource;
    PlayerMovement playerMovement;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
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
                    isControllable = false;
                    audioSource.Stop();
                    audioSource.PlayOneShot(explosionAudio);
                    explosionParticleSystem.Play();
                    break;
                default:
                    break;
            }
        }
    }
}
