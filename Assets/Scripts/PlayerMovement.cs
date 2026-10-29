using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Input Actions")]
    [SerializeField] private InputAction thrust;
    [SerializeField] private InputAction rotation;
    [SerializeField] private InputAction shoot;
    [Header("Movement Settings")]
    [SerializeField] private float moveForce = 10f;
    [SerializeField] private float rotationForce = 0.5f;
    [SerializeField] private ParticleSystem thrustParticleSystem;

    [Header("Audio Settings")]
    [SerializeField] private AudioClip thrustAudioClip;

    Rigidbody rb;
    AudioSource audioSource;
    PlayerShooting playerShooting;
    bool isControlling = true;

    void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
        shoot.Enable();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        playerShooting = GetComponent<PlayerShooting>();
    }

    void FixedUpdate()
    {
        if (!isControlling) return;
        ProcessThrust();
        ProcessRotation();
        ProcessShooting();
    }

    private void ProcessShooting()
    {
        if (shoot.IsPressed()) playerShooting.Shoot();
    }

    private void ProcessRotation()
    {
        rb.freezeRotation = true; // freezing rotation so we can manually rotate
        float rotationValue = rotation.ReadValue<float>();
        if (rotationValue < 0)
        {
            RotateRight();
        } else if(rotationValue > 0) 
        {
            RotateLeft();
        }else
        {
            StopRotation();
        }
    }

    private void StopRotation()
    {
        
    }

    private void RotateLeft()
    {
        transform.Rotate(Vector3.back * rotationForce);
    }

    private void RotateRight()
    {
        transform.Rotate(Vector3.forward * rotationForce);
    }

    private void ProcessThrust()
    {
        if (thrust.IsPressed())
        {
            StartThrusting();
        } else
        {
            StopThrusting();
        }
    }

    private void StopThrusting()
    {
        // TODO: Stop Particles, Stop Sound
        thrustParticleSystem.Stop();
        audioSource.Stop();
    }

    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * moveForce);
        if (thrustParticleSystem.isPlaying)
        {
            thrustParticleSystem.Play();
        }

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(thrustAudioClip);
        }
    }

    public void StopControlling()
    {
        isControlling = false;
    }
}
