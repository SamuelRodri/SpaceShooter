using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ShootSystem : MonoBehaviour
{
    [SerializeField] private int poolSize;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private float shootCoolDown;
    [SerializeField] private Transform[] shootPoints;

    private bool canShoot = true;
    private ObjectPool bulletPool;

    private void Start()
    {
        bulletPool = GetComponent<ObjectPool>();
        bulletPool.InitializePool(bulletPrefab, poolSize);
    }

    public bool Shoot()
    {
        if (bulletPool == null) return false;

        if (!canShoot) return false;

        for (int i = 0; i < shootPoints.Length; i++)
        {
            Bullet bullet = (Bullet)bulletPool.GetPooled();
            bullet.gameObject.SetActive(true);
            bullet.transform.position = shootPoints[i].position;
            bullet.transform.eulerAngles = shootPoints[i].localEulerAngles;

            bullet.Spawn();
        }

        StartCoroutine(CoolDownShoot());

        return true;
    }

    private IEnumerator CoolDownShoot()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootCoolDown);
        canShoot = true;
    }
}
