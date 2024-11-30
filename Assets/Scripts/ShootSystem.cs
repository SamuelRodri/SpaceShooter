using System.Collections;
using UnityEngine;

public class ShootSystem : ObjectSpawner
{
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private float shootCoolDown;
    [SerializeField] private Transform[] shootPoints;

    private bool canShoot = true;

    private void Awake()
    {
        poolPrefab = bulletPrefab;
        InitializePool();    
    }

    public void Shoot()
    {
        if (!canShoot) return;

        for (int i = 0; i < shootPoints.Length; i++)
        {
            Bullet bullet = (Bullet)pool.Get();
            bullet.gameObject.SetActive(true);
            bullet.transform.position = shootPoints[i].position;
            bullet.transform.eulerAngles = shootPoints[i].localEulerAngles;

            bullet.Spawn();
        }

        StartCoroutine(CoolDownShoot());
    }

    private IEnumerator CoolDownShoot()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootCoolDown);
        canShoot = true;
    }
}
