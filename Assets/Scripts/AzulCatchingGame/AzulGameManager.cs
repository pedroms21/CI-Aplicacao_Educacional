using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class AzulGameManager : MonoBehaviour
{
    public static AzulGameManager instance;

    [Header("Estatísticas")]
    public int score = 0;
    public int lives = 3;
    public int scoreToWin = 400;

    [Header("Aumento de Dificuldade")]
    public int scoreMilestone = 80; 
    private int nextMilestone; 
    public float gravityMultiplier = 1.25f; 

    [Header("Interface (UI)")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    
    [Header("Ecrã de Fim de Jogo")]
    public GameObject endGamePanel;
    public TextMeshProUGUI endGameText;

    // --- NOVA SECÇÃO DE SOM ---
    [Header("Sons")]
    public AudioClip somSaudavel;
    public AudioClip somErro;
    private AudioSource audioSource;
    // --------------------------

    private bool isGameOver = false;
    private Vector2 defaultGravity = new Vector2(0, -9.81f);

    void Awake()
    {
        if (instance == null) instance = this;
        
        // Vai buscar o componente de áudio que vamos adicionar a seguir
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) 
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Start()
    {
        Time.timeScale = 1f; 
        Physics2D.gravity = defaultGravity; 
        nextMilestone = scoreMilestone; 

        if (endGamePanel != null) endGamePanel.SetActive(false);
        UpdateUI();
    }

    public void AddScore(int amount)
    {
        if (isGameOver) return; 

        score += amount;
        UpdateUI();
        
        // Toca o som positivo!
        if (somSaudavel != null) audioSource.PlayOneShot(somSaudavel);

        if (score >= nextMilestone)
        {
            IncreaseDifficulty();
            nextMilestone += scoreMilestone; 
        }

        if (score >= scoreToWin)
        {
            EndGame(true);
        }
    }

    void IncreaseDifficulty()
    {
        Physics2D.gravity = new Vector2(0, Physics2D.gravity.y * gravityMultiplier);
        Debug.Log("Dificuldade Aumentada! Gravidade: " + Physics2D.gravity.y);
    }

    public void LoseLife(int amount)
    {
        if (isGameOver) return; 

        lives -= amount;
        if (lives < 0) lives = 0;
        
        UpdateUI();
        
        // Toca o som de erro!
        if (somErro != null) audioSource.PlayOneShot(somErro);
        
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
        Physics2D.gravity = defaultGravity; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}