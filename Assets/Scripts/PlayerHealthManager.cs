using UnityEngine;
using System.Collections;
using System;

public class PlayerHealthManager : MonoBehaviour
{
    [SerializeField] private int max_Health = 100;
    private int current_health;
    [SerializeField] private int impactDamage = 25;
    [SerializeField] private int lifeHealed = 50;
    private float invulnerableTime = 3f;
    private bool isInvulnerable = false;
    AudioSource audioSource;
    [SerializeField] private AudioClip explosionAudio;
    PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        audioSource = GetComponent<AudioSource>();
        current_health = max_Health;
    }

    public void Impacted()
    {
        if (isInvulnerable) return;
        current_health -= impactDamage;
        Debug.Log("Vida: " + current_health.ToString());
        audioSource.Stop();
        audioSource.PlayOneShot(explosionAudio);
        if(current_health <= 0)
        {
            playerMovement.StopControlling();
            return;
        }
        StartCoroutine(Invulnerability());
    }

    private IEnumerator Invulnerability()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(invulnerableTime);
        isInvulnerable = false;
    }

    public void HealPlayer()
    {
        current_health += lifeHealed;
        if(current_health > max_Health)
        {
            current_health = max_Health;
        } 
    }

    public bool HaveMaxHealth()
    {
        return current_health == max_Health;
    }
}
