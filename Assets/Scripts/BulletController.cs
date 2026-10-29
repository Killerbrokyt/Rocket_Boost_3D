using System;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [Header("Bullet Settings")]
    [SerializeField] private float bulletLifeTime = 1f;
    private void OnEnable()
    {
        Invoke(nameof(DesactivateBullet), bulletLifeTime);
    }

    private void DesactivateBullet()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        DesactivateBullet();

        //TODO: Particles
        //TODO: if bullet hits an enemy, reduce enemy health
        //TODO: if bullet hits obstacle, destroy obstacle
        //TODO: if bullet hits player, reduce player health
    }
}
