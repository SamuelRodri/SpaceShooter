using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float rangeY;

    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    void Update()
    {

    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            Vector2 instancePoint = new Vector2(transform.position.x, Random.Range(-rangeY, rangeY));
            Instantiate(enemyPrefab, instancePoint, enemyPrefab.transform.rotation);
            yield return new WaitForSeconds(1f);
        }
    }
}
