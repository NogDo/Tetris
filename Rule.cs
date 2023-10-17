using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTetris
{
    class Rule
    {
        // 게임 화면 크기
        public const int FormWidth = 1280;
        public const int FormHeight = 840;
        // 게임판 블럭 수, 블럭 픽셀 크기
        public const int PixelCountX = 10;
        public const int PixelCountY = 23;
        public const int PixelSizeX = 40;
        public const int PixelSizeY = 40;
        // 게임판 시작좌표, 끝좌표
        public const int GameBoardStartX = 440;
        public const int GameBoardEndX = 840;
        public const int GameBoardStartY = 20;
        public const int GameBoardEndY = 820;
        // 미리보기, 저장블럭 픽셀 크기
        public const int PreviewPixelSizeX = 20;
        public const int PreviewPixelSizeY = 20;
        // 저장블럭 보여줄 창 시작좌표, 끝좌표, 창 크기
        public const int StoreStartX = 280;
        public const int StoreEndX = 400;
        public const int StoreStartY = 20;
        public const int StoreEndY = 140;
        public const int StoreSize = 120;
        // 미리보기 블럭 보여줄 창 시작좌표, 끝좌표, 창 크기
        public const int NextStartX = 880;
        public const int NextEndX = 1000;
        public const int NextStartY = 20;
        public const int NextEndY = 380;
        public const int NextSizeWidth = 120;
        public const int NextSizeHeight = 360;
        // 게임정보 창 시작좌표, 끝좌표, 창 크기
        public const int InformationStartX = 280;
        public const int InformationEndX = 400;
        public const int InformationStartY = 160;
        public const int InformationEndY = 600;
        public const int InformationSizeWidth = 120;
        public const int InformationSizeHeight = 340;
    }
}
