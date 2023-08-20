using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionController : Singleton<ProgressionController>
{
    ProgresssionSaver ProgresssionSaver = new ProgresssionSaver();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void SaveGameData(CardData[] cards)
    {
        ProgresssionSaver.SaveCards(cards);
    }

    public CardData[] LoadGamedata()
    {
      return  ProgresssionSaver.LoadCards();
    }
}
