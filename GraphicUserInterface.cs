using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Media;
using System.Runtime.InteropServices;

namespace MyTetris
{
    class GraphicUserInterface
    {
        public enum MODE
        {
            INITIALIZE,
            PLAY,
            PAUSE,
            GAMEOVER
        }

        public Bitmap PlayButton;
        public Bitmap PlayButtonOnMouse;
        public Bitmap PlayButtonImage;

        public Bitmap QuitButton;
        public Bitmap QuitButtonOnMouse;
        public Bitmap QuitButtonImage;

        public Bitmap PauseButton;
        public Bitmap PauseButtonOnMouse;
        public Bitmap PauseButtonImage;

        public Bitmap PauseBoard;

        public Bitmap ResumeButton;
        public Bitmap ResumeButtonOnMouse;
        public Bitmap ResumeButtonImage;

        public Bitmap PauseQuitButton;
        public Bitmap PauseQuitButtonOnMouse;
        public Bitmap PauseQuitButtonImage;

        public Bitmap GameOverBoard;
        public Bitmap HighScoreBoard;

        public Bitmap RetryButton;
        public Bitmap RetryButtonOnMouse;
        public Bitmap RetryButtonImage;

        public Bitmap HomeButton;
        public Bitmap HomeButtonOnMouse;
        public Bitmap HomeButtonImage;

        public Bitmap OKButton;
        public Bitmap OKButtonOnMouse;
        public Bitmap OKButtonImage;

        private SolidBrush backgroundBrush, boardBrush;

        private SoundPlayer mainTheme;

        [DllImport("winmm.dll", SetLastError = true)]
        private static extern int waveOutSetVolume(IntPtr device, uint volume);


        public bool IsClickOK { get; set; }
        public MODE mode { get; private set; }

        public GraphicUserInterface()
        {
            // 플레이 버튼
            PlayButton = Properties.Resources.PlayButton;
            PlayButtonOnMouse = Properties.Resources.PlayButtonOnMouse;
            // 종료 버튼
            QuitButton = Properties.Resources.QuitButton;
            QuitButtonOnMouse = Properties.Resources.QuitButtonOnMouse;
            // 정지 버튼
            PauseButton = Properties.Resources.PauseButton;
            PauseButtonOnMouse = Properties.Resources.PauseButtonOnMouse;
            // 정지 버튼 누르면 나오는 창
            PauseBoard = Properties.Resources.PauseBoard;
            // 다시 시작 버튼
            ResumeButton = Properties.Resources.ResumeButton;
            ResumeButtonOnMouse = Properties.Resources.ResumeButtonOnMouse;
            // 정지창에 있는 게임 종료 버튼
            PauseQuitButton = Properties.Resources.PauseQuitButton;
            PauseQuitButtonOnMouse = Properties.Resources.PauseQuitButtonOnMouse;
            // 게임 오버 창
            GameOverBoard = Properties.Resources.GameOverBoard;
            // 하이 스코어 창
            HighScoreBoard = Properties.Resources.HighScoreBoard;
            // Retry 버튼
            RetryButton = Properties.Resources.RetryButton;
            RetryButtonOnMouse = Properties.Resources.RetryButtonOnMouse;
            // Home 버튼
            HomeButton = Properties.Resources.HomeButton;
            HomeButtonOnMouse = Properties.Resources.HomeButtonOnMouse;
            // OK 버튼
            OKButton = Properties.Resources.OKButton;
            OKButtonOnMouse = Properties.Resources.OKButtonOnMouse;

            // 배경 투명하게
            PauseBoard.MakeTransparent(PauseBoard.GetPixel(0, 0));
            ResumeButtonOnMouse.MakeTransparent(ResumeButtonOnMouse.GetPixel(0, 0));
            ResumeButton.MakeTransparent(ResumeButton.GetPixel(0, 0));
            PauseQuitButtonOnMouse.MakeTransparent(PauseQuitButtonOnMouse.GetPixel(0, 0));
            PauseQuitButton.MakeTransparent(PauseQuitButton.GetPixel(0, 0));
            GameOverBoard.MakeTransparent(GameOverBoard.GetPixel(0, 0));
            HighScoreBoard.MakeTransparent(HighScoreBoard.GetPixel(0, 0));
            RetryButton.MakeTransparent(RetryButton.GetPixel(0, 0));
            RetryButtonOnMouse.MakeTransparent(RetryButtonOnMouse.GetPixel(0, 0));
            HomeButton.MakeTransparent(HomeButton.GetPixel(0, 0));
            HomeButtonOnMouse.MakeTransparent(HomeButtonOnMouse.GetPixel(0, 0));
            OKButton.MakeTransparent(OKButton.GetPixel(0, 0));
            OKButtonOnMouse.MakeTransparent(OKButtonOnMouse.GetPixel(0, 0));

            // 보여줄 이미지 초기값
            PlayButtonImage = PlayButton;
            QuitButtonImage = QuitButton;
            PauseButtonImage = PauseButton;
            ResumeButtonImage = ResumeButton;
            PauseQuitButtonImage = PauseQuitButton;
            RetryButtonImage = RetryButton;
            HomeButtonImage = HomeButton;
            OKButtonImage = OKButton;

            backgroundBrush = new SolidBrush(Color.FromArgb(55, 55, 55));
            boardBrush = new SolidBrush(Color.FromArgb(35, 35, 35));

            mode = MODE.INITIALIZE;

            IsClickOK = false;

            // bgm
            mainTheme = new SoundPlayer("D:/CS/MyTetris/Sound/mainTheme.wav");
        }

