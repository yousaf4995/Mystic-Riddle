using GameCard;
using System;

namespace CardModel
{
    public interface Icard
    {
        public void Init(CardData cardData, Action<Card> cardClickEvent, Action callBack);
        public void Flip();
        public void CardClicked(Card card);
        public void CardMatched();
        public void CardMissMatched();

    }
}