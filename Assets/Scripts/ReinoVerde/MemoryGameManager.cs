using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Necessário para o TextMeshPro
using UnityEngine.SceneManagement;

public class MemoryGameManager : MonoBehaviour
{
    [Header("Configurações UI")]
    public GameObject cardPrefab;
    public Transform gridPanel;
    public GridLayoutGroup gridLayout;

    [Header("Sprites das Cartas")]
    public Sprite spriteCostas; // A folha/logo
    public Sprite[] spritesVegetais; // Adiciona aqui as imagens de alface, cenoura, etc.

    [Header("HUD e Ecrã Final")]
    public TextMeshProUGUI timerText;
    public GameObject endGamePanel;
    public TextMeshProUGUI endMessageText;

    private int totalCartas;
    private float tempoRestante;
    private bool jogoAtivo = false;
    private bool canClick = true; // Bloqueia os cliques enquanto compara 2 cartas

    private int paresEncontrados = 0;
    private int paresNecessarios;

    private CardController primeiraCarta;
    private CardController segundaCarta;

    void Start()
    {
        endGamePanel.SetActive(false);
        ConstruirNivel();
        jogoAtivo = true;
    }

    void Update()
    {
        if (jogoAtivo)
        {
            tempoRestante -= Time.deltaTime;

            // Atualiza o texto do timer (mostra apenas segundos inteiros)
            timerText.text = "Tempo: " + Mathf.Ceil(tempoRestante).ToString() + "s";

            if (tempoRestante <= 0)
            {
                tempoRestante = 0;
                TerminarJogo(false); // Fim do tempo = Derrota
            }
        }
    }

    void ConstruirNivel()
    {
        // Limpa quaisquer cartas que tenham ficado esquecidas no GridPanel
        foreach (Transform child in gridPanel)
        {
            Destroy(child.gameObject);
        }

        int dificuldade = PlayerPrefs.GetInt("DificuldadeMemoria", 1);

        // Ajusta as regras consoante a dificuldade
        switch (dificuldade)
        {
            case 1: // Fácil
                totalCartas = 6;
                gridLayout.constraintCount = 3;
                tempoRestante = 30f;
                break;
            case 2: // Médio
                totalCartas = 12;
                gridLayout.constraintCount = 4;
                tempoRestante = 70f;
                break;
            case 3: // Difícil
                totalCartas = 20;
                gridLayout.constraintCount = 5;
                tempoRestante = 90f;
                break;
        }

        paresNecessarios = totalCartas / 2;

        // Criar a lista de IDs para fazer os pares (ex: para 6 cartas -> 0,0,1,1,2,2)
        List<int> cardIDs = new List<int>();
        for (int i = 0; i < paresNecessarios; i++)
        {
            cardIDs.Add(i);
            cardIDs.Add(i);
        }

        // Baralhar a lista de IDs
        for (int i = 0; i < cardIDs.Count; i++)
        {
            int temp = cardIDs[i];
            int randomIndex = Random.Range(i, cardIDs.Count);
            cardIDs[i] = cardIDs[randomIndex];
            cardIDs[randomIndex] = temp;
        }

        // Instanciar as cartas
        for (int i = 0; i < totalCartas; i++)
        {
            GameObject novaCarta = Instantiate(cardPrefab, gridPanel);

            novaCarta.SetActive(true);
            if (novaCarta.TryGetComponent<Image>(out Image img))
            {
                img.enabled = true;
            }
            if (novaCarta.TryGetComponent<Button>(out Button btn))
            {
                btn.enabled = true;
            }
            foreach (Transform child in novaCarta.transform)
            {
                child.gameObject.SetActive(true);
            }

            CardController controller = novaCarta.GetComponent<CardController>();

            int idDaCarta = cardIDs[i];
            Sprite rosto = spritesVegetais[idDaCarta];

            controller.Setup(spriteCostas, rosto, idDaCarta, this);
        }
    }

    // Função chamada pelas cartas
    public void CardRevealed(CardController cartaClicada)
    {
        if (primeiraCarta == null)
        {
            primeiraCarta = cartaClicada;
        }
        else if (segundaCarta == null)
        {
            segundaCarta = cartaClicada;
            StartCoroutine(VerificarPar());
        }
    }

    // Retorna se a criança pode clicar noutra carta ou não
    public bool CanClick()
    {
        return canClick && jogoAtivo;
    }

    private IEnumerator VerificarPar()
    {
        canClick = false; // Bloqueia cliques temporariamente

        // Verifica se os IDs são iguais
        if (primeiraCarta.GetCardID() == segundaCarta.GetCardID())
        {
            // Acertou!
            primeiraCarta.SetMatched();
            segundaCarta.SetMatched();
            paresEncontrados++;

            if (paresEncontrados >= paresNecessarios)
            {
                TerminarJogo(true); // Encontrou tudo = Vitória
            }
        }
        else
        {
            // Falhou. Espera 1 segundo para a criança memorizar e depois vira as cartas para baixo
            yield return new WaitForSeconds(1f);
            primeiraCarta.Hide();
            segundaCarta.Hide();
        }

        // Limpa a seleção e permite clicar de novo
        primeiraCarta = null;
        segundaCarta = null;
        canClick = true;
    }

    private void TerminarJogo(bool vitoria)
    {
        jogoAtivo = false;
        endGamePanel.SetActive(true);

        if (vitoria)
        {
            endMessageText.text = "Parabéns! Tens uma memória de ferro!";
            endMessageText.color = Color.green;
        }
        else
        {
            endMessageText.text = "O tempo acabou. Tenta novamente!";
            endMessageText.color = Color.red;
        }
    }

    // Funções para os botões do Ecrã Final
    public void JogarNovamente()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void VoltarAoMenu()
    {
        SceneManager.LoadScene("Reino_Verde_Jogo"); // O nome da cena anterior de seleção de níveis
    }
}