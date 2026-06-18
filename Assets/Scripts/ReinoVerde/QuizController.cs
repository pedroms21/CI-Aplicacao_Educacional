using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QuizController : MonoBehaviour
{
    [Header("Os Botões de Resposta")]
    public Button btnOpcaoA;
    public Button btnOpcaoB;

    [Tooltip("Coloca o visto se a Opção A for a correta. Tira o visto se for a B.")]
    public bool opcaoA_EstaCorreta;

    [Header("A Cientista")]
    public Image cientistaImage; // Arrasta a imagem da tua cientista para aqui
    public Sprite cientistaNormal; // A expressão original
    public Sprite cientistaOps;    // A expressão de erro

    [Header("Cores do Feedback")]
    public Color corNormal = Color.white;
    public Color corCerta = Color.green;
    public Color corErrada = Color.red;

    private LessonManager lessonManager;
    private bool jaAcertou = false;

    void Start()
    {
        // Encontra automaticamente o LessonManager que criaste antes
        lessonManager = FindObjectOfType<LessonManager>();

        // Diz aos botões o que fazer quando são clicados (assim não precisas de configurar no Inspector!)
        btnOpcaoA.onClick.AddListener(() => VerificarResposta(btnOpcaoA, opcaoA_EstaCorreta));
        btnOpcaoB.onClick.AddListener(() => VerificarResposta(btnOpcaoB, !opcaoA_EstaCorreta));
    }

    private void OnEnable()
    {
        // Sempre que o slide aparece, garante que as cores e a cientista estão normais
        btnOpcaoA.GetComponent<Image>().color = corNormal;
        btnOpcaoB.GetComponent<Image>().color = corNormal;

        if (cientistaImage != null && cientistaNormal != null)
            cientistaImage.sprite = cientistaNormal;

        jaAcertou = false;
    }

    public void VerificarResposta(Button botaoClicado, bool estaCorreta)
    {
        // Se já acertou, ignora outros cliques enquanto espera os 2 segundos
        if (jaAcertou) return;

        if (estaCorreta)
        {
            jaAcertou = true;
            botaoClicado.GetComponent<Image>().color = corCerta;

            // Garante que a cientista fica feliz de novo se ele acertar à segunda tentativa
            cientistaImage.sprite = cientistaNormal;

            // Inicia a contagem de 2 segundos
            StartCoroutine(EsperarEAvancar());
        }
        else
        {
            // Errou! Pinta de vermelho e muda a cara da cientista
            botaoClicado.GetComponent<Image>().color = corErrada;
            cientistaImage.sprite = cientistaOps;
        }
    }

    private IEnumerator EsperarEAvancar()
    {
        yield return new WaitForSeconds(2f);
        lessonManager.AvancarSlide();
    }
}