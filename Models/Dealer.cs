namespace OOPBlackJack.Models
{
    public class Dealer
    {
        public Hand Hand { get; private set; }

        public Dealer()
        {
            Hand = new Hand();
        }

        public void Reset()
        {
            Hand = new Hand();
        }

        public void Deal(Shoe shoe)
        {
            var firstCard = shoe.DrawCard();
            //1e kaart wordt geflipt
            firstCard.Flip();
            Hand.AddCard(firstCard);

            var secondCard = shoe.DrawCard();
            //2e kaart blijft facedown
            Hand.AddCard(secondCard);
        }

        public void Hit(Shoe shoe)
        {
            var newCard = shoe.DrawCard();
            newCard.Flip();
            Hand.AddCard(newCard);
        }

        public void Stand()
        {
            Hand.Stand();
        }

        public void FlipSecondCard()
        {
            if (Hand.Cards.Count >= 2)
            {
                Hand.Cards[1].Flip();
            }
        }
    }
}