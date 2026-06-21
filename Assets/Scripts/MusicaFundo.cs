using UnityEngine;

public class MusicaFundo : MonoBehaviour
{
    // Guarda a memória de quem é o leitor de música original
    private static MusicaFundo instancia;

    void Awake()
    {
        // Se ainda não existir nenhum leitor de música...
        if (instancia == null)
        {
            instancia = this; // Eu sou o original!
            DontDestroyOnLoad(gameObject); // Protege-me para não ser destruído ao mudar de cena
        }
        else
        {
            // Se já houver uma música a tocar e eu for uma cópia (por exemplo, voltaste ao Menu), destrói-me!
            Destroy(gameObject);
        }
    }
}