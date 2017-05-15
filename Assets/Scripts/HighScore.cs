using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class HighScore : IComparable<HighScore>
{
    public int Score { set; get; }

    public string Name { set; get; }

    public DateTime Date { set; get; }

    public int Count { set; get; }

    public HighScore(string name, int count, int score, DateTime date)
    {
        this.Score = score;
        this.Name = name;
        this.Date = date;
        this.Count = count;
    }

    public int CompareTo(HighScore other)
    {
        if (other.Score < this.Score)
            return -1;
        else if (other.Score > this.Score)
            return 1;
        else if (other.Date < this.Date)
            return -1;
        else if (other.Date > this.Date)
            return 1;
        return 0;
    }
}

