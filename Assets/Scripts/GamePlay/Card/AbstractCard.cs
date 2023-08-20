using GameCard;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CardModel
{
    public abstract class AbstractCard : MonoBehaviour, Icard
    {
        [SerializeField] internal bool isFliped = false;
        [SerializeField] internal UIButton cardBtn;
        [SerializeField] internal Image cardFaceImage;
        [Space]
        [Header("Card Data")]
        public Action<Card> OnCardClicked;
        public Action<bool> onCardFliped;

        public abstract void Init(CardData cardData, Action<Card> cardClickEvent);

        public abstract void CardClicked();
        public abstract void Flip();
        public abstract void FlipNormalFace();
        public abstract void FlipSpecificFace();
        public abstract void CalculateFlip();

        public abstract void CardMatched();

        public abstract void CardMissMatched();
    }
}
