using UnityEngine;
using UnityEngine.UI;

public class CardController : MonoBehaviour
{
    [Header("Elementos Visuais")]
    public Image cardImage; // A imagem principal do prefab (arrasta o componente Image para aqui)

    private Sprite faceHidden;
    private Sprite faceRevealed;
    private int cardID;
    private MemoryGameManager gameManager;
    private bool isRevealed = false;
    private bool isMatched = false;

    // Configura a carta quando ela é criada pelo Manager
    public void Setup(Sprite hidden, Sprite revealed, int id, MemoryGameManager manager)
    {
        faceHidden = hidden;
        faceRevealed = revealed;
        cardID = id;
        gameManager = manager;
        cardImage.sprite = faceHidden;
    }

    // Função que será chamada quando a criança clicar no botão
    public void OnCardClicked()
    {
        // Se já estiver virada, se já fez par, ou se o jogo estiver bloqueado, não faz nada
        if (isRevealed || isMatched || !gameManager.CanClick()) return;

        Reveal();
        gameManager.CardRevealed(this); // Avisa o manager que esta carta foi virada
    }

    public void Reveal()
    {
        isRevealed = true;
        cardImage.sprite = faceRevealed;
    }

    public void Hide()
    {
        isRevealed = false;
        cardImage.sprite = faceHidden;
    }

    public void SetMatched()
    {
        isMatched = true;
    }

    public int GetCardID()
    {
        return cardID;
    }
}