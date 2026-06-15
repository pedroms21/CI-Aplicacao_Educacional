using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Difficulty { Easy, Medium, Hard }

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Game Settings")]
    [SerializeField] private float gameDuration = 60f;
    [SerializeField] private Difficulty currentDifficulty = Difficulty.Easy;

    public int Score { get; private set; }
    public float TimeRemaining { get; private set; }
    public bool IsGameOver { get; private set; }

    // Events for the UI to listen to
    public event Action<int> OnScoreChanged;
    public event Action<float> OnTimeChanged;
    public event Action<Difficulty> OnDifficultySet;
    public event Action OnGameOver;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        TimeRemaining = gameDuration;
        Score = 0;
        IsGameOver = false;
        
        OnScoreChanged?.Invoke(Score);
        OnDifficultySet?.Invoke(currentDifficulty);
    }

    private void Update()
    {
        if (IsGameOver) return;

        TimeRemaining -= Time.deltaTime;
        OnTimeChanged?.Invoke(TimeRemaining);

        if (TimeRemaining <= 0)
        {
            EndGame();
        }
    }

    public void AddScore(int points)
    {
        if (IsGameOver) return;
        Score += points;
        OnScoreChanged?.Invoke(Score);
    }

    public Difficulty GetDifficulty()
    {
        return currentDifficulty;
    }

    private void EndGame()
    {
        IsGameOver = true;
        TimeRemaining = 0;
        OnTimeChanged?.Invoke(TimeRemaining);
        OnGameOver?.Invoke();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}