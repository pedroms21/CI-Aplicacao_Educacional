using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // Biblioteca essencial para detetar o rato

public class EfeitoLuzTV : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Configurações da Luz")]
    public Image imagemLuz;
    public float velocidade = 1.5f;       // Rapidez da pulsação
    public float opacidadeMinima = 0.2f;  // Quão transparente fica
    public float opacidadeMaxima = 1f;    // Quão forte fica
    
    [Header("Configurações de Tamanho")]
    public float escalaMinima = 1f;       // Tamanho normal
    public float escalaMaxima = 1.15f;    // Tamanho máximo (Hover)

    private float tempoAcumulado = 0f;
    private bool ratoEmCima = false;
    private float valorAtual = 0f;

    void Start()
    {
        if (imagemLuz == null)
        {
            imagemLuz = GetComponent<Image>();
        }
    }

    void Update()
    {
        if (ratoEmCima)
        {
            // O rato está em cima: vai suavemente para o tamanho e brilho máximos (valor 1)
            valorAtual = Mathf.Lerp(valorAtual, 1f, Time.deltaTime * 10f);
        }
        else
        {
            // O rato não está em cima: o tempo avança e cria o efeito de pulsação normal
            tempoAcumulado += Time.deltaTime * velocidade;
            valorAtual = Mathf.PingPong(tempoAcumulado, 1f);
        }

        // 1. Animar a Opacidade (Alpha)
        if (imagemLuz != null)
        {
            float alphaAtual = Mathf.Lerp(opacidadeMinima, opacidadeMaxima, valorAtual);
            Color corAtual = imagemLuz.color;
            corAtual.a = alphaAtual;
            imagemLuz.color = corAtual;

            // 2. Animar o Tamanho (Escala) do ecrã (e de todos os seus filhos, como o texto "Jogar")
            float escalaAtual = Mathf.Lerp(escalaMinima, escalaMaxima, valorAtual);
            imagemLuz.rectTransform.localScale = new Vector3(escalaAtual, escalaAtual, 1f);
        }
    }

    // O Unity chama esta função automaticamente quando o rato ENTRA no objeto
    public void OnPointerEnter(PointerEventData eventData)
    {
        ratoEmCima = true;
    }

    // O Unity chama esta função automaticamente quando o rato SAI do objeto
    public void OnPointerExit(PointerEventData eventData)
    {
        ratoEmCima = false;
        
        // Sincroniza o tempo para que a pulsação recomece perfeitamente a partir do topo
        tempoAcumulado = 1f; 
    }
}