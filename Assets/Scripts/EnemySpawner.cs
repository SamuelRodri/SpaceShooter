using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject smallEnemyPrefab;
    [SerializeField] private GameObject[] bigEnemiesPrefabs;
    [SerializeField] private float rangeY;

    [SerializeField] private float smallEnemiesRange;
    private float enemiesToBoss;
    private float currentEnemies;

    void Start()
    {
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
                Vector2 instancePoint = new Vector2(transform.position.x, Random.Range(-rangeY, rangeY));
                Instantiate(smallEnemyPrefab, instancePoint, smallEnemyPrefab.transform.rotation);
                yield return new WaitForSeconds(1f);
                currentEnemies++;
            }
            else
            {
                int bigEnemyIndex = Random.Range(0, bigEnemiesPrefabs.Length);
                var prefab = bigEnemiesPrefabs[bigEnemyIndex];
                Vector2 instancePoint = new Vector2(transform.position.x, Random.Range(-rangeY, rangeY));
                Instantiate(prefab, instancePoint, prefab.transform.rotation);
                yield return new WaitForSeconds(1f);
                currentEnemies = 0;
            }

        }
    }
}
