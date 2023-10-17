using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MyTetris
{
    class Board
    {
        public int[,] gameBoard; // 화면상에 보여줄 게임판을 보여주기 위해 값을 저장할 배열
        public int[,] tempBoard; // gameBoard를 잠시 저장할 공간 (줄을 지우거나, 블럭 회전으로 곂치거나 나가는 것을 방지하기 위해 씀)
        public char[,] colorBoard; // 쌓인 블럭들의 색깔을 저장할 배열
        public char[,] colorTempBoard; // colorBoard의 복사본. (꽉찬 줄을 지우고 위의 있던 블럭을 한칸 씩 당길 때 씀)
        public int[,] downPreviewBoard; // 블럭이 내려갔을 때의 모습을 보여주기 위해 값을 저장할 배열
        public bool overlab;
        // 블럭 이미지
        private static Bitmap BlockRed;
        private static Bitmap BlockOrange;
        private static Bitmap BlockYellow;
        private static Bitmap BlockGreen;
        private static Bitmap BlockBlue;
        private static Bitmap BlockSky;
        private static Bitmap BlockPurple;
        private static Image BlockRedPreview;
        private static Image BlockOrangePreview;
        private static Image BlockYellowPreview;
        private static Image BlockGreenPreview;
        private static Image BlockBluePreview;
        private static Image BlockSkyPreview;
        private static Image BlockPurplePreview;

        private Pen varPen;
        private Rectangle rect;
        private Rectangle previewRect;
        private Point point1, point2;

        static Board()
        {
            BlockRed = Properties.Resources.BlockRed;
            BlockOrange = Properties.Resources.BlockOrange;
            BlockYellow = Properties.Resources.BlockYellow;
            BlockGreen = Properties.Resources.BlockGreen;
            BlockBlue = Properties.Resources.BlockBlue;
            BlockSky = Properties.Resources.BlockSky;
            BlockPurple = Properties.Resources.BlockPurple;
            BlockRedPreview = Properties.Resources.BlockRedPreview;
            BlockOrangePreview = Properties.Resources.BlockOrangePreview;
            BlockYellowPreview = Properties.Resources.BlockYellowPreview;
            BlockGreenPreview = Properties.Resources.BlockGreenPreview;
            BlockBluePreview = Properties.Resources.BlockBluePreview;
            BlockSkyPreview = Properties.Resources.BlockSkyPreview;
            BlockPurplePreview = Properties.Resources.BlockPurplePreview;
        }

        public Board()
        {
            // 게임판의 크기와 동일한 2차원 배열들
            tempBoard = new int[Rule.PixelCountX, Rule.PixelCountY];
            gameBoard = new int[Rule.PixelCountX, Rule.PixelCountY];
            colorBoard = new char[Rule.PixelCountX, Rule.PixelCountY];
            colorTempBoard = new char[Rule.PixelCountX, Rule.PixelCountY];
            downPreviewBoard = new int[Rule.PixelCountX, Rule.PixelCountY];
            varPen = new Pen(Color.FromArgb(100, 255, 255, 255), 1);
            point1 = new Point();
            point2 = new Point();
            // 쓰일 Board들을 전부 초기화 해준다
            ResetGameBoard();
            ResetTempBoard();
            ResetColorBoard();
            ResetDownPreviewBoard();
        }

        public void ResetGameBoard() // gameBoard 초기화
        {
            for (int i = 0; i < Rule.PixelCountX; i++)
                for (int j = 0; j < Rule.PixelCountY; j++)
                    gameBoard[i, j] = 0;
        }
        public void ResetTempBoard() // gameBoard 초기화
        {
            for (int i = 0; i < Rule.PixelCountX; i++)
                for (int j = 0; j < Rule.PixelCountY; j++)
                {
                    tempBoard[i, j] = 0;
                    colorTempBoard[i, j] = ' ';
                }
        }
        public void ResetColorBoard() // colorBoard 초기화
        {
            for (int i = 0; i < Rule.PixelCountX; i++)
                for (int j = 0; j < Rule.PixelCountY; j++)
                    colorBoard[i, j] = ' ';
        }
        public void ResetDownPreviewBoard() // downPreviewBoard 초기화
        {
            for (int i = 0; i < Rule.PixelCountX; i++)
                for (int j = 0; j < Rule.PixelCountY; j++)
                    downPreviewBoard[i, j] = 0;
        }
        public void GameBoard_To_TempBoard() // gameBoard를 tempBoard로 복사
        {
            for (int i = 0; i < Rule.PixelCountX; i++)
                for (int j = 0; j < Rule.PixelCountY; j++)
                    if (gameBoard[i, j] == 1)
                        tempBoard[i, j] += 1;
        }

        public bool MovableRight(Tetrimino tetrimino) // 오른쪽으로 이동 할 수 있는지를 판단
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    if (tetrimino.GetPixel(i, j) == 1 && (tetrimino.X + i >= Rule.PixelCountX - 1 || gameBoard[tetrimino.X + i + 1, tetrimino.Y + j] == 1))
                        return false;

            return true;
        }
        public bool MovalbleLeft(Tetrimino tetrimino) // 왼쪽으로 이동 할 수 있는지를 판단
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    if (tetrimino.GetPixel(i, j) == 1 && (tetrimino.X + i == 0 || gameBoard[tetrimino.X + i - 1, tetrimino.Y + j] == 1))
                        return false;
            return true;
        }
        public bool MovableDown(Tetrimino tetrimino) // 아래로 이동 할 수 있는지를 판단
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    if (tetrimino.GetPixel(i, j) == 1 && (tetrimino.Y + j >= Rule.PixelCountY - 1 || gameBoard[tetrimino.X + i, tetrimino.Y + j + 1] == 1))
                        return false;
            return true;
        }
        public bool MoveblePreviewDown(Tetrimino tetrimino) // 내려갈 곳을 미리보여주는 블럭이 더 내려갈 수 있는지를 판단
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    if (tetrimino.GetPixel(i, j) == 1 && (tetrimino.DownPreviewY + j >= Rule.PixelCountY - 1 || gameBoard[tetrimino.X + i, tetrimino.DownPreviewY + j + 1] == 1))
                        return false;
            return true;
        }
        
        public bool Rotatable(Tetrimino tetrimino) // 회전가능 여부를 판단
        {
            try
            {
                tetrimino.MinoLocation_To_Temp(); // 복사블럭의 위치를 현재 블럭의 위치로 설정
                ResetTempBoard();
                tetrimino.TempBlockTurn();
                FixTempBoard(tetrimino);

                GameBoard_To_TempBoard();
                overlab = isOverlab();

                if (overlab) // 복사블럭을 오른쪽으로 한칸 옮긴후에 겹치는지를 다시 판단
                {
                    ResetTempBoard();
                    tetrimino.TempMoveRight();
                    FixTempBoard(tetrimino);

                    GameBoard_To_TempBoard();
                    overlab = isOverlab();
                    
                    if (overlab)
                        tetrimino.TempMoveLeft();
                }

                if (overlab) // 복사블럭을 왼쪽으로 한칸 옮긴후에 겹치는지를 다시 판단
                {
                    ResetTempBoard();
                    tetrimino.TempMoveLeft();
                    FixTempBoard(tetrimino);

                    GameBoard_To_TempBoard();
                    overlab = isOverlab();
                    if (overlab)
                    {
                        tetrimino.TempMoveRight();

                        if (tetrimino.Shape == 0)
                        {
                            ResetTempBoard();
                            tetrimino.TempMoveLeft();
                            tetrimino.TempMoveLeft();
                            FixTempBoard(tetrimino);

                            GameBoard_To_TempBoard();
                            overlab = isOverlab();
                            if (overlab)
                            {
                                tetrimino.TempMoveRight();
                                tetrimino.TempMoveRight();
                            }
                            else
                            {
                                tetrimino.TempLocation_To_Mino();
                                return true;
                            }
                        }

                        return false;
                    }
                }
                tetrimino.TempLocation_To_Mino();
                return true;
            }
            catch (IndexOutOfRangeException e) // 블럭 회전시에 블럭이 게임판 밖으로 빠져나갔다면
            {
                if (tetrimino.TempX == Rule.PixelCountX - 2) // 오른쪽 맨끝에서 돌렸을 때
                {
                    if (tetrimino.Shape == 0) // 막대 모양이면 왼쪽으로 두칸을 옮겨줌
                    {
                        tetrimino.TempMoveLeft();
                        tetrimino.TempMoveLeft();
                    }
                    else
                        tetrimino.TempMoveLeft();
                }
                else if (tetrimino.TempX == Rule.PixelCountX - 3) // 오른쪽 맨끝 보다 한칸 왼쪽에서 돌렸을 때
                {
                    if (tetrimino.Shape == 0)
                    {
                        tetrimino.TempMoveLeft();
                        if (overlab)
                        {
                            tetrimino.TempMoveLeft();
                        }

                        ResetTempBoard();
                        FixTempBoard(tetrimino);

                        GameBoard_To_TempBoard();
                        if (isOverlab())
                        {
                            tetrimino.TempMoveLeft();
                        }
                    }
                    else
                        tetrimino.TempMoveLeft();
                }
                else if (tetrimino.TempX == -1) // 왼쪽 맨끝에서 돌렸을 때
                    tetrimino.TempMoveRight();
                /* 막대모양 예외처리... 두번째 칸에서 돌렸는데 바로 오른쪽에 블럭이 있는 경우
                 게임판 밖으로 빠져나가 오류가 발생함.. 그래서 따로 예외처리를 해둠*/
                else if (tetrimino.TempX == -2)
                {
                    tetrimino.TempMoveRight();
                    tetrimino.TempMoveRight();
                }

                if (tetrimino.TempY == Rule.PixelCountY - 3) // 맨 밑에서 블럭을 돌렸을 때
                    tetrimino.TempMoveUp();

                /* 위쪽의 코드들은 게임판을 벗어나는 블럭들을 강제로 게임판 안으로 이동시키는 코드들임
                 그 과정에서 원래 있던 블럭들과 겹칠 수가 있음 그런 상황들을 처리해 주기 위한 코드임*/
                ResetTempBoard();
                FixTempBoard(tetrimino);

                GameBoard_To_TempBoard();
                if (isOverlab())
                {
                    tetrimino.TempBlockTurnBack();
                    return false;
                }

                tetrimino.TempLocation_To_Mino();
                return true;
            }
        }
        public bool isOverlab() // 블럭 회전시 기존블럭들과 겹치는지 판단
        {
            for (int i = 0; i < Rule.PixelCountX; i++) // 회전된 블럭과 기존의 블럭이 겹치는지 판단
                for (int j = 0; j < Rule.PixelCountY; j++)
                    if (tempBoard[i, j] > 1)
                        return true;

            return false;
        }

        public void FixMino(Tetrimino tetrimino) // 블럭이 쌓임
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    if (tetrimino.GetPixel(i, j) == 1)
                    {
                        gameBoard[tetrimino.X + i, tetrimino.Y + j] = 1;
                        colorBoard[tetrimino.X + i, tetrimino.Y + j] = tetrimino.GetColorChar();
                    }
        }
        public void FixTempBoard(Tetrimino tetrimino) // 복사된 게임판의 값을 변경
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    if (tetrimino.GetTempPixel(i, j) == 1)
                        tempBoard[tetrimino.TempX + i, tetrimino.TempY + j] += 1;
        }
        public void DownPreviewFixMino(Tetrimino tetrimino) // 블럭이 내려갈 곳을 미리 보여주기 위해 downPreviewBoard에 값을 저장
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    if (tetrimino.GetPixel(i, j) == 1)
                        downPreviewBoard[tetrimino.X + i, tetrimino.DownPreviewY + j] = 1;
        }
        public int RemoveLine() // 채워진 줄 지우기
        {
            bool isEmpty = true;
            int line = 0;
            int count = 0;
            ResetTempBoard();

            // 줄에 블럭들이 전부 채워졌는지 판단
            for (int j = 0; j < Rule.PixelCountY; j++)
            {
                for (int i = 0; i < Rule.PixelCountX; i++)
                {
                    if (gameBoard[i, j] == 1)
                    {
                        isEmpty = false;
                        line = j;
                    }
                    else
                    {
                        isEmpty = true;
                        break;
                    }
                }
                if (!isEmpty)
                {
                    count++;
                    break;
                }

            }

            // 블럭이 전부 채워졌다면
            if (!isEmpty)
            {
                // 블럭이 채워진 줄을 없앰
                for (int i = 0; i < Rule.PixelCountX; i++)
                {
                    gameBoard[i, line] = 0;
                    colorBoard[i, line] = ' ';
                }

                // 지워진 줄 위에 있던 블럭들의 y좌표를 한칸 낮춰서 tempBaord에 복사하고 gameBoard를 초기화
                for (int j = 0; j < line; j++)
                    for (int i = 0; i < Rule.PixelCountX; i++)
                    {
                        if (gameBoard[i, j] == 1)
                            tempBoard[i, j + 1] = 1;
                        gameBoard[i, j] = 0;

                        if (colorBoard[i, j] != ' ')
                            colorTempBoard[i, j + 1] = colorBoard[i, j];
                        colorBoard[i, j] = ' ';
                    }

                // 복사한 tempBoard를 다시 gameBoard에 넣어줌
                for (int i = 0; i < Rule.PixelCountX; i++)
                    for (int j = 0; j < Rule.PixelCountY; j++)
                    {
                        if (tempBoard[i, j] == 1)
                            gameBoard[i, j] = 1;
                        if (colorTempBoard[i, j] != ' ')
                            colorBoard[i, j] = colorTempBoard[i, j];
                    }

            }
            ResetTempBoard();
            return count;
        }

        public bool IsGameOver()
        {
            for (int i = 0; i < Rule.PixelCountX; i++)
                for (int j = 0; j < 3; j++)
                    if (gameBoard[i, j] == 1)
                        return true;

            return false;
        }

        public void Draw(Graphics g, char color) // 화면에 게임판이랑 격자 그리기
        {
            // 도형이 움직일 수 없을 때 보드에 도형을 그림
            for (int i = 0; i < Rule.PixelCountX; i++)
                for (int j = 3; j < Rule.PixelCountY; j++)
                {
                    if (gameBoard[i, j] == 1)
                    {
                        int x = Rule.GameBoardStartX + i * Rule.PixelSizeX;
                        int y = Rule.GameBoardStartY + (j - 3) * Rule.PixelSizeY;
                        rect = new Rectangle(x, y, Rule.PixelSizeX, Rule.PixelSizeY);

                        switch (colorBoard[i, j])
                        {
                            case 'r':
                                g.DrawImage(BlockRed, x, y, Rule.PixelSizeX, Rule.PixelSizeY);
                                break;
                            case 'o':
                                g.DrawImage(BlockOrange, x, y, Rule.PixelSizeX, Rule.PixelSizeY);
                                break;
                            case 'y':
                                g.DrawImage(BlockYellow, x, y, Rule.PixelSizeX, Rule.PixelSizeY);
                                break;
                            case 'g':
                                g.DrawImage(BlockGreen, x, y, Rule.PixelSizeX, Rule.PixelSizeY);
                                break;
                            case 'b':
                                g.DrawImage(BlockBlue, x, y, Rule.PixelSizeX, Rule.PixelSizeY);
                                break;
                            case 'd':
                                g.DrawImage(BlockSky, x, y, Rule.PixelSizeX, Rule.PixelSizeY);
                                break;
                            case 'p':
                                g.DrawImage(BlockPurple, x, y, Rule.PixelSizeX, Rule.PixelSizeY);
                                break;
                        }
                    }
                }

            // 도형이 다 내려갔을 때를 미리보여줌
            for (int i = 0; i < Rule.PixelCountX; i++)
                for (int j = 3; j < Rule.PixelCountY; j++)
                    if (downPreviewBoard[i, j] == 1)
                    {
                        int x = Rule.GameBoardStartX + i * Rule.PixelSizeX;
                        int y = Rule.GameBoardStartY + (j - 3) * Rule.PixelSizeY;

                        previewRect = new Rectangle(x, y, Rule.PixelSizeX, Rule.PixelSizeY);
                        switch (color)
                        {
                            case 'r':
                                g.DrawImage(BlockRedPreview, x, y, Rule.PixelSizeX, Rule.PixelSizeY);
                                break;
                            case 'o':
                                g.DrawImage(BlockOrangePreview, x, y, Rule.PixelSizeX, Rule.PixelSizeY);
                                break;
                            case 'y':
                                g.DrawImage(BlockYellowPreview, x, y, Rule.PixelSizeX, Rule.PixelSizeY);
                                break;
                            case 'g':
                                g.DrawImage(BlockGreenPreview, x, y, Rule.PixelSizeX, Rule.PixelSizeY);
                                break;
                            case 'b':
                                g.DrawImage(BlockBluePreview, x, y, Rule.PixelSizeX, Rule.PixelSizeY);
                                break;
                            case 'd':
                                g.DrawImage(BlockSkyPreview, x, y, Rule.PixelSizeX, Rule.PixelSizeY);
                                break;
                            case 'p':
                                g.DrawImage(BlockPurplePreview, x, y, Rule.PixelSizeX, Rule.PixelSizeY);
                                break;
                        }

                    }

            // 격자 그리기
            for (int i = Rule.GameBoardStartY; i <= Rule.GameBoardEndY; i += Rule.PixelSizeY)
            {
                point1.X = Rule.GameBoardStartX;
                point1.Y = i;
                point2.X = Rule.GameBoardEndX;
                point2.Y = i;
                g.DrawLine(varPen, point1, point2);
            }

            for (int i = Rule.GameBoardStartX; i <= Rule.GameBoardEndX; i += Rule.PixelSizeX)
            {
                point1.X = i;
                point1.Y = Rule.GameBoardStartY;
                point2.X = i;
                point2.Y = Rule.GameBoardEndY;
                g.DrawLine(varPen, point1, point2);
            }
        }
    }
}
