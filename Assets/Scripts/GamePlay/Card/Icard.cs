namespace CardModel
{
    public interface Icard
    {
        public void Init(CardData cardData);
        public void Flip();
        public void OnCardClicked();
        
    }
}