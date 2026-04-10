using OOPBlackJack.Enums;
using OOPBlackJack.Models;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using Timer = System.Windows.Forms.Timer;
namespace OOPBlackJack
{
    public partial class Form1 : Form
    {
        private Table table;
        private Label labelDealerFeedback;
        private Timer feedbackTimer;

        public Form1()
        {
            InitializeComponent();

            buttonNewRound.Visible = false;

            labelDealerFeedback = new Label
            {
                AutoSize = true,
                Font = new Font("Arial", 12, FontStyle.Bold),
                ForeColor = Color.Black,
                Text = "",
                Margin = new Padding(10)
            };
            this.Controls.Add(labelDealerFeedback);

            //timer om feedback te verbergen na 3 seconden
            feedbackTimer = new Timer();
            feedbackTimer.Interval = 4000; 
            feedbackTimer.Tick += (s, e) =>
            {
                labelDealerFeedback.Text = "";
                feedbackTimer.Stop();
            };
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

            int dealerValue = table.Dealer.Hand.GetValue();
            bool correctChoice = dealerValue < 17;

            table.DealerHit();

            ShowDealerFeedback(correctChoice);

            DisplayAll();
            UpdateTitle();

            CheckEndRound();
        }

        private void buttonStand_Click(object sender, EventArgs e)
        {
            if (table == null || table.State != GameState.DEALERTURN) return;

            int dealerValue = table.Dealer.Hand.GetValue();
            bool correctChoice = dealerValue >= 17;
            
            table.DealerStand();

            ShowDealerFeedback(correctChoice);

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

            //panels leegmaken
            foreach (var panel in panels)
            {
                panel.Controls.Clear();
            }

            for (int i = 0; i < table.Players.Count; i++)
            {
                var player = table.Players[i];

                for (int h = 0; h < player.Hands.Count; h++)
                {
                    var hand = player.Hands[h];

                    FlowLayoutPanel handPanel = new FlowLayoutPanel
                    {
                        AutoSize = true,
                        FlowDirection = FlowDirection.LeftToRight,
                        WrapContents = false,
                        Margin = new Padding(0, 0, 5, 0),
                        Padding = new Padding(2),
                        BackColor = (table.ActivePlayerIndex == i && table.ActiveHandIndex == h)
                            ? Color.LightGoldenrodYellow //highlight actieve hand
                            : Color.Transparent,
                        BorderStyle = BorderStyle.FixedSingle
                    };

                    //kaarten toevoegen aan handPanel
                    foreach (var card in hand.Cards)
                    {
                        handPanel.Controls.Add(CreateCard(card));
                    }

                    panels[i].Controls.Add(handPanel);

                    if (h < player.Hands.Count - 1)
                    {
                        panels[i].Controls.Add(new Label()
                        {
                            Text = "  |  ", //kleine scheiding tussen handen
                            AutoSize = true,
                            Font = new Font("Arial", 14, FontStyle.Bold),
                            TextAlign = ContentAlignment.MiddleCenter,
                            Margin = new Padding(3, 0, 3, 0)
                        });
                    }
                }
            }

            flowLayoutPanelDealer.Controls.Clear();

            //highlight de dealer wanneer het dealer zijn beurt is
            flowLayoutPanelDealer.BackColor = table.State == GameState.DEALERTURN
                ? Color.LightGoldenrodYellow
                : Color.Transparent;

            foreach (var card in table.Dealer.Hand.Cards)
            {
                flowLayoutPanelDealer.Controls.Add(CreateCard(card));
            }
        }

        private PictureBox CreateCard(Card card)
        {
            PictureBox pb = new PictureBox
            {
                Width = 60, 
                Height = 90,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Margin = new Padding(2),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White
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
                ? table.GetActivePlayer().Name + $" (Balance: {table.GetActivePlayer().Balance})"
                : "Dealer";

            this.Text = $"Speler: {player} | State: {table.State} | Punten: {table.Points}";
        }
        private void CheckEndRound()
        {
            if (table.State == GameState.ROUNDFINISHED)
            {
                ResultsForm form = new ResultsForm();

                form.LoadResults(
                    table.Players,
                    table.Dealer.Hand.GetValue(),
                    table.Dealer.Hand.IsBusted()
                );

                form.ShowDialog();

                buttonNewRound.Visible = true;
            }
        }

        private void buttonPLayerHit_Click(object sender, EventArgs e)
        {
            if (table == null) return;

            table.PlayerHit();
            DisplayAll();
            UpdateTitle();
        }

        private void buttonPlayerStand_Click(object sender, EventArgs e)
        {
            if (table == null) return;

            table.PlayerStand();
            DisplayAll();
            UpdateTitle();
        }

        private void buttonPlayerDouble_Click(object sender, EventArgs e)
        {
            if (table == null) return;

            table.PlayerDouble();
            DisplayAll();
            UpdateTitle();
        }

        private void buttonPlayerSplit_Click(object sender, EventArgs e)
        {
            if (table == null) return;

            table.PlayerSplit();
            DisplayAll();
            UpdateTitle();
        }

        private void ShowDealerFeedback(bool correct)
        {
            labelDealerFeedback.Text = correct
                ? "Dealer maakte een goede keuze!"
                : "Dealer maakte een foute keuze!";

            labelDealerFeedback.ForeColor = correct
                ? Color.LimeGreen
                : Color.DarkRed;

            labelDealerFeedback.BackColor = Color.Transparent;

            feedbackTimer.Stop();
            feedbackTimer.Start();
        }
    }
}