using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GamePlayInfoPanel : MonoBehaviour
{
    public InfoTexts gamePlayInfoTxts;
    //
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

    void Start()
    {

    }
    public void Initialized()
    {
        SetCorrectCardsMacth(0);
        SetCardAttempts(0);
    }

    public void SetCorrectCardsMacth(int score)
    {
       gamePlayInfoTxts.correctCardsInfoTxt.text = score + " / " + GameController.ProgressionController.CardData.maxCardToPlay;
    }

    public void SetCardAttempts(int score)
    {
        gamePlayInfoTxts.attemptsCardsInfoTxt.text = score + "";
       // gamePlayInfoTxts.attemptsCardsInfoTxt.text = score + " / " + GameController.ProgressionController.CardData.maxCardToPlay;
    }

    public void SetInCorrectCardsMacth(int score)
    {
        //  gamePlayInfoTxts.attemptsCardsInfoTxt.text = score + " / " + GameController.ProgressionController.MaxCardToPlay;
    }
}
[System.Serializable]
public struct InfoTexts
{
    public TMP_Text correctCardsInfoTxt;
    public TMP_Text attemptsCardsInfoTxt;
}