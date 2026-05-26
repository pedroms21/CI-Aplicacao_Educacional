using UnityEngine;

public class VooComCurvas : MonoBehaviour
{
    [Header("Configurações de Voo (Reta)")]
    public float velocidadeMinima = 30f;
    public float velocidadeMaxima = 60f;

    [Header("Limites do Ecrã (Eixo X LOCAL)")]

    public float limiteDesaparecer = 15f;
    public float posicaoRenascer = -15f;

    [Header("Efeito Ondulado (Curvas)")]
    public float amplitudeOndulacao = 20f;
    public float frequenciaOndulacao = 1.5f;

    [Header("Inclinação (Rotação Realista)")]
    public float maxInclinacaoGraus = 20f; // Ângulo máximo que o pássaro inclina

    private float velocidadeAtual;
    private float alturaBase;
    private float progressoHorizontal;

    void Start()
    {
        // Usamos localPosition porque os pássaros podem ser filhos de um Canvas
        alturaBase = transform.localPosition.y;

        // Define uma velocidade aleatória no início
        DefinirNovaVelocidade();
    }

    void Update()
    {
        float deltaTime = Time.deltaTime;

        // 1. Movimento Horizontal Constante
        progressoHorizontal += velocidadeAtual * deltaTime;
        float novoX = transform.localPosition.x + (velocidadeAtual * deltaTime);

        // 2. Movimento Vertical
        // Usamos Time.time para que a onda continue suave mesmo após o respawn
        float offsetOndulacao = Mathf.Sin(Time.time * frequenciaOndulacao) * amplitudeOndulacao;
        float novoY = alturaBase + offsetOndulacao;

        // 3. Atualizar a Posição Local
        transform.localPosition = new Vector3(novoX, novoY, transform.localPosition.z);

        // 4. Rotação (Inclinação baseada na subida ou descida)
        // Usamos Mathf.Cos porque ele a curva do Seno
        float inclinaçãoZ = Mathf.Cos(Time.time * frequenciaOndulacao) * maxInclinacaoGraus;

        // Aplica a rotação apenas no eixo Z
        transform.localRotation = Quaternion.Euler(0, 0, inclinaçãoZ);

        // 5. Verificação de Limites para Respawn
        if (transform.localPosition.x > limiteDesaparecer)
        {
            ReposicionarPassaro();
        }
    }

    void ReposicionarPassaro()
    {
        transform.localPosition = new Vector3(posicaoRenascer, transform.localPosition.y, transform.localPosition.z);

        DefinirNovaVelocidade();
    }

    void DefinirNovaVelocidade()
    {
        velocidadeAtual = Random.Range(velocidadeMinima, velocidadeMaxima);
    }
}