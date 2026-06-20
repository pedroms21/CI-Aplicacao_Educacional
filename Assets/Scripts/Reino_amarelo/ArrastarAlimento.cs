using UnityEngine;
using UnityEngine.EventSystems;

public class ArrastarAlimento : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("Configuração")]
    public string idDoAlimento; // Ex: "Pao", "Massa", "Bolo"
    public bool isIntruso; // Marca com 'certo' se for um doce/intruso

    private Vector2 posicaoInicial;
    private Transform paiInicial;
    private CanvasGroup canvasGroup;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        posicaoInicial = transform.position;
        paiInicial = transform.parent;
        transform.SetAsLastSibling();
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
       transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        // Se largar fora de um slot, volta à posição inicial na mesa
        if (transform.parent == paiInicial)
        {
            transform.position = posicaoInicial;
        }
    }
}