using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTetris
{
    [Serializable]
    class ScoreInformation : IComparable<ScoreInformation>
    {
        public int Score { get; private set; }
        public string Name { get; private set; }

        public ScoreInformation(string name, int score)
        {
            this.Name = name;
            this.Score = score;
        }

        public int CompareTo(ScoreInformation other)
        {
            return other.Score - Score;
        }
    }
}
