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
            Hand.AddCard(shoe.DrawCard());
            Hand.AddCard(shoe.DrawCard());
        }

        public void HitCard(Card card)
        {
            Hand.AddCard(card);
        }

        public void Stand()
        {
            Hand.Stand();
        }

        public void PlayTurn(Shoe shoe)
        {
            //dealer moet blijven hitten tot 17
            while (Hand.GetValue() < 17)
            {
                Hand.AddCard(shoe.DrawCard());
            }

            Stand();
        }
    }
}