using JetBrains.Annotations;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float minSpawnInterval = 2f;
    [SerializeField] private float maxSpawnInterval = 5f;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private PlayerMovement playerMovement;
    
    private Transform[] spawnerLocations;
    private float nextSpawnTime;

    void Start()
    {
        GameObject[] spawnersArray = GameObject.FindGameObjectsWithTag("Spawner");
        spawnerLocations = new Transform[spawnersArray.Length];
        
        for (int i = 0; i < spawnersArray.Length; i++)
        {
            spawnerLocations[i] = spawnersArray[i].transform;
        }
        
        if (spawnerLocations.Length == 0)
        {
            Debug.LogWarning("No Spawner locations found! Make sure to tag spawner objects with 'Spawner' tag.");
            return;
        }
        
        nextSpawnTime = Time.time + Random.Range(minSpawnInterval, maxSpawnInterval);
    }

    void Update()
    {
        if (spawnerLocations.Length == 0)
            return;
        
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemyAtRandomLocation();
            nextSpawnTime = Time.time + Random.Range(minSpawnInterval, maxSpawnInterval);
        }
    }
    
    private void SpawnEnemyAtRandomLocation()
    {
        Transform randomSpawner = spawnerLocations[Random.Range(0, spawnerLocations.Length)];
        GameObject spawnedEnemy = Instantiate(enemyPrefab, randomSpawner.position, randomSpawner.rotation);
        
        EnemyMovement enemyMovement = spawnedEnemy.GetComponent<EnemyMovement>();
            enemyMovement.playerTransform = playerTransform;
            enemyMovement.patrolPoints = patrolPoints;
        EnemyDamage enemyDamage = spawnedEnemy.GetComponent<EnemyDamage>();
            enemyDamage.playerHealth = playerHealth;
            enemyDamage.playerMovement = playerMovement;
    }
}
