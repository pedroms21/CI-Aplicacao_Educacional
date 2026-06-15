using UnityEngine;

public class CatchableItem : MonoBehaviour
{
    [Header("Item Configuration")]
    [SerializeField] private bool isPositiveItem;
    [SerializeField] private int pointValue = 10;
    
    private float currentFallSpeed;
    private bool isZigZag;
    private bool isDiagonal;
    private float diagonalDirection;
    
    private float startX;
    private float frequency = 3f;
    private float magnitude = 1.5f;

    // Called by the Spawner when pulling from the pool
    public void Initialize(float fallSpeed, Difficulty difficulty)
    {
        currentFallSpeed = fallSpeed;
        startX = transform.position.x;
        
        isZigZag = false;
        isDiagonal = false;

        if (difficulty == Difficulty.Hard)
        {
            int randomMove = Random.Range(0, 3); // 0 = straight, 1 = zigzag, 2 = diagonal
            if (randomMove == 1) isZigZag = true;
            else if (randomMove == 2) 
            {
                isDiagonal = true;
                diagonalDirection = Random.Range(0, 2) == 0 ? -1f : 1f;
            }
        }
    }

    private void Update()
    {
        if (GameManager.Instance.IsGameOver) return;

        Vector3 newPos = transform.position;
        newPos.y -= currentFallSpeed * Time.deltaTime;

        if (isZigZag)
        {
            newPos.x = startX + Mathf.Sin(Time.time * frequency) * magnitude;
        }
        else if (isDiagonal)
        {
            newPos.x += (currentFallSpeed * 0.5f) * diagonalDirection * Time.deltaTime;
        }

        transform.position = newPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            int finalPoints = isPositiveItem ? pointValue : -pointValue;
            GameManager.Instance.AddScore(finalPoints);
            gameObject.SetActive(false); // Return to pool
        }
        else if (collision.CompareTag("BottomBoundary"))
        {
            gameObject.SetActive(false); // Return to pool without points
        }
    }
}