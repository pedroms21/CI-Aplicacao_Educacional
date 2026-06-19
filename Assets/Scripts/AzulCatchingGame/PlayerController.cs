using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections; // Obrigatório para usarmos o "Temporizador" do piscar

public class PlayerController : MonoBehaviour
{
    [Header("Configurações do Jogador")]
    public float screenLimit = 8.5f; 

    [Header("Efeitos Visuais")]
    public ParticleSystem particulasErro; // Onde vamos colocar o nosso efeito
    private SpriteRenderer spriteRenderer;
    private Color corOriginal = Color.white;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        
        // Vai buscar o componente de imagem da Arca e guarda a cor normal dela
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null) 
        {
            corOriginal = spriteRenderer.color;
        }
    }

    void Update()
    {
        if (Pointer.current != null)
        {
            Vector2 pointerScreenPosition = Pointer.current.position.ReadValue();
            Vector3 pointerWorldPosition = mainCamera.ScreenToWorldPoint(pointerScreenPosition);
            float targetX = Mathf.Clamp(pointerWorldPosition.x, -screenLimit, screenLimit);
            transform.position = new Vector3(targetX, transform.position.y, transform.position.z);
        }
    }

    // --- FUNÇÕES DE EFEITOS VISUAIS ---

    public void EfeitoSaudavel()
    {
        StartCoroutine(PiscarCor(Color.green));
    }

    public void EfeitoErro()
    {
        StartCoroutine(PiscarCor(Color.red));
        
        // Dispara a explosão de partículas!
        if (particulasErro != null)
        {
            particulasErro.Play();
        }
    }

    // Temporizador que muda a cor e volta ao normal
    private IEnumerator PiscarCor(Color corDoPiscar)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = corDoPiscar; // Pinta a arca
            yield return new WaitForSeconds(0.15f); // Espera um milésimo de segundo
            spriteRenderer.color = corOriginal; // Volta ao normal
        }
    }
}