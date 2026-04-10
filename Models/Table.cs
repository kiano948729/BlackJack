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
        public int ActiveHandIndex { get; private set; }

        public Table(int shoeSize, int amountPlayers)
        {
            if (amountPlayers < 1 || amountPlayers > 4)
                throw new ArgumentException("Aantal spelers moet tussen 1 en 4 liggen.");

            Shoe = new Shoe(shoeSize);
            Dealer = new Dealer();
            Players = new List<Player>();

            for (int i = 0; i < amountPlayers; i++)
            {
                var player = new Player($"Speler {i + 1}", 100);
                player.AddHand(new Hand(10));
                Players.Add(player);
            }

            State = GameState.WAITING;
        }

        public void StartRound()
        {
            Dealer.Reset();
            ActivePlayerIndex = 0;
            ActiveHandIndex = 0;

            foreach (var player in Players)
            {
                player.Hands.Clear();
                var hand = new Hand();
                int betAmount = 10;

                if (player.CanBet(betAmount))
                {
                    player.PlaceBet(hand, betAmount);
                }

                player.AddHand(hand);

                var c1 = Shoe.DrawCard(); c1.Flip();
                var c2 = Shoe.DrawCard(); c2.Flip();

                hand.AddCard(c1);
                hand.AddCard(c2);
            }

            Dealer.Deal(Shoe);

            State = GameState.PLAYERTURN;
        }

        public Player GetActivePlayer()
        {
            if (ActivePlayerIndex < Players.Count)
                return Players[ActivePlayerIndex];

            return null;
        }

        public void NextPlayer()
        {
            ActivePlayerIndex++;

            if (ActivePlayerIndex >= Players.Count)
            {
                State = GameState.DEALERTURN;
            }
        }

        public void PlayerHit()
        {
            if (State != GameState.PLAYERTURN) return;

            var hand = GetActiveHand();
            if (hand == null) return;

            var card = Shoe.DrawCard();
            card.Flip();
            hand.AddCard(card);

            if (hand.IsBusted())
            {
                hand.Stand(); //direct stand bij busted
                NextHand();
            }
        }

        public void PlayerStand()
        {
            if (State != GameState.PLAYERTURN) return;

            var hand = GetActiveHand();
            if (hand == null) return;

            hand.Stand();
            NextHand();
        }

        private void NextHand()
        {
            var player = GetActivePlayer();
            if (player == null) return;

            ActiveHandIndex++;

            if (ActiveHandIndex >= player.Hands.Count)
            {
                ActiveHandIndex = 0;
                NextPlayer();
            }
        }

        public void PlayerDouble()
        {
            if (State != GameState.PLAYERTURN) return;

            var hand = GetActiveHand();
            if (hand == null) return;

            var card = Shoe.DrawCard();
            card.Flip();

            hand.DoubleDown(card, hand.Bet);
            NextHand();
        }

        public void PlayerSplit()
        {
            if (State != GameState.PLAYERTURN) return;

            var player = GetActivePlayer();
            if (player == null) return;

            player.Split(ActiveHandIndex);

            //nieuwe kaarten uitdelen aan gesplitte hands
            var hand = GetActiveHand();
            if (hand.Cards.Count == 1)
            {
                var card = Shoe.DrawCard();
                card.Flip();
                hand.AddCard(card);
            }
        }

        public Hand GetActiveHand()
        {
            var player = GetActivePlayer();

            if (player == null) return null;

            if (ActiveHandIndex < player.Hands.Count)
                return player.Hands[ActiveHandIndex];

            return null;
        }

        public void DealerHit()
        {
            if (State != GameState.DEALERTURN) return;

            int dealerValue = Dealer.Hand.GetValue();

            if (dealerValue < 17)
            {
                Points += 1;
            }
            else
            {
                Points -= 1;
            }

            Dealer.Hit(Shoe);

            if (Dealer.Hand.IsBusted())
            {
                EndRound();
            }
        }

        public void DealerStand()
        {
            if (State != GameState.DEALERTURN) return;

            int dealerValue = Dealer.Hand.GetValue();

            if (dealerValue >= 17)
            {
                Points += 1;
            }
            else
            {
                Points -= 1;
            }

            EndRound();
        }

        private void EndRound()
        {
            State = GameState.ROUNDFINISHED;
            CheckResults();
        }

        private void CheckResults()
        {
            int dealerTotal = Dealer.Hand.GetValue();
            bool dealerBusted = Dealer.Hand.IsBusted();

            foreach (var player in Players)
            {
                foreach (var hand in player.Hands)
                {
                    if (hand.Bet == 0) continue;

                    if (hand.IsBusted())
                    {
                        Points += 1;
                    }
                    else if (dealerBusted)
                    {
                        Points -= 1;
                        player.WinBet(hand.Bet);
                    }
                    else if (dealerTotal > hand.GetValue())
                    {
                        Points += 1;
                        player.WinBet(hand.Bet);
                    }
                    else if (dealerTotal < hand.GetValue())
                    {
                        Points -= 1;
                    }
                    else
                    {
                        player.PushBet(hand.Bet);
                    }
                }
            }
        }

        public void NewRound()
        {
            if (State != GameState.ROUNDFINISHED) return;
            StartRound();
        }

        public string GetResults()
        {
            string results = "";
            int dealerValue = Dealer.Hand.GetValue();
            bool dealerBusted = Dealer.Hand.IsBusted();

            foreach (var player in Players)
            {
                results += $"\n{player.Name} (Balance: {player.Balance})\n";

                for (int h = 0; h < player.Hands.Count; h++)
                {
                    var hand = player.Hands[h];
                    int val = hand.GetValue();

                    int bet = hand.Bet;

                    string result;
                    int winLoss = 0;

                    if (hand.IsBusted())
                    {
                        result = "verliest (busted)";
                        winLoss = -bet;
                    }
                    else if (dealerBusted)
                    {
                        result = "wint (dealer busted)";
                        winLoss = bet;
                    }
                    else if (val > dealerValue)
                    {
                        result = "wint";
                        winLoss = bet;
                    }
                    else if (val < dealerValue)
                    {
                        result = "verliest";
                        winLoss = -bet;
                    }
                    else
                    {
                        result = "gelijkspel";
                        winLoss = 0;
                    }

                    //toon handnummer bij meerdere handen
                    string handLabel = player.Hands.Count > 1 ? $"Hand {h + 1}" : "";

                    results += $"{handLabel}: {val} -> {result} ({winLoss:+0;-0;0})\n";
                }

                results += $"Saldo na ronde: {player.Balance}\n";
            }

            results += $"\nDealer: {dealerValue}";
            return results;
        }
    }
}