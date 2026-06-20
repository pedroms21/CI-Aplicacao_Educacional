using UnityEngine;
using UnityEngine.SceneManagement;

public class SelecaoNiveis : MonoBehaviour
{
    // Função para o Botão Fácil
    public void CarregarNivelFacil()
    {
        // Substitui pelo nome exato da tua cena do nível fácil
        SceneManager.LoadScene("Reino_vermelho_apanhada_N1");
    }

    // Função para o Botão Médio
    public void CarregarNivelMedio()
    {
        // Substitui pelo nome exato da tua cena do nível médio
        SceneManager.LoadScene("Reino_vermelho_apanhada_N2");
    }

    // Função para o Botão Difícil
    public void CarregarNivelDificil()
    {
        // Substitui pelo nome exato da tua cena do nível difícil
        SceneManager.LoadScene("Reino_vermelho_apanhada_N3");
    }
}