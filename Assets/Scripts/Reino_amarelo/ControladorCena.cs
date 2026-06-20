using UnityEngine;
using System.Collections;

public class ControladorCena : MonoBehaviour
{
    [Header("Objetos da Interface")]
    public GameObject popUpChat;
    public GameObject botaoComecar; // <- NOVO: para o botão desaparecer também!
    public RectTransform painelJogo; 
    public GestorDeNivel gestor;

    private Vector2 posicaoVisivel;
    private Vector2 posicaoEscondida;

    void Start()
    {
        // Guarda a posição onde a mesa está agora (no centro do ecrã)
        posicaoVisivel = painelJogo.anchoredPosition;
        
        // Define a posição escondida bem lá no alto (2500 pixeis para cima)
        posicaoEscondida = posicaoVisivel + new Vector2(0, 2500f); 
        
        // Põe a mesa lá em cima logo no início
        painelJogo.anchoredPosition = posicaoEscondida;
        
        // Garante que o Chat e o Botão estão visíveis quando o jogo arranca
        popUpChat.SetActive(true);
        if (botaoComecar != null) botaoComecar.SetActive(true);
    }

    public void ClicouNoBotaoComecar()
    {
        // Desliga o chat e o botão "Começar"
        popUpChat.SetActive(false);
        if (botaoComecar != null) botaoComecar.SetActive(false);
        
        // Começa a animação da mesa a descer
        StartCoroutine(DeslizarPainelJogo());
    }

    IEnumerator DeslizarPainelJogo()
    {
        float tempo = 0;
        float duracao = 1f; // Demora 1 segundo a cair

        while (tempo < duracao)
        {
            tempo += Time.deltaTime;
            
            // Move suavemente do topo até ao centro
            painelJogo.anchoredPosition = Vector2.Lerp(posicaoEscondida, posicaoVisivel, tempo / duracao);
            yield return null;
        }

        // Garante que a mesa bate na posição exata no final
        painelJogo.anchoredPosition = posicaoVisivel;
        
        // Avisa o Gestor que o jogo já começou e já podem arrastar a comida!
        gestor.IniciarJogo(); 
    }
}