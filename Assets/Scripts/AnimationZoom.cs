using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class SchoolButtonAction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 originalScale;
    private bool isHovering = false;

    [Header("Efeitos de Hover e Animação")]
    public float hoverScaleMultiplier = 1.10f; // Tamanho (1.10) quando o rato está por cima
    public float pulseMaxMultiplier = 1.05f;   // Tamanho máximo do pulsar automático (1.05)
    public float pulseSpeed = 1.5f;            // Velocidade da animação (ajusta ao teu gosto)

    void Start()
    {
        // Guarda o tamanho original (base) para fazer os cálculos
        originalScale = transform.localScale;
    }

    void Update()
    {
        // Se o rato NÃO estiver por cima, faz a animação de pulsar
        if (!isHovering)
        {
            // O Mathf.Sin cria uma onda suave. Convertemos o valor para ir de 0 a 1.
            float ondaSuave = Mathf.Sin(Time.time * pulseSpeed) * 0.5f + 0.5f;
            
            // Calculamos o quanto queremos que cresça (ex: 1.05 - 1 = 0.05)
            float amplitude = pulseMaxMultiplier - 1f;
            
            // Aplicamos a onda à escala original
            transform.localScale = originalScale * (1f + (ondaSuave * amplitude));
        }
    }

    // Quando o rato entra no objeto
    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
        // Pára de pulsar e salta logo para o tamanho de Hover (1.10)
        transform.localScale = originalScale * hoverScaleMultiplier;
    }

    // Quando o rato sai do objeto
    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
        // A variável fica false e o Update() volta a fazer o objeto pulsar automaticamente
    }

}