using OOPBlackJack.Enums;

namespace OOPBlackJack.Models
{
    public class Card
    {
        public int Id { get; }
        public Suit Suit { get; }
        public Rank Rank { get; }
        public int Value { get; }
        public bool IsFaceUp { get; private set; }
        public string ImagePath { get; }

        public Card(int id, Suit suit, Rank rank, string imagePath)
        {
            Id = id;
            Suit = suit;
            Rank = rank;
            Value = (int)rank;
            ImagePath = imagePath;
            IsFaceUp = false;
        }

        public void Flip()
        {
            IsFaceUp = !IsFaceUp;
        }
    }
}