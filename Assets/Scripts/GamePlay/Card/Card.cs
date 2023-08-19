using System;
using UnityEngine;
using UnityEngine.UI;
using CardModel;
namespace GamePlay
{
    public class Card : AbstractCard
    {
        public UIButton cardBtn;
        public Image cardFaceImage;
        [Space]
        [Header("Card Data")]
        public CardData CardData;
        public override void Init(CardData CardData)
        {
            cardBtn.onClick.RemoveListener(OnCardClicked);
            cardBtn.onClick.AddListener(OnCardClicked);
        }

        public override void Flip()
        {

        }

        public override void OnCardClicked()
        {
            Debug.Log("Card Clicked with Card Type : " + CardData.CardType);
        }

        public override void CalculateFlip()
        {

        }



        public override void FlipNormalFace()
        {

        }
        public override void FlipSpecificFace()
        {

        }
    }
}

[Serializable]
public struct CardData
{
    public int CardType;
    public Sprite normalFaceSprite;
    public Sprite specificFaceSprite;
}