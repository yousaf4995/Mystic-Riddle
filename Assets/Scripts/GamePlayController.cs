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
    [Tooltip("Will assign ids and sprites to spawned cards according to its type")]
    public CardSpawnType cardSpawnType = CardSpawnType.RowsWise;// will assign ids and sprites to spawned cards according to its type
    [Space]
    public GameObject cardPrefab;
    public Transform cardContainer;

    [Space]
    [Header("Sprites")]
    public SpriteData cardSprites;
    public List<CardData> cardsInGamePlay;

    [Space]
    [Header("Logic")]
    public Card firstCard;
    public Card secondCard;


    // private area
    //  bool isAutoSaveOn => PlayerPrefsManager.IsAutoSavedEnable();
    bool isAutoSaveOn = false;
    CardGridLayout gridLayout;
    GameManager GameController
    {
        get
        {
            var gc = GameManager.Instance;

            if (!gc || gc == null)
                gc = FindAnyObjectByType<GameManager>();

            return gc;

        }
    }

    ProgressionController ProgressionController
    {
        get
        {
            var gc = GameManager.Instance.ProgressionController;

            if (!gc || gc == null)
                gc = FindAnyObjectByType<ProgressionController>();

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

        GameController.GamePlayTimer.UnSubscribeTimerComplete(OnTimerEnd);
        GameController.GamePlayTimer.SubscribeTimerComplete(OnTimerEnd);

        var cardDatasLoaded = ProgressionController.LoadGamedata();
        if (cardDatasLoaded.cardDataArray != null && cardDatasLoaded.cardDataArray.Length > 0)
        {
            InitializeGame();
        }
        else
        {
            UiController.Instance.StartScreen.Initialized();
        }

    }


    public void InitializeGame()
    {
        cardsInGamePlay.Clear();
        UiController.initialize();

        var cardDataLoaded = ProgressionController.LoadGamedata();

        if (cardDataLoaded.cardDataArray != null && cardDataLoaded.cardDataArray.Length > 0)
        {
            SavedGameLoad(cardDataLoaded);
          
            // accidental missleading content management
          //  print("InitializeGame :" + AllCardsArePlayed());
            //if (AllCardsArePlayed())
            //{
            //    FreshGameLoad();
            //}
            ProgressionController.DeleteSavedDta();

        }
        else
        {
            Debug.Log("populating from fresh data");
            FreshGameLoad();
        }


        gridLayout.CalculateLayoutInputVertical();
        isAutoSaveOn = PlayerPrefsManager.IsAutoSavedEnable();

        int length = (rows * colums);
        ProgressionController.CardData.maxCardToPlay = length / 2;

        UiController.GamePlayInfoPanel.SetCorrectCardsMacth(ProgressionController.CardData.correctCardsPlayed);
        UiController.GamePlayInfoPanel.SetCardAttempts(ProgressionController.CardData.attemptsCounter);

        SoundManager.Instance.PlayGameStartSound();

        GameController.GamePlayTimer.RestartTimer();
    }

    void FreshGameLoad()
    {

        gridLayout.gridRows = rows;
        gridLayout.gridColumns = colums;


        int totallCards = (rows * colums);

        ProgressionController.CardData.rows = rows;
        ProgressionController.CardData.colums = colums;

        CardData[] cardsData = GenerateFreshCardData(totallCards);

        ShuffleArray(cardsData);
        SpawnShuffledCards(cardsData);
    }
    void SavedGameLoad( CardDataWrapper cardDataLoaded)
    {
        GameController.Toast.ShowToast("Placed from Saved Data");
        //// we dont need shuffel because we saved as it with lst shuffle 

        gridLayout.gridRows = rows = cardDataLoaded.rows;
        gridLayout.gridColumns = colums = cardDataLoaded.colums;

        SpawnShuffledCards(cardDataLoaded.cardDataArray);
        ProgressionController.DeleteSavedDta();
    }

    void SpawnShuffledCards(CardData[] shuffledCardData)
    {
        for (int i = 0; i < shuffledCardData.Length; i++)
        {
            GameObject cardGO = Instantiate(cardPrefab, cardContainer, false);
            Card currentCard = cardGO.GetComponent<Card>();
            CardData cData = shuffledCardData[i];
            currentCard.cardData = cData;
            currentCard.Init(cData, CardClicked, null);

            cardGO.transform.localScale = (cData.cardState == CardState.Correct) ? Vector3.zero : Vector3.one;

            cardsInGamePlay.Add(currentCard.cardData);
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

            int currentCardType = i % rows;// budefault rows base

            //(Tackled) it may create incorrect pair type more if sprites size is higher and higher
            // sprite size should be equal or less
            if (cardSpawnType == CardSpawnType.SpriteBase)
                currentCardType = i % cardSprites.cardSprites.Length / 2; /* .Length;*/

            // it may also can create incorreect pair states more if sprites are less
            // sprite size should be equal or greater
            else if (cardSpawnType == CardSpawnType.CardsSizeBase)
                currentCardType = i % (length / 2);


            CardData cData = new CardData();
            cData.CardType = currentCardType;
            cData.normalFaceSprite = cardSprites.normalSprite;
            cData.specificFaceSprite = (currentCardType < cardSprites.cardSprites.Length) ? cardSprites.cardSprites[currentCardType] : cardSprites.cardSprites[currentCardType / 2];
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

        OnCardActionsComplete();
    }
    private void OnCardActionsComplete()
    {
        print("OnCardActionsComplete");
        if (firstCard != null && secondCard != null)
        {
            UiController.GamePlayInfoPanel.SetCardAttempts(++GameController.ProgressionController.CardData.attemptsCounter);

            if (firstCard.cardData.CardType == secondCard.cardData.CardType)
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
        ProgressionController.CardData.correctCardsPlayed++;
        UiController.GamePlayInfoPanel.SetCorrectCardsMacth(ProgressionController.CardData.correctCardsPlayed);

        AutoSave();

        if (ProgressionController.CardData.correctCardsPlayed >= ProgressionController.CardData.maxCardToPlay)
        {
            DisplayComplete();
        }

     
        //else if(AllCardsArePlayed())
        //    DisplayComplete();

        // print("AddCorrectScore :"+AllCardsArePlayed());
    }
    void AddInCorrectScore()
    {

        UiController.GamePlayInfoPanel.SetInCorrectCardsMacth(++ProgressionController.CardData.inCorrectCardsPlayed);
        if (ProgressionController.CardData.inCorrectCardsPlayed >= ProgressionController.CardData.maxCardToPlay)
        {
            // DisplayComplete();
        }
    }

    void DisplayComplete()
    {
        ProgressionController.DeleteSavedDta();
        UiController.CompleteScreen.DisplayCompleteScreen();// loose screen
        SoundManager.Instance.PlayWinSound();
    }

    void AutoSave()
    {
        if (isAutoSaveOn)
        {
            SaveGame();
        }
    }
    public void SaveGame(Action onComplete=null)
    {
        if (firstCard != null)
            firstCard.ResetCard();
        if (secondCard != null)
            secondCard.ResetCard();

        CardDataWrapper wrapper = GameManager.Instance.ProgressionController.CardData;
        wrapper.cardDataArray = GameController.GamePlayController.cardsInGamePlay.ToArray();
        if (wrapper.cardDataArray == null || wrapper.cardDataArray.Length < 1)
        {
            print("No data to save ");
            return;

        }

        GameController.ProgressionController.SaveGameData(wrapper, onComplete); 
    }
    //Timer 
    private void OnTimerEnd()
    {
        //TODO : Fail, Time Over or complete screen
        DisplayComplete();
        ProgressionController.DeleteSavedDta();
    }

    // precaution Methods// can be called on addscore method to completed
    // the game if accidentaly some score card macth, missmatched 
    bool AllCardsArePlayed()
    {
        bool areDone = true;
        foreach (var item in cardsInGamePlay)
        {
            if (item.cardState == CardState.None)
            {
                areDone = false;
                break;
            }
        }
        return areDone;
    }
}

[System.Serializable]
public struct SpriteData
{
    public Sprite normalSprite;
    public Sprite[] cardSprites;
}
