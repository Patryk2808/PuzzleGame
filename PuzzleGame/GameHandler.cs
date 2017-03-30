using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuzzleGame
{
    public class GameHandler
    {
        public enum GameMode {Locked, GamePlaying, Edit}

        private int _maxLifes = 3;
        private int _maxTime = 10;
        private int _score = 0;
        private int _lifes = 3;
        private int _timeLeft = 10;
        private int _fieldsToWin;
        private GameMode _gameMode;
        private GameField[,] _gameFields;
        private Timer _timer;

        public delegate void LifeEventHandler(int lifes);
        public delegate void ScoreEventHandler(int score);
        public delegate void ProgressEventHandler(int value, int max);
        public delegate void GameFieldEventHandler(int[,] blackFields);

        public event ScoreEventHandler ScoreChanged;
        public event ProgressEventHandler ProgressChanged;
        public event LifeEventHandler LifeChanged;
        public event GameFieldEventHandler FieldChanged;

        public GameHandler(GameField[,] gameFields)
        {
            _gameMode = GameMode.Locked;
            this._gameFields = gameFields;
            _timer = new Timer();
            _timer.Interval = 1000;
            _timer.Tick += _timer_Tick;
            _timer.Start();

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    gameFields[i, j].LeftButtonClick += gameField_LeftButtonClick;
                    gameFields[i, j].RightButtonClick += gameField_RightButtonClick;
                }
            }
        }
        public void ChangeSettings()
        {
            _gameMode = GameMode.Locked;
            SettingsForm fm = new SettingsForm(_maxTime, _maxLifes);
            DialogResult result = fm.ShowDialog();
            if (result == DialogResult.OK)
            {
                _maxLifes = fm.Lifes;
                _maxTime = fm.Time;
                _lifes = _maxLifes;
                ProgressChanged?.Invoke(_maxTime, _maxTime);
                LifeChanged?.Invoke(_lifes);
            }
        }
        public void NewGame(bool resetScore = true, bool resetFields = true)
        {
            if (resetScore)
            {
                _score = 0;
                ScoreChanged?.Invoke(_score);
            }

            int[,] blackField = new int[2, 4];
            _gameMode = GameMode.GamePlaying;
            _timeLeft = _maxTime;
            _lifes = _maxLifes;
            _fieldsToWin = 0;
            Random rnd = new Random();

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (resetFields)
                    {
                        _gameFields[i, j].Reset();
                        bool isBlack = rnd.Next(2) == 0;
                        _gameFields[i, j].IsSetToBeBlack = isBlack;
                        if (isBlack)
                        {
                            blackField[0, i]++;
                            blackField[1, j]++;
                            _fieldsToWin++;
                        }
                    }
                    else
                    {
                        if (_gameFields[i, j].IsSetToBeBlack)
                        {
                            blackField[0, i]++;
                            blackField[1, j]++;
                            _fieldsToWin++;
                        }
                    }
                }
            }
            FieldChanged?.Invoke(blackField);
            LifeChanged?.Invoke(_lifes);
            ProgressChanged?.Invoke(_timeLeft, _maxTime);
        }
        public void Load()
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Puzle Game File|*.pg";
            file.ShowDialog();
            if (file.FileName != "" && File.Exists(file.FileName))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream fs = new FileStream(file.FileName, FileMode.Open);
                bool[,] fields = (bool[,])bf.Deserialize(fs);
                fs.Close();
                forAllFields(x => x.Reset());
                for(int j=0; j<4; j++)
                for (int i = 0; i < 4; i++)
                    _gameFields[j, i].IsSetToBeBlack = fields[j, i];
                NewGame(true, false);
            }

        }
        public void Save()
        {
            SaveFileDialog file = new SaveFileDialog();
            file.Filter = "Puzzle Game File|*.pg";
            file.ShowDialog();
            if (file.FileName != "")
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream fs = new FileStream(file.FileName, FileMode.Create);
                bool[,] fields = new bool[4,4];
                for (int j = 0; j < 4; j++)
                    for (int i = 0; i < 4; i++)
                        fields[j, i] = _gameFields[j, i].IsSetToBeBlack;

                bf.Serialize(fs, fields);
                fs.Close();

            }

        }

        public void GameModeChange(bool isEdit = false)
        {
            if (isEdit)
            {
                forAllFields(x =>
                {
                    x.IsSetToBeBlack = false;
                    x.FieldColor= Color.White;
                    x.Text = "";
                });
                _gameMode = GameMode.Edit;
            }
            else
            {
                forAllFields(x =>
                {
                    x.Clear();
                });

                NewGame(true, false);
                _gameMode = GameMode.GamePlaying;
            }
        }
        private void _timer_Tick(object sender, EventArgs e)
        {
            if (_gameMode != GameMode.GamePlaying)
                return;

            if (--_timeLeft < 0)
                loseGame();
            else
            {
                ProgressChanged?.Invoke(_timeLeft, _maxTime);
            }
        }
        private void gameField_LeftButtonClick(object sender, EventArgs e)
        {
            GameField gf = (sender as GameField);
            switch (_gameMode)
            {
                case GameMode.Locked:
                    return;
                case GameMode.Edit:
                    editMode_leftClicked(gf);
                    break;
                case GameMode.GamePlaying:
                    gameMode_leftClicked(gf);
                    break;
            }

        }

        private void editMode_leftClicked(GameField gf)
        {
            gf.IsSetToBeBlack = true;
            gf.FieldColor = Color.Black;
        }

        private void gameField_RightButtonClick(object sender, EventArgs e)
        {
            GameField gf = (sender as GameField);
            switch (_gameMode)
            {
                case GameMode.Locked:
                    return;
                case GameMode.Edit:
                    editMode_rightClicked(gf);
                    break;
                case GameMode.GamePlaying:
                    gameMode_rightClicked(gf);
                    break;
            }
        }

        private void editMode_rightClicked(GameField gf)
        {
            gf.IsSetToBeBlack = false;
            gf.FieldColor = Color.White; 
        }

        private void gameMode_rightClicked(GameField gf)
        {
            
            if (gf.FieldColor == Color.Black)
                return;

            gf.Text = "";
            gf.FieldColor = Color.White;
        }

        private void gameMode_leftClicked(GameField gf)
        {
            if (gf.IsSetToBeBlack)
            {
                _fieldsToWin--;
                gf.FieldColor = Color.Black;
                _score += 50;
                if (_fieldsToWin <= 0)
                {
                    _score += 500;
                    NewGame(false);
                }
                ScoreChanged?.Invoke(_score);

            }
            else
            {
                _lifes--;
                LifeChanged?.Invoke(_lifes);
                gf.SetRedWithTimer();
                if (_lifes <= 0)
                {
                    loseGame();
                }
            }
        }
        
        private void loseGame()
        {
            _gameMode = GameMode.Locked;
            MessageBox.Show($"Your final score is: {_score}", "Congratulations");
        }
        private void forAllFields(Action<GameField> task)
        {
            for(int j=0; j<4; j++)
            for (int i = 0; i < 4; i++)
            {
                task(_gameFields[j, i]);
            }
        }
        
    }
}
