using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Esta função pode ser chamada pelos botões
    public void MudarCena(string nomeDaCena)
    {
        // Carrega a cena com o nome exato que escreveres no Unity
        SceneManager.LoadScene(nomeDaCena);
    }
}