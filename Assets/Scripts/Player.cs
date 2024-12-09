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
    private ShipAudio audioSystem;

    private float lives = 100;
    public float Lives { get => lives; set => lives = value; }

    private void Start()
    {
        shootSystem = GetComponent<ShootSystem>();
        audioSystem = GetComponent<ShipAudio>();
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
            if (shootSystem.Shoot()) audioSystem.PlayShootAudio();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (lives <= 0) return;

        if (collision.CompareTag("EnemyBullet") || collision.CompareTag("Enemy"))
        {
            audioSystem.PlayHitAudio();

            lives -= 20;

            if (lives <= 0)
            {
                audioSystem.PlayDestroyAudio(OnSoundDestroyFinished);
                return;
            }

            OnPlayerHit?.Invoke();
        }
    }

    private void OnSoundDestroyFinished()
    {
        gameObject.SetActive(false);
        OnPlayerDead?.Invoke();
    }
}
