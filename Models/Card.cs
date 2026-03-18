using OOPBlackJack.Enums;

namespace OOPBlackJack.Models
{
    public class Card
    {
        public Suit Suit { get; }
        public Rank Rank { get; }
        public int Value => GetBlackjackValue();
        public bool IsFaceUp { get; private set; }
        public string ImagePath { get; }

        public Card(Suit suit, Rank rank, string imagePath)
        {
            Suit = suit;
            Rank = rank;
            ImagePath = imagePath;
            IsFaceUp = false;
        }

        private int GetBlackjackValue()
        {
            return Rank switch
            {
                Rank.ACE => 11,
                Rank.JACK or Rank.QUEEN or Rank.KING or Rank.TEN => 10,
                _ => (int)Rank + 1 
            };
        }

        public void Flip()
        {
            IsFaceUp = !IsFaceUp;
        }

        public override string ToString()
        {
            return $"{Rank} of {Suit}";
        }
    }
}