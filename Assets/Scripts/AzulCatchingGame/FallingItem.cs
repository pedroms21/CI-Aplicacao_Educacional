using UnityEngine;

public class FallingItem : MonoBehaviour
{
    [Header("Configurações do Item")]
    public bool isHealthy = true; // Define se o item é bom ou mau (útil para o Nível 2)
    public float destroyYLimit = -6f; // Ponto abaixo do ecrã onde o item é destruído

    void Update()
    {
        // Se o item passar do fundo do ecrã
        if (transform.position.y < destroyYLimit)
        {
            // Se era um item saudável e o deixámos fugir, perdemos vida!
            if (isHealthy)
            {
                AzulGameManager.instance.LoseLife(1);
            }

            // Destrói o objeto na mesma para não ocupar memória
            Destroy(gameObject);
        }
    }

    // Isto deteta quando o item entra dentro do balde (Player)
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (isHealthy)
            {
                // Dá 10 pontos!
                AzulGameManager.instance.AddScore(10);
            }
            else
            {
                // Tira 1 vida!
                AzulGameManager.instance.LoseLife(1);
            }

            Destroy(gameObject);
        }
    }
}