using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ProgresssionSaver
{
    string basePath = "cards.json";

    // Calling method to save card data
    public void SaveCards(CardDataWrapper gameplayCards, Action onComplete = null)
    {
        SaveCardDataToFile(gameplayCards, onComplete);
    }
    private void SaveCardDataToFile(CardDataWrapper cardDataArray, Action onComplete = null)
    {
       
        string json = JsonUtility.ToJson(cardDataArray);
      //  Debug.Log(json);
        File.WriteAllText(Application.persistentDataPath + basePath, json);

       // Debug.Log(Application.persistentDataPath + basePath);
        onComplete?.Invoke();
    }

    // load

    // Calling method to load card data
    public CardDataWrapper LoadCards()
    {
        return LoadCardDataFromFile();
    }
    private CardDataWrapper LoadCardDataFromFile()
    {
        CardDataWrapper wrapper = new CardDataWrapper();
        if (File.Exists(Application.persistentDataPath + basePath))
        {
            string json = File.ReadAllText(Application.persistentDataPath + basePath);
             wrapper = JsonUtility.FromJson<CardDataWrapper>(json);

            return wrapper;
        }
        return wrapper;
    }

    public void DeleteSavedDta()
    {
        if (File.Exists(Application.persistentDataPath + basePath))
        {
            File.Delete(Application.persistentDataPath + basePath);
        }
        else
        {
            Debug.Log("No file to delete");
        }
    }

}


[Serializable]
public class CardDataWrapper
{
    public int correctCardsPlayed;
    public int inCorrectCardsPlayed;
    public int attemptsCounter;
    public int maxCardToPlay;
    [Space]
    public int rows;
    public int colums;
    public CardData[] cardDataArray;
}
