using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Windows.Forms;

namespace OOPBlackJack
{
    public partial class ResultsForm : Form
    {
        private DataGridView grid;

        public ResultsForm()
        {
            InitializeComponent();

            this.Text = "Blackjack Resultaten";
            this.Width = 800;
            this.Height = 500;
            this.StartPosition = FormStartPosition.CenterScreen;

            grid = new DataGridView();
            grid.Dock = DockStyle.Fill;
            grid.ReadOnly = true;
            grid.AllowUserToAddRows = false;
            grid.RowHeadersVisible = false;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            grid.Columns.Add("player", "Speler");
            grid.Columns.Add("hand", "Hand");
            grid.Columns.Add("value", "Waarde");
            grid.Columns.Add("result", "Resultaat");
            grid.Columns.Add("change", "Winst/Verlies");

            this.Controls.Add(grid);
        }

        public void LoadResults(dynamic players, int dealerValue, bool dealerBusted)
        {
            grid.Rows.Clear();

            foreach (var player in players)
            {
                for (int i = 0; i < player.Hands.Count; i++)
                {
                    var hand = player.Hands[i];
                    int val = hand.GetValue();
                    int bet = hand.Bet;

                    string result;
                    int change;

                    if (hand.IsBusted())
                    {
                        result = "Verlies (Bust)";
                        change = -bet;
                    }
                    else if (dealerBusted)
                    {
                        result = "Winst (Dealer Bust)";
                        change = bet;
                    }
                    else if (val > dealerValue)
                    {
                        result = "Winst";
                        change = bet;
                    }
                    else if (val < dealerValue)
                    {
                        result = "Verlies";
                        change = -bet;
                    }
                    else
                    {
                        result = "Gelijk";
                        change = 0;
                    }

                    int rowIndex = grid.Rows.Add(
                        player.Name,
                        i + 1,
                        val,
                        result,
                        change
                    );

                    if (change > 0)
                        grid.Rows[rowIndex].DefaultCellStyle.BackColor = Color.LightGreen;
                    else if (change < 0)
                        grid.Rows[rowIndex].DefaultCellStyle.BackColor = Color.LightCoral;
                    else
                        grid.Rows[rowIndex].DefaultCellStyle.BackColor = Color.LightGray;
                }
            }
        }
    }
}
