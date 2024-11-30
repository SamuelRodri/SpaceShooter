using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPooled : MonoBehaviour
{
    public delegate void Release(ObjectPooled pooled);
    public event Release OnRelease;

    [SerializeField] private float timeToDestroy;
    private float destroyTimer;

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
        
        destroyTimer = 0;
        OnRelease?.Invoke(this);
    }
}
