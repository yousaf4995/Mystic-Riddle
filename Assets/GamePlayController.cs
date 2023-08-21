using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCard;
using System;

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

    // private area
    CardGridLayout gridLayout;
    GameController GameController
    {
        get
        {
            var gc = GameController.Instance;

            if (!gc || gc == null)
                gc = FindAnyObjectByType<GameController>();

            return gc;

        }
    }
    UiController UiController
    {
        get
        {
            var uc = UiController.Instance;

            if (!uc || uc == null)
                uc = FindAnyObjectByType<UiController>();

            return uc;

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        gridLayout = cardContainer.GetComponent<CardGridLayout>();
        //InitializeGame();
    }

    public void InitializeGame()
    {
        cardsInGamePlay.Clear();
        UiController.initialize();
        gridLayout.gridRows = rows;
        gridLayout.gridColumns = colums;

        int length = (rows * colums);
        GameController.ProgressionController.MaxCardToPlay = length / 2;

        CardData[] cardDatasLoaded = ProgressionController.Instance.LoadGamedata();

        if (cardDatasLoaded != null && cardDatasLoaded.Length > 0)
        {
            // Debug.Log("populating from saved data"); 

            SpawnShuffledCards(cardDatasLoaded);
            ProgressionController.Instance.DeleteSavedDta();
            //// we dont need shuffel because we saved as it is
            //for (int i = 0; i < length; i++)
            //{
            //    int currentCardType = i % (length / 2); ;
            //    GameObject cardGO = Instantiate(cardPrefab, cardContainer, false);
            //    Card currentCard = cardGO.GetComponent<Card>();
            //    CardData cData = cardDatasLoaded[i];

            //    // cData.normalFaceSprite = cardSprites.normalSprite;
            //    // cData.specificFaceSprite = cardSprites.cardSprites[currentCardType];

            //    currentCard.CardData = cData;
            //    currentCard.Init(cData, CardClicked, OnCardActionsComplete);

            //    cardsInGamePlay.Add(currentCard.CardData);
            //    ProgressionController.Instance.DeleteSavedDta();
            //}
        }
        else
        {
            Debug.Log("populating from fresh data");
            CardData[] cardsData = GenerateFreshCardData(length);
            ShuffleArray(cardsData);
            SpawnShuffledCards(cardsData);

            //for (int i = 0; i < length; i++)
            //{
            //    int currentCardType = i % (length / 2); ;
            //    GameObject cardGO = Instantiate(cardPrefab, cardContainer, false);
            //    Card currentCard = cardGO.GetComponent<Card>();
            //    CardData cData = new CardData();
            //    cData.CardType = currentCardType;
            //    cData.normalFaceSprite = cardSprites.normalSprite;
            //    cData.specificFaceSprite = cardSprites.cardSprites[currentCardType];
            //    currentCard.CardData = cData;
            //    currentCard.Init(cData, CardClicked, OnCardActionsComplete);

            //    cardsInGamePlay.Add(currentCard.CardData);
            //}
        }


        gridLayout.CalculateLayoutInputVertical();

        ProgressionController pc = GameController.ProgressionController;

        UiController.GamePlayInfoPanel.SetCorrectCardsMacth(pc.CorrectCardsScore);
        UiController.GamePlayInfoPanel.SetInCorrectCardsMacth(pc.inCorrectCardsScore);

        SoundManager.Instance.PlayGameStartSound();
    }
    void SpawnShuffledCards(CardData[] shuffledCardData)
    {
        for (int i = 0; i < shuffledCardData.Length; i++)
        {
            GameObject cardGO = Instantiate(cardPrefab, cardContainer, false);
            Card currentCard = cardGO.GetComponent<Card>();
            CardData cData = shuffledCardData[i];
            currentCard.CardData = cData;
            currentCard.Init(cData, CardClicked, OnCardActionsComplete);

            cardGO.transform.localScale = (cData?.cardState == CardState.Correct) ? Vector3.zero : Vector3.one;

            cardsInGamePlay.Add(currentCard.CardData);
        }
    }
    CardData[] GenerateFreshCardData(int length)
    {
        CardData[] freshCardData = new CardData[length];

        for (int i = 0; i < length; i++)
        {
            //according to sprite size
          //  int currentCardType = i % cardSprites.cardSprites.Length;

            // OR 
            //TODO can be choosed to populate only images as per card size like 5x2, 5 rows and 2 columns
            int currentCardType = i % rows;


            CardData cData = new CardData();
            cData.CardType = currentCardType;
            cData.normalFaceSprite = cardSprites.normalSprite;
            cData.specificFaceSprite = cardSprites.cardSprites[currentCardType];
            freshCardData[i] = cData;
        }

        return freshCardData;
    }
    private void ShuffleArray<T>(T[] array)
    {
        int n = array.Length;
        for (int i = n - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            T temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }


    public void CardClicked(Card cardData)

    {
        if (firstCard == null)
            firstCard = cardData;
        else if (secondCard == null)
            secondCard = cardData;

    }
    private void OnCardActionsComplete()
    {
        print("OnCardActionsComplete");
        if (firstCard != null && secondCard != null)
        {
            if (firstCard.CardData.CardType == secondCard.CardData.CardType)
            {
                // Debug.Log("Correct Match");
                string[] successText = { "Nice", "Weldone", "Aawasome" };
                GameController.Toast.ShowToast(successText[UnityEngine.Random.Range(0, successText.Length)]);
                SoundManager.Instance.PlayCorrectSound();

                firstCard.CardMatched();
                firstCard = null;
                secondCard.CardMatched();
                secondCard = null;

                AddCorrectScore();
            }
            else
            {
                //  Debug.Log("In Correct Match");

                GameController.Toast.ShowToast();
                SoundManager.Instance.PlayInCorrectSound();
                firstCard.CardMissMatched();
                firstCard = null;

                secondCard.CardMissMatched();
                secondCard = null;
                AddInCorrectScore();
            }

        }
    }

    void AddCorrectScore()
    {
        ProgressionController pc = GameController.ProgressionController;
        UiController.GamePlayInfoPanel.SetCorrectCardsMacth(++pc.CorrectCardsScore);
        if (pc.CorrectCardsScore >= pc.MaxCardToPlay)
        {
            DisplayComplete();
        }
    }
    void AddInCorrectScore()
    {
        ProgressionController pc = GameController.ProgressionController;
        UiController.GamePlayInfoPanel.SetInCorrectCardsMacth(++pc.inCorrectCardsScore);
        if (pc.inCorrectCardsScore >= pc.MaxCardToPlay)
        {
            DisplayComplete();
        }
    }

    void DisplayComplete()
    {
        UiController.CompleteScreen.DisplayCompleteScreen();// loose screen
        SoundManager.Instance.PlayWinSound();
    }
}

[System.Serializable]
public struct SpriteData
{
    public Sprite normalSprite;
    public Sprite[] cardSprites;
}