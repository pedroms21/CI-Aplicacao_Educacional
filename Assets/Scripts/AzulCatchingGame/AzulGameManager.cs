using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class AzulGameManager : MonoBehaviour
{
    public static AzulGameManager instance;

    [Header("Estatísticas")]
    public int score = 0;
    public int lives = 3;
    public int scoreToWin = 400; // Atualizado para a tua meta!

    [Header("Aumento de Dificuldade")]
    public int scoreMilestone = 80; // A cada 80 pontos...
    private int nextMilestone; // Guarda o próximo objetivo (80, 160, 240...)
    public PlayerController playerController; // Ligação à tua Arca
    public float playerSpeedBoost = 2f; // Quanto a arca fica mais rápida
    public float gravityMultiplier = 1.25f; // A gravidade aumenta 25%

    [Header("Interface (UI)")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    
    [Header("Ecrã de Fim de Jogo")]
    public GameObject endGamePanel;
    public TextMeshProUGUI endGameText;

    private bool isGameOver = false;
    private Vector2 defaultGravity = new Vector2(0, -9.81f); // A gravidade normal do Unity

    void Awake()
    {
        if (instance == null) instance = this;
    }

    void Start()
    {
        Time.timeScale = 1f; 
        
        // MUITO IMPORTANTE: Reinicia a gravidade sempre que o nível começa!
        Physics2D.gravity = defaultGravity; 
        
        nextMilestone = scoreMilestone; // O primeiro marco é aos 80

        if (endGamePanel != null) endGamePanel.SetActive(false);
        UpdateUI();
    }

    public void AddScore(int amount)
    {
        if (isGameOver) return; 

        score += amount;
        UpdateUI();

        // Verifica se chegámos aos 80, 160, 240, etc...
        if (score >= nextMilestone)
        {
            IncreaseDifficulty();
            nextMilestone += scoreMilestone; // Define o próximo marco
        }

        // Verifica se ganhámos!
        if (score >= scoreToWin)
        {
            EndGame(true);
        }
    }

    void IncreaseDifficulty()
    {
        // 1. Aumenta a velocidade da tua Arca
        if (playerController != null)
        {
            playerController.speed += playerSpeedBoost;
        }

        // 2. Aumenta a velocidade de queda (Gravidade mais forte!)
        Physics2D.gravity = new Vector2(0, Physics2D.gravity.y * gravityMultiplier);
        
        Debug.Log("Dificuldade Aumentada! Pontos: " + score);
    }

    public void LoseLife(int amount)
    {
        if (isGameOver) return; 

        lives -= amount;
        if (lives < 0) lives = 0;
        
        UpdateUI();
        
        if (lives == 0)
        {
            EndGame(false);
        }
    }

    void UpdateUI()
    {
        if (scoreText != null) scoreText.text = "Pontos: " + score;
        if (livesText != null) livesText.text = "Vidas: " + lives;
    }

    void EndGame(bool won)
    {
        isGameOver = true;
        Time.timeScale = 0f; 

        if (endGamePanel != null) endGamePanel.SetActive(true);

        if (won)
        {
            endGameText.text = "Vitória!\nChegaste aos " + scoreToWin + " pontos!";
            endGameText.color = Color.green; 
        }
        else
        {
            endGameText.text = "Fim de Jogo!\nFicaste sem vidas!";
            endGameText.color = Color.red; 
        }
    }

    public void RestartGame()
    {
        Physics2D.gravity = defaultGravity; // Prevenção para garantir que não reinicia ultra-rápido
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}