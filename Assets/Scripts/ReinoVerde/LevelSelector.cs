using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    // O nome exato da tua cena de jogo da memória
    public string nomeCenaJogo = "Reino_Verde_Memoria";

    public void EscolherNivelFacil()
    {
        // 1 = Fácil (6 cartas)
        PlayerPrefs.SetInt("DificuldadeMemoria", 1);
        SceneManager.LoadScene(nomeCenaJogo);
    }

    public void EscolherNivelMedio()
    {
        // 2 = Médio (12 cartas)
        PlayerPrefs.SetInt("DificuldadeMemoria", 2);
        SceneManager.LoadScene(nomeCenaJogo);
    }

    public void EscolherNivelDificil()
    {
        // 3 = Difícil (20 cartas)
        PlayerPrefs.SetInt("DificuldadeMemoria", 3);
        SceneManager.LoadScene(nomeCenaJogo);
    }
}