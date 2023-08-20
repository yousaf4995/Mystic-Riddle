using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButton : Button
{
    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        SoundManager.Instance.PlayButtonClickSound();
        // Debug.Log("OnPointer Down");
    }
   
}