namespace OOPBlackJack.Models
{
    public class Hand
    {
        private List<Card> cards = new();
        public IReadOnlyList<Card> Cards => cards.AsReadOnly();
        public bool CanPlay { get; private set; } = true;
        public bool HasPassed { get; private set; } = false;
        public int Bet { get; private set; }

        public Hand(int bet = 0)
        {
            Bet = bet;
        }

        public void AddCard(Card card)
        {
            if (!CanPlay) return;

            cards.Add(card);

            if (IsBusted())
            {
                CanPlay = false;
            }
        }

        public void Stand()
        {
            HasPassed = true;
            CanPlay = false;
        }

        public void DoubleDown(Card card, int amount)
        {
            if (!CanPlay) return;

            Bet += amount;
            AddCard(card);

            Stand();// na double down moet je stoppen
        }

        public int GetValue()
        {
            int total = 0;
            int aceCount = 0;

            foreach (var card in cards)
            {
                if (card.Rank == Enums.Rank.ACE)
                {
                    aceCount++;
                    total += 11;
                }
                else
                {
                    total += card.Value;
                }
            }

            while (total > 21 && aceCount > 0)
            {
                total -= 10;
                aceCount--;
            }

            return total;
        }

        public bool IsBusted()
        {
            return GetValue() > 21;
        }
    }
}