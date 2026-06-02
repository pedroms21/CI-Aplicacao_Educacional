using System.Collections;
using UnityEngine;

public class SlideUpUI : MonoBehaviour
{
    [Header("Configurações da Animação")]
    public RectTransform uiElement;
    public float slideDistance = 800f; // Quantos píxeis desce para ficar fora do ecrã
    public float slideDuration = 1f;   // Tempo total da animação

    [Header("Efeito de Mola")]
    [Range(0f, 3f)]
    public float forcaMola = 1.2f;     // Controla o ressalto. 1.2 é um ressalto suave e natural.

    private Vector2 originalPosition;

    void Start()
    {
        // Se não arrastarmos nada no Inspector, ele usa o RectTransform do próprio objeto
        if (uiElement == null)
            uiElement = GetComponent<RectTransform>();

        // 1. Guarda a posição exata onde os elementos estão no editor
        originalPosition = uiElement.anchoredPosition;

        // 2. Esconde o elemento empurrando-o para baixo no eixo Y
        uiElement.anchoredPosition = new Vector2(originalPosition.x, originalPosition.y - slideDistance);

        // 3. Começa a animação
        StartCoroutine(SlideIn());
    }

    IEnumerator SlideIn()
    {
        float elapsedTime = 0f;
        Vector2 startPosition = uiElement.anchoredPosition;

        while (elapsedTime < slideDuration)
        {
            elapsedTime += Time.deltaTime;
            
            // t representa a percentagem de tempo que já passou (de 0 a 1)
            float t = elapsedTime / slideDuration;
            
            // Fórmula matemática 'Ease Out Back' para criar o efeito de mola
            t = t - 1f;
            float easedT = t * t * ((forcaMola + 1f) * t + forcaMola) + 1f;

            // Utilizamos LerpUnclamped para permitir que o valor passe do ponto final e volte
            uiElement.anchoredPosition = Vector2.LerpUnclamped(startPosition, originalPosition, easedT);
            
            yield return null;
        }

        // Garante que no final da animação a UI fica trancada na posição perfeita
        uiElement.anchoredPosition = originalPosition;
    }
}