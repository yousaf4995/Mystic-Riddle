using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionController : Singleton<ProgressionController>
{
    private int correctCardsScore = 0;
    private int incorrectCardsScore = 0;
    private int maxCardToPlay = 0;


    public int CorrectCardsScore { get => correctCardsScore; set => correctCardsScore = value; }
    public int IncorrectCardsScore { get => incorrectCardsScore; set => incorrectCardsScore = value; }
    public int MaxCardToPlay { get => maxCardToPlay; set => maxCardToPlay = value; }


    ProgresssionSaver ProgresssionSaver = new ProgresssionSaver();
    // Start is called before the first frame update
    void Start()
    {
        
    }



    // Update is called once per frame
    public void SaveGameData(CardData[] cards, Action onComplete = null)
    {
        ProgresssionSaver.SaveCards(cards);
    }

    public CardData[] LoadGamedata()
    {
      return  ProgresssionSaver.LoadCards();
    }
}
