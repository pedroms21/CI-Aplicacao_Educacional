using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 10f;
    
    private float screenLeftBound;
    private float screenRightBound;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        CalculateBoundaries();
    }

    private void Update()
    {
        if (GameManager.Instance.IsGameOver) return;
        HandleInput();
    }

    private void CalculateBoundaries()
    {
        // Calculates screen edges in world coordinates based on camera size
        float halfPlayerWidth = GetComponent<SpriteRenderer>().bounds.extents.x;
        Vector3 leftEdge = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector3 rightEdge = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, mainCamera.nearClipPlane));

        screenLeftBound = leftEdge.x + halfPlayerWidth;
        screenRightBound = rightEdge.x - halfPlayerWidth;
    }

    private void HandleInput()
    {
        Vector3 targetPosition = transform.position;

        // 1. Keyboard Input
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        if (horizontalInput != 0)
        {
            targetPosition.x += horizontalInput * speed * Time.deltaTime;
        }
        // 2. Mouse / Touch Input
        else if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            // Move smoothly towards the touch position on the X axis
            targetPosition.x = Mathf.Lerp(transform.position.x, mousePos.x, speed * Time.deltaTime);
        }

        // Clamp position within boundaries
        targetPosition.x = Mathf.Clamp(targetPosition.x, screenLeftBound, screenRightBound);
        transform.position = targetPosition;
    }
}