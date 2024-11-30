using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectSpawner : MonoBehaviour
{
    protected ObjectPool<ObjectPooled> pool;
    protected ObjectPooled poolPrefab;

    protected void InitializePool()
    {
        pool = new ObjectPool<ObjectPooled>(CreatePooled, null, ReleasePooled, DestroyPooled);
    }

    protected ObjectPooled CreatePooled()
    {
        ObjectPooled pooled = Instantiate(poolPrefab, transform.position, poolPrefab.transform.rotation);
        pooled.OnRelease += pool.Release;

        return pooled;
    }

    private void ReleasePooled(ObjectPooled pooled)
        => pooled.gameObject.SetActive(false);

    private void DestroyPooled(ObjectPooled pooled)
        => Destroy(pooled.gameObject);
}
