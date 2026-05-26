using UnityEngine;
using UnityEngine.EventSystems;

public class EfeitoHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 escalaOriginal;
    private Vector3 escalaDestino;

    [Header("Configurações")]
    public float fatorEscala = 1.15f;
    public float velocidadeAnimacao = 10f;

    [Header("Componentes")]
    public Animator animator;

    void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

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
        // Para a animação atual desativando o Animator
        if (animator != null)
        {
            animator.enabled = false;
        }

        // Define a nova escala alvo
        escalaDestino = escalaOriginal * fatorEscala;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Retoma a animação original
        if (animator != null)
        {
            animator.enabled = true;
        }

        // Volta ao tamanho normal
        escalaDestino = escalaOriginal;
    }
}