using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class MenuController : MonoBehaviour
{
    [Header("Som")]
    public AudioSource fonteDeAudio;
    public AudioClip somDeClique;

    // O histórico global
    private static Stack<string> historicoDeCenas = new Stack<string>();

    // 1. BOTÕES DE AVANÇAR (Menu -> Ilha -> Sala)
    public void ClicarBotao(string nomeDaProximaCena)
    {
        TocarSom();
        historicoDeCenas.Push(SceneManager.GetActiveScene().name);
        StartCoroutine(EsperarECarregar(nomeDaProximaCena));
    }

    // 2. BOTÕES DE "VOLTAR ATRÁS" (Recua apenas um passo)
    public void ClicarBotaoVoltar()
    {
        TocarSom();

        if (historicoDeCenas.Count > 0)
        {
            string cenaAnterior = historicoDeCenas.Pop();
            StartCoroutine(EsperarECarregar(cenaAnterior));
        }
        else
        {
            Debug.LogWarning("Histórico vazio! A carregar o Menu Principal.");
            StartCoroutine(EsperarECarregar("MenuPrincipal")); // Altera se o teu menu tiver outro nome
        }
    }

    // 3. NOVO: BOTÃO DE "IR PARA O MENU PRINCIPAL" (Volta à estaca zero)
    public void VoltarDiretoParaMenu(string nomeDoMenu)
    {
        TocarSom();
        
        // Limpa todo o histórico guardado!
        historicoDeCenas.Clear(); 
        
        StartCoroutine(EsperarECarregar(nomeDoMenu));
    }

    // Função auxiliar para não repetirmos o código do som
    private void TocarSom()
    {
        if (fonteDeAudio != null && somDeClique != null)
        {
            fonteDeAudio.PlayOneShot(somDeClique);
        }
    }

    // A tua Coroutine original
    // A tua Coroutine corrigida
    private IEnumerator EsperarECarregar(string nome)
    {
        // 1. Espera 0.4s usando o tempo REAL (ignora o Time.timeScale = 0)
        yield return new WaitForSecondsRealtime(0.4f);
        
        // 2. MUITO IMPORTANTE: Descongela o jogo antes de carregar a próxima cena
        Time.timeScale = 1f; 
        
        // 3. Muda de cena
        SceneManager.LoadScene(nome);
    }
}