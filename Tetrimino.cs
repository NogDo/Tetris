using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MyTetris
{
    class Tetrimino
    {
        private static Random random = new Random();
        public static readonly int[,,,] mino1 = new int[7, 4, 4, 4]
            {
                #region 벽돌1
                {
                    {
                        {0,0,1,0 },
                        {0,0,1,0 },
                        {0,0,1,0 },
                        {0,0,1,0 }
                    },
                    {
                        {0,0,0,0 },
                        {1,1,1,1 },
                        {0,0,0,0 },
                        {0,0,0,0 }
                    },
                    {
                        {0,0,1,0 },
                        {0,0,1,0 },
                        {0,0,1,0 },
                        {0,0,1,0 }
                    },
                    {
                        {0,0,0,0 },
                        {1,1,1,1 },
                        {0,0,0,0 },
                        {0,0,0,0 }
                    }
                },
            #endregion 벽돌1
                #region 벽돌2
                {
                    {
                        {0,0,0,0 },
                        {0,1,1,0 },
                        {0,1,1,0 },
                        {0,0,0,0 }
                    },
                    {
                        {0,0,0,0 },
                        {0,1,1,0 },
                        {0,1,1,0 },
                        {0,0,0,0 }
                    },
                    {
                        {0,0,0,0 },
                        {0,1,1,0 },
                        {0,1,1,0 },
                        {0,0,0,0 }
                    },
                    {
                        {0,0,0,0 },
                        {0,1,1,0 },
                        {0,1,1,0 },
                        {0,0,0,0 }
                    }
                },
            #endregion 벽돌2
                #region 벽돌3
                {
                    {
                        {0,0,1,0 },
                        {0,0,1,0 },
                        {0,1,1,0 },
                        {0,0,0,0 }
                    },
                    {
                        {0,0,0,0 },
                        {0,1,0,0 },
                        {0,1,1,1 },
                        {0,0,0,0 }
                    },
                    {
                        {0,0,0,0 },
                        {0,1,1,0 },
                        {0,1,0,0 },
                        {0,1,0,0 }
                    },
                    {
                        {0,0,0,0 },
                        {1,1,1,0 },
                        {0,0,1,0 },
                        {0,0,0,0 }
                    }
                },
            #endregion 벽돌3
                #region 벽돌4
                {
                    {
                        {0,1,0,0 },
                        {0,1,0,0 },
                        {0,1,1,0 },
                        {0,0,0,0 }
                    },
                    {
                        {0,0,0,0 },
                        {0,1,1,1 },
                        {0,1,0,0 },
                        {0,0,0,0 }
                    },
                    {
                        {0,0,0,0 },
                        {0,1,1,0 },
                        {0,0,1,0 },
                        {0,0,1,0 }
                    },
                    {
                        {0,0,0,0 },
                        {0,0,1,0 },
                        {1,1,1,0 },
                        {0,0,0,0 }
                    }
                },
            #endregion 벽돌4
                #region 벽돌5
                {
                    {
                        {0,0,0,0 },
                        {0,1,1,0 },
                        {0,0,1,1 },
                        {0,0,0,0 }
                    },
                    {
                        {0,0,1,0 },
                        {0,1,1,0 },
                        {0,1,0,0 },
                        {0,0,0,0 }
                    },
                    {
                        {0,0,0,0 },
                        {0,1,1,0 },
                        {0,0,1,1 },
                        {0,0,0,0 }
                    },
                    {
                        {0,0,1,0 },
                        {0,1,1,0 },
                        {0,1,0,0 },
                        {0,0,0,0 }
                    }
                },
            #endregion 벽돌5
                #region 벽돌6
                {
                    {
                        {0,0,0,0 },
                        {0,1,1,0 },
                        {1,1,0,0 },
                        {0,0,0,0 }
                    },
                    {
                        {0,1,0,0 },
                        {0,1,1,0 },
                        {0,0,1,0 },
                        {0,0,0,0 }
                    },
                    {
                        {0,0,0,0 },
                        {0,1,1,0 },
                        {1,1,0,0 },
                        {0,0,0,0 }
                    },
                    {
                        {0,1,0,0 },
                        {0,1,1,0 },
                        {0,0,1,0 },
                        {0,0,0,0 }
                    }
                },
            #endregion 벽돌6
                #region 벽돌7
                {
                    {
                        {0,0,1,0 },
                        {0,1,1,1 },
                        {0,0,0,0 },
                        {0,0,0,0 }
                    },
                    {
                        {0,0,1,0 },
                        {0,0,1,1 },
                        {0,0,1,0 },
                        {0,0,0,0 }
                    },
                    {
                        {0,0,0,0 },
                        {0,1,1,1 },
                        {0,0,1,0 },
                        {0,0,0,0 }
                    },
                    {
                        {0,0,1,0 },
                        {0,1,1,0 },
                        {0,0,1,0 },
                        {0,0,0,0 }
                    }
                }
                #endregion 벽돌7
            }; // 블럭 모양
        // 블럭 이미지
        private static Bitmap BlockRed;
        private static Bitmap BlockOrange;
        private static Bitmap BlockYellow;
        private static Bitmap BlockGreen;
        private static Bitmap BlockBlue;
        private static Bitmap BlockSky;
        private static Bitmap BlockPurple;

        public int X { get; set; } // 플레이 되는 블럭의 현재 X좌표 값
        public int Y { get; set; } // 플레이 되는 블럭의 현재 Y좌표 값
        public int Shape { get; set; } // 모양 값
        public int Turn { get; private set; } // 회전 값
        public int TempX { get; set; } // 복사 블럭의 현재 X좌표 값
        public int TempY { get; set; } // 복사 블럭의 현재 Y좌표 값
        public int MinoTempTurn { get; set; } // 복사 블럭의 회전 값
        public int StoreShape { get; set; } // 저장블럭의 모양 값
        public int StoreTurn { get; set; } // 저장블럭의 회전 값
        public int TempShape { get; set; } // 현재 블럭과 저장 블럭을 바꿀 때 현재 블럭의 모양값을 임시 저장할 공간
        public int TempTurn { get; set; } // 현재 블럭과 저장 블럭을 바꿀 때 현재 블럭의 회전값을 임시 저장할 공간
        public int TempStoreShape { get; set; } // 현재 블럭과 저장 블럭을 바꿀 때 저장 블럭의 모양값을 임시 저장할 공간
        public int TempStoreTurn { get; set; } // 현재 블럭과 저장 블럭을 바꿀 때 저장 블럭의 회전값을 임시 저장할 공간
        public int[] NextShape { get; set; } // 다음에 나올 블럭 3개의 모양 값
        public int[] NextTurn { get; set; } // 다음에 나올 블럭 3개의 회전 값
        public int DownPreviewX { get; set; } // 현재 플레이 중인 블럭이 도착할 최종 X좌표 값
        public int DownPreviewY { get; set; } // 현재 플레이 중인 블럭이 도착할 최종 Y좌표 값

        static Tetrimino()
        {
            BlockRed = Properties.Resources.BlockRed;
            BlockOrange = Properties.Resources.BlockOrange;
            BlockYellow = Properties.Resources.BlockYellow;
            BlockGreen = Properties.Resources.BlockGreen;
            BlockBlue = Properties.Resources.BlockBlue;
            BlockSky = Properties.Resources.BlockSky;
            BlockPurple = Properties.Resources.BlockPurple;
    }

        public Tetrimino()
        {
            NextShape = new int[3];
            NextTurn = new int[3];
            X = Rule.PixelCountX / 2 - 2;
            Y = 0;
            Shape = random.Next() % 7;
            Turn = random.Next() % 4;
            TempX = X;
            TempY = Y;
            MinoTempTurn = Turn;
            DownPreviewX = X;
            DownPreviewY = Y;
        }

        public void MoveRight() // 오른쪽으로 한칸 이동
        {
            X++;
            TempX = X;
        }
        public void MoveLeft() // 왼쪽으로 한칸 이동
        {
            X--;
            TempX = X;
        }
        public void MoveDown() // 아래로 한칸 이동
        {
            Y++;
            TempY = Y;
        }
        public void BlockTurn() // 블럭을 회전 시킴
        {
            Turn = (++Turn % 4 == 0) ? 0 : Turn++;
            MinoTempTurn = Turn;
        }

        public void TempMoveRight() // 복사블럭 오른쪽으로 한칸 이동
        {
            TempX++;
        }
        public void TempMoveLeft() // 복사블럭 왼쪽으로 한칸 이동
        {
            TempX--;
        }
        public void TempMoveUp() // 복사블럭 위쪽으로 한칸 이동
        {
            TempY--;
        }
        public void TempBlockTurn() // 복사블럭 회전
        {
            MinoTempTurn = (++MinoTempTurn % 4 == 0) ? 0 : MinoTempTurn++;
        }
        public void TempBlockTurnBack() // 복사블럭 반대 회전
        {
            MinoTempTurn = (--MinoTempTurn == -1) ? 3 : MinoTempTurn--;
        }
        public void TempLocation_To_Mino() // 현재블럭의 위치를 복사블럭의 위치로 설정
        {
            X = TempX;
            Y = TempY;
        }
        public void MinoLocation_To_Temp() // 복사블럭의 위치를 현재블럭의 위치로 설정
        {
            MinoTempTurn = Turn;
            TempX = X;
            TempY = Y;
        }

        public int GetPixel(int i, int j) // 현재 블럭의 모양을 반환
        {
            return mino1[Shape, Turn, i, j];
        }
        public int GetTempPixel(int i, int j) // 복사 블럭의 모양을 반환
        {
            return mino1[Shape, MinoTempTurn, i, j];
        }
        public int GetStorePixel(int i, int j) // 저장 될 블럭(현재 게임판에서 플레이 되고 있는 블럭)의 모양을 반환
        {
            return mino1[StoreShape, StoreTurn, i, j];
        }
        public int GetNextPixel(int i, int j, int z) // 앞으로 나올 블럭들을 반환
        {
            return mino1[NextShape[z], NextTurn[z], i, j];
        }
        public char GetColorChar() // 색깔의 앞글자를 반환
        {
            switch (Shape)
            {
                case 0:
                    return 'r';
                case 1:
                    return 'o';
                case 2:
                    return 'y';
                case 3:
                    return 'g';
                case 4:
                    return 'b';
                case 5:
                    return 'd';
                case 6:
                    return 'p';
                default:
                    return 'r';
            }
        }
        public char GetNextBlockColor(int i) // 미리보기 블럭들의 색깔을 반환
        {
            switch (NextShape[i])
            {
                case 0:
                    return 'r';
                case 1:
                    return 'o';
                case 2:
                    return 'y';
                case 3:
                    return 'g';
                case 4:
                    return 'b';
                case 5:
                    return 'd';
                case 6:
                    return 'p';
                default:
                    return 'r';
            }
        }
        
        public void ResetXY() // 블럭을 저장하거나 블럭이 더이상 아래로 내려갈 수 없을 때 새로운 블럭을 떨어뜨리기 위해 x,y값을 초기화
        {
            X = Rule.PixelCountX / 2 - 2;
            Y = 0;
        }
        public void Mino_To_Store() // 현재 플레이 되는 블럭의 모양값과 턴값을 저장 블럭에 저장한다.
        {
            StoreShape = Shape;
            StoreTurn = Turn;
        }
        public void Change() // 현재 플레이 되는 블럭과 저장 블럭을 서로 바꾼다.
        {
            TempShape = Shape;
            TempTurn = Turn;
            TempStoreShape = StoreShape;
            TempStoreTurn = StoreTurn;
            Shape = TempStoreShape;
            Turn = TempStoreTurn;
            StoreShape = TempShape;
            StoreTurn = TempTurn;
        }

        public void SetNextBlock() // 다음 나올 3개의 블럭을 정한다. (게임이 실행되고 최초 1번만 실행이 된다.)
        {
            NextShape[0] = random.Next() % 7;
            NextShape[1] = random.Next() % 7;
            NextShape[2] = random.Next() % 7;
            NextTurn[0] = random.Next() % 4;
            NextTurn[1] = random.Next() % 4;
            NextTurn[2] = random.Next() % 4;
        }
        public void PullNextBlock() // 미리보기 블럭을 하나씩 당기고 마지막 미리보기 블럭은 새롭게 지정
        {
            Shape = NextShape[0];
            Turn = NextTurn[0];
            NextShape[0] = NextShape[1];
            NextTurn[0] = NextTurn[1];
            NextShape[1] = NextShape[2];
            NextTurn[1] = NextTurn[2];
            NextShape[2] = random.Next() % 7;
            NextTurn[2] = random.Next() % 4;
        }


        public void DownPreview(Board board) // 블럭이 다 내려갔을 때의 모습을 미리보기
        {
            DownPreviewY = 0;
            while (board.MoveblePreviewDown(this))
            {
                DownPreviewY++;
            }

            if (DownPreviewY >= Y) // 블럭을 중간에 끼워맞출 때 블럭 잔상이 남는 버그가 있어서 그것을 방지하기 위해 씀
                board.DownPreviewFixMino(this);
        }

        public void Draw(Graphics g)
        {
            // 도형 그리기
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    if (mino1[Shape, Turn, i, j] == 1)
                    {
                        int x = Rule.GameBoardStartX + (X + i) * Rule.PixelSizeX;
                        int y = Rule.GameBoardStartY + (Y + j - 3) * Rule.PixelSizeY;

                        if(y >= Rule.GameBoardStartY)
                        {
                            switch (Shape)
                            {
                                case 0:
                                    g.DrawImage(BlockRed, x, y, Rule.PixelSizeX, Rule.PixelSizeY);
                                    break;
                                case 1:
                                    g.DrawImage(BlockOrange, x, y, Rule.PixelSizeX, Rule.PixelSizeY);
                                    break;
                                case 2:
                                    g.DrawImage(BlockYellow, x, y, Rule.PixelSizeX, Rule.PixelSizeY);
                                    break;
                                case 3:
                                    g.DrawImage(BlockGreen, x, y, Rule.PixelSizeX, Rule.PixelSizeY);
                                    break;
                                case 4:
                                    g.DrawImage(BlockBlue, x, y, Rule.PixelSizeX, Rule.PixelSizeY);
                                    break;
                                case 5:
                                    g.DrawImage(BlockSky, x, y, Rule.PixelSizeX, Rule.PixelSizeY);
                                    break;
                                case 6:
                                    g.DrawImage(BlockPurple, x, y, Rule.PixelSizeX, Rule.PixelSizeY);
                                    break;
                            }
                        }
                    }
        }
    }
}
