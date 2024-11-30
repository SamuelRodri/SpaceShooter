using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : PooledObject
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float lifes;

    private ShootSystem shootSystem;
    private ObjectPool bulletPool;

    [SerializeField] private int points;
    public int Points { get => points; set => points = value; }

    void Start()
    {
        shootSystem = GetComponent<ShootSystem>();
    }

    void Update()
    {
        transform.Translate(Vector2.left * movementSpeed * Time.deltaTime, Space.World);
        shootSystem.Shoot();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            Destroy(collision.gameObject);

            lifes--;

            if (lifes <= 0)
            {
                DestroyPooled();
                FindObjectOfType<GameManager>().UpdateScore(points);
            }
        }
    }
}
