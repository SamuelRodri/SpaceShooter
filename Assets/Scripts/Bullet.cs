using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : ObjectPooled
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private Vector2 direction;

    void Update()
    {
        transform.Translate(Vector3.up * movementSpeed * Time.deltaTime, Space.Self);
    }
}