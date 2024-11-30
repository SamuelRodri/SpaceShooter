using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PooledObject : MonoBehaviour
{
    public delegate void Release(PooledObject pooled);
    public event Release OnRelease;

    [SerializeField] private float timeToDestroy;
    private float destroyTimer;

    [HideInInspector] public bool isActive = false;

    public void Spawn()
    {
        StartCoroutine(UpdateTimer());
    }

    protected IEnumerator UpdateTimer()
    {
        while (destroyTimer < timeToDestroy)
        {
            destroyTimer += Time.deltaTime;
            yield return null;
        }

        isActive = false;
        destroyTimer = 0;
        OnRelease?.Invoke(this);
    }
}
