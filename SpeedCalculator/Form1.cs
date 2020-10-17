using RaidLib.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpeedCalculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.UpdateSpeeds();
        }

        private void OnUpdateSpeed(object sender, EventArgs e)
        {
            this.UpdateSpeeds();
        }

        private void ExtractSpeed(TextBox tb, ref int val)
        {
            int v;
            if (Int32.TryParse(tb.Text.Trim(), out v))
            {
                val = v;
                tb.ForeColor = Color.Black;
            }
            else
            {
                tb.ForeColor = Color.Red;
            }
        }

        private void UpdateSpeeds()
        {
            int baseSpeed = 0;
            int weaponSpeed = 0;
            int helmetSpeed = 0; 
            int shieldSpeed = 0; 
            int glovesSpeed = 0; 
            int chestSpeed = 0; 
            int bootsSpeed = 0; 
            int amuletSpeed = 0;
            int bannerSpeed = 0;

            this.ExtractSpeed(this.tbBase, ref baseSpeed);
            this.ExtractSpeed(this.tbWeapon, ref weaponSpeed);
            this.ExtractSpeed(this.tbHelmet, ref helmetSpeed);
            this.ExtractSpeed(this.tbShield, ref shieldSpeed);
            this.ExtractSpeed(this.tbGloves, ref glovesSpeed);
            this.ExtractSpeed(this.tbChest, ref chestSpeed);
            this.ExtractSpeed(this.tbBoots, ref bootsSpeed);
            this.ExtractSpeed(this.tbAmulet, ref amuletSpeed);
            this.ExtractSpeed(this.tbBanner, ref bannerSpeed);

            int speedSets = 0;
            this.ExtractSpeed(this.tbSpeedSets, ref speedSets);

            int artifactSpeed = weaponSpeed + helmetSpeed + shieldSpeed + glovesSpeed + chestSpeed + bootsSpeed + amuletSpeed + bannerSpeed;
            double speedSetBoost = baseSpeed * speedSets * Constants.SetBonus.Speed;

            this.tbUI.Text = (baseSpeed + artifactSpeed + (int)Math.Round(speedSetBoost)).ToString();
            this.tbEffective.Text = ((double)baseSpeed + (double)artifactSpeed + speedSetBoost).ToString();
        }
    }
}
