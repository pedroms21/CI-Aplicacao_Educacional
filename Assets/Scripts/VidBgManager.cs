using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using System.IO;

[RequireComponent(typeof(VideoPlayer))]
public class VidBgManager : MonoBehaviour
{
    [Header("UI References")]
    [Tooltip("Arrasta a tua Raw Image (BackgroundVideoDisplay) para aqui.")]
    public RawImage videoDisplay;

    [Header("Configuração do Vídeo")]
    [Tooltip("Escreve o nome exato do ficheiro que guardaste na pasta StreamingAssets.")]
    public string nomeDoVideo = "bg.mp4";

    private VideoPlayer videoPlayer;

    void Start()
    {
        // 1. Começamos com a imagem invisível (Alpha 0) para o jogador não ver lixo visual ou ecrã preto
        if (videoDisplay != null)
        {
            Color c = videoDisplay.color;
            c.a = 0f;
            videoDisplay.color = c;
        }

        videoPlayer = GetComponent<VideoPlayer>();

        // 2. Forçamos o leitor a saber que vai ler a partir de um caminho/URL
        videoPlayer.source = VideoSource.Url;

        // 3. Geramos o caminho dinâmico para a pasta StreamingAssets
        // No PC gera um caminho de disco (C:/...), na WebWebGL gera um link web (http://...)
        string caminhoCompleto = Path.Combine(Application.streamingAssetsPath, nomeDoVideo);
        videoPlayer.url = caminhoCompleto;

        // 4. Subscrevemos o evento: "Quando o primeiro frame estiver pronto na memória, avisa-me"
        videoPlayer.prepareCompleted += OnVideoPrepared;

        // 5. Começa a carregar o vídeo em background de forma silenciosa
        videoPlayer.Prepare();
    }

    void OnVideoPrepared(VideoPlayer vp)
    {
        // 6. O primeiro frame do oceano já está carregado e congelado! 
        // Tornamos a imagem visível (Alpha 1) e damos ordem de Play. O loop é perfeito!
        if (videoDisplay != null)
        {
            Color c = videoDisplay.color;
            c.a = 1f;
            videoDisplay.color = c;
        }
        videoPlayer.Play();
    }

    void OnDestroy()
    {
        // Limpeza obrigatória para evitar memory leaks ao mudar de cena
        if (videoPlayer != null)
        {
            videoPlayer.prepareCompleted -= OnVideoPrepared;
        }
    }
}