using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour
{
    [Header("Bullet Settings")]
    [SerializeField] private float bulletLifeTime = 1f;
    [SerializeField] private ParticleSystem bulletParticleSystem;

    private BoxCollider boxCollider;
    private int damage;
    private bool hasImpacted;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    public void SetDamage(int bulletDamage)
    {
        damage = bulletDamage;
    }

    private void OnEnable()
    {
        // Estado limpio cada vez que sale del pool
        hasImpacted = false;
        boxCollider.enabled = true;
        bulletParticleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

        Invoke(nameof(DesactivateBullet), bulletLifeTime);
    }

    private void OnDisable()
    {
        CancelInvoke();
        StopAllCoroutines();
    }

    private void DesactivateBullet()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasImpacted) return;
        hasImpacted = true;

        // El impacto manda: cancelamos el timer de vida para que no corte las partículas
        CancelInvoke(nameof(DesactivateBullet));

        if (other.CompareTag("Explosion") && other.TryGetComponent(out EnemyHeathManager enemy))
        {
            enemy.Impacted(damage);
        }

        if (other.CompareTag("Bomb"))
        {
            if (other.TryGetComponent(out SphereCollider sphere)) sphere.enabled = true;
            if (other.TryGetComponent(out ParticleSystem ps)) ps.Play();
            Destroy(other.gameObject, 3f);
        }

        StartCoroutine(DeactivateAfterParticles());
    }

    private IEnumerator DeactivateAfterParticles()
    {
        boxCollider.enabled = false;
        bulletParticleSystem.Play();

        var main = bulletParticleSystem.main;
        yield return new WaitForSeconds(main.duration + main.startLifetime.constantMax);

        DesactivateBullet();
    }
}