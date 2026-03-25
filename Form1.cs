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
        }

        // START GAME
        private void buttonStart_Click(object sender, EventArgs e)
        {
            string inputPlayers = Microsoft.VisualBasic.Interaction.InputBox("Aantal spelers (1-5)", "Start spel");
            if (!int.TryParse(inputPlayers, out int players) || players < 1 || players > 5)
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
            if (table == null || table.State != GameState.PLAYERTURN) return;

            table.PlayerHit();
            DisplayAll();
            UpdateTitle();

            if (table.State == GameState.ROUNDFINISHED)
            {
                MessageBox.Show(table.GetResults());
                UpdateTitle();
            }
        }

        private void buttonStand_Click(object sender, EventArgs e)
        {
            if (table == null || table.State != GameState.PLAYERTURN) return;

            table.PlayerStand();
            DisplayAll();
            UpdateTitle();

            if (table.State == GameState.ROUNDFINISHED)
            {
                MessageBox.Show(table.GetResults());
                UpdateTitle();
            }
        }

        // UI UPDATE
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

            if (File.Exists(card.ImagePath))
            {
                pb.Image = Image.FromFile(card.ImagePath);
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
    }
}