        public void GameOver()
        {
            mode = MODE.GAMEOVER;
        }

        public void IsMouseOnButton(int x, int y, bool isClick, Form1 form, Preview preview, Tetrimino tetrimino, ScoreBoard scoreBoard)
        {
            switch (mode)
            {
                case MODE.INITIALIZE:
                    // PlayButton
                    if (x > Rule.GameBoardStartX + 100 && y > 300 && x < Rule.GameBoardStartX + 100 + PlayButtonImage.Width && y < 300 + PlayButtonImage.Height)
                    {
                        PlayButtonImage = PlayButtonOnMouse;
                        if (isClick)
                        {
                            mode = MODE.PLAY;
                            preview.FixNextPixel(tetrimino);
                            preview.SetNextBlockColor(tetrimino);

                            waveOutSetVolume(IntPtr.Zero, (uint)0x44444444);
                            mainTheme.PlayLooping();
                        }
                    }
                    else
                        PlayButtonImage = PlayButton;
                    // QuitButton
                    if (x > Rule.GameBoardStartX + 100 && y > 400 && x < Rule.GameBoardStartX + 100 + QuitButtonImage.Width && y < 400 + QuitButtonImage.Height)
                    {
                        QuitButtonImage = QuitButtonOnMouse;
                        if (isClick)
                            form.Close();
                    }
                    else
                        QuitButtonImage = QuitButton;
                    break;
                case MODE.PLAY:
                    // PauseButton
                    if (x > 1215 && y > 15 && x < 1215 + PauseButtonImage.Width && y < 15 + PauseButtonImage.Height)
                    {
                        PauseButtonImage = PauseButtonOnMouse;
                        if (isClick)
                        {
                            mode = MODE.PAUSE;
                            mainTheme.Stop();
                        }
                    }
                    else
                        PauseButtonImage = PauseButton;
                    break;
                case MODE.PAUSE:
                    // ResumeButton
                    if (x > Rule.GameBoardStartX + 100 && y > 300 && x < Rule.GameBoardStartX + 100 + ResumeButtonImage.Width && y < 300 + ResumeButtonImage.Height)
                    {
                        ResumeButtonImage = ResumeButtonOnMouse;
                        if (isClick)
                        {
                            mode = MODE.PLAY;
                            mainTheme.PlayLooping();
                        }
                    }
                    else
                        ResumeButtonImage = ResumeButton;
                    // QuitButton (Pause됐을 때 나오는 QuitButton)
                    if (x > Rule.GameBoardStartX + 100 && y > 400 && x < Rule.GameBoardStartX + 100 + PauseQuitButtonImage.Width && y < 400 + PauseQuitButtonImage.Height)
                    {
                        PauseQuitButtonImage = PauseQuitButtonOnMouse;
                        if (isClick)
                        {
                            form.Close();
                            mainTheme.Dispose();
                        }
                    }
                    else
                        PauseQuitButtonImage = PauseQuitButton;
                    break;
                case MODE.GAMEOVER:
                    mainTheme.Stop();
                    // 점수가 5등안에 들었을 때 실행
                    if (scoreBoard.isHigh(preview.GetScore()))
                    {
                        if (x > Rule.GameBoardStartX + HighScoreBoard.Width / 2 - 25 && y > 530 && x < Rule.GameBoardStartX + HighScoreBoard.Width / 2 - 25 + OKButtonImage.Width && y < 530 + OKButtonImage.Height)
                        {
                            OKButtonImage = OKButtonOnMouse;
                            if (isClick)
                                IsClickOK = true;
                        }
                        else
                            OKButtonImage = OKButton;
                    }
                    
                    if (IsClickOK || !scoreBoard.isHigh(preview.GetScore()))
                    {
                        // RetryButton
                        if (x > Rule.GameBoardStartX + GameOverBoard.Width / 2 - 50 && y > 500 && x < Rule.GameBoardStartX + GameOverBoard.Width / 2 - 50 + RetryButtonImage.Width && y < 500 + RetryButtonImage.Height)
                        {
                            RetryButtonImage = RetryButtonOnMouse;
                            if (isClick)
                            {
                                form.ReStart(true);
                                mainTheme.PlayLooping();
                                mode = MODE.PLAY;
                            }
                        }
                        else
                            RetryButtonImage = RetryButton;
                        // HomeButton
                        if (x > Rule.GameBoardStartX + GameOverBoard.Width / 2 + 50 && y > 500 && x < Rule.GameBoardStartX + GameOverBoard.Width / 2 + 50 + HomeButtonImage.Width && y < 500 + HomeButtonImage.Height)
                        {
                            HomeButtonImage = HomeButtonOnMouse;
                            if (isClick)
                            {
                                form.ReStart(false);
                                mode = MODE.INITIALIZE;
                            }
                        }
                        else
                            HomeButtonImage = HomeButton;
                    }
                    break;
            }
        }

        public void Draw(Graphics g)
        {
            // 배경
            g.FillRectangle(backgroundBrush, 0, 0, Rule.FormWidth, Rule.FormHeight);
            g.FillRectangle(boardBrush, Rule.GameBoardStartX, Rule.GameBoardStartY, Rule.PixelCountX * Rule.PixelSizeX, (Rule.PixelCountY - 3) * Rule.PixelSizeY);
        }
    }
}
