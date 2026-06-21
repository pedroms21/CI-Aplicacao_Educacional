using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
[RequireComponent(typeof(AudioSource))]
public class WebGLVideoSetup : MonoBehaviour
{
    [Header("Video File Setup")]
    public string videoFileName;

    [Header("UI Elements")]
    public GameObject playButton;

    private VideoPlayer videoPlayer;
    private AudioSource audioSource;

    void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        audioSource = GetComponent<AudioSource>();

        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        videoPlayer.SetTargetAudioSource(0, audioSource);
    }

    void Start()
    {
        videoPlayer.source = VideoSource.Url;
        videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, videoFileName);

        // AVISAR QUANDO COMEÇA E ACABA:
        videoPlayer.started += QuandoOVideoComecar;
        videoPlayer.loopPointReached += QuandoOVideoAcabar; // loopPointReached significa "chegou ao fim"
    }

    // Quando clicas play e o vídeo arranca
    void QuandoOVideoComecar(VideoPlayer vp)
    {
        if (MusicaFundo.instancia != null)
        {
            MusicaFundo.instancia.PausarMusica();
        }
    }

    // Quando o vídeo chega ao último segundo
    void QuandoOVideoAcabar(VideoPlayer vp)
    {
        if (MusicaFundo.instancia != null)
        {
            MusicaFundo.instancia.RetomarMusica();
        }

        // (Opcional) Fazer o botão de play voltar a aparecer no fim
        if (playButton != null) playButton.SetActive(true);
    }

    void OnEnable()
    {
        if (playButton != null) playButton.SetActive(true);
        if (videoPlayer != null) videoPlayer.Stop();
    }

    // SEGURANÇA: Se a criança fechar o slide ou saltar a cena antes do vídeo acabar, a música volta!
    void OnDisable()
    {
        if (MusicaFundo.instancia != null)
        {
            MusicaFundo.instancia.RetomarMusica();
        }
    }
}