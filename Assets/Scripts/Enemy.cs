using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float lifes;

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

            lifes--;

            if(lifes <= 0)
                Destroy(gameObject);
        }
    }
}
