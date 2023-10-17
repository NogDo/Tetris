using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.Runtime.InteropServices;

namespace MyTetris
{
    public enum KEY
    {
        LEFT,
        RIGHT
    }

    public partial class Form1 : Form
    {
        // 클래스
        private Board board;
        private Tetrimino tetrimino;
        private Preview preview;
        private GraphicUserInterface gui;
        private ScoreBoard scoreBoard;

        private int tick;
        private int gravitySpeed; // 블럭이 떨어지는 속도
        private int removeCount; // 지워진 줄의 갯수를 저장할 공간
        private int lineCount; // 게임 진행 중 총 지워진 줄의 갯수를 저장할 공간
        private int moveTickCount; // 블럭 이동 틱
        private bool isStore; // 블럭 저장
        private bool press; // 키가 눌려있는지 판단
        private bool isFirst; // 키가 처음눌렸는지 판단
        private bool isPressSapce;
        private string name;
        private StringFormat centerFormat;

        private static Font textFont;
        private static SolidBrush fontBrush;

        public KEY Key { get; private set; }

        public Form1()
        {
            InitializeComponent();
        }

        static Form1()
        {
            textFont = new Font("휴먼둥근헤드라인", 18, FontStyle.Bold);
            fontBrush = new SolidBrush(Color.FromArgb(255, 255, 255));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DoubleBuffered = true; // 더블버퍼링
            this.SetClientSizeCore(Rule.FormWidth, Rule.FormHeight); // 게임 화면 크기

            // 각 클래스들의 생성자
            board = new Board();
            tetrimino = new Tetrimino();
            preview = new Preview();
            gui = new GraphicUserInterface();
            scoreBoard = new ScoreBoard();

            // 각각의 변수들을 초기화 해준다
            tick = 0;
            removeCount = 0;
            lineCount = 0;
            moveTickCount = 0;
            gravitySpeed = preview.LevelUp();
            isStore = false;
            press = false;
            isFirst = true;
            isPressSapce = false;
            name = "";
            centerFormat = new StringFormat() { Alignment = StringAlignment.Center };


            // 다음블럭 3개를 결정
            tetrimino.SetNextBlock();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            gui.Draw(e.Graphics);
            preview.Draw(e.Graphics);
            switch (gui.mode)
            {
                case GraphicUserInterface.MODE.INITIALIZE:
                    e.Graphics.DrawImage(gui.PlayButtonImage, Rule.GameBoardStartX + 100, 300);
                    e.Graphics.DrawImage(gui.QuitButtonImage, Rule.GameBoardStartX + 100, 400);
                    scoreBoard.Draw(e.Graphics);
                    break;
                case GraphicUserInterface.MODE.PLAY:
                    e.Graphics.DrawImage(gui.PauseButtonImage, 1215, 15);
                    tetrimino.Draw(e.Graphics);
                    board.Draw(e.Graphics, tetrimino.GetColorChar());
                    break;
                case GraphicUserInterface.MODE.PAUSE:
                    e.Graphics.DrawImage(gui.PauseButtonImage, 1215, 15);
                    tetrimino.Draw(e.Graphics);
                    board.Draw(e.Graphics, tetrimino.GetColorChar());

                    e.Graphics.DrawImage(gui.PauseBoard, Rule.GameBoardStartX + 25, 150);
                    e.Graphics.DrawImage(gui.ResumeButtonImage, Rule.GameBoardStartX + 100, 300);
                    e.Graphics.DrawImage(gui.PauseQuitButtonImage, Rule.GameBoardStartX + 100, 400);
                    break;
                case GraphicUserInterface.MODE.GAMEOVER:
                    if (scoreBoard.isHigh(preview.GetScore()))
                    {
                        e.Graphics.DrawImage(gui.HighScoreBoard, Rule.GameBoardStartX + 25, 250);
                        e.Graphics.DrawImage(gui.OKButtonImage, Rule.GameBoardStartX + gui.HighScoreBoard.Width / 2 - 25, 530);
                        e.Graphics.DrawString(preview.GetScore().ToString(), textFont, fontBrush, Rule.GameBoardStartX + gui.HighScoreBoard.Width / 2 + 25, 350, centerFormat);
                        e.Graphics.DrawString(name, textFont, fontBrush, Rule.GameBoardStartX + gui.HighScoreBoard.Width / 2 + 25, 475, centerFormat);
                    }

                    if (gui.IsClickOK || !scoreBoard.isHigh(preview.GetScore()))
                    {
                        e.Graphics.DrawImage(gui.GameOverBoard, Rule.GameBoardStartX + 25, 250);
                        e.Graphics.DrawImage(gui.RetryButtonImage, Rule.GameBoardStartX + gui.GameOverBoard.Width / 2 - 50, 500);
                        e.Graphics.DrawImage(gui.HomeButtonImage, Rule.GameBoardStartX + gui.GameOverBoard.Width / 2 + 50, 500);
                    }
                    break;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (gui.mode == GraphicUserInterface.MODE.PLAY) // 게임 플레이 중일 때
            {
                if (!isPressSapce)
                {
                    switch (e.KeyCode)
                    {
                        case Keys.Left: // 왼쪽 방향키
                            press = true;
                            Key = KEY.LEFT;
                            if (isFirst)
                            {
                                KeyPressing();
                                isFirst = false;
                            }
                            break;
                        case Keys.Right: // 오른쪽 방향키
                            press = true;
                            Key = KEY.RIGHT;
                            if (isFirst)
                            {
                                KeyPressing();
                                isFirst = false;
                            }
                            break;
                        case Keys.Up: // 윗 방향키
                            if (board.Rotatable(tetrimino))
                            {
                                board.ResetDownPreviewBoard();
                                tetrimino.BlockTurn();
                            }
                            break;
                        case Keys.Down: // 아래 방향키
                            if (board.MovableDown(tetrimino))
                            {
                                tetrimino.MoveDown();
                                preview.AddScore();
                            }
                            break;
                        case Keys.Space: // 스페이스바
                            isPressSapce = true; // 스페이스바 누르고 바로 방향키누르면 블럭이 움직이는 현상이 발생. 그 현상을 방지하고자 변수하나를 만들어서
                            // 블럭이 다 내려가서 안착할 때 까지 움직일 수 없음
                            while (board.MovableDown(tetrimino))
                            {
                                tetrimino.MoveDown();
                                preview.AddScore();
                            }
                            break;
                        case Keys.ShiftKey: // 쉬프트
                            if (preview.isEmpty(tetrimino)) // 저장블럭이 없다면
                            {
                                board.ResetDownPreviewBoard(); // downPreviewBoard의 요소값들을 전부 0으로 초기화
                                                               // 저장될 블럭의 색과 모양을 지정해주고 화면에 보여주기 위해 배열의 요소값들을 바꿔준다.
                                preview.SetStoreBlockColor(tetrimino);
                                tetrimino.Mino_To_Store();
                                preview.FixStorePixel(tetrimino);
                                // 다음 나올 블럭들을 당기고 화면에 보여주기 위해 값을 바꿔 준다
                                tetrimino.PullNextBlock();
                                preview.FixNextPixel(tetrimino);
                                preview.SetNextBlockColor(tetrimino);

                                tetrimino.ResetXY(); // 플레이 될 블럭의 x, y좌표를 초기화 시킴
                            }
                            else // 저장블럭이 이미 있는 상태라면
                            {
                                if (isStore == false) // 이미 저장을 했다면 블럭이 바뀌기 전까지는 저장할 수 없다.
                                {
                                    board.ResetDownPreviewBoard(); // downPreviewBoard의 요소값들을 전부 0으로 초기화
                                                                   // 저장될 블럭의 색을 지정해주고, 저장 블럭과 현재 플레이 되는 블럭을 바꾸고 화면에 출력
                                    preview.SetStoreBlockColor(tetrimino);
                                    tetrimino.Change();
                                    preview.FixStorePixel(tetrimino);

                                    tetrimino.ResetXY(); // 플레이 될 블럭의 x, y좌표를 초기화 시킴
                                    Invalidate(); // 화면 갱신
                                    isStore = true; // 저장은 한번만 가능
                                }
                            }
                            break;
                    }
                }
            }
            else if (gui.mode == GraphicUserInterface.MODE.GAMEOVER) // 기록을 새웠을 때
            {
                if (e.KeyCode == Keys.Back)
                {
                    if (!name.Equals(""))
                        name = name.Substring(0, name.Length - 1);
                }
                else if ((char)e.KeyCode >= 'A' && (char)e.KeyCode <= 'Z')
                    if (name.Length < 3)
                        name += e.KeyCode;

                Invalidate();
            }

            if (e.KeyCode == Keys.Tab)
            {
                scoreBoard.ResetList();
                Invalidate();
                MessageBox.Show("리스트를 리셋 했습니다");
            }

        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (gui.mode == GraphicUserInterface.MODE.PLAY)
                switch (e.KeyCode)
                {
                    case Keys.Left: // 왼쪽 방향키
                        press = false;
                        isFirst = true;
                        moveTickCount = 0;
                        break;
                    case Keys.Right: // 오른쪽 방향키
                        press = false;
                        isFirst = true;
                        moveTickCount = 0;
                        break;
                }
        }

        private void KeyPressing()
        {
            if (press)
            {
                switch (Key)
                {
                    case KEY.LEFT:
                        if (board.MovalbleLeft(tetrimino))
                            tetrimino.MoveLeft();
                        board.ResetDownPreviewBoard();
                        break;
                    case KEY.RIGHT:
                        if (board.MovableRight(tetrimino))
                            tetrimino.MoveRight();
                        board.ResetDownPreviewBoard();
                        break;
                }
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            gui.IsMouseOnButton(e.X, e.Y, false, this, preview, tetrimino, scoreBoard);
            Invalidate();
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            gui.IsMouseOnButton(e.X, e.Y, true, this, preview, tetrimino, scoreBoard);
            Invalidate();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (gui.mode == GraphicUserInterface.MODE.PLAY)
            {
                if (!isFirst)
                    if (++moveTickCount % 8 == 0)
                    {
                        KeyPressing();
                        moveTickCount = 0;
                    }

                if (++tick >= 30 - gravitySpeed) // 아래로 내려가는 속도
                {
                    if (board.MovableDown(tetrimino)) // 아래로 이동할 수 있는지 판단
                    {
                        tetrimino.MoveDown();
                    }
                    else // 더이상 아래로 내려갈 수 없을 때
                    {
                        if (board.IsGameOver())
                        {
                            gui.GameOver();
                        }
                        else
                        {
                            gravitySpeed = preview.LevelUp(); // 레벨이 올라갈 수록 속도가 빨라진다.
                            board.FixMino(tetrimino); // 현재 플레이되는 블럭을 gameBoard에 저장한다.
                            // 만약 그 과정에서 채워진 줄이 있다면 줄을 지우고, 현재 지워진 줄의 갯수를 반환하고, 줄의 갯수에 따라 콤보 점수를 반환한다
                            for (int i = 0; i < 4; i++)
                                removeCount += board.RemoveLine();
                            lineCount += removeCount;
                            preview.LineCount(lineCount);
                            preview.AddComboScore(removeCount);
                            // 다음 나올 블럭들을 당기고 화면에 보여주기 위해 값을 바꿔 준다
                            tetrimino.PullNextBlock();
                            preview.FixNextPixel(tetrimino);
                            preview.SetNextBlockColor(tetrimino);

                            tetrimino.ResetXY(); // 플레이 될 블럭의 x, y좌표를 초기화 시킴
                            board.ResetDownPreviewBoard(); // downPreviewBoard의 요소값들을 전부 0으로 초기화
                            removeCount = 0; // 지운 줄의 갯수를 다시 0으로 초기화
                            isStore = false; // 저장을 다시 false값으로 바꿔준다
                            isPressSapce = false;
                        }
                    }
                    tick = 0;
                }
                tetrimino.DownPreview(board);
            }
            Invalidate();
        }

        public void ReStart(bool isReStart)
        {
            if (scoreBoard.isHigh(preview.GetScore()))
                scoreBoard.AddList(name, preview.GetScore());
            scoreBoard.SaveFile();

            // 각 클래스들의 생성자
            board = new Board();
            tetrimino = new Tetrimino();
            preview = new Preview();

            // 각각의 변수들을 초기화 해준다
            tick = 0;
            removeCount = 0;
            lineCount = 0;
            moveTickCount = 0;
            gravitySpeed = preview.LevelUp();
            isStore = false;
            press = false;
            isFirst = true;
            gui.IsClickOK = false;
            name = "";

            // 다음블럭 3개를 결정하고 보여준다.
            tetrimino.SetNextBlock();
            if (isReStart)
            {
                preview.FixNextPixel(tetrimino);
                preview.SetNextBlockColor(tetrimino);
            }
        }
    }
}
