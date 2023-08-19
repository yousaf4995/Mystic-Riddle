using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayController : MonoBehaviour
{
    [Range(2,10)]
    public int rows = 2;
    [Range(2, 10)]
    public int colums = 2;
    public GameObject cardPrefab;
    public Transform cardContainer;
    public List<CardData> cardsInGamePlay;

    [Header("Logic")]
    public CardData firstCard;
    public CardData secondCard;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void CardClicked(CardData cardData)
    {
        if (firstCard == null)
            firstCard = cardData;

        if (secondCard == null)
            secondCard = cardData;

        if (firstCard != null && secondCard != null)
        {
            if (firstCard.CardType == secondCard.CardType)
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
