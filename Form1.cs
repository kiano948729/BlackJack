using OOPBlackJack.Models;
using System.Diagnostics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace OOPBlackJack
{
    public partial class Form1 : Form
    {
        private Deck deck;
        private Shoe shoe;
        private Hand hand;
        private Player player;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Vraag gebruiker om aantal decks
            string input = Microsoft.VisualBasic.Interaction.InputBox(
                "Hoeveel decks wil je in de shoe?",
                "Aantal decks");

            // Controleer of de gebruiker Cancel heeft geklikt of niets heeft ingevuld
            if (string.IsNullOrWhiteSpace(input))
            {
                MessageBox.Show("Shoe is niet aangemaakt.");
                return;
            }

            if (!int.TryParse(input, out int numberOfDecks) || numberOfDecks < 1)
            {
                MessageBox.Show("Ongeldig aantal decks. Er wordt geen shoe aangemaakt.");
                return;
            }

            try
            {
                shoe = new Shoe(numberOfDecks);
                MessageBox.Show($"Shoe gemaakt met {shoe.CardsRemaining()} kaarten ({numberOfDecks} decks)");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fout bij aanmaken shoe: {ex.Message}");
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            if (shoe == null)
            {
                MessageBox.Show("Maak eerst een shoe.");
                return;
            }

            shoe.Shuffle();

            Card topCard = shoe.Cards[0];
            MessageBox.Show($"Shoe geschud\nBovenste kaart is nu: {topCard.Rank} van {topCard.Suit}.");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            player = new Player("Speler", 100);
            hand = new Hand(10);

            player.AddHand(hand);

            MessageBox.Show("Player en Hand aangemaakt!");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (shoe == null || hand == null)
            {
                MessageBox.Show("Maak eerst een shoe en hand!");
                return;
            }

            if (shoe.CardsRemaining() == 0)
            {
                MessageBox.Show("Geen kaarten meer in de shoe.");
                return;
            }

            Card card = shoe.DrawCard();
            hand.AddCard(card);
            DisplayHand();

            MessageBox.Show(
                $"Kaart: {card.Rank} van {card.Suit}\n" +
                $"Totaal: {hand.GetValue()}\n" +
                $"Busted: {hand.IsBusted()}"
            );
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (hand == null)
            {
                MessageBox.Show("Maak eerst een hand!");
                return;
            }

            hand.Stand();

            MessageBox.Show($"Hand staat nu stil.\nCanPlay: {hand.CanPlay}");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (hand == null)
            {
                MessageBox.Show("Maak eerst een hand!");
                return;
            }

            string cards = "";

            foreach (var c in hand.Cards)
            {
                cards += $"{c.Rank} van {c.Suit}\n";
            }

            MessageBox.Show(
                $"Kaarten:\n{cards}\n" +
                $"Totaal: {hand.GetValue()}"
            );
        }
        private void DisplayHand()
        {
            flowLayoutPanelHand.Controls.Clear();

            foreach (var card in hand.Cards)
            {
                PictureBox pb = new PictureBox();
                pb.Width = 80;
                pb.Height = 120;
                pb.SizeMode = PictureBoxSizeMode.StretchImage;

                if (File.Exists(card.ImagePath))
                {
                    pb.Image = Image.FromFile(card.ImagePath);
                }

                flowLayoutPanelHand.Controls.Add(pb);
            }
        }
        private void flowLayoutPanelHand_Paint(object sender, PaintEventArgs e)
        {
        }

    }
}