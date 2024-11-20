using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private Vector2 direction;

    [SerializeField] private float timeToDestroy;
    private float destroyTimer;

    private ObjectPool<Bullet> originPool;
    public ObjectPool<Bullet> OriginPool { get => originPool; set { originPool = value; } }


    void Update()
    {
        transform.Translate(Vector3.up * movementSpeed * Time.deltaTime, Space.Self);

        destroyTimer += Time.deltaTime;

        if (destroyTimer >= timeToDestroy)
        {
            destroyTimer = 0;
            originPool.Release(this);
        }
    }
}
