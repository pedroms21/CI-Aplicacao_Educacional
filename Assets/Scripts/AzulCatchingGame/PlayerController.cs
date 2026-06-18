using UnityEngine;
using UnityEngine.InputSystem; // Obrigatório para o novo sistema!

public class PlayerController : MonoBehaviour
{
    [Header("Configurações do Jogador")]
    public float speed = 10f;
    public float screenLimit = 8.5f; 

    // Variável para guardar a nossa ação de movimento
    private InputAction moveAction;

    void Awake()
    {
        // 1. Criar a ação de movimento por código (sem precisar do Inspector)
        moveAction = new InputAction("Move");

        // 2. Adicionar as teclas (Setas Esquerda/Direita e teclas A/D)
        moveAction.AddCompositeBinding("1DAxis")
            .With("Negative", "<Keyboard>/leftArrow")
            .With("Positive", "<Keyboard>/rightArrow")
            .With("Negative", "<Keyboard>/a")
            .With("Positive", "<Keyboard>/d");
    }

    // O novo sistema obriga a ligar e desligar os controlos
    void OnEnable()
    {
        moveAction.Enable();
    }

    void OnDisable()
    {
        moveAction.Disable();
    }

    void Update()
    {
        // 3. Ler o valor (-1 para esquerda, 1 para direita, 0 para parado)
        float horizontalInput = moveAction.ReadValue<float>();

        // 4. Mover o balde
        Vector3 moveDirection = new Vector3(horizontalInput, 0, 0);
        transform.Translate(moveDirection * speed * Time.deltaTime);

        // 5. Impedir que o balde saia pelas laterais do ecrã
        if (transform.position.x > screenLimit)
        {
            transform.position = new Vector3(screenLimit, transform.position.y, 0);
        }
        else if (transform.position.x < -screenLimit)
        {
            transform.position = new Vector3(-screenLimit, transform.position.y, 0);
        }
    }
}