using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float shootCoolDown;
    [SerializeField] private float hLimit;
    [SerializeField] private float vLimit;
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private Transform[] shootPoints;

    private bool canShoot = true;
    private float lives = 100;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        LimitMove();

        Shoot();
    }

    void Move()
    {
        float inputH = Input.GetAxisRaw("Horizontal");
        float inputV = Input.GetAxisRaw("Vertical");

        transform.Translate(new Vector2(inputH, inputV).normalized * movementSpeed * Time.deltaTime);
    }

    void LimitMove()
    {
        float clampedH = Mathf.Clamp(transform.position.x, -hLimit, hLimit);
        float clampedV = Mathf.Clamp(transform.position.y, -vLimit, vLimit);

        transform.position = new Vector2(clampedH, clampedV);
    }

    void Shoot()
    {
        if (Input.GetKey(KeyCode.Space) && canShoot)
        {
            foreach(Transform shootPoint in shootPoints)
            {
                Instantiate(bulletPrefab, shootPoint.position, bulletPrefab.transform.rotation);
            }

            StartCoroutine(CoolDownShoot());
        }
    }

    private IEnumerator CoolDownShoot()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootCoolDown);
        canShoot = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyBullet") || collision.CompareTag("Enemy"))
        {
            lives -= 20;
            Destroy(collision.gameObject);

            if (lives <= 0)
                Destroy(gameObject);
        }
    }
}
