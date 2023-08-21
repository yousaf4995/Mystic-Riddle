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
        public float rotationSpeed = 1.5f;
        [Range(1, 20)]
        public float scaleSpeed = 2f;

        [Space]
        [Header("Test")]
        public bool testGamePaly = false;
        public TMPro.TMP_Text testIdTxt;
        private void Start()
        {
        }
        public override void Init(CardData CardData, Action<Card> cardClickEvent, Action callBack)
        {
            DoScale();
            AddListners(cardClickEvent, callBack);
            FillData(CardData);

            DisplayTestId();
        }

        void DisplayTestId()
        {
            testIdTxt.gameObject.SetActive(testGamePaly);
            testIdTxt.text = string.Empty + CardData.CardType;// ToString();
        }
        void AddListners(Action<Card> CardData, Action callBack)
        {
            cardBtn.onClick.RemoveListener(() => { CardClicked(this); }); // Remove the old listener
            cardBtn.onClick.AddListener(() => CardClicked(this));

            UnSubscribeOnCardClickedEvent(CardData);// Remove Before the new listener
            SubscribeOnCardClickedEvent(CardData);// Add the new listener

            UnSubscribeOnCallBack(callBack);// Remove Before the new listener
            SubscribeOnCallBack(callBack);// Add the new listener
        }
        void FillData(CardData cData)
        {
            this.CardData = cData;
            cardFaceImage.sprite = this.CardData.normalFaceSprite;
        }

        public override void Flip()
        {

        }
        public override void CardClicked(Card card)
        {
            OnCardClicked?.Invoke(card);
            FlipSpecificFace();
            // Debug.Log("Card Clicked with Card Type : " + CardData.CardType);
        }

        public override void CardMatched()
        {
            // this.gameObject.SetActive(false);

            CardData.cardState = CardState.Correct;
            this.transform.localScale = Vector3.zero;
        }
        public override void CardMissMatched()
        {
            FlipNormalFace();

            CardData.cardState = CardState.InCorrect;
            cardBtn.interactable = true;
        }

        public void DoScale()
        {
            Transform transformToRotate = transform; // You can replace this with the actual transform you want to rotate

            StopCoroutine(transformToRotate.DoScale(Vector3.one, scaleSpeed));
            StartCoroutine(transformToRotate.DoScale(Vector3.one, scaleSpeed));

        }

        public override void FlipNormalFace()
        {
            cardBtn.interactable = true;
            isFliped = false;

            Transform transformToRotate = transform; // You can replace this with the actual transform you want to rotate

            StopCoroutine(transformToRotate.DoRotation(0, rotationSpeed, OnFlipStart, OnFlipMiddle, OnFlipComplete));
            StartCoroutine(transformToRotate.DoRotation(0, rotationSpeed, OnFlipStart, OnFlipMiddle, OnFlipComplete));

        }
        public override void FlipSpecificFace()
        {
            isFliped = true;
            cardBtn.interactable = false;
            Transform transformToRotate = transform;
            StopCoroutine(transformToRotate.DoRotation(180, rotationSpeed, OnFlipStart, OnFlipMiddle, OnFlipComplete));
            StartCoroutine(transformToRotate.DoRotation(180, rotationSpeed, OnFlipStart, OnFlipMiddle, OnFlipComplete));

        }

        void OnFlipStart()
        {
            //  Debug.Log("OnFlip Start : " + CardData.CardType);  
            // cardFaceImage.sprite = IsFliped ? this.CardData.normalFaceSprite : CardData.specificFaceSprite;
        }
        void OnFlipMiddle()
        {

            // isFliped = !isFliped;
            //  Debug.Log("OnFlip Middle After : " + isFliped + "of card type :" + CardData.CardType);
            cardFaceImage.sprite = IsFliped ? this.CardData.specificFaceSprite : CardData.normalFaceSprite;
        }
        void OnFlipComplete()
        {

            // Debug.Log("OnFlip Complete Before : " + isFliped + " with card Type : " + CardData.CardType);

            if (isFliped)// prevent extra call on complete
            {
                OncallBack?.Invoke();
            }
        }
     
        #region Events Subscription
        public void SubscribeOnCardClickedEvent(Action<Card> onClicked)
        {
            OnCardClicked += onClicked;
        }
        public void UnSubscribeOnCardClickedEvent(Action<Card> onClicked)
        {
            OnCardClicked -= onClicked;
        }
        public void SubscribeOnCallBack(Action callBack)
        {
            OncallBack += callBack;
        }
        public void UnSubscribeOnCallBack(Action callBack)
        {
            OncallBack -= callBack;
        }

        public void SubscribeOnCardClickedEvent(Action<bool> onFliped)
        {
            OnCardFliped += onFliped;
        }
        public void UnSubscribeOnCardClickedEvent(Action<bool> onFliped)
        {
            OnCardFliped -= onFliped;
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
    // just for save purpose
    public CardState cardState;
}