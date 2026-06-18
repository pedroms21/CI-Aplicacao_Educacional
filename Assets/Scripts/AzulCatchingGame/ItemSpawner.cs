using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [Header("Configurações do Spawner")]
    // Agora usamos parênteses retos [] para dizer que é uma Lista de vários itens
    public GameObject[] itemPrefabs; 
    public float spawnInterval = 1.5f; 
    public float spawnXLimit = 8f; 

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnNewItem();
            timer = 0f;
        }
    }

    void SpawnNewItem()
    {
        // Prevenção: Se a lista estiver vazia, não faz nada
        if (itemPrefabs.Length == 0) return;

        // 1. Gerar posição X aleatória
        float randomX = Random.Range(-spawnXLimit, spawnXLimit);
        Vector3 spawnPosition = new Vector3(randomX, transform.position.y, 0);

        // 2. Escolher um número à sorte entre 0 e o tamanho da nossa lista
        int randomIndex = Random.Range(0, itemPrefabs.Length);

        // 3. Criar o item sorteado
        Instantiate(itemPrefabs[randomIndex], spawnPosition, Quaternion.identity);
    }
}