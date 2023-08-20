using GameCard;
using System;

namespace CardModel
{
    public interface Icard
    {
        public void Init(CardData cardData, Action<Card> cardClickEvent);
        public void Flip();
        public void CardClicked();
        public void CardMatched();
        public void CardMissMatched();

    }
}