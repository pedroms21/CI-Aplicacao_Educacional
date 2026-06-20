using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ControladorJogo : MonoBehaviour
{
    public static ControladorJogo instancia;

    public enum Dificuldade { Facil, Medio, Dificil }

    [Header("Configuracao do Nivel")]
    public Dificuldade nivelAtual = Dificuldade.Facil;

    [Header("Configuracoes dos Ninhos")]
    public List<Alimento> listaAlimentosNosNinhos;

    [Header("Sprites de Alimentos")]
    public List<Sprite> proteinas;
    public List<Sprite> intrusos;

    [Header("Estado do Jogo")]
    public int pontuacao = 0;
    public float tempoRestante = 20f;
    private bool jogoAtivo = true;

    [Header("Componentes de Interface")]
    public TextMeshProUGUI textoPontos;
    public TextMeshProUGUI textoTempo;

    [Header("Componentes do Fim de Jogo")]
    public GameObject painelFimDeJogo;
    public TextMeshProUGUI textoMensagemFinal;

    void Awake()
    {
        instancia = this;
    }

    void Start()
    {
        StartCoroutine(FluxoDoJogo());
    }

    void Update()
    {
        if (!jogoAtivo) return;

        tempoRestante -= Time.deltaTime;
        textoTempo.text = "Tempo restante: " + Mathf.Max(0, Mathf.RoundToInt(tempoRestante)).ToString();

        if (tempoRestante <= 0)
        {
            FimDeJogo();
        }
    }

    IEnumerator FluxoDoJogo()
    {
        if (listaAlimentosNosNinhos == null || listaAlimentosNosNinhos.Count == 0 || proteinas == null || proteinas.Count == 0)
        {
            Debug.LogError("Erro: Verifica as listas no Inspector do _GeradorJogo!");
            yield break;
        }

        while (jogoAtivo)
        {
            float tempoMinEspera = 1.4f;
            float tempoMaxEspera = 2.2f;
            float tempoVisivel = 2.0f;

            if (nivelAtual == Dificuldade.Medio)
            {
                tempoMinEspera = 1.0f;
                tempoMaxEspera = 1.8f;
                tempoVisivel = 1.5f;
            }
            else if (nivelAtual == Dificuldade.Dificil)
            {
                tempoMinEspera = 0.6f;
                tempoMaxEspera = 1.2f;
                tempoVisivel = 1.0f;
            }

            yield return new WaitForSeconds(Random.Range(tempoMinEspera, tempoMaxEspera));

            int ninhoAleatorio = Random.Range(0, listaAlimentosNosNinhos.Count);

            bool criarProteina = true;
            Sprite spriteEscolhido = null;

            if (nivelAtual == Dificuldade.Facil)
            {
                criarProteina = true;
                spriteEscolhido = proteinas[Random.Range(0, proteinas.Count)];
            }
            else if (nivelAtual == Dificuldade.Medio)
            {
                if (intrusos != null && intrusos.Count > 0 && Random.value > 1.0f)
                {
                    criarProteina = false;
                    spriteEscolhido = intrusos[0];
                }
                else
                {
                    criarProteina = true;
                    spriteEscolhido = proteinas[Random.Range(0, proteinas.Count)];
                }
            }
            else if (nivelAtual == Dificuldade.Dificil)
            {
                if (intrusos != null && intrusos.Count > 0 && Random.value > 0.8f)
                {
                    criarProteina = false;
                    spriteEscolhido = intrusos[Random.Range(0, intrusos.Count)];
                }
                else
                {
                    criarProteina = true;
                    spriteEscolhido = proteinas[Random.Range(0, proteinas.Count)];
                }
            }

            if (spriteEscolhido != null)
            {
                listaAlimentosNosNinhos[ninhoAleatorio].AtivarAlimento(spriteEscolhido, criarProteina, tempoVisivel);
            }
        }
    }

    public void AdicionarPontos(int valor)
    {
        if (!jogoAtivo) return;
        pontuacao += valor;
        textoPontos.text = "Pontuacao: " + pontuacao.ToString();
    }

    void FimDeJogo()
    {
        jogoAtivo = false;

        if (painelFimDeJogo != null)
        {
            painelFimDeJogo.SetActive(true);
        }

        if (textoMensagemFinal != null)
        {
            // 1. Vai buscar o texto exato que escreveste no Unity (ex: "Parabéns! Conseguiste X pontos!")
            string textoOriginal = textoMensagemFinal.text;

            // 2. Substitui o "X" pela pontuação real e atualiza o ecrã
            textoMensagemFinal.text = textoOriginal.Replace("X", pontuacao.ToString());
        }

        Debug.Log("O jogo terminou e o ecra de parabens foi ativado!");
    }

    public void BotaoJogarNovamente()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BotaoVoltarAoMenu()
    {
        SceneManager.LoadScene("Reino_vermelho");
    }
}