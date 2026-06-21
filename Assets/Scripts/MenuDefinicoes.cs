using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MenuDefinicoes : MonoBehaviour
{
    [Header("O UI do Menu")]
    public GameObject painelDefinicoes;
    public Slider sliderGeral; // NOVO SLIDER
    public Slider sliderMusica;
    public Slider sliderVideos;

    [Header("A Nossa Mesa de Mistura")]
    public AudioMixer mixerGeral;

    void Start()
    {
        painelDefinicoes.SetActive(false);

        // Vai à memória buscar os volumes
        float volumeGeral = PlayerPrefs.GetFloat("VolumeGeral", 1f);
        float volumeMusica = PlayerPrefs.GetFloat("VolumeMusica", 1f);
        float volumeVideos = PlayerPrefs.GetFloat("VolumeVideos", 1f);

        // Atualiza a posição visual das barrinhas
        if (sliderGeral != null) sliderGeral.value = volumeGeral;
        if (sliderMusica != null) sliderMusica.value = volumeMusica;
        if (sliderVideos != null) sliderVideos.value = volumeVideos;

        // Adiciona as ordens de mudança
        if (sliderGeral != null) sliderGeral.onValueChanged.AddListener(MudarVolumeGeral);
        if (sliderMusica != null) sliderMusica.onValueChanged.AddListener(MudarVolumeMusica);
        if (sliderVideos != null) sliderVideos.onValueChanged.AddListener(MudarVolumeVideos);

        // Aplica o som inicial
        MudarVolumeGeral(volumeGeral);
        MudarVolumeMusica(volumeMusica);
        MudarVolumeVideos(volumeVideos);
    }

    public void AbrirPainel() => painelDefinicoes.SetActive(true);
    public void FecharPainel() => painelDefinicoes.SetActive(false);

    // NOVA FUNÇÃO
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
}