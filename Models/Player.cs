namespace OOPBlackJack.Models
{
    public class Player
    {
        public string Name { get; set; }
        public List<Hand> Hands { get; private set; } = new();
        public int Balance { get; private set; }
        public int InsuranceBet { get; private set; }

        public Hand Hand => Hands[0];
        public Player(string name, int balance)
        {
            Name = name;
            Balance = balance;
        }

        public void AddHand(Hand hand)
        {
            Hands.Add(hand);
        }

        public bool CanBet(int amount)
        {
            return amount > 0 && amount <= Balance;
        }

        public void PlaceBet(Hand hand, int amount)
        {
            if (!CanBet(amount)) return;

            hand.SetBet(amount);
            Balance -= amount;
        }

        public void SettleBet(Hand hand, int result)
        {
            //result:
            //1 = win
            //0 = push
            //-1 = lose

            if (result == 1)
            {
                Balance += hand.Bet * 2; //return bet + winst
            }
            else if (result == 0)
            {
                Balance += hand.Bet; 
            }
        }

        public void Split(int handIdx)
        {
            var hand = Hands[handIdx];

            if (hand.Cards.Count != 2) { 
                return; 
            }

            if (hand.Cards[0].Rank != hand.Cards[1].Rank) { 
                return; 
            }

            Hand newHand = new Hand(hand.Bet);

            //kaart verplaatsen
            var secondCard = hand.Cards[1];

            //omdat Cards readonly is -> nieuwe hands maken
            Hand updatedHand1 = new Hand(hand.Bet);
            updatedHand1.AddCard(hand.Cards[0]);

            newHand.AddCard(secondCard);

            Hands[handIdx] = updatedHand1;
            Hands.Add(newHand);

            Balance -= hand.Bet;
        }

        public void PlaceInsurance(int amount)
        {
            if (amount > Balance) return;

            InsuranceBet = amount;
            Balance -= amount;
        }
    }
}