using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerDisplayHelper : MonoBehaviour
{
    public TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        GameController.Instance.GamePlayTimer.UnSubscribeTimer(SubscribeTimer);
        GameController.Instance.GamePlayTimer.SubscribeTimer(SubscribeTimer);
    }

    //private void OnDisable()
    //{
    //    GamePlayTimer.Instance.UnSubscribeTimer(SubscribeTimer);
    //}

    public void SubscribeTimer(int hour, int mint, int sec)
    {
        // with hours
        //string remainingTimeText =
        //    string.Format("{0:00}:{1:00}:{2:00}", hour, mint, sec);


        string remainingTimeText =
            string.Format("{0:00}:{1:00}", mint, sec);

        text.text = remainingTimeText;
    }
}