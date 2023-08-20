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
        public Action OncallBack;
        public Action<bool> OnCardFliped;  
        public Action<bool> OnCardFlipedStarted;    
        public Action<bool> OnCardFlipedMiddle;

        public abstract void Init(CardData cardData, Action<Card> cardClickEvent, Action callBack);

        public abstract void CardClicked(Card card);
        public abstract void Flip();
        public abstract void FlipNormalFace();
        public abstract void FlipSpecificFace();

        public abstract void CardMatched();

        public abstract void CardMissMatched();
    }
}
