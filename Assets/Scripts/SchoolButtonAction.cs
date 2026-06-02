using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class SchoolButtonAction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 originalScale;

    [Header("Efeitos de Hover")]
    public float scaleMultiplier = 1.05f; // Aumenta 5% o tamanho ao passar o rato

    void Start()
    {
        // Guarda o tamanho original para restaurar quando o rato sair
        originalScale = transform.localScale;
    }

    // Quando o rato entra no objeto (Hover)
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = originalScale * scaleMultiplier;
    }

    // Quando o rato sai do objeto
    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = originalScale;
    }

    // Função para o evento OnClick do Button
    public void IrParaAula()
    {
        SceneManager.LoadScene("Reino_amarelo_Aula");
    }
}