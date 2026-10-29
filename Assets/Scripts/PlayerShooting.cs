using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("Ammo Settings")]
    [SerializeField] private int curretAmmo = 10;
    [SerializeField] private int maxAmmo = 10;
    [SerializeField] private bool infiniteAmmo = false;

    [Header("Shooting Settings")]
    [SerializeField] private float bulletSpeed = 20f;
    [SerializeField] private float bulletRate = 0.5f;
    [SerializeField] private int bulletDamage = 1;

    private Transform bulletSpawnPoint;

    private ObjectPool bulletPool;

    private float lastShotTime;

    private void Start()
    {
        bulletSpawnPoint = transform.Find("ShootPoint");
        bulletPool = GetComponent<ObjectPool>();
    }

    public bool CanShoot()
    {
        return (Time.time - lastShotTime >= bulletRate) && (infiniteAmmo || curretAmmo > 0);
    }

    public void Shoot()
    {
        if (!CanShoot()) return;
        lastShotTime = Time.time;

        // Reduce the ammo
        if (!infiniteAmmo) curretAmmo--;

        GameObject bullet = bulletPool.GetObjectFromPool();
        bullet.transform.position = bulletSpawnPoint.position;
        bullet.transform.rotation = bulletSpawnPoint.rotation;

        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        bulletRigidbody.linearVelocity = bulletSpawnPoint.up * bulletSpeed;
    }
}
