using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuzzleGame
{
    public partial class SettingsForm : Form
    {
        public int Lifes { get; set; }
        public int Time { get; set; }

        public SettingsForm(int time, int lifes)
        {
            InitializeComponent();
            numericUpDown1.Value = lifes;
            numericUpDown2.Value = time;
            KeyPreview = true;
        }

        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Lifes = (int)numericUpDown1.Value;
            Time = (int)numericUpDown2.Value;
        }

        private void SettingsForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }

            if (e.KeyCode == Keys.Enter)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
