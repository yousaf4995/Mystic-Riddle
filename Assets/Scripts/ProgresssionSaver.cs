using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ProgresssionSaver
{
    string basePath = Application.persistentDataPath + "cards.json";

    // Calling method to save card data
    public void SaveCards(CardData[] gameplayCards, Action onComplete = null)
    {
        SaveCardDataToFile(gameplayCards, onComplete);
    }
    private void SaveCardDataToFile(CardData[] cardDataArray, Action onComplete = null)
    {
        string json = JsonUtility.ToJson(cardDataArray);
        File.WriteAllText(basePath, json);
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
        if (File.Exists(basePath))
        {
            string json = File.ReadAllText(basePath);
            return JsonUtility.FromJson<CardData[]>(json);
        }
        return null;
    }



}
