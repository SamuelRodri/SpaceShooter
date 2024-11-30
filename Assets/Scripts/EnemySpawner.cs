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
    private ObjectPool smallEnemyPool;
    private ObjectPool bigEnemyPool;

    private void Awake()
    {
        smallEnemyPool = gameObject.AddComponent<ObjectPool>();
    }

    void Start()
    {
        smallEnemyPool.InitializePool(smallEnemyPrefab, smallEnemySize);

        enemiesToBoss = Random.Range(smallEnemiesRange - 5f, smallEnemiesRange + 5f);
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
                //smallEnemy.transform.eulerAngles = shootPoints[i].localEulerAngles;
                yield return new WaitForSeconds(timeToSpawn);
                currentEnemies++;
            }
            else
            {
                int bigEnemyIndex = Random.Range(0, bigEnemiesPrefabs.Length);
                var prefab = bigEnemiesPrefabs[bigEnemyIndex];
                Vector2 instancePoint = new Vector2(transform.position.x, Random.Range(-rangeY, rangeY));
                Instantiate(prefab, instancePoint, prefab.transform.rotation);
                yield return new WaitForSeconds(timeToSpawn);
                currentEnemies = 0;
            }

        }
    }
}
