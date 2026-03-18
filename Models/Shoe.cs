using System.Collections.Generic;

namespace OOPBlackJack.Models
{
    public class Shoe
    {
        private List<Card> cards;
        public IReadOnlyList<Card> Cards => cards.AsReadOnly();

        public Shoe(int numberOfDecks)
        {
            if (numberOfDecks < 1)
                throw new System.ArgumentException("Een shoe moet minstens 1 deck bevatten.");

            cards = new List<Card>();
            for (int i = 0; i < numberOfDecks; i++)
            {
                Deck deck = new Deck();
                cards.AddRange(deck.Cards);
            }

            ShuffleHelper.Shuffle(cards);// shuffle meteen bij aanmaak
        }

        public void Shuffle()
        {
            ShuffleHelper.Shuffle(cards);
        }

        public Card DrawCard()
        {
            if (cards.Count == 0)
                throw new System.InvalidOperationException("Geen kaarten meer in de shoe.");

            Card card = cards[0];
            cards.RemoveAt(0);
            return card;
        }

        public int CardsRemaining() => cards.Count;
    }
}