using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePlayer : MonoBehaviour
{
    private List<HighScore> highScore = new List<HighScore>();
    public GameObject scorePrefab;

    public Transform scoreParent;
    int i = 1;

    private void Update()
    {
        if (PlayerControl.isSendCount == true)
        {
            ShowScores();
            PlayerControl.isSendCount = false;
            print("SIZE_MAP_3: " + PlayerControl.arrayScore.Count);
        }

    }

    [RPC]
    public void DestroyScore()
    {

        foreach (Transform child in scoreParent)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
    private void ShowScores()
    {
        GetComponent<NetworkView>().RPC("DestroyScore", RPCMode.All);
        GetComponent<NetworkView>().RPC("SpawnScore", RPCMode.All);
        StartCoroutine("WaitTimeScore");

    }

    [RPC]
    public void SpawnScore()
    {
        foreach (HighScore tmpScore in PlayerControl.arrayScore)
        {
            GameObject tmpObject = Instantiate(scorePrefab);
            tmpObject.GetComponent<HighScoreScript>().SetScore(tmpScore.Name, tmpScore.Score.ToString(), tmpScore.Count.ToString());
            tmpObject.transform.SetParent(scoreParent);
        }


    }
    IEnumerator WaitTimeScore()
    {
        yield return new WaitForSeconds(1);
        GameObject tempObject = Instantiate(scorePrefab);
        tempObject.GetComponent<HighScoreScript>().SetScore("", "", "");
        tempObject.transform.SetParent(scoreParent);

    }
}
