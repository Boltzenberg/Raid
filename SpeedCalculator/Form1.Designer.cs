namespace SpeedCalculator
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.tbBase = new System.Windows.Forms.TextBox();
            this.tbWeapon = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbHelmet = new System.Windows.Forms.TextBox();
            this.tbGloves = new System.Windows.Forms.TextBox();
            this.tbChest = new System.Windows.Forms.TextBox();
            this.tbBoots = new System.Windows.Forms.TextBox();
            this.tbAmulet = new System.Windows.Forms.TextBox();
            this.tbBanner = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tbUI = new System.Windows.Forms.TextBox();
            this.tbEffective = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tbSpeedSets = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tbShield = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 23);
            this.label1.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Base Speed:";
            // 
            // tbBase
            // 
            this.tbBase.Location = new System.Drawing.Point(105, 20);
            this.tbBase.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.tbBase.Name = "tbBase";
            this.tbBase.Size = new System.Drawing.Size(50, 20);
            this.tbBase.TabIndex = 1;
            this.tbBase.Text = "0";
            this.tbBase.TextChanged += new System.EventHandler(this.OnUpdateSpeed);
            // 
            // tbWeapon
            // 
            this.tbWeapon.Location = new System.Drawing.Point(105, 42);
            this.tbWeapon.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.tbWeapon.Name = "tbWeapon";
            this.tbWeapon.Size = new System.Drawing.Size(50, 20);
            this.tbWeapon.TabIndex = 2;
            this.tbWeapon.Text = "0";
            this.tbWeapon.TextChanged += new System.EventHandler(this.OnUpdateSpeed);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 45);
            this.label2.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Weapon Speed:";
            // 
            // tbHelmet
            // 
            this.tbHelmet.Location = new System.Drawing.Point(105, 64);
            this.tbHelmet.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.tbHelmet.Name = "tbHelmet";
            this.tbHelmet.Size = new System.Drawing.Size(50, 20);
            this.tbHelmet.TabIndex = 4;
            this.tbHelmet.Text = "0";
            this.tbHelmet.TextChanged += new System.EventHandler(this.OnUpdateSpeed);
            // 
            // tbGloves
            // 
            this.tbGloves.Location = new System.Drawing.Point(105, 108);
            this.tbGloves.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.tbGloves.Name = "tbGloves";
            this.tbGloves.Size = new System.Drawing.Size(50, 20);
            this.tbGloves.TabIndex = 6;
            this.tbGloves.Text = "0";
            this.tbGloves.TextChanged += new System.EventHandler(this.OnUpdateSpeed);
            // 
            // tbChest
            // 
            this.tbChest.Location = new System.Drawing.Point(105, 130);
            this.tbChest.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.tbChest.Name = "tbChest";
            this.tbChest.Size = new System.Drawing.Size(50, 20);
            this.tbChest.TabIndex = 7;
            this.tbChest.Text = "0";
            this.tbChest.TextChanged += new System.EventHandler(this.OnUpdateSpeed);
            // 
            // tbBoots
            // 
            this.tbBoots.Location = new System.Drawing.Point(105, 152);
            this.tbBoots.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.tbBoots.Name = "tbBoots";
            this.tbBoots.Size = new System.Drawing.Size(50, 20);
            this.tbBoots.TabIndex = 8;
            this.tbBoots.Text = "0";
            this.tbBoots.TextChanged += new System.EventHandler(this.OnUpdateSpeed);
            // 
            // tbAmulet
            // 
            this.tbAmulet.Location = new System.Drawing.Point(105, 174);
            this.tbAmulet.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.tbAmulet.Name = "tbAmulet";
            this.tbAmulet.Size = new System.Drawing.Size(50, 20);
            this.tbAmulet.TabIndex = 9;
            this.tbAmulet.Text = "0";
            this.tbAmulet.TextChanged += new System.EventHandler(this.OnUpdateSpeed);
            // 
            // tbBanner
            // 
            this.tbBanner.Location = new System.Drawing.Point(105, 196);
            this.tbBanner.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.tbBanner.Name = "tbBanner";
            this.tbBanner.Size = new System.Drawing.Size(50, 20);
            this.tbBanner.TabIndex = 10;
            this.tbBanner.Text = "0";
            this.tbBanner.TextChanged += new System.EventHandler(this.OnUpdateSpeed);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 67);
            this.label3.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Helmet Speed:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 111);
            this.label5.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Gloves Speed:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 133);
            this.label6.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Chest Speed:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 155);
            this.label7.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Boots Speed:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(11, 177);
            this.label8.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(76, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "Amulet Speed:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 199);
            this.label9.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(78, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "Banner Speed:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(21, 255);
            this.label10.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(55, 13);
            this.label10.TabIndex = 18;
            this.label10.Text = "UI Speed:";
            // 
            // tbUI
            // 
            this.tbUI.Location = new System.Drawing.Point(105, 254);
            this.tbUI.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.tbUI.Name = "tbUI";
            this.tbUI.ReadOnly = true;
            this.tbUI.Size = new System.Drawing.Size(50, 20);
            this.tbUI.TabIndex = 19;
            // 
            // tbEffective
            // 
            this.tbEffective.Location = new System.Drawing.Point(105, 278);
            this.tbEffective.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.tbEffective.Name = "tbEffective";
            this.tbEffective.ReadOnly = true;
            this.tbEffective.Size = new System.Drawing.Size(50, 20);
            this.tbEffective.TabIndex = 20;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 280);
            this.label11.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(86, 13);
            this.label11.TabIndex = 21;
            this.label11.Text = "Effective Speed:";
            // 
            // tbSpeedSets
            // 
            this.tbSpeedSets.Location = new System.Drawing.Point(105, 218);
            this.tbSpeedSets.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.tbSpeedSets.Name = "tbSpeedSets";
            this.tbSpeedSets.Size = new System.Drawing.Size(50, 20);
            this.tbSpeedSets.TabIndex = 11;
            this.tbSpeedSets.Text = "0";
            this.tbSpeedSets.TextChanged += new System.EventHandler(this.OnUpdateSpeed);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(11, 221);
            this.label12.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(65, 13);
            this.label12.TabIndex = 23;
            this.label12.Text = "Speed Sets:";
            // 
            // tbShield
            // 
            this.tbShield.Location = new System.Drawing.Point(105, 86);
            this.tbShield.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.tbShield.Name = "tbShield";
            this.tbShield.Size = new System.Drawing.Size(50, 20);
            this.tbShield.TabIndex = 5;
            this.tbShield.Text = "0";
            this.tbShield.TextChanged += new System.EventHandler(this.OnUpdateSpeed);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 89);
            this.label4.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Shield Speed:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(233, 327);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.tbSpeedSets);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.tbEffective);
            this.Controls.Add(this.tbUI);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbBanner);
            this.Controls.Add(this.tbAmulet);
            this.Controls.Add(this.tbBoots);
            this.Controls.Add(this.tbChest);
            this.Controls.Add(this.tbGloves);
            this.Controls.Add(this.tbShield);
            this.Controls.Add(this.tbHelmet);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbWeapon);
            this.Controls.Add(this.tbBase);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.Name = "Form1";
            this.Text = "Speed Calculator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbBase;
        private System.Windows.Forms.TextBox tbWeapon;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbHelmet;
        private System.Windows.Forms.TextBox tbGloves;
        private System.Windows.Forms.TextBox tbChest;
        private System.Windows.Forms.TextBox tbBoots;
        private System.Windows.Forms.TextBox tbAmulet;
        private System.Windows.Forms.TextBox tbBanner;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbUI;
        private System.Windows.Forms.TextBox tbEffective;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbSpeedSets;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbShield;
        private System.Windows.Forms.Label label4;
    }
}

