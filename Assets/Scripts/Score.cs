using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour {

    public static int ScorePlayer { set; get; }

    public static int CountPlay { set; get; }

    public Score(int score, int count)
    {
        ScorePlayer = score;
        CountPlay = count;
    }
}
