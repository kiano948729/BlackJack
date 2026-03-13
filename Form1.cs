using OOPBlackJack.Models;
using System.Diagnostics;

namespace OOPBlackJack
{
    public partial class Form1 : Form
    {
        private Deck deck;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            deck = new Deck();
            MessageBox.Show($"Deck gemaakt met {deck.Cards.Count} kaarten");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (deck == null)
            {
                MessageBox.Show("Maak eerst een deck");
                return;
            }

            if (deck.CardsRemaining() == 0)
            {
                MessageBox.Show("Geen kaarten meer in het deck");
                return;
            }

            Card card = deck.DrawCard();
            string path = card.ImagePath;

            Debug.WriteLine($"Full path: {Path.GetFullPath(path)}");
            MessageBox.Show($"Je trok: {card.Rank} of {card.Suit}");

            try
            {
                pictureBox1.Image = Image.FromFile(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Afbeelding niet gevonden\n\nPad:\n{path}\n\nError:\n{ex.Message}");
                pictureBox1.Image = null;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (deck == null)
            {
                MessageBox.Show("Maak eerst een deck");
                return;
            }

            deck.Shuffle();

            Card topCard = deck.Cards[0];

            MessageBox.Show($"Deck geschud\nBovenste kaart is nu: {topCard.Rank} of {topCard.Suit}");
        }
    }
}