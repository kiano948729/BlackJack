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
        public int ActivePlayerIndex { get; private set; }

        public Table(int shoeSize, int amountPlayers)
        {
            if (amountPlayers < 1 || amountPlayers > 5)
                throw new ArgumentException("Aantal spelers moet tussen 1 en 5 liggen.");

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
            ActivePlayerIndex = 0;
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

            ActivePlayerIndex = 0;
            State = GameState.PLAYERTURN;
        }

        public Player GetActivePlayer()
        {
            if (ActivePlayerIndex < Players.Count)
                return Players[ActivePlayerIndex];

            return null;
        }

        public void NextMove()
        {
            if (State == GameState.PLAYERTURN)
            {
                ActivePlayerIndex++;

                if (ActivePlayerIndex >= Players.Count)
                {
                    State = GameState.DEALERTURN;
                    DealerTurn();
                }
            }
        }

        public void PlayerHit()
        {
            if (State != GameState.PLAYERTURN) return;

            var player = GetActivePlayer();
            var hand = player.Hands[0];

            hand.AddCard(Shoe.DrawCard());

            if (hand.IsBusted())
            {
                Points -= 1;
                NextMove();
            }
        }

        public void PlayerStand()
        {
            if (State != GameState.PLAYERTURN) return;

            GetActivePlayer().Hands[0].Stand();
            NextMove();
        }

        private void DealerTurn()
        {
            while (Dealer.Hand.GetValue() < 17)
            {
                Dealer.Hand.AddCard(Shoe.DrawCard());
            }

            State = GameState.ROUNDFINISHED;
            CheckResults();
        }

        private void CheckResults()
        {
            int dealerTotal = Dealer.Hand.GetValue();

            foreach (var player in Players)
            {
                var hand = player.Hand;

                if (hand.IsBusted())
                {
                    Points -= 1;
                }
                else if (dealerTotal > 21 || hand.GetValue() > dealerTotal)
                {
                    Points += 1;
                }
                else if (hand.GetValue() < dealerTotal)
                {
                    Points -= 1;
                }
            }
        }

        public void Reset()
        {
            Shoe = new Shoe(Shoe.CardsRemaining());
            Dealer.Reset();
            Players.Clear();

            State = GameState.WAITING;
            Points = 0;
        }
        public void PlayPlayers()
        {
            foreach (var player in Players)
            {
                var hand = player.Hands[0];

                while (hand.GetValue() < 17)
                {
                    hand.AddCard(Shoe.DrawCard());
                }

                hand.Stand();
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
                {
                    result = "verliest (busted)";
                }
                else if (dealerBusted)
                {
                    result = "wint (dealer busted)";
                }
                else if (playerValue > dealerValue)
                {
                    result = "wint";
                }
                else if (playerValue < dealerValue)
                {
                    result = "verliest";
                }
                else
                {
                    result = "gelijkspel";
                }   

                results += $"{player.Name}: {playerValue} -> {result}\n";
            }

            results += $"\nDealer: {dealerValue}";
            return results;
        }
    }
}