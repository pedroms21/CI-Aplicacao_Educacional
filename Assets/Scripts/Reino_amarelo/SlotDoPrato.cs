using UnityEngine;
using UnityEngine.EventSystems;

public class SlotDoPrato : MonoBehaviour, IDropHandler
{
    [Header("Qual alimento encaixa aqui?")]
    public string idEsperado;
    
    private GestorDeNivel gestor;

    void Start()
    {
        gestor = FindFirstObjectByType<GestorDeNivel>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            ArrastarAlimento alimentoLargado = eventData.pointerDrag.GetComponent<ArrastarAlimento>();

            if (alimentoLargado != null)
            {
                // ERRO 1: A criança tentou colocar um intruso (ex: Doce) no prato!
                if (alimentoLargado.isIntruso)
                {
                    gestor.PerderVida();
                    return; // O intruso volta para a mesa (graças ao script de arrastar)
                }

                // ACERTO: É o alimento certo e no buraco certo
                if (alimentoLargado.idDoAlimento == idEsperado)
                {
                    alimentoLargado.transform.SetParent(transform);
                    alimentoLargado.transform.position = transform.position;
                    alimentoLargado.enabled = false; 
                    gestor.AdicionarAcerto();
                }
                // ERRO 2: É um alimento bom, mas tentou meter no buraco errado (ex: Pão no buraco da Batata)
                else 
                {
                    gestor.PerderVida();
                }
            }
        }
    }
}