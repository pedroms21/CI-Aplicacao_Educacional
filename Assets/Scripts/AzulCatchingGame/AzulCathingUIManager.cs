using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI difficultyText;
    [SerializeField] private GameObject gameOverPanel;

    private void Start()
    {
        gameOverPanel.SetActive(false);

        // Subscribe to GameManager events
        GameManager.Instance.OnScoreChanged += UpdateScore;
        GameManager.Instance.OnTimeChanged += UpdateTime;
        GameManager.Instance.OnDifficultySet += UpdateDifficulty;
        GameManager.Instance.OnGameOver += ShowGameOverPanel;
    }

    private void OnDestroy()
    {
        // Always unsubscribe to prevent memory leaks
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnScoreChanged -= UpdateScore;
            GameManager.Instance.OnTimeChanged -= UpdateTime;
            GameManager.Instance.OnDifficultySet -= UpdateDifficulty;
            GameManager.Instance.OnGameOver -= ShowGameOverPanel;
        }
    }

    private void UpdateScore(int newScore)
    {
        scoreText.text = "Score: " + newScore;
    }

    private void UpdateTime(float newTime)
    {
        timeText.text = "Time: " + Mathf.CeilToInt(newTime);
    }

    private void UpdateDifficulty(Difficulty diff)
    {
        difficultyText.text = "Mode: " + diff.ToString();
    }

    private void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }
}