using System;
using UnityEngine;
using UnityEngine.UI;
using CardModel;
namespace GameCard
{
    public class Card : AbstractCard
    {
        [Space]
        [Header("Card Data")]
        public CardData CardData;

        public bool IsFliped => isFliped;
        [Range(1, 20)]
        public float rotationSpeed = 20f;
        private void Start()
        {
            cardBtn.onClick.RemoveListener(CardClicked);
            cardBtn.onClick.AddListener(CardClicked);
        }
        public override void Init(CardData CardData, Action<Card> cardClickEvent)
        {
            AddListner(cardClickEvent);
            FillData(CardData);
        }

        void AddListner(Action<Card> CardData)
        {
            cardBtn.onClick.RemoveListener(() => { CardData(this); }); // Remove the old listener
            cardBtn.onClick.AddListener(() => CardData?.Invoke(this)); // Add the new listener
        }
        void FillData(CardData cData)
        {
            this.CardData = cData;
            cardFaceImage.sprite = this.CardData.normalFaceSprite;
        }

        public override void Flip()
        {

        }

        public override void CardClicked()
        {
            OnCardClicked?.Invoke(this);
            FlipSpecificFace();
            Debug.Log("override Card Clicked with Card Type : " + CardData.CardType);
        }
        public void CardClicked(Card card)
        {
            OnCardClicked?.Invoke(card);
            CalculateFlip();
            Debug.Log("Card Clicked with Card Type : " + CardData.CardType);
        }
        public override void CalculateFlip()
        {
            if (isFliped)
                FlipNormalFace();
            else
                FlipSpecificFace();
        }

        public override void FlipNormalFace()
        {
            Transform transformToRotate = transform; // You can replace this with the actual transform you want to rotate

            StartCoroutine(transformToRotate.DoRotation(true, rotationSpeed, OnFlipComplete));

            // transform.DoRotation(true, rotationSpeed, OnFlipComplete);
        }
        public override void FlipSpecificFace()
        {
            StartCoroutine(transform.DoRotation(false, rotationSpeed, OnFlipComplete));
        }

        public override void CardMatched()
        {

        }

        public override void CardMissMatched()
        {

        }
        void OnFlipComplete()
        {
            isFliped = !isFliped;
        }

        #region Events
        public void SubscribeOnCardClickedEvent(Action<Card> onClicked)
        {
            OnCardClicked += onClicked;
        }
        public void UnSubscribeOnCardClickedEvent(Action<Card> onClicked)
        {
            OnCardClicked -= onClicked;
        }

        public void SubscribeOnCardClickedEvent(Action<bool> onFliped)
        {
            onCardFliped += onFliped;
        }
        public void UnSubscribeOnCardClickedEvent(Action<bool> onFliped)
        {
            onCardFliped -= onFliped;
        }


        #endregion
    }
}

[Serializable]
public class CardData
{
    public int CardType = -1;
    public Sprite normalFaceSprite;
    public Sprite specificFaceSprite;
}