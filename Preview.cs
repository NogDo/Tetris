using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MyTetris
{
    class Preview
    {
        // 브러쉬, 폰트, 영역, 위치 값
        private static SolidBrush grayBrush;
        private static SolidBrush darkGrayBrush;
        private static SolidBrush fontBrush;
        private static Font textFont;
        private static Font numberFont;
        private static Rectangle storeRect;
        private static Rectangle nextRect;
        private static Rectangle informationRect;
        private static Rectangle scoreRect;
        private static Rectangle levelRect;
        private static Rectangle lineRect;
        private static Point scoreTextPoint;
        private static Point scoreNumberPoint;
        private static Point levelTextPoint;
        private static Point levelNumberPoint;
        private static Point lineTextPoint;
        private static Point lineNumberPoint;
        // 블럭 이미지
        private static Bitmap BlockRed;
        private static Bitmap BlockOrange;
        private static Bitmap BlockYellow;
        private static Bitmap BlockGreen;
        private static Bitmap BlockBlue;
        private static Bitmap BlockSky;
        private static Bitmap BlockPurple;

        private Bitmap[] BlockImage; // 다음 나올 블럭들 이미지
        private int[,,] nextBoard; // 다음에 나올 블럭이 담길 배열
        private int[,] storeBoard; // 저장 블럭이 담길 배열
        private int score; // 점수
        private int level; // 레벨
        private int line; // 지워진 줄 총 갯수
        private char storeBlockColor; // 저장 블럭의 색깔

        static Preview()
        {
            grayBrush = new SolidBrush(Color.FromArgb(150, 35, 35, 35));
            darkGrayBrush = new SolidBrush(Color.FromArgb(150, 15, 15, 15));
            fontBrush = new SolidBrush(Color.FromArgb(255, 255, 255));

            textFont = new Font("휴먼둥근헤드라인", 18, FontStyle.Bold);
            numberFont = new Font("휴먼둥근헤드라인", 10, FontStyle.Bold);

            storeRect = new Rectangle(Rule.StoreStartX, Rule.StoreStartY, Rule.StoreSize, Rule.StoreSize);
            nextRect = new Rectangle(Rule.NextStartX, Rule.NextStartY, Rule.NextSizeWidth, Rule.NextSizeHeight);
            informationRect = new Rectangle(Rule.InformationStartX, Rule.InformationStartY, Rule.InformationSizeWidth, Rule.InformationSizeHeight);
            scoreRect = new Rectangle(Rule.InformationStartX + 10, Rule.InformationStartY + 50, Rule.InformationSizeWidth - 20, 40);
            levelRect = new Rectangle(Rule.InformationStartX + 10, Rule.InformationStartY + 150, Rule.InformationSizeWidth - 20, 40);
            lineRect = new Rectangle(Rule.InformationStartX + 10, Rule.InformationStartY + 250, Rule.InformationSizeWidth - 20, 40);

            scoreTextPoint = new Point(Rule.InformationStartX + 8, Rule.InformationStartY + 10);
            scoreNumberPoint = new Point(Rule.InformationStartX + 15, Rule.InformationStartY + 65);
            levelTextPoint = new Point(Rule.InformationStartX + 9, Rule.InformationStartY + 110);
            levelNumberPoint = new Point(Rule.InformationStartX + 15, Rule.InformationStartY + 165);
            lineTextPoint = new Point(Rule.InformationStartX + 20, Rule.InformationStartY + 210);
            lineNumberPoint = new Point(Rule.InformationStartX + 15, Rule.InformationStartY + 265);

            BlockRed = Properties.Resources.BlockRed;
            BlockOrange = Properties.Resources.BlockOrange;
            BlockYellow = Properties.Resources.BlockYellow;
            BlockGreen = Properties.Resources.BlockGreen;
            BlockBlue = Properties.Resources.BlockBlue;
            BlockSky = Properties.Resources.BlockSky;
            BlockPurple = Properties.Resources.BlockPurple;
        }

        public Preview()
        {
            // 변수들 크기를 선언하고 초기화
            storeBoard = new int[4, 4];
            nextBoard = new int[3, 4, 4];
            BlockImage = new Bitmap[3];
            score = 0;
            level = 1;
            line = 0;
            storeBlockColor = ' ';
            ResetStoreBoard();
        }
        public int GetScore()
        {
            return score;
        }

        public void ResetStoreBoard() // 저장블럭이 담길 배열을 초기화
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    storeBoard[i, j] = 0;
        }
        public void ResetNextBoard() // 다음 블럭들이 담길 배열을 초기화
        {
            for (int z = 0; z < 3; z++)
                for (int i = 0; i < 4; i++)
                    for (int j = 0; j < 4; j++)
                        nextBoard[z, i, j] = 0;
        }

        public void SetStoreBlockColor(Tetrimino tetrimino) // 저장블럭의 색깔을 지정
        {
            storeBlockColor = tetrimino.GetColorChar();
        }
        public void FixStorePixel(Tetrimino tetrimino) // 저장블럭을 화면에 그리기위해 storeBoard의 값을 바꿔준다
        {
            ResetStoreBoard();
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    if (tetrimino.GetStorePixel(i, j) == 1)
                        storeBoard[i, j] = 1;
        }
        public void SetNextBlockColor(Tetrimino tetrimino) // 다음 블럭들의 색깔을 지정
        {
            for (int i = 0; i < 3; i++)
                switch (tetrimino.GetNextBlockColor(i))
                {
                    case 'r':
                        BlockImage[i] = BlockRed;
                        break;
                    case 'o':
                        BlockImage[i] = BlockOrange;
                        break;
                    case 'y':
                        BlockImage[i] = BlockYellow;
                        break;
                    case 'g':
                        BlockImage[i] = BlockGreen;
                        break;
                    case 'b':
                        BlockImage[i] = BlockBlue;
                        break;
                    case 'd':
                        BlockImage[i] = BlockSky;
                        break;
                    case 'p':
                        BlockImage[i] = BlockPurple;
                        break;
                }
        }
        public void FixNextPixel(Tetrimino tetrimino) // 다음 블럭들을 화면에 그리기위해 nextBoard의 값을 바꿔준다
        {
            ResetNextBoard();
            for (int z = 0; z < 3; z++)
                for (int i = 0; i < 4; i++)
                    for (int j = 0; j < 4; j++)
                        if (tetrimino.GetNextPixel(i, j, z) == 1)
                            nextBoard[z, i, j] = 1;
        }

        public bool isEmpty(Tetrimino tetrimino) // 저장 블럭이 담길 공간이 비어있는지 판단
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    if (storeBoard[i, j] == 1)
                        return false;
            return true;
        }

        public void AddScore() // 점수를 더한다.
        {
            score += 3;
        }
        public void AddComboScore(int removeCount) // 지운 줄의 갯수에 따라 점수를 더한다
        {
            int comboScore = 0;

            if (removeCount == 0)
                comboScore = 0;
            else if (removeCount == 1)
                comboScore = 100;
            else if (removeCount == 2)
                comboScore = 200;
            else if (removeCount == 3)
                comboScore = 400;
            else if (removeCount == 4)
                comboScore = 1000;

            score += comboScore;
        }
        public int LevelUp() // 스코어에 따른 레벨을 증가시킨다.
        {
            level = (score / 1000 + 1 > 25) ? 25 : score / 1000 + 1;
            return level;
        }
        public void LineCount(int lineCount) // 현재 지운 줄의 개수
        {
            line = lineCount;
        }

        public void Draw(Graphics g)
        {
            // 저장된 블럭이 들어갈 영역
            g.FillRectangle(grayBrush, storeRect);

            // 저장된 블럭 출력
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    if (storeBoard[i, j] == 1)
                    {
                        int x = Rule.StoreStartX + 20 + i * Rule.PreviewPixelSizeX;
                        int y = Rule.StoreStartY + 20 + j * Rule.PreviewPixelSizeY;

                        switch (storeBlockColor)
                        {
                            case 'r':
                                g.DrawImage(BlockRed, x, y, Rule.PreviewPixelSizeX, Rule.PreviewPixelSizeY);
                                break;
                            case 'o':
                                g.DrawImage(BlockOrange, x, y, Rule.PreviewPixelSizeX, Rule.PreviewPixelSizeY);
                                break;
                            case 'y':
                                g.DrawImage(BlockYellow, x, y, Rule.PreviewPixelSizeX, Rule.PreviewPixelSizeY);
                                break;
                            case 'g':
                                g.DrawImage(BlockGreen, x, y, Rule.PreviewPixelSizeX, Rule.PreviewPixelSizeY);
                                break;
                            case 'b':
                                g.DrawImage(BlockBlue, x, y, Rule.PreviewPixelSizeX, Rule.PreviewPixelSizeY);
                                break;
                            case 'd':
                                g.DrawImage(BlockSky, x, y, Rule.PreviewPixelSizeX, Rule.PreviewPixelSizeY);
                                break;
                            case 'p':
                                g.DrawImage(BlockPurple, x, y, Rule.PreviewPixelSizeX, Rule.PreviewPixelSizeY);
                                break;
                        }
                    }

            // 미리보기 블럭이 들어갈 영역
            g.FillRectangle(grayBrush, nextRect);

            // 미리보기 블럭 출력
            for (int z = 0; z < 3; z++)
                for (int i = 0; i < 4; i++)
                    for (int j = 0; j < 4; j++)
                        if (nextBoard[z, i, j] == 1)
                        {
                            if (z == 0)
                            {
                                int x = Rule.NextStartX + 20 + i * Rule.PreviewPixelSizeX;
                                int y = Rule.NextStartY + 20 + j * Rule.PreviewPixelSizeY;
                                g.DrawImage(BlockImage[z], x, y, Rule.PreviewPixelSizeX, Rule.PreviewPixelSizeY);
                            }
                            else if (z == 1)
                            {
                                int x = Rule.NextStartX + 20 + i * Rule.PreviewPixelSizeX;
                                int y = Rule.NextStartY + 140 + j * Rule.PreviewPixelSizeY;
                                g.DrawImage(BlockImage[z], x, y, Rule.PreviewPixelSizeX, Rule.PreviewPixelSizeY);
                            }
                            else
                            {
                                int x = Rule.NextStartX + 20 + i * Rule.PreviewPixelSizeX;
                                int y = Rule.NextStartY + 260 + j * Rule.PreviewPixelSizeY;
                                g.DrawImage(BlockImage[z], x, y, Rule.PreviewPixelSizeX, Rule.PreviewPixelSizeY);
                            }
                        }

            // 스코어, 레벨, 라인을 담을 영역
            g.FillRectangle(grayBrush, informationRect);
            // 스코어 글자
            g.DrawString("SCORE", textFont, fontBrush, scoreTextPoint);

            // 점수출력 될 영역 및 점수 출력
            g.FillRectangle(darkGrayBrush, scoreRect);
            g.DrawString(Convert.ToString(score), numberFont, fontBrush, scoreNumberPoint);

            // 레벨 글자
            g.DrawString("LEVEL", textFont, fontBrush, levelTextPoint);

            // 레벨출력 될 영역 및 레벨 출력
            g.FillRectangle(darkGrayBrush, levelRect);
            g.DrawString(Convert.ToString(level), numberFont, fontBrush, levelNumberPoint);

            // 라인 글자
            g.DrawString("LINE", textFont, fontBrush, lineTextPoint);

            // 라인출력 될 영역 및 라인 출력
            g.FillRectangle(darkGrayBrush, lineRect);
            g.DrawString(Convert.ToString(line), numberFont, fontBrush, lineNumberPoint);
        }
    }
}
