using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private GameObject bulletPrefab;

    private ShootSystem shootSystem;
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
            Destroy(gameObject);
        }
    }
}
