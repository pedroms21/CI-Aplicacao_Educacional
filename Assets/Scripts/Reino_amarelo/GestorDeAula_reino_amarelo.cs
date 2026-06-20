using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections; 

public class GestorDeAula : MonoBehaviour
{
    [Header("Estrutura")]
    public GameObject quadroGrande; 
    public GameObject caixaDeChat; 
    public GameObject painelLicao; 

    [Header("Componente de Quiz (Novo)")]
    public GestorDeQuiz gestorDeQuiz; // Referência para o script do Quiz separado

    [Header("Elementos da Lição")]
    public TextMeshProUGUI textoDaLicao; 
    public Image imagemDaLicao;          
    public string[] textosPorPagina;  
    public Sprite[] imagensPorPagina; 

    [Header("Animação GIF (Página 2)")]
    public Sprite[] framesDoGif;         
    public float velocidadeGif = 0.5f;   

    private int paginaAtual = 0;
    private Coroutine animacaoGifCoroutine;

    void Start()
    {
        quadroGrande.SetActive(false);
        painelLicao.SetActive(true);
        if (caixaDeChat != null) caixaDeChat.SetActive(true); 
    }

    public void AbrirAula()
    {
        quadroGrande.SetActive(true);
        if (caixaDeChat != null) caixaDeChat.SetActive(false); 
        
        paginaAtual = 0;
        AtualizarQuadro();
        StartCoroutine(AnimarQuadroSubir());
    }

    public void AvancarPagina()
    {
        paginaAtual++;
        if (paginaAtual < textosPorPagina.Length) 
        {
            AtualizarQuadro();
        }
        else 
        {
            // As páginas da lição acabaram! Passa o testemunho ao Quiz
            if (gestorDeQuiz != null)
            {
                painelLicao.SetActive(false);
                gestorDeQuiz.IniciarQuiz();
            }
        }
    }

    private void UpdateQuadroVisual() // Método auxiliar interno
    {
        textoDaLicao.text = textosPorPagina[paginaAtual];

        if (paginaAtual == 1 && framesDoGif.Length > 0)
            animacaoGifCoroutine = StartCoroutine(TocarGif());
        else
            imagemDaLicao.sprite = imagensPorPagina[paginaAtual];
    }

    private void AtualizarQuadro()
    {
        if (animacaoGifCoroutine != null) StopCoroutine(animacaoGifCoroutine);
        UpdateQuadroVisual();
    }

    private IEnumerator TocarGif()
    {
        int f = 0;
        while (true) {
            imagemDaLicao.sprite = framesDoGif[f];
            f = (f + 1) % framesDoGif.Length;
            yield return new WaitForSeconds(velocidadeGif);
        }
    }

    // Função pública que o GestorDeQuiz vai chamar se a criança clicar em "Voltar a Aprender"
    public void RecomecarLicao()
    {
        painelLicao.SetActive(true);
        paginaAtual = 0;
        AtualizarQuadro();
    }

    private IEnumerator AnimarQuadroSubir()
    {
        RectTransform rect = quadroGrande.GetComponent<RectTransform>();
        Vector2 posEscondida = new Vector2(0, -1200f);
        Vector2 posCentro = new Vector2(0, 0f);
        float t = 0;
        while (t < 1) {
            t += Time.deltaTime / 0.6f;
            rect.anchoredPosition = Vector2.Lerp(posEscondida, posCentro, Mathf.SmoothStep(0, 1, t));
            yield return null;
        }
    }
}