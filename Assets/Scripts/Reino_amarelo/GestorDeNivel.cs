using UnityEngine;
using TMPro;

public class GestorDeNivel : MonoBehaviour
{
    [Header("Regras do Nível")]
    public int totalParaGanhar = 4;
    private int acertosAtuais = 0;

    [Header("Sistema de Vidas (Nível 1 e 2)")]
    public bool usarVidas = true;
    public int vidasMaximas = 3;
    private int vidasAtuais;
    public TextMeshProUGUI textoVidas; 

    [Header("Temporizador (Para o Nível 3)")]
    public bool usarTemporizador = false;
    public float tempoMaximo = 30f;
    public TextMeshProUGUI textoDoTempo;
    private float tempoAtual;

    [Header("Paineis de Fim de Jogo")]
    public GameObject painelVitoria;
    public GameObject painelDerrota;

    [Header("Efeitos Sonoros")]
    public AudioClip somAcerto;
    public AudioClip somErro;
    public AudioClip somVitoria;
    public AudioClip somDerrota; 
    
    private AudioSource leitorDeSom;

    private bool jogoAdecorrer = false;

    void Start()
    {
        leitorDeSom = GetComponent<AudioSource>();

        tempoAtual = tempoMaximo;
        vidasAtuais = vidasMaximas;

        if (painelVitoria) painelVitoria.SetActive(false);
        if (painelDerrota) painelDerrota.SetActive(false);
        
        if (textoDoTempo != null) textoDoTempo.gameObject.SetActive(usarTemporizador);
        if (textoVidas != null) 
        {
            textoVidas.gameObject.SetActive(usarVidas);
            AtualizarTextoVidas();
        }
    }

    void Update()
    {
        if (usarTemporizador && jogoAdecorrer)
        {
            tempoAtual -= Time.deltaTime;
            if (textoDoTempo != null) textoDoTempo.text = "Tempo: " + Mathf.CeilToInt(tempoAtual).ToString();

            if (tempoAtual <= 0)
            {
                tempoAtual = 0;
                jogoAdecorrer = false;
                Derrota();
            }
        }
    }

    public void AdicionarAcerto()
    {
        if (!jogoAdecorrer) return; 

        acertosAtuais++; // Primeiro adicionamos o ponto

        // Depois verificamos se é o último ponto
        if (acertosAtuais >= totalParaGanhar)
        {
            Vitoria(); // Vai para a vitória (e toca apenas o som de vitória)
        }
        else
        {
            // Se NÃO for o último ponto, toca o som de acerto normal
            if (somAcerto != null && leitorDeSom != null) leitorDeSom.PlayOneShot(somAcerto);
        }
    }

    public void PerderVida()
    {
        if (!jogoAdecorrer || !usarVidas) return; 

        if (somErro != null && leitorDeSom != null) leitorDeSom.PlayOneShot(somErro);

        vidasAtuais--;
        AtualizarTextoVidas();

        if (vidasAtuais <= 0)
        {
            jogoAdecorrer = false;
            Derrota();
        }
    }

    void AtualizarTextoVidas()
    {
        if (textoVidas != null)
        {
            textoVidas.text = "Vidas: " + vidasAtuais.ToString();
        }
    }

    void Vitoria()
    {
        jogoAdecorrer = false;
        if (painelVitoria) painelVitoria.SetActive(true);

        if (somVitoria != null && leitorDeSom != null) leitorDeSom.PlayOneShot(somVitoria);
    }

    void Derrota()
    {
        jogoAdecorrer = false;
        if (painelDerrota) painelDerrota.SetActive(true);

        if (somDerrota != null && leitorDeSom != null) leitorDeSom.PlayOneShot(somDerrota);
    }

    public void IniciarJogo()
    {
        jogoAdecorrer = true;
    }
}