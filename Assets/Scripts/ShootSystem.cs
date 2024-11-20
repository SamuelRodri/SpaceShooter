using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Pool;

public class ShootSystem : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private float shootCoolDown;
    [SerializeField] private Transform[] shootPoints;

    private ObjectPool<Bullet> bulletPool;
    private bool canShoot = true;

    private void Awake()
    {
        bulletPool = new ObjectPool<Bullet>(CreateBullet, null, ReleaseBullet, DestroyBullet);
    }

    public void Shoot()
    {
        if (!canShoot) return;

        for (int i = 0; i < shootPoints.Length; i++)
        {
            Bullet bullet = bulletPool.Get();
            bullet.gameObject.SetActive(true);
            bullet.transform.position = shootPoints[i].position;
            bullet.transform.eulerAngles = shootPoints[i].localEulerAngles;
        }

        StartCoroutine(CoolDownShoot());
    }

    private IEnumerator CoolDownShoot()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootCoolDown);
        canShoot = true;
    }

    #region Pool Methods
    private Bullet CreateBullet()
    {
        Bullet bullet = Instantiate(bulletPrefab, transform.position, bulletPrefab.transform.rotation);
        bullet.OriginPool = bulletPool;
        return bullet;
    }

    private void ReleaseBullet(Bullet bullet)
        => bullet.gameObject.SetActive(false);

    private void DestroyBullet(Bullet bullet)
        => Destroy(bullet.gameObject);
    #endregion
}
