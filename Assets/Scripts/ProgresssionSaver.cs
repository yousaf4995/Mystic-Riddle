using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ProgresssionSaver
{
    string basePath = "cards.json";

    // Calling method to save card data
    public void SaveCards(CardData[] gameplayCards, Action onComplete = null)
    {
        SaveCardDataToFile(gameplayCards, onComplete);
    }
    private void SaveCardDataToFile(CardData[] cardDataArray, Action onComplete = null)
    {
        CardDataWrapper wrapper = new CardDataWrapper { cardDataArray = cardDataArray };
        string json = JsonUtility.ToJson(wrapper);
        File.WriteAllText(Application.persistentDataPath + basePath, json);

        Debug.Log(Application.persistentDataPath + basePath);
        onComplete?.Invoke();
    }

    // load

    // Calling method to load card data
    public CardData[] LoadCards()
    {
        return LoadCardDataFromFile();
    }
    private CardData[] LoadCardDataFromFile()
    {
        if (File.Exists(Application.persistentDataPath + basePath))
        {
            string json = File.ReadAllText(Application.persistentDataPath + basePath);
            CardDataWrapper wrapper = JsonUtility.FromJson<CardDataWrapper>(json);
            return wrapper.cardDataArray;
        }
        return null;
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
    public CardData[] cardDataArray;
}
