using UnityEngine;
using System.Collections;

public class EnemyHeathManager : MonoBehaviour
{
    [SerializeField] private int maxhealth = 2;
    private int currentHealth;
    [SerializeField] private float deadTime = 1f;
    [SerializeField] private ParticleSystem deadParticleSystem;

    private void Start()
    {
        currentHealth = maxhealth;
    }

    public void Impacted(int bulletDamage)
    {
        currentHealth -= bulletDamage;
        if (currentHealth <= 0)
        {
            StartCoroutine(Dead());
        }
    }

    public IEnumerator Dead()
    {
        deadParticleSystem.Play();
        yield return new WaitForSeconds(deadTime);
        Destroy(gameObject);
    }
}
