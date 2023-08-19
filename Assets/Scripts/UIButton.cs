using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButton : Button, IPointerDownHandler, IPointerUpHandler
{

    public override void OnPointerDown(PointerEventData eventData)
    {
        SoundManager.Instance.PlayButtonClickSound();
        Debug.Log("OnPointer Down");
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
      
        Debug.Log("OnPointer Up");
    }
}
