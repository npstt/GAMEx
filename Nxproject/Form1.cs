using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace Nxproject
{
    public partial class Form1 : Form
    {
        WindowsMediaPlayer GameSong;
        WindowsMediaPlayer Shoot;
        WindowsMediaPlayer Explosion;

        PictureBox[] Stars;
        PictureBox[] Enemis;
        PictureBox[] Munitions;
        PictureBox[] EnemisMunition;

        Random rnd;

        int BackgroundSpeed;
        int MunitionSpeed;
        int PlayerSpeed;
        int EnemiSpeed;
        int EnemisMunitionSpeed;
        int Score;
        int Level;
        int hard;
        bool pause;
        bool gameIsOver;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pause = false;
            gameIsOver = false;
            BackgroundSpeed = 4;
            EnemisMunitionSpeed = 4;
            MunitionSpeed = 20;
            PlayerSpeed = 6;
            EnemiSpeed = 4;
            Score = 0;
            Level = 1;
            hard = 9;
            


            GameSong = new WindowsMediaPlayer();
            Shoot = new WindowsMediaPlayer();
            Explosion = new WindowsMediaPlayer();

            GameSong.URL = "music\\backgroud.mp3";
            Shoot.URL = "music\\shot.mp3";
            Explosion.URL = "music\\Gutak.mp3";

            GameSong.settings.setMode("loop", true);
            GameSong.settings.volume = 17;
            Shoot.settings.volume = 25;
            Explosion.settings.volume = 0;

            Image enemi1 = Image.FromFile("Picx\\E1.png");
            Image enemi2 = Image.FromFile("Picx\\E2.png");
            Image enemi3 = Image.FromFile("Picx\\E3.png");
            Image enemi4 = Image.FromFile("Picx\\E4.png");
            Image boss1 = Image.FromFile("Picx\\Boss1.png");
            Image boss2 = Image.FromFile("Picx\\Boss2.png");
            Image munition = Image.FromFile("Picx\\shoting.jpg");

            Stars = new PictureBox[10];
            Enemis = new PictureBox[10];
            Munitions = new PictureBox[3];
            EnemisMunition = new PictureBox[10];

            rnd = new Random();

            for (int i = 0; i < Munitions.Length;i++)
            {
                Munitions[i] = new PictureBox();
            }


            Stars = new PictureBox[10];
            rnd = new Random();
            for (int i = 0; i < Stars.Length; i++)
            {
                Stars[i] = new PictureBox();
                Stars[i].BorderStyle = BorderStyle.None;
                Stars[i].Location = new Point(rnd.Next(20, 580), rnd.Next(-10, 400));
                if (i % 2 == 1)
                {
                    Stars[i].Size = new Size(2, 2);
                    Stars[i].BackColor = Color.Wheat;
                }
                else
                {
                    Stars[i].Size = new Size(3, 3);
                    Stars[i].BackColor = Color.DarkGray;
                }
                this.Controls.Add(Stars[i]);
            }

            for (int i = 0; i < Enemis.Length; i++)
            {
                Enemis[i] = new PictureBox();
                Enemis[i].Size = new Size(40, 40);
                Enemis[i].SizeMode = PictureBoxSizeMode.Zoom;
                Enemis[i].BorderStyle = BorderStyle.None;
                Enemis[i].Visible = false;
                this.Controls.Add(Enemis[i]);
                Enemis[i].Location = new Point((i + 1) * 50, -50);
            }

            Enemis[0].Image = boss1;
            Enemis[1].Image = enemi2;
            Enemis[2].Image = enemi3;
            Enemis[3].Image = enemi4;
            Enemis[4].Image = enemi1;
            Enemis[5].Image = enemi3;
            Enemis[6].Image = enemi2;
            Enemis[7].Image = enemi3;
            Enemis[8].Image = enemi2;
            Enemis[9].Image = boss2;

            for (int i = 0; i < EnemisMunition.Length; i++)
            {
                EnemisMunition[i] = new PictureBox();
                EnemisMunition[i].Size = new Size(2, 25);
                EnemisMunition[i].Visible = false;
                EnemisMunition[i].BackColor = Color.Yellow;
                int x = rnd.Next(0, 10);
                EnemisMunition[i].Location = new Point(Enemis[x].Location.X, Enemis[x].Location.Y - 20);
                this.Controls.Add(EnemisMunition[i]);
            }

            for (int i = 0; i < Munitions.Length; i++)
            {
                Munitions[i] = new PictureBox();
                Munitions[i].Size = new Size(8, 8);
                Munitions[i].Image = munition;
                Munitions[i].SizeMode = PictureBoxSizeMode.Zoom;
                Munitions[i].BorderStyle = BorderStyle.None;
                this.Controls.Add(Munitions[i]);
            }
            GameSong.controls.play();

        }

        private void MoveBgTimer_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < Stars.Length / 2; i++)
            {
                Stars[i].Top += BackgroundSpeed;
                if (Stars[i].Top >= this.Height)
                {
                    Stars[i].Top = -Stars[i].Height;
                }
            }

            for (int i = Stars.Length / 2; i < Stars.Length; i++)
            {
                Stars[i].Top += BackgroundSpeed - 2;
                if (Stars[i].Top >= this.Height)
                {
                    Stars[i].Top = -Stars[i].Height;
                }
            }
        } //make

        private void LeftMoveTimer_Tick(object sender, EventArgs e)
        {
            if (Player.Left > 10)
            {
                Player.Left -= PlayerSpeed;
            }
        }//make

        private void RightMoveTimer_Tick(object sender, EventArgs e)
        {
            if (Player.Left < 500)
            {
                Player.Left += PlayerSpeed;
            }
        }//make

        private void DownMoveTimer_Tick(object sender, EventArgs e)
        {
            if (Player.Top < 400)
            {
                Player.Top += PlayerSpeed;
            }
        }//make

        private void UpmoveTimer_Tick(object sender, EventArgs e)
        {
            if (Player.Top > 10)
            {
                Player.Top -= PlayerSpeed;
            }
        }//make

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (!pause)
            {
                if (e.KeyCode == Keys.Right)
                {
                    RightMoveTimer.Start();
                }
                if (e.KeyCode == Keys.Left)
                {
                    LeftMoveTimer.Start();
                }
                if (e.KeyCode == Keys.Down)
                {
                    DownMoveTimer.Start();
                }
                if (e.KeyCode == Keys.Up)
                {
                    UpMoveTimer.Start();
                }
            }
        }//make

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            RightMoveTimer.Stop();
            LeftMoveTimer.Stop();
            DownMoveTimer.Stop();
            UpMoveTimer.Stop();

            if (e.KeyCode == Keys.Space)
            {
                if (!gameIsOver)
                {
                    if (pause)
                    {
                        StartTimers();
                        Text_lbl.Visible = false;
                        GameSong.controls.play(); ;
                        pause = false;
                    }
                    else
                    {
                        Text_lbl.Location = new Point(20, 150);
                        Text_lbl.Text = "พักหายใจ";
                        Text_lbl.Visible = true;
                        GameSong.controls.pause();
                        StopTimers();
                        pause = true;
                    }
                }
            }
        }

        private void StartTimers()
        {
            MoveBgTimer.Start();
            MoveEnemisTimer.Start();
            MoveMunitionTimer.Start();
            EnemisMunitionTimer.Start();
        }

        private void MoveMunitionTion_Tick(object sender, EventArgs e)
        {
            Shoot.controls.play();

            for (int i = 0; i < Munitions.Length; i++)
            {
                if (Munitions[i].Top > 0)
                {
                    Munitions[i].Visible = true;
                    Munitions[i].Top -= MunitionSpeed;

                    Collision();
                }
                else
                {
                    Munitions[i].Visible = false;
                    Munitions[i].Location = new Point(Player.Location.X + 20, Player.Location.Y - i * 30);
                }
            }
        }

        private void Collision()
        {
            for (int i = 0; i < Enemis.Length; i++)
            {
                if (Munitions[0].Bounds.IntersectsWith(Enemis[i].Bounds) || Munitions[1].Bounds.IntersectsWith(Enemis[i].Bounds) || Munitions[2].Bounds.IntersectsWith(Enemis[i].Bounds))
                {
                    Explosion.controls.play();
                    Score += 1;
                    scorelbl.Text = (Score < 10) ? "0" + Score.ToString() : Score.ToString();

                    if (Score % 30 == 0)
                    {
                        Level += 1;
                        levelbl.Text = (Level < 10) ? "0" + Level.ToString() : Level.ToString();

                        if (EnemiSpeed <= 10 && EnemisMunitionSpeed <= 10 && hard >= 0)
                        {
                            hard--;
                            EnemiSpeed++;
                            EnemisMunitionSpeed++;
                        }

                        if (Level == 10)
                        {
                            GameOver("สุดยอดมาก คุณปราบเหล่าเหล่าร้ายได้แล้วสินะ");
                        }
                    }
                    Enemis[i].Location = new Point((i + 1) * 50, -100);
                }
                if (Player.Bounds.IntersectsWith(Enemis[i].Bounds))
                {
                    Explosion.settings.volume = 30;
                    Explosion.controls.play();
                    Player.Visible = false;
                    GameOver("GAME OVER");
                }
            }
        }

        private void EnemisMunitionTimer_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < (EnemisMunition.Length - hard); i++)
            {
                if (EnemisMunition[i].Top < this.Height)
                {
                    EnemisMunition[i].Visible = true;
                    EnemisMunition[i].Top += EnemisMunitionSpeed;
                    CollisionWithEnemisMunition();
                }
                else
                {
                    EnemisMunition[i].Visible = false;
                    int x = rnd.Next(0, 10);
                    EnemisMunition[i].Location = new Point(Enemis[x].Location.X + 20, Enemis[x].Location.Y + 30);
                }
            }
        }

        private void CollisionWithEnemisMunition()
        {
            for (int i = 0; i < EnemisMunition.Length; i++)
            {
                if (EnemisMunition[i].Bounds.IntersectsWith(Player.Bounds))
                {
                    EnemisMunition[i].Visible = false;
                    Explosion.settings.volume = 30;
                    Explosion.controls.play();
                    Player.Visible = false;
                    GameOver("Game Over");
                }
            }
        }

        private void GameOver(string str)
        {
            GameSong.controls.stop();
            Text_lbl.Location = new Point(60, 110);
            Text_lbl.Text = str.Trim();
            Text_lbl.Visible = true;
            gameIsOver = true;
            pause = true;
            ReplayBtn.Text = "Replay";
            ReplayBtn.Visible = true;
            QuitBtn.Visible = true;
            StopTimers();
            GameSong.settings.volume = 0;
            Shoot.settings.volume = 0;
        }

        private void StopTimers()
        {
            MoveBgTimer.Stop();
            MoveEnemisTimer.Stop();
            MoveMunitionTimer.Stop();
            EnemisMunitionTimer.Stop();
        }

        private void MoveEnemisTimer_Tick(object sender, EventArgs e)
        {
            MoveEnemisVertical(Enemis, EnemiSpeed);
        }

        private void MoveEnemisVertical(PictureBox[] enemis, int Speed)
        {
            for (int i = 0; i < enemis.Length; i++)
            {
                enemis[i].Visible = true;
                enemis[i].Top += Speed;

                if(enemis[i].Top > this.Height)
                {
                    Enemis[i].Location = new Point((i + 1) * 50, -200);
                }
            }
        }

        private void ReplayBtn_Click(object sender, EventArgs e)
        {
            Application.Restart();
            //Environment.Exit(0);
        }

        private void QuitBtn_Click(object sender, EventArgs e)
        {
            Environment.Exit(1);
        }
    }
}
