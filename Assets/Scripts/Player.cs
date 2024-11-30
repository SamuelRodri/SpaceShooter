using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public delegate void PlayerHit();
    public event PlayerHit OnPlayerHit;

    public delegate void PlayerDead();
    public event PlayerDead OnPlayerDead;

    [SerializeField] private float movementSpeed;
    [SerializeField] private float hLimit;
    [SerializeField] private float vLimit;

    private ShootSystem shootSystem;

    private float lives = 100;
    public float Lives { get => lives; set => lives = value; }

    private void Start()
    {
        shootSystem = GetComponent<ShootSystem>();
    }

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
        if (Input.GetKey(KeyCode.Space))
        {
            shootSystem.Shoot();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyBullet") || collision.CompareTag("Enemy"))
        {
            lives -= 20;
            Destroy(collision.gameObject);

            if (lives <= 0)
            {
                gameObject.SetActive(false);
                OnPlayerDead?.Invoke();
                return;
            }

            OnPlayerHit?.Invoke();
        }
    }
}
