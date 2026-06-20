using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class WebGLVideoSetup : MonoBehaviour
{
    [Header("Video File Setup")]
    [Tooltip("Nome do ficheiro de vídeo na pasta StreamingAssets (ex: VideoDoLeite.mp4)")]
    public string videoFileName;

    [Header("UI Elements")]
    [Tooltip("Arraste o botão de Play da Hierarchy para aqui")]
    public GameObject playButton; // Referência para o botão

    private VideoPlayer videoPlayer;

    void Awake()
    {
        // Vai buscar o VideoPlayer logo no início
        videoPlayer = GetComponent<VideoPlayer>();
    }

    void Start()
    {
        // Configura o caminho do vídeo (URL para WebGL)
        videoPlayer.source = VideoSource.Url;
        videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, videoFileName);
    }

    // O OnEnable é executado SEMPRE que este objeto (o ecrã) é ativado/aparece no jogo
    void OnEnable()
    {
        // 1. Faz o botão Play voltar a aparecer
        if (playButton != null)
        {
            playButton.SetActive(true);
        }

        // 2. Faz reset ao vídeo para garantir que ele não fica a meio quando voltamos
        if (videoPlayer != null)
        {
            videoPlayer.Stop();
        }
    }
}