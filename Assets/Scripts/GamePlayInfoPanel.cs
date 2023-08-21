using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GamePlayInfoPanel : MonoBehaviour
{
    public InfoTexts gamePlayInfoTxts;
    //
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

    void Start()
    {

    }
    public void Initialized()
    {
        SetCorrectCardsMacth(0);
        SetInCorrectCardsMacth(0);
    }

    public void SetCorrectCardsMacth(int score)
    {
       gamePlayInfoTxts.correctCardsInfoTxt.text = score + " / " + GameController.ProgressionController.MaxCardToPlay;
    }

    public void SetInCorrectCardsMacth(int score)
    {
        gamePlayInfoTxts.inCorrectCardsInfoTxt.text = score + " / " + GameController.ProgressionController.MaxCardToPlay;
    }
}
[System.Serializable]
public struct InfoTexts
{
    public TMP_Text correctCardsInfoTxt;
    public TMP_Text inCorrectCardsInfoTxt;
}