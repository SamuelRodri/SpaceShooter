using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform[] shootPoints;

    void Start()
    {
        StartCoroutine(Shoot());
    }

    void Update()
    {
        transform.Translate(Vector2.left * movementSpeed * Time.deltaTime, Space.World);
    }

    private IEnumerator Shoot()
    {
        while (true)
        {
            foreach (Transform shootPoint in shootPoints)
            {
                Instantiate(bulletPrefab, shootPoint.position, bulletPrefab.transform.rotation);
            }
            yield return new WaitForSeconds(1.2f);
        }
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
