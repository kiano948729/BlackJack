using OOPBlackJack.Enums;
using OOPBlackJack.Models;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace OOPBlackJack
{
    public partial class Form1 : Form
    {
        private Table table;

        public Form1()
        {
            InitializeComponent();

            buttonNewRound.Visible = false;
        }

        //START GAME
        private void buttonStart_Click(object sender, EventArgs e)
        {
            string inputPlayers = Microsoft.VisualBasic.Interaction.InputBox("Aantal spelers (1-4)", "Start spel");
            if (!int.TryParse(inputPlayers, out int players) || players < 1 || players > 4)
            {
                MessageBox.Show("Ongeldig aantal spelers");
                return;
            }

            string inputDecks = Microsoft.VisualBasic.Interaction.InputBox("Aantal decks", "Shoe");
            if (!int.TryParse(inputDecks, out int decks) || decks < 1)
            {
                MessageBox.Show("Ongeldig aantal decks");
                return;
            }

            table = new Table(decks, players);

            table.StartRound();

            DisplayAll();
            UpdateTitle();
        }

        private void buttonHit_Click(object sender, EventArgs e)
        {
            if (table == null || table.State != GameState.DEALERTURN) return;

            table.DealerHit();

            DisplayAll();
            UpdateTitle();

            CheckEndRound();
        }

        private void buttonStand_Click(object sender, EventArgs e)
        {
            if (table == null || table.State != GameState.DEALERTURN) return;

            table.DealerStand();

            DisplayAll();
            UpdateTitle();

            CheckEndRound();
        }

        private void buttonFlip_Click(object sender, EventArgs e)
        {
            if (table == null || table.Dealer.Hand.Cards.Count < 2) return;

            table.Dealer.FlipSecondCard();

            DisplayAll();
        }

        private void buttonNewRound_Click(object sender, EventArgs e)
        {
            if (table == null)
            {
                MessageBox.Show("Start eerst een game");
                return;
            }

            table.NewRound();

            buttonNewRound.Visible = false;

            DisplayAll();
            UpdateTitle();
        }

        //UI UPDATE
        private void DisplayAll()
        {
            if (table == null) return;

            FlowLayoutPanel[] panels =
            {
                flowLayoutPanel1,
                flowLayoutPanel2,
                flowLayoutPanel3,
                flowLayoutPanel4,
            };

            for (int i = 0; i < table.Players.Count; i++)
            {
                panels[i].Controls.Clear();
                foreach (var card in table.Players[i].Hands[0].Cards)
                {
                    panels[i].Controls.Add(CreateCard(card));
                }
            }

            flowLayoutPanelDealer.Controls.Clear();
            foreach (var card in table.Dealer.Hand.Cards)
            {
                flowLayoutPanelDealer.Controls.Add(CreateCard(card));
            }
        }

        private PictureBox CreateCard(Card card)
        {
            PictureBox pb = new PictureBox
            {
                Width = 80,
                Height = 120,
                SizeMode = PictureBoxSizeMode.StretchImage
            };

            string path = card.IsFaceUp ? card.ImagePath : "PNG-cards-1.3/face_down.png";

            if (File.Exists(path))
            {
                pb.Image = Image.FromFile(path);
            }
            else
            {
                pb.BackColor = Color.Green;
            }

            return pb;
        }

        private void UpdateTitle()
        {
            if (table == null) return;

            string player = table.GetActivePlayer() != null
                ? table.GetActivePlayer().Name
                : "Dealer";

            this.Text = $"Speler: {player} | State: {table.State} | Punten: {table.Points}";
        }
        private void CheckEndRound()
        {
            if (table.State == GameState.ROUNDFINISHED)
            {
                MessageBox.Show(table.GetResults());
                buttonNewRound.Visible = true;
            }
        }
    }
}