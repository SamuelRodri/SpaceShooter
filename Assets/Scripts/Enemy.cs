using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : PooledObject
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float lives;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private ShootSystem shootSystem;
    private ShipAudio audioSystem;

    [SerializeField] private int points;
    public int Points { get => points; set => points = value; }

    void Start()
    {
        shootSystem = GetComponent<ShootSystem>();
        audioSystem = GetComponent<ShipAudio>();
    }

    void Update()
    {
        transform.Translate(Vector2.left * movementSpeed * Time.deltaTime, Space.World);
        if (shootSystem.Shoot()) audioSystem.PlayShootAudio();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (lives <= 0) return;

        if (collision.CompareTag("PlayerBullet"))
        {
            lives--;

            if (lives <= 0)
            {
                audioSystem.PlayDestroyAudio(OnFinishDestroyAudio);
                spriteRenderer.enabled = false;
                return;
            }

            audioSystem.PlayHitAudio();
        }
    }

    private void OnFinishDestroyAudio()
    {
        DestroyPooled();
        FindObjectOfType<GameManager>().UpdateScore(points);
    }
}
