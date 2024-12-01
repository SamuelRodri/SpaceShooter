using System.Collections;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [SerializeField] private float timeToSpawn;
    [SerializeField] private HealthPowerUp healthPrefab;
    [SerializeField] private ShieldPowerUp shieldPrefab;
    [SerializeField] private int maxSize;
    [SerializeField] private float rangeY;

    private ObjectPool healthPowerUpPool;
    private ObjectPool shieldPowerUpPool;

    // Start is called before the first frame update
    void Start()
    {
        healthPowerUpPool = gameObject.AddComponent<ObjectPool>();
        healthPowerUpPool.InitializePool(healthPrefab, maxSize);

        shieldPowerUpPool = gameObject.AddComponent<ObjectPool>();
        shieldPowerUpPool.InitializePool(shieldPrefab, maxSize);

        StartCoroutine(SpawnPowerUp());
    }

    private IEnumerator SpawnPowerUp()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeToSpawn);

            PowerUp powerUp;

            if(Random.Range(0, 10) > 7)
            {
                powerUp = (PowerUp)healthPowerUpPool.GetPooled();
            }
            else
            {
                powerUp = (PowerUp)shieldPowerUpPool.GetPooled();
            }

            powerUp.gameObject.SetActive(true);
            powerUp.transform.position = new Vector2(transform.position.x, Random.Range(-rangeY, rangeY));
        }
    }
}
