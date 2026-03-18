using OOPBlackJack.Models;
using System.Diagnostics;

namespace OOPBlackJack
{
    public partial class Form1 : Form
    {
        private Deck deck;
        private Shoe shoe;

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

        private void button2_Click(object sender, EventArgs e)
        {
            if (shoe == null)
            {
                MessageBox.Show("Maak eerst een shoe.");
                return;
            }

            if (shoe.CardsRemaining() == 0)
            {
                MessageBox.Show("Geen kaarten meer in de shoe.");
                return;
            }

            Card card = shoe.DrawCard();
            string path = card.ImagePath;

            Debug.WriteLine($"Full path: {Path.GetFullPath(path)}");

            MessageBox.Show($"Je trok: {card.Rank} van {card.Suit} (waarde {card.Value})");

            try
            {
                if (File.Exists(path))
                {
                    pictureBox1.Image = Image.FromFile(path);
                }
                else
                {
                    MessageBox.Show($"Afbeelding niet gevonden\nPad:\n{path}");
                    pictureBox1.Image = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Afbeelding niet gevonden\n\nPad:\n{path}\n\nError:\n{ex.Message}");
                pictureBox1.Image = null;
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
    }
}