using UnityEngine;
using UnityEngine.SceneManagement; // Obrigatório para manipular cenas

public class BotaoVoltar : MonoBehaviour
{
    public void VoltarCenaAnterior()
    {
        // Pega o número (índice) da cena atual
        int indiceCenaAtual = SceneManager.GetActiveScene().buildIndex;
        
        // Carrega a cena anterior na lista do Build Settings
        // O Math.Max evita que o jogo tente carregar uma cena menor que 0
        int cenaAnterior = Mathf.Max(0, indiceCenaAtual - 1);
        
        SceneManager.LoadScene(cenaAnterior);
    }
}