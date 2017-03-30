using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuzzleGame
{
    public partial class GameField : UserControl
    {
        public bool IsSetToBeBlack { get; set; }

        public new string Text
        {
            get
            {
                return field.Text;
            }
            set { field.Text = value; }
        }
        public Color FieldColor
        {
            get { return field.BackColor; }
            set { field.BackColor = value; }
        }

        public event EventHandler RightButtonClick;
        public event EventHandler LeftButtonClick;

        public GameField()
        {
            InitializeComponent();
        }

        public void Reset()
        {
            field.Text = "?";
            field.BackColor = Color.RoyalBlue;
            IsSetToBeBlack = false;
        }
        public void Clear()
        {
            FieldColor = Color.RoyalBlue;
            field.Text = "?";
        }
        public void SetRedWithTimer()
        {
            FieldColor = Color.Red;
            field.Text = "";
            Timer tm = new Timer();
            tm.Interval = 500;
            tm.Tick += Tm_Tick;
            tm.Start();
        }

        private void Tm_Tick(object sender, EventArgs e)
        {
            (sender as Timer).Stop();
            Clear();
        }
        private void field_MouseEnter(object sender, EventArgs e)
        {
            if (field.BackColor != Color.RoyalBlue)
                return;

            field.BackColor = Color.Yellow;
            field.Text = "";
        }
        private void field_MouseLeave(object sender, EventArgs e)
        {
            if (field.BackColor != Color.Yellow)
                return;

            Clear();
        }
        private void field_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
                RightButtonClick?.Invoke(this, e);
            if(e.Button == MouseButtons.Left)
                LeftButtonClick?.Invoke(this, e);
        }
    }
}
