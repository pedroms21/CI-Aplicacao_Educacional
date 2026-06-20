using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Alimento : MonoBehaviour, IPointerClickHandler
{
    public bool ehProteina;
    private bool ativo = false;
    private Image imagemUI;

    void Awake()
    {
        imagemUI = GetComponent<Image>();
        imagemUI.enabled = false;
    }

    // Agora a função pede também o tempo que deve ficar visível (tempoExposicao)
    public void AtivarAlimento(Sprite novoSprite, bool proteina, float tempoExposicao)
    {
        if (ativo) return;

        ehProteina = proteina;
        imagemUI.sprite = novoSprite;

        StartCoroutine(CicloDeVida(tempoExposicao));
    }

    System.Collections.IEnumerator CicloDeVida(float tempo)
    {
        ativo = true;
        imagemUI.enabled = true;

        // Fica visível pelo tempo ditado pelo nível de dificuldade
        yield return new WaitForSeconds(tempo);

        Esconder();
    }

    void Esconder()
    {
        ativo = false;
        imagemUI.enabled = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!ativo) return;

        if (ehProteina)
        {
            ControladorJogo.instancia.AdicionarPontos(10);
        }
        else
        {
            ControladorJogo.instancia.AdicionarPontos(-5);
        }

        Esconder();
    }
}