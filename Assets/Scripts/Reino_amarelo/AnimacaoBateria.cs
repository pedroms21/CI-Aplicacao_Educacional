using UnityEngine;
using UnityEngine.UI; // Importante para podermos mexer em imagens da UI
using System.Collections;

public class AnimacaoBateria : MonoBehaviour
{
    [Header("As tuas Imagens")]
    public Sprite frameCheio;  // Arrasta para aqui o frame01
    public Sprite frameVazio;  // Arrasta para aqui o frame02
    
    [Header("Configurações")]
    public float tempoDeCadaFrame = 0.5f; // Tempo em segundos antes de trocar
    
    private Image imagemUI;

    void Start()
    {
        // Vai buscar automaticamente o componente de Imagem que está neste objeto
        imagemUI = GetComponent<Image>();

        if (imagemUI != null)
        {
            // Começa o loop infinito
            StartCoroutine(LoopDeAnimacao());
        }
        else
        {
            Debug.LogWarning("Não encontrei um componente 'Image' neste objeto!");
        }
    }

    IEnumerator LoopDeAnimacao()
    {
        // O "while (true)" faz com que isto repita para sempre enquanto o slide estiver ativo
        while (true)
        {
            // Mostra o frame com energia
            imagemUI.sprite = frameCheio;
            // Espera X segundos
            yield return new WaitForSeconds(tempoDeCadaFrame);

            // Mostra o frame sem bateria
            imagemUI.sprite = frameVazio;
            // Espera X segundos
            yield return new WaitForSeconds(tempoDeCadaFrame);
        }
    }
}