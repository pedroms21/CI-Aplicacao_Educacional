using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuAmarelo : MonoBehaviour
{
    public void EscolherNivel1()
    {
        SceneManager.LoadScene("Reino_amarelo_Jogo_Nivel1");
    }

    public void EscolherNivel2()
    {
        SceneManager.LoadScene("Reino_amarelo_Jogo_Nivel2");
    }

    public void EscolherNivel3()
    {
        SceneManager.LoadScene("Reino_amarelo_Jogo_Nivel3");
    }

    // --- NOVAS FUNÇÕES PARA OS PAINÉIS DE FIM DE JOGO ---

    public void TentarNovamente()
    {
        // O Unity vê em que cena estamos agora mesmo e carrega-a de novo do zero
        Scene cenaAtual = SceneManager.GetActiveScene();
        SceneManager.LoadScene(cenaAtual.name);
    }

    public void SairParaSaladeNiveis()
    {
        // Substitui pelo nome exato da tua cena da sala de jogos
        SceneManager.LoadScene("Reino_amarelo_Jogo");
    }

    public void SairParaReinoAmarelo()
    {
        // Substitui pelo nome exato da tua cena da sala de jogos
        SceneManager.LoadScene("Reino_amarelo");
    }
}