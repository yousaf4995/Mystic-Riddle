using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCard;

public class GamePlayController : MonoBehaviour
{
    [Range(2, 10)]
    public int rows = 2;
    [Range(2, 10)]
    public int colums = 2;
    public GameObject cardPrefab;
    public Transform cardContainer;

    [Space]
    [Header("Sprites")]
    public SpriteData cardSprites;

    public List<CardData> cardsInGamePlay;

    [Header("Logic")]
    public Card firstCard;
    public Card secondCard;
    // Start is called before the first frame update
    void Start()
    {

    }

    void InitializeGame()
    {
        for (int i = 0; i < (rows * colums); i++)
        {
            GameObject cardGO = Instantiate(cardPrefab, cardContainer, false);
            Card currentCard = cardGO.GetComponent<Card>();
            CardData cData = new CardData();
            cData.CardType = i;
          
            currentCard.Init(cData);
        }
    }

    public void CardClicked(Card cardData)
    {
        if (firstCard == null)
            firstCard = cardData;

        if (secondCard == null)
            secondCard = cardData;

        if (firstCard != null && secondCard != null)
        {
            if (firstCard.CardData.CardType == secondCard.CardData.CardType)
            {
                Debug.Log("Correct Match");
            }
            else
            {
                Debug.Log("In Correct Match");
            }

        }

    }
}

[System.Serializable]
public struct SpriteData
{
    public Sprite normalSprite;
    public Sprite[] cardSprites;
}
