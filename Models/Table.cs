using OOPBlackJack.Enums;
using System;
using System.Collections.Generic;

namespace OOPBlackJack.Models
{
    public class Table
    {
        public Shoe Shoe { get; private set; }
        public Dealer Dealer { get; private set; }
        public List<Player> Players { get; private set; }

        public GameState State { get; private set; }
        public int Points { get; private set; }


        public Table(int shoeSize, int amountPlayers)
        {
            if (amountPlayers < 1 || amountPlayers > 4)
                throw new ArgumentException("Aantal spelers moet tussen 1 en 4 liggen.");

            Shoe = new Shoe(shoeSize);
            Dealer = new Dealer();
            Players = new List<Player>();

            for (int i = 0; i < amountPlayers; i++)
            {
                Player player = new Player($"Speler {i + 1}", 100);
                player.AddHand(new Hand(10));
                Players.Add(player);
            }

            State = GameState.WAITING;
            Points = 0;
        }

        public void StartRound()
        {
            Dealer.Reset();

            foreach (var player in Players)
            {
                player.Hands.Clear();

                Hand hand = new Hand(10);
                player.AddHand(hand);

                hand.AddCard(Shoe.DrawCard());
                hand.AddCard(Shoe.DrawCard());
            }

            Dealer.Deal(Shoe);

            PlayPlayers(); 

            State = GameState.DEALERTURN;
        }

        private void PlayPlayers()
        {
            foreach (var player in Players)
            {
                var hand = player.Hands[0];

                while (hand.GetValue() < 21)
                {
                    if(hand.GetValue() == 11)
                    {

                    }
                    hand.AddCard(Shoe.DrawCard());
                }

                hand.Stand();
            }
        }

        public void DealerHit()
        {
            if (State != GameState.DEALERTURN) return;

            Dealer.Hand.AddCard(Shoe.DrawCard());

            if (Dealer.Hand.IsBusted())
            {
                State = GameState.ROUNDFINISHED;
                CheckResults();
            }
        }

        public void DealerStand()
        {
            if (State != GameState.DEALERTURN) return;

            State = GameState.ROUNDFINISHED;
            CheckResults();
        }

        private void CheckResults()
        {
            int dealerTotal = Dealer.Hand.GetValue();
            bool dealerBusted = Dealer.Hand.IsBusted();

            foreach (var player in Players)
            {
                var hand = player.Hands[0];

                if (hand.IsBusted())
                {
                    Points += 1;
                }
                else if (dealerBusted)
                {
                    Points -= 1;
                }
                else if (dealerTotal > hand.GetValue())
                {
                    Points += 1;
                }
                else if (dealerTotal < hand.GetValue())
                {
                    Points -= 1;
                }
            }
        }

        public string GetResults()
        {
            string results = "";
            int dealerValue = Dealer.Hand.GetValue();
            bool dealerBusted = Dealer.Hand.IsBusted();

            foreach (var player in Players)
            {
                var hand = player.Hands[0];
                int playerValue = hand.GetValue();

                string result;

                if (hand.IsBusted())
                    result = "verliest (busted)";
                else if (dealerBusted)
                    result = "wint (dealer busted)";
                else if (dealerValue > playerValue)
                    result = "verliest";
                else if (dealerValue < playerValue)
                    result = "wint";
                else
                    result = "gelijkspel";

                results += $"{player.Name}: {playerValue} -> {result}\n";
            }

            results += $"\nDealer: {dealerValue}";
            return results;
        }

        public void Reset()
        {
            Shoe = new Shoe(1);
            Dealer.Reset();
            Players.Clear();

            State = GameState.WAITING;
            Points = 0;
        }

        public Player GetActivePlayer()
        {
            if (State == GameState.DEALERTURN)
                return null;

            if (Players.Count > 0)
                return Players[0]; 

            return null;
        }
    }
}