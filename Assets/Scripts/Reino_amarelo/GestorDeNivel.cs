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
    public TextMeshProUGUI textoVidas; // Local onde vai aparecer "Vidas: 3"

    [Header("Temporizador (Para o Nível 3)")]
    public bool usarTemporizador = false;
    public float tempoMaximo = 30f;
    public TextMeshProUGUI textoDoTempo;
    private float tempoAtual;

    [Header("Paineis de Fim de Jogo")]
    public GameObject painelVitoria;
    public GameObject painelDerrota;

    private bool jogoAdecorrer = false;

    void Start()
    {
        tempoAtual = tempoMaximo;
        vidasAtuais = vidasMaximas;

        if (painelVitoria) painelVitoria.SetActive(false);
        if (painelDerrota) painelDerrota.SetActive(false);
        
        // Esconder ou mostrar os textos no ecrã consoante as configurações
        if (textoDoTempo != null) textoDoTempo.gameObject.SetActive(usarTemporizador);
        if (textoVidas != null) 
        {
            textoVidas.gameObject.SetActive(usarVidas);
            AtualizarTextoVidas();
        }
    }

    void Update()
    {
        // Só corre o tempo se estiver ligado e o jogo a decorrer
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
        if (!jogoAdecorrer) return; // Se o jogo já acabou, ignora

        acertosAtuais++;
        if (acertosAtuais >= totalParaGanhar)
        {
            Vitoria();
        }
    }

    public void PerderVida()
    {
        if (!jogoAdecorrer || !usarVidas) return; // Só perde vida se o sistema estiver ligado

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
    }

    void Derrota()
    {
        jogoAdecorrer = false;
        if (painelDerrota) painelDerrota.SetActive(true);
    }

    public void IniciarJogo()
{
    jogoAdecorrer = true;
}
}