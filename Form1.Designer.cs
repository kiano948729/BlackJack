namespace OOPBlackJack
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            buttonStart = new Button();
            buttonHit = new Button();
            buttonStand = new Button();
            flowLayoutPanel1 = new FlowLayoutPanel();
            flowLayoutPanelDealer = new FlowLayoutPanel();
            flowLayoutPanel2 = new FlowLayoutPanel();
            flowLayoutPanel3 = new FlowLayoutPanel();
            flowLayoutPanel4 = new FlowLayoutPanel();
            buttonFlip = new Button();
            buttonNewRound = new Button();
            buttonPLayerHit = new Button();
            buttonPlayerStand = new Button();
            buttonPlayerDouble = new Button();
            buttonPlayerSplit = new Button();
            SuspendLayout();
            // 
            // buttonStart
            // 
            buttonStart.Location = new Point(50, 530);
            buttonStart.Name = "buttonStart";
            buttonStart.Size = new Size(150, 45);
            buttonStart.TabIndex = 0;
            buttonStart.Text = "Start Game";
            buttonStart.UseVisualStyleBackColor = true;
            buttonStart.Click += buttonStart_Click;
            // 
            // buttonHit
            // 
            buttonHit.Location = new Point(512, 530);
            buttonHit.Name = "buttonHit";
            buttonHit.Size = new Size(150, 45);
            buttonHit.TabIndex = 1;
            buttonHit.Text = "Hit";
            buttonHit.UseVisualStyleBackColor = true;
            buttonHit.Click += buttonHit_Click;
            // 
            // buttonStand
            // 
            buttonStand.Location = new Point(668, 530);
            buttonStand.Name = "buttonStand";
            buttonStand.Size = new Size(150, 45);
            buttonStand.TabIndex = 2;
            buttonStand.Text = "Stand";
            buttonStand.UseVisualStyleBackColor = true;
            buttonStand.Click += buttonStand_Click;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.BackColor = Color.Transparent;
            flowLayoutPanel1.Location = new Point(131, 189);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(262, 187);
            flowLayoutPanel1.TabIndex = 3;
            // 
            // flowLayoutPanelDealer
            // 
            flowLayoutPanelDealer.BackColor = Color.Transparent;
            flowLayoutPanelDealer.Location = new Point(378, 382);
            flowLayoutPanelDealer.Name = "flowLayoutPanelDealer";
            flowLayoutPanelDealer.Size = new Size(498, 120);
            flowLayoutPanelDealer.TabIndex = 8;
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.BackColor = Color.Transparent;
            flowLayoutPanel2.Location = new Point(399, 189);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Size = new Size(262, 187);
            flowLayoutPanel2.TabIndex = 4;
            // 
            // flowLayoutPanel3
            // 
            flowLayoutPanel3.BackColor = Color.Transparent;
            flowLayoutPanel3.Location = new Point(667, 189);
            flowLayoutPanel3.Name = "flowLayoutPanel3";
            flowLayoutPanel3.Size = new Size(262, 187);
            flowLayoutPanel3.TabIndex = 5;
            // 
            // flowLayoutPanel4
            // 
            flowLayoutPanel4.BackColor = Color.Transparent;
            flowLayoutPanel4.Location = new Point(935, 189);
            flowLayoutPanel4.Name = "flowLayoutPanel4";
            flowLayoutPanel4.Size = new Size(262, 187);
            flowLayoutPanel4.TabIndex = 6;
            // 
            // buttonFlip
            // 
            buttonFlip.Location = new Point(356, 530);
            buttonFlip.Name = "buttonFlip";
            buttonFlip.Size = new Size(150, 45);
            buttonFlip.TabIndex = 9;
            buttonFlip.Text = "Flip";
            buttonFlip.UseVisualStyleBackColor = true;
            buttonFlip.Click += buttonFlip_Click;
            // 
            // buttonNewRound
            // 
            buttonNewRound.Location = new Point(824, 530);
            buttonNewRound.Name = "buttonNewRound";
            buttonNewRound.Size = new Size(150, 45);
            buttonNewRound.TabIndex = 10;
            buttonNewRound.Text = "NewRound";
            buttonNewRound.UseVisualStyleBackColor = true;
            buttonNewRound.Click += buttonNewRound_Click;
            // 
            // buttonPLayerHit
            // 
            buttonPLayerHit.Location = new Point(112, 25);
            buttonPLayerHit.Name = "buttonPLayerHit";
            buttonPLayerHit.Size = new Size(78, 33);
            buttonPLayerHit.TabIndex = 11;
            buttonPLayerHit.Text = "PlayerHit";
            buttonPLayerHit.UseVisualStyleBackColor = true;
            buttonPLayerHit.Click += buttonPLayerHit_Click;
            // 
            // buttonPlayerStand
            // 
            buttonPlayerStand.Location = new Point(196, 25);
            buttonPlayerStand.Name = "buttonPlayerStand";
            buttonPlayerStand.Size = new Size(78, 33);
            buttonPlayerStand.TabIndex = 12;
            buttonPlayerStand.Text = "PlayerStand";
            buttonPlayerStand.UseVisualStyleBackColor = true;
            buttonPlayerStand.Click += buttonPlayerStand_Click;
            // 
            // buttonPlayerDouble
            // 
            buttonPlayerDouble.Location = new Point(280, 25);
            buttonPlayerDouble.Name = "buttonPlayerDouble";
            buttonPlayerDouble.Size = new Size(94, 33);
            buttonPlayerDouble.TabIndex = 13;
            buttonPlayerDouble.Text = "PlayerDouble";
            buttonPlayerDouble.UseVisualStyleBackColor = true;
            buttonPlayerDouble.Click += buttonPlayerDouble_Click;
            // 
            // buttonPlayerSplit
            // 
            buttonPlayerSplit.Location = new Point(385, 25);
            buttonPlayerSplit.Name = "buttonPlayerSplit";
            buttonPlayerSplit.Size = new Size(107, 33);
            buttonPlayerSplit.TabIndex = 14;
            buttonPlayerSplit.Text = "PlayerSplit";
            buttonPlayerSplit.UseVisualStyleBackColor = true;
            buttonPlayerSplit.Click += buttonPlayerSplit_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.ForestGreen;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(1218, 629);
            Controls.Add(buttonPlayerSplit);
            Controls.Add(buttonPlayerDouble);
            Controls.Add(buttonPlayerStand);
            Controls.Add(buttonPLayerHit);
            Controls.Add(buttonNewRound);
            Controls.Add(buttonFlip);
            Controls.Add(flowLayoutPanel4);
            Controls.Add(flowLayoutPanel3);
            Controls.Add(flowLayoutPanel2);
            Controls.Add(buttonStart);
            Controls.Add(buttonHit);
            Controls.Add(buttonStand);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(flowLayoutPanelDealer);
            Name = "Form1";
            Text = "Blackjack (Dealer View)";
            ResumeLayout(false);
        }

        private Button buttonStart;
        private Button buttonHit;
        private Button buttonStand;

        private FlowLayoutPanel flowLayoutPanel1;
        private FlowLayoutPanel flowLayoutPanelDealer;
        private FlowLayoutPanel flowLayoutPanel2;
        private FlowLayoutPanel flowLayoutPanel3;
        private FlowLayoutPanel flowLayoutPanel4;
        private Button buttonFlip;
        private Button buttonNewRound;
        private Button buttonPLayerHit;
        private Button buttonPlayerStand;
        private Button buttonPlayerDouble;
        private Button buttonPlayerSplit;
    }
}