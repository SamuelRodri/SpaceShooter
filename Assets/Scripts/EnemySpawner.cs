using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float timeToSpawn;
    [SerializeField] private Enemy smallEnemyPrefab;
    [SerializeField] private Enemy[] bigEnemiesPrefabs;
    [SerializeField] private float rangeY;

    [SerializeField] private float smallEnemiesRange;
    private float enemiesToBoss;
    private float currentEnemies;

    [SerializeField] private int smallEnemySize;
    [SerializeField] private int bigEnemySize;
    private ObjectPool smallEnemyPool;

    private List<ObjectPool> bigEnemiesPools = new List<ObjectPool>();

    void Start()
    {
        smallEnemyPool = gameObject.AddComponent<ObjectPool>();
        smallEnemyPool.InitializePool(smallEnemyPrefab, smallEnemySize);

        for(int i = 0; i < bigEnemiesPrefabs.Length; i++)
        {
            var pool = gameObject.AddComponent<ObjectPool>();
            bigEnemiesPools.Add(pool);
            pool.InitializePool(bigEnemiesPrefabs[i], bigEnemySize);
        }

        enemiesToBoss = Mathf.Max(Random.Range(smallEnemiesRange - 5f, smallEnemiesRange + 5f), 1);
        currentEnemies = 0;
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            if (currentEnemies < enemiesToBoss)
            {
                Enemy smallEnemy = (Enemy)smallEnemyPool.GetPooled();
                smallEnemy.gameObject.SetActive(true);
                smallEnemy.transform.position = new Vector2(transform.position.x, Random.Range(-rangeY, rangeY));
                yield return new WaitForSeconds(timeToSpawn);
                currentEnemies++;
            }
            else
            {
                int bigEnemyIndex = Random.Range(0, bigEnemiesPrefabs.Length);
                var pool = bigEnemiesPools[bigEnemyIndex];
                Enemy bigEnemy = (Enemy)pool.GetPooled();
                bigEnemy.gameObject.SetActive(true);
                bigEnemy.transform.position = new Vector2(transform.position.x, Random.Range(-rangeY, rangeY));
                yield return new WaitForSeconds(timeToSpawn);
                currentEnemies = 0;
            }

        }
    }
}
