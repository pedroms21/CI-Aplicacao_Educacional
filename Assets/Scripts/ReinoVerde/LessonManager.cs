using UnityEngine;
using UnityEngine.SceneManagement; 

public class LessonManager : MonoBehaviour
{
    [Header("Os painéis da aula por ordem")]
    public GameObject[] slides;

    private int slideAtual = -1;

    void Start()
    {
        // Garante que, ao iniciar, nenhum slide está visível
        for (int i = 0; i < slides.Length; i++)
        {
            slides[i].SetActive(false);
        }
    }

    public void IniciarAula()
    {
        if (slides.Length > 0)
        {
            slideAtual = 0;
            slides[slideAtual].SetActive(true);
        }
    }

    public void AvancarSlide()
    {
        // Verifica se ainda temos slides para a frente
        if (slideAtual >= 0 && slideAtual < slides.Length - 1)
        {
            slides[slideAtual].SetActive(false); // Desliga o atual
            slideAtual++;                        // Avança o número
            slides[slideAtual].SetActive(true);  // Liga o novo
        }
    }

    public void RepetirAula()
    {
        // Desliga todos os slides para limpar o ecrã
        for (int i = 0; i < slides.Length; i++)
        {
            slides[i].SetActive(false);
        }

        // Volta a ligar apenas o primeiro slide (Slide 0)
        slideAtual = 0;
        slides[slideAtual].SetActive(true);
    }

    // Função para o botão "Voltar ao Menu"
    public void VoltarAoMenu()
    {
        SceneManager.LoadScene("MainScene");
    }
}