using System.Collections;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [SerializeField] private float timeToSpawn;
    [SerializeField] private HealthPowerUp healthPrefab;
    [SerializeField] private int maxSize;
    [SerializeField] private float rangeY;

    private ObjectPool healthPowerUpPool;

    // Start is called before the first frame update
    void Start()
    {
        healthPowerUpPool = gameObject.AddComponent<ObjectPool>();
        healthPowerUpPool.InitializePool(healthPrefab, maxSize);

        StartCoroutine(SpawnPowerUp());
    }

    private IEnumerator SpawnPowerUp()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeToSpawn);
            PowerUp powerUp = (PowerUp)healthPowerUpPool.GetPooled();
            powerUp.gameObject.SetActive(true);
            powerUp.transform.position = new Vector2(transform.position.x, Random.Range(-rangeY, rangeY));
        }
    }
}
