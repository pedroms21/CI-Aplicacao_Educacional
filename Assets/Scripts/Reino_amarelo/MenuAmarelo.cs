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
}