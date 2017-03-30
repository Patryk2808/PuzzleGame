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
    public partial class Form1 : Form
    {
        private GameHandler game;


        public Form1()
        {
            InitializeComponent();
            InitGame();
        }


        private void InitGame()
        {
            GameField[,] gameFields = new GameField[4,4];
            gameFields[0, 0] = gameField1;
            gameFields[0, 1] = gameField2;
            gameFields[0, 2] = gameField3;
            gameFields[0, 3] = gameField4;
            gameFields[1, 0] = gameField5;
            gameFields[1, 1] = gameField6;
            gameFields[1, 2] = gameField7;
            gameFields[1, 3] = gameField8;
            gameFields[2, 0] = gameField9;
            gameFields[2, 1] = gameField10;
            gameFields[2, 2] = gameField11;
            gameFields[2, 3] = gameField12;
            gameFields[3, 0] = gameField13;
            gameFields[3, 1] = gameField14;
            gameFields[3, 2] = gameField15;
            gameFields[3, 3] = gameField16;

            game = new GameHandler(gameFields);
            game.FieldChanged += Game_FieldChanged;
            game.ScoreChanged += Game_ScoreChanged;
            game.LifeChanged += Game_LifeChanged;
            game.ProgressChanged += Game_ProgressChanged;
        }

        private void Game_ProgressChanged(int value, int max)
        {
            toolStripProgressBar1.Maximum = max;
            toolStripProgressBar1.Value = value;
        }
        private void Game_LifeChanged(int lifes)
        {
            lifeLabel.Text = lifes.ToString();
        }
        private void Game_ScoreChanged(int score)
        {
            scoreLabel.Text = score.ToString();
        }
        private void Game_FieldChanged(int[,] blackFields)
        {
            label1.Text = blackFields[0, 0].ToString();
            label2.Text = blackFields[0, 1].ToString();
            label3.Text = blackFields[0, 2].ToString();
            label4.Text = blackFields[0, 3].ToString();

            label5.Text = blackFields[1, 0].ToString();
            label6.Text = blackFields[1, 1].ToString();
            label7.Text = blackFields[1, 2].ToString();
            label8.Text = blackFields[1, 3].ToString();
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            game.NewGame();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Exit", MessageBoxButtons.YesNo) == DialogResult.No)
                e.Cancel = true;
        }
        private void gameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editToolStripMenuItem.Checked = false;
            gameToolStripMenuItem.Checked = true;

            newGameToolStripMenuItem.Enabled = true;
            settingsToolStripMenuItem.Enabled = true;
            openToolStripMenuItem.Enabled = true;
            saveToolStripMenuItem.Enabled = false;

            game.GameModeChange(false);
            menuStrip1.BackColor = SystemColors.Control;
        }
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editToolStripMenuItem.Checked = true;
            gameToolStripMenuItem.Checked = false;

            newGameToolStripMenuItem.Enabled = false;
            settingsToolStripMenuItem.Enabled = false;
            openToolStripMenuItem.Enabled = false;
            saveToolStripMenuItem.Enabled = true;

            game.GameModeChange(true);
            menuStrip1.BackColor = Color.Yellow;

            label1.Text = "";
            label2.Text = "";
            label3.Text = "";
            label4.Text = "";

            label5.Text = "";
            label6.Text = "";
            label7.Text = "";
            label8.Text = "";
        }
        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
           game.ChangeSettings();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            game.Load();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            game.Save();
        }
    }

}
