using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Diagnostics;

namespace MyTetris
{
    class ScoreBoard
    {
        private static Font font;
        private static SolidBrush brush;
        private static SolidBrush scoreRectBrush;
        private static Rectangle scoreRect;

        private List<ScoreInformation> scoreList;
        private ScoreInformation[] scoreInformation;

        static ScoreBoard()
        {
            font = new Font("휴먼둥근헤드라인", 10, FontStyle.Bold);

            brush = new SolidBrush(Color.FromArgb(255, 255, 255));
            scoreRectBrush = new SolidBrush(Color.FromArgb(10, 10, 10));

            scoreRect = new Rectangle(Rule.GameBoardStartX + 50, 500, 300, 200);
        }

        public ScoreBoard()
        {
            scoreList = new List<ScoreInformation>();
            scoreInformation = new ScoreInformation[6];
            ResetScoreInformation();
            OpenFile();
        }

        public void ResetScoreInformation()
        {
            for (int i = 0; i < 6; i++)
                scoreInformation[i] = new ScoreInformation(" ", 0);
        }

        public void SortList()
        {
            scoreList.AddRange(scoreInformation);
            scoreList.Sort((x, y) => y.Score.CompareTo(x.Score));
            if (scoreList.Count > 5)
                scoreList.RemoveAt(5);
        }
        public void AddList(string name, int score)
        {
            scoreInformation[5] = new ScoreInformation(name, score);
            SortList();
        }

        public bool isHigh(int score)
        {
            return score > scoreList[4].Score;
        }

        public void SaveFile()
        {
            FileStream fs = new FileStream("DataFile.dat", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(fs, scoreList);

            fs.Close();
        }
        public void OpenFile()
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream("DataFile.dat", FileMode.Open);
            }
            catch (FileNotFoundException e)
            {
                ResetScoreInformation();
                scoreList.RemoveRange(0, scoreList.Count);
                scoreList.AddRange(scoreInformation);
                SaveFile();
                fs = new FileStream("DataFile.dat", FileMode.Open);
            }

            BinaryFormatter formatter = new BinaryFormatter();
            scoreList = (List<ScoreInformation>)formatter.Deserialize(fs);

            fs.Close();
        }

        public void ResetList()
        {
            ResetScoreInformation();
            scoreList.RemoveRange(0, scoreList.Count);
            scoreList.AddRange(scoreInformation);
            SaveFile();
            OpenFile();
        }

        public void Draw(Graphics g)
        {
            g.FillRectangle(scoreRectBrush, scoreRect);
            g.DrawString("SCORE", font, brush, (Rule.GameBoardStartX + Rule.GameBoardEndX) / 2 - 30, 510);
            for (int i = 0; i < 5; i++)
            {
                g.DrawString(String.Format("{0, -3}", scoreList[i].Name), font, brush, Rule.GameBoardStartX + 60, 550 + i * 30);
                g.DrawString(String.Format("{0, 0:D6}", scoreList[i].Score), font, brush, Rule.GameBoardStartX + 280, 550 + i * 30);
            }
        }
    }
}
