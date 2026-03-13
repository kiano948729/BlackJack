using OOPBlackJack.Enums;

namespace OOPBlackJack.Models
{
    public class Deck
    {
        private readonly Random random = new();
        public List<Card> Cards { get; }

        public Deck()
        {
            Cards = new List<Card>();
            GenerateDeck();
        }

        private void GenerateDeck()
        {
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                {
                    string rankName = rank switch
                    {
                        Rank.ACE => "ace",
                        Rank.JACK => "jack",
                        Rank.QUEEN => "queen",
                        Rank.KING => "king",
                        // omdat enum bij 0 begint (ACE=0)
                        _ => ((int)rank + 1).ToString() 
                    };

                    Cards.Add(new Card(
                        suit,
                        rank,
                        $"PNG-cards-1.3/{rankName}_of_{suit.ToString().ToLower()}.png"
                    ));
                }
            }
        }

        // Fisher-Yates shuffle algoritme
        public void Shuffle()
        {
            for (int i = Cards.Count - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                Card temp = Cards[i];
                Cards[i] = Cards[j];
                Cards[j] = temp;
            }
        }

        public Card DrawCard()
        {
            if (Cards.Count == 0)
                throw new InvalidOperationException("Geen kaarten over in het deck.");

            Card card = Cards[0];
            Cards.RemoveAt(0);
            return card;
        }

        public int CardsRemaining()
        {
            return Cards.Count;
        }
    }
}