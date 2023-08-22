using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionController : Singleton<ProgressionController>
{
    //[SerializeField] private int correctCardsScore = 0;
    //[SerializeField] private int incorrectCardsScore = 0;
    //[SerializeField] private int maxCardToPlay = 0;
    //[SerializeField] private int attemptsCounter = 0; 
    //[Space]
    //[SerializeField] private int rows = 0;
    //[SerializeField] private int colums = 0;

    [Space]
    [SerializeField]
   private CardDataWrapper CardDataWrapper;

    //public int CorrectCardsScore { get => correctCardsScore; set => correctCardsScore = value; }
    //public int inCorrectCardsScore { get => incorrectCardsScore; set => incorrectCardsScore = value; }
    //public int AttemptsCounter { get => attemptsCounter; set => attemptsCounter = value; }
    //public int MaxCardToPlay { get => maxCardToPlay; set => maxCardToPlay = value; }
    //public int Rows { get => rows; set => rows = value; }
    //public int Colums { get => colums; set => colums = value; }

    public CardDataWrapper CardData { get => CardDataWrapper; set => CardDataWrapper = value; }


    ProgresssionSaver ProgresssionSaver = new ProgresssionSaver();
    // Start is called before the first frame update
    void Start()
    {

    }



    // Update is called once per frame
    public void SaveGameData(CardDataWrapper cards, Action onComplete = null)
    {
        ProgresssionSaver.SaveCards(cards,onComplete);
    }

    public CardDataWrapper LoadGamedata()
    {
        var loadedData= ProgresssionSaver.LoadCards();
        CardData = loadedData;
        return CardData;
    }

    [ContextMenu("DeleteSavedDta")]
    public void DeleteSavedDta()
    {
        ProgresssionSaver.DeleteSavedDta();
    }
}
