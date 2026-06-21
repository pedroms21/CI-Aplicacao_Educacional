using UnityEngine;

public class MusicaFundo : MonoBehaviour
{
    // O 'public static' permite que qualquer script no jogo fale com esta música!
    public static MusicaFundo instancia;
    private AudioSource meuAudio;

    void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject);
            meuAudio = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Função para o vídeo mandar pausar
    public void PausarMusica()
    {
        if (meuAudio != null) meuAudio.Pause();
    }

    // Função para o vídeo mandar recomeçar
    public void RetomarMusica()
    {
        if (meuAudio != null) meuAudio.UnPause();
    }
}