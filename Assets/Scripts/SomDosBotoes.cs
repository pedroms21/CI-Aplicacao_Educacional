using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio; 

public class SomDosBotoes : MonoBehaviour
{
    [Header("Configuracoes de Audio")]
    public AudioClip somDeClique;
    public AudioMixerGroup grupoMixerSFX; 
    
    private AudioSource leitorDeSom;

    void Start()
    {
        leitorDeSom = gameObject.AddComponent<AudioSource>();
        leitorDeSom.playOnAwake = false;

        if (grupoMixerSFX != null)
        {
            leitorDeSom.outputAudioMixerGroup = grupoMixerSFX;
        }

        Button[] todosOsBotoes = GetComponentsInChildren<Button>(true);
        foreach (Button botao in todosOsBotoes)
        {
            botao.onClick.AddListener(TocarSomDeClique);
        }
    }

    void TocarSomDeClique()
    {
        if (somDeClique != null && leitorDeSom != null)
        {
            leitorDeSom.PlayOneShot(somDeClique);
        }
    }
}