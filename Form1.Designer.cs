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
            flowLayoutPanel1.Location = new Point(131, 256);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(262, 120);
            flowLayoutPanel1.TabIndex = 3;
            // 
            // flowLayoutPanelDealer
            // 
            flowLayoutPanelDealer.BackColor = Color.Transparent;
            flowLayoutPanelDealer.Location = new Point(449, 382);
            flowLayoutPanelDealer.Name = "flowLayoutPanelDealer";
            flowLayoutPanelDealer.Size = new Size(340, 142);
            flowLayoutPanelDealer.TabIndex = 8;
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.BackColor = Color.Transparent;
            flowLayoutPanel2.Location = new Point(399, 256);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Size = new Size(262, 120);
            flowLayoutPanel2.TabIndex = 4;
            // 
            // flowLayoutPanel3
            // 
            flowLayoutPanel3.BackColor = Color.Transparent;
            flowLayoutPanel3.Location = new Point(667, 256);
            flowLayoutPanel3.Name = "flowLayoutPanel3";
            flowLayoutPanel3.Size = new Size(262, 120);
            flowLayoutPanel3.TabIndex = 5;
            // 
            // flowLayoutPanel4
            // 
            flowLayoutPanel4.BackColor = Color.Transparent;
            flowLayoutPanel4.Location = new Point(935, 256);
            flowLayoutPanel4.Name = "flowLayoutPanel4";
            flowLayoutPanel4.Size = new Size(262, 120);
            flowLayoutPanel4.TabIndex = 6;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.ForestGreen;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(1218, 629);
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
    }
}