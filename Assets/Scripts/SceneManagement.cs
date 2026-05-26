using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; // Necessário para podermos criar "pausas" no tempo

public class MenuController : MonoBehaviour
{
    [Header("Som")]
    public AudioSource fonteDeAudio;
    public AudioClip somDeClique;

    // Esta é a nova função que os botões vão chamar
    public void ClicarBotao(string nomeDaCena)
    {
        // Toca o som (se os ficheiros estiverem lá)
        if (fonteDeAudio != null && somDeClique != null)
        {
            fonteDeAudio.PlayOneShot(somDeClique);
        }

        // Inicia a rotina de atraso
        StartCoroutine(EsperarECarregar(nomeDaCena));
    }

    // Função especial (Coroutine) que consegue "parar o tempo"
    private IEnumerator EsperarECarregar(string nome)
    {
        // Espera 0.4 segundos. Podes alterar este valor se quiseres!
        yield return new WaitForSeconds(0.4f);

        // Agora sim, muda de cena
        SceneManager.LoadScene(nome);
    }
}