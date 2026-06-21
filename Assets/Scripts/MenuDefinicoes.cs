using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MenuDefinicoes : MonoBehaviour
{
    [Header("O UI do Menu")]
    public GameObject painelDefinicoes;
    public Slider sliderGeral;
    public Slider sliderMusica;
    public Slider sliderVideos;
    public Slider sliderEfeitos;

    [Header("A Nossa Mesa de Mistura")]
    public AudioMixer mixerGeral;

    [Header("Valores Iniciais (Primeira vez a jogar)")]
    [Tooltip("Define o volume inicial de 0.0 (silêncio) a 1.0 (máximo)")]
    [Range(0f, 1f)] public float defaultGeral = 1f;
    [Range(0f, 1f)] public float defaultMusica = 0.4f;
    [Range(0f, 1f)] public float defaultVideos = 1f;
    [Range(0f, 1f)] public float defaultEfeitos = 1f;

    void Start()
    {
        painelDefinicoes.SetActive(false);

        // O segundo valor (ex: defaultMusica) só é usado se for a primeira vez!
        float volumeGeral = PlayerPrefs.GetFloat("VolumeGeral", defaultGeral);
        float volumeMusica = PlayerPrefs.GetFloat("VolumeMusica", defaultMusica);
        float volumeVideos = PlayerPrefs.GetFloat("VolumeVideos", defaultVideos);
        float volumeEfeitos = PlayerPrefs.GetFloat("VolumeEfeitos", defaultEfeitos);

        // Atualiza a posição visual das barrinhas para corresponder aos valores
        if (sliderGeral != null) sliderGeral.value = volumeGeral;
        if (sliderMusica != null) sliderMusica.value = volumeMusica;
        if (sliderVideos != null) sliderVideos.value = volumeVideos;
        if (sliderEfeitos != null) sliderEfeitos.value = volumeEfeitos;

        // Adiciona as ordens de mudança
        if (sliderGeral != null) sliderGeral.onValueChanged.AddListener(MudarVolumeGeral);
        if (sliderMusica != null) sliderMusica.onValueChanged.AddListener(MudarVolumeMusica);
        if (sliderVideos != null) sliderVideos.onValueChanged.AddListener(MudarVolumeVideos);
        if (sliderEfeitos != null) sliderEfeitos.onValueChanged.AddListener(MudarVolumeEfeitos);

        // Aplica o som aos ouvidos do jogo
        MudarVolumeGeral(volumeGeral);
        MudarVolumeMusica(volumeMusica);
        MudarVolumeVideos(volumeVideos);
        MudarVolumeEfeitos(volumeEfeitos);
    }

    public void AbrirPainel() => painelDefinicoes.SetActive(true);
    public void FecharPainel() => painelDefinicoes.SetActive(false);

    public void MudarVolumeGeral(float novoVolume)
    {
        float volumeAjustado = Mathf.Max(novoVolume, 0.0001f);
        mixerGeral.SetFloat("VolGeral", Mathf.Log10(volumeAjustado) * 20f);
        PlayerPrefs.SetFloat("VolumeGeral", novoVolume);
    }

    public void MudarVolumeMusica(float novoVolume)
    {
        float volumeAjustado = Mathf.Max(novoVolume, 0.0001f);
        mixerGeral.SetFloat("VolMusica", Mathf.Log10(volumeAjustado) * 20f);
        PlayerPrefs.SetFloat("VolumeMusica", novoVolume);
    }

    public void MudarVolumeVideos(float novoVolume)
    {
        float volumeAjustado = Mathf.Max(novoVolume, 0.0001f);
        mixerGeral.SetFloat("VolVideos", Mathf.Log10(volumeAjustado) * 20f);
        PlayerPrefs.SetFloat("VolumeVideos", novoVolume);
    }

    public void MudarVolumeEfeitos(float novoVolume)
    {
        float volumeAjustado = Mathf.Max(novoVolume, 0.0001f);
        mixerGeral.SetFloat("VolEfeitos", Mathf.Log10(volumeAjustado) * 20f);
        PlayerPrefs.SetFloat("VolumeEfeitos", novoVolume);
    }
}