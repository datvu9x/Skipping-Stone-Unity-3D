using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreScript : MonoBehaviour {

    public GameObject score;
    public GameObject scoreName;
    public GameObject count;

    public void SetScore(string name, string score, string count)
    {
        this.count.GetComponent<Text>().text = count;
        this.score.GetComponent<Text>().text = score;
        this.scoreName.GetComponent<Text>().text = name;
    }
}
