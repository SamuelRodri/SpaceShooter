using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private List<PooledObject> pool;
    private int maxSize;

    private PooledObject pooledPrefab;

    public void InitializePool(PooledObject pooled, int size)
    {
        pool = new List<PooledObject>();
        pooledPrefab = pooled;
        maxSize = size;
    }

    public PooledObject GetPooled()
    {
        if (pool.Count == 0)
            return CreatePooled();

        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].isActive)
            {
                pool[i].isActive = true;
                return pool[i];
            }
        }

        maxSize++;
        return CreatePooled();
    }

    private PooledObject CreatePooled()
    {
        PooledObject pooled = Instantiate(pooledPrefab, transform.position, pooledPrefab.transform.rotation);
        pooled.isActive = true;
        pooled.OnRelease += ReleasePooled;
        pool.Add(pooled);

        return pooled;
    }

    private void ReleasePooled(PooledObject pooled)
    {
        pooled.gameObject.SetActive(false);
    }

    private void DestroyPooled(PooledObject pooled)
        => Destroy(pooled.gameObject);
}
