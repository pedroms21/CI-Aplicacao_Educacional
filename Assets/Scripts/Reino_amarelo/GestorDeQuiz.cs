using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections; 

[System.Serializable]
public class PerguntaQuiz
{
    [TextArea(2, 3)]
    public string textoDaPergunta;
    public Sprite imagemBotao1;
    public Sprite imagemBotao2;
    public int botaoCorreto; 
}

public class GestorDeQuiz : MonoBehaviour
{
    [Header("Estrutura do Quiz")]
    public GameObject painelQuiz;  
    public GestorDeAula gestorDeAula; // Referência para avisar a lição quando voltar atrás

    [Header("Configurações das Perguntas")]
    public PerguntaQuiz[] perguntasDoQuiz; 
    public TextMeshProUGUI textoPerguntaUI;
    public Image imagemBotao1UI;
    public Image imagemBotao2UI;
    public GameObject botao1, botao2; 
    public GameObject botaoSairDaSala, botaoVoltarAprender;

    [Header("Feedback da Professora")]
    public Image imagemDaProfessora; 
    public Sprite professoraNormal;  
    public Sprite professoraErro;    

    [Header("Textos Finais (Editável no Unity)")]
    [TextArea(2, 4)]
    public string textoFimDoQuiz = "Parabéns! Já sabes tudo sobre o Reino Amarelo.\nPodes sair da sala ou voltar a aprender.";

    private int perguntaAtual = 0;

    void Start()
    {
        if (painelQuiz) painelQuiz.SetActive(false);
        if (botaoSairDaSala) botaoSairDaSala.SetActive(false);
        if (botaoVoltarAprender) botaoVoltarAprender.SetActive(false);

        if (imagemDaProfessora != null && professoraNormal != null) 
            imagemDaProfessora.sprite = professoraNormal;
    }

    // Esta função é ativada pelo GestorDeAula quando a lição chega ao fim
    public void IniciarQuiz()
    {
        if (painelQuiz) painelQuiz.SetActive(true);
        if (botaoSairDaSala) botaoSairDaSala.SetActive(false);
        if (botaoVoltarAprender) botaoVoltarAprender.SetActive(false);
        
        perguntaAtual = 0;
        MostrarPergunta();
    }

    private void MostrarPergunta()
    {
        if (imagemDaProfessora != null && professoraNormal != null) 
            imagemDaProfessora.sprite = professoraNormal;

        if (perguntaAtual < perguntasDoQuiz.Length)
        {
            botao1.SetActive(true);
            botao2.SetActive(true);

            imagemBotao1UI.color = Color.white;
            imagemBotao2UI.color = Color.white;
            botao1.GetComponent<Button>().interactable = true;
            botao2.GetComponent<Button>().interactable = true;
            
            textoPerguntaUI.text = perguntasDoQuiz[perguntaAtual].textoDaPergunta;
            imagemBotao1UI.sprite = perguntasDoQuiz[perguntaAtual].imagemBotao1;
            imagemBotao2UI.sprite = perguntasDoQuiz[perguntaAtual].imagemBotao2;
        }
        else
        {
            textoPerguntaUI.text = textoFimDoQuiz;
            botao1.SetActive(false); 
            botao2.SetActive(false);
            if (botaoSairDaSala) botaoSairDaSala.SetActive(true); 
            if (botaoVoltarAprender) botaoVoltarAprender.SetActive(true);
        }
    }

    public void ReceberResposta(int n)
    {
        if (n == perguntasDoQuiz[perguntaAtual].botaoCorreto) 
        {
            StartCoroutine(AnimacaoRespostaCerta());
        }
        else 
        {
            if (n == 1) imagemBotao1UI.color = Color.red;
            else imagemBotao2UI.color = Color.red;
            
            textoPerguntaUI.text = "Ups, tenta de novo!\n" + perguntasDoQuiz[perguntaAtual].textoDaPergunta;

            if (imagemDaProfessora != null && professoraErro != null) 
                imagemDaProfessora.sprite = professoraErro;
        }
    }

    private IEnumerator AnimacaoRespostaCerta()
    {
        botao1.GetComponent<Button>().interactable = false;
        botao2.GetComponent<Button>().interactable = false;
        textoPerguntaUI.text = "Acertaste! Muito bem!";

        if (imagemDaProfessora != null && professoraNormal != null) 
            imagemDaProfessora.sprite = professoraNormal;

        yield return new WaitForSeconds(1.5f);
        perguntaAtual++; 
        MostrarPergunta(); 
    }

    public void SairDaSala() => SceneManager.LoadScene("Reino_amarelo");

    public void VoltarAAprender()
    {
        if (botaoSairDaSala) botaoSairDaSala.SetActive(false);
        if (botaoVoltarAprender) botaoVoltarAprender.SetActive(false);
        if (painelQuiz) painelQuiz.SetActive(false);
        
        // Avisa o script da lição para reativar o quadro
        if (gestorDeAula != null)
        {
            gestorDeAula.RecomecarLicao();
        }
    }
}