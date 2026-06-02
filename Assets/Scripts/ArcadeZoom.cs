using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // Biblioteca necessária para carregar as cenas

public class ArcadeZoomTransition : MonoBehaviour
{
    [Header("Configurações da Máquina")]
    public RectTransform maquinaArcade; // A imagem da máquina de arcada
    public string nomeDaCenaParaCarregar = "NomeDaCena"; // O nome exato da cena do minijogo

    [Header("Animação de Zoom")]
    public float duracaoZoom = 1.5f; // Quanto tempo demora a animação
    public Vector3 escalaFinal = new Vector3(8f, 8f, 1f); // O tamanho final da máquina
    public Vector2 posicaoFinal = Vector2.zero; // (0,0) centra o ecrã no meio do Canvas

    // Esta é a função que o botão da arcada vai chamar quando for clicado
    public void IniciarTransicao()
    {
        StartCoroutine(FazerZoomECarregarCena());
    }

    private IEnumerator FazerZoomECarregarCena()
    {
        float tempoDecorrido = 0f;
        
        // Guarda a posição e o tamanho onde a máquina está antes de a animação começar
        Vector3 escalaInicial = maquinaArcade.localScale;
        Vector2 posicaoInicial = maquinaArcade.anchoredPosition;

        while (tempoDecorrido < duracaoZoom)
        {
            tempoDecorrido += Time.deltaTime;
            
            // t vai de 0 a 1 ao longo da duração
            float t = tempoDecorrido / duracaoZoom;

            // Fórmula Ease-in-Out para a câmara acelerar suavemente e travar suavemente no final
            float smoothT = t * t * (3f - 2f * t);

            // Aumenta a máquina e move-a para o centro simultaneamente
            maquinaArcade.localScale = Vector3.Lerp(escalaInicial, escalaFinal, smoothT);
            maquinaArcade.anchoredPosition = Vector2.Lerp(posicaoInicial, posicaoFinal, smoothT);

            yield return null; // Espera até ao próximo frame e repete
        }

        // Garante que a máquina fica perfeitamente nos valores finais
        maquinaArcade.localScale = escalaFinal;
        maquinaArcade.anchoredPosition = posicaoFinal;

        // Dá uma pequena pausa de impacto (meio segundo) com o ecrã gigante antes de mudar de cena
        yield return new WaitForSeconds(0.5f);

        // Finalmente, carrega o nível pretendido
        SceneManager.LoadScene(nomeDaCenaParaCarregar);
    }
}