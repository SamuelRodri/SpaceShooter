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

    private void Awake()
    {
        bulletPool = GetComponent<ObjectPool>();
        bulletPool.InitializePool(bulletPrefab, poolSize);
    }

    public void Shoot()
    {
        if (!canShoot) return;

        for (int i = 0; i < shootPoints.Length; i++)
        {
            Bullet bullet = (Bullet)bulletPool.GetPooled();
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
