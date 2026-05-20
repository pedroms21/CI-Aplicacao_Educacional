using UnityEngine;
using UnityEngine.EventSystems;

public class EfeitoHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 escalaOriginal;
    private Vector3 escalaDestino;

    [Header("Configurações")]
    public float fatorEscala = 1.15f;
    public float velocidadeAnimacao = 10f;

    void Start()
    {
        // Guarda o tamanho original
        escalaOriginal = transform.localScale;
        // No início, o destino é igual ao original
        escalaDestino = escalaOriginal;
    }

    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, escalaDestino, Time.deltaTime * velocidadeAnimacao);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        escalaDestino = escalaOriginal * fatorEscala;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        escalaDestino = escalaOriginal;
    }
}