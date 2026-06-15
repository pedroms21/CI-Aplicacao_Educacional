using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [Header("Item Arrays")]
    [SerializeField] private GameObject[] positivePrefabs;
    [SerializeField] private GameObject[] negativePrefabs;

    [Header("Spawn Settings")]
    [SerializeField] private float spawnWidth = 2.5f; // Distance from center
    [SerializeField] private int poolSizePerItem = 5;

    private float spawnTimer;
    private float currentSpawnRate;
    private float currentFallSpeed;

    private void Start()
    {
        InitializePools();
        ApplyDifficultySettings();
    }

    private void InitializePools()
    {
        foreach (var item in positivePrefabs) ObjectPooler.Instance.CreatePool(item, poolSizePerItem);
        foreach (var item in negativePrefabs) ObjectPooler.Instance.CreatePool(item, poolSizePerItem);
    }

    private void ApplyDifficultySettings()
    {
        Difficulty diff = GameManager.Instance.GetDifficulty();

        switch (diff)
        {
            case Difficulty.Easy:
                currentSpawnRate = 2.0f;
                currentFallSpeed = 3.0f;
                break;
            case Difficulty.Medium:
                currentSpawnRate = 1.2f;
                currentFallSpeed = 5.0f;
                break;
            case Difficulty.Hard:
                currentSpawnRate = 0.7f;
                currentFallSpeed = 7.5f;
                break;
        }
    }

    private void Update()
    {
        if (GameManager.Instance.IsGameOver) return;

        spawnTimer += Time.deltaTime;
        if (spawnTimer >= currentSpawnRate)
        {
            SpawnItem();
            spawnTimer = 0f;
        }
    }

    private void SpawnItem()
    {
        Difficulty diff = GameManager.Instance.GetDifficulty();
        GameObject prefabToSpawn;

        // Easy mode: 100% positive items. Medium/Hard: 60% chance for positive, 40% for negative.
        bool spawnPositive = diff == Difficulty.Easy || Random.value > 0.4f;

        if (spawnPositive)
        {
            prefabToSpawn = positivePrefabs[Random.Range(0, positivePrefabs.Length)];
        }
        else
        {
            prefabToSpawn = negativePrefabs[Random.Range(0, negativePrefabs.Length)];
        }

        GameObject spawnedObj = ObjectPooler.Instance.GetObject(prefabToSpawn);
        
        float randomX = Random.Range(-spawnWidth, spawnWidth);
        spawnedObj.transform.position = new Vector3(randomX, transform.position.y, 0f);
        spawnedObj.SetActive(true);

        // Send settings to the item
        spawnedObj.GetComponent<CatchableItem>().Initialize(currentFallSpeed, diff);
    }
}