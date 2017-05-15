using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//using System.Data;
using System.IO;
public class HighScoresManager : MonoBehaviour
{
    private SQLiteDB db = null;
    private List<HighScore> highScores = new List<HighScore>();

    public GameObject scorePrefab;

    public Transform scoreParent;

    public string connectionString;
    //Use this for initialization
    IEnumerator Download(WWW www)
    {
        yield return www;
    }
    private void Start()
    {
        db = new SQLiteDB();

        //a product persistant database path.
        string filename = Application.persistentDataPath + "/HighScore.sqlite";
        print(filename);
        //check if database already exists.

        if (!File.Exists(filename))
        {

            //ok , this is first time application start!
            //so lets copy prebuild dtabase from StreamingAssets and load store to persistancePath with Test2

            string dbfilename = "HighScore.sqlite";

            byte[] bytes = null;


#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
            string dbpath = "file://" + Application.streamingAssetsPath + "/" + dbfilename;
            WWW www = new WWW(dbpath);
            Download(www);
            bytes = www.bytes;
#elif UNITY_WEBPLAYER
    					string dbpath = "StreamingAssets/" + dbfilename;								
    					WWW www = new WWW(dbpath);
    					Download(www);
    					bytes = www.bytes;
#elif UNITY_IPHONE 
            string dbpath = Application.dataPath + "/Raw/" + dbfilename;					
    					try{	
    						using ( FileStream fs = new FileStream(dbpath, FileMode.Open, FileAccess.Read, FileShare.Read) ){
    							bytes = new byte[fs.Length];
    							fs.Read(bytes,0,(int)fs.Length);
    						}			
    					} catch (Exception e){
    						
    					}
#elif UNITY_ANDROID
    					string dbpath = Application.streamingAssetsPath + "/" + dbfilename;	           
    					WWW www = new WWW(dbpath);
    					while (!www.isDone) {;}
    					//Download(www);
    					bytes = www.bytes;
#endif
            if (bytes != null)
            {
                try
                {
                    System.IO.File.WriteAllBytes(filename, bytes);
                    //initialize database


                    db.Open(filename);


                }
                catch (Exception e)
                {
                    print(e.StackTrace);
                }
            }
        }
        else
        {
            //it mean we already download prebuild data base and store into persistantPath
            // lest update, I will call Test

            try
            {

                //initialize database


                db.Open(filename);

            }
            catch (Exception e)
            {
                print(e.ToString());
            }

        }


        ShowScores();
    }

    //Update is called once per frame
    void Update()
    {

    }

    //private void InsertScore(string name, int newScore)
    //{
    //    using (IDbConnection dbConnection = new SqliteConnection(connectionString))
    //    {
    //        dbConnection.Open();

    //        using (IDbCommand dbCmd = dbConnection.CreateCommand())
    //        {
    //            string sqlQuery = String.Format("INSERT INTO HighScore(Name, Score) VALUES(\"{0}\", \"{1}\")", name, newScore);

    //            dbCmd.CommandText = sqlQuery;
    //            dbCmd.ExecuteScalar();
    //            dbConnection.Close();

    //        }
    //    }
    //}

    //private void DeleteScore(int id)
    //{
    //    using (IDbConnection dbConnection = new SqliteConnection(connectionString))
    //    {
    //        dbConnection.Open();

    //        using (IDbCommand dbCmd = dbConnection.CreateCommand())
    //        {
    //            string sqlQuery = String.Format("Delete from HighScore,Player where PlayerID = \"{0}\"", id);

    //            dbCmd.CommandText = sqlQuery;
    //            dbCmd.ExecuteScalar();
    //            dbConnection.Close();

    //        }
    //    }
    //}
    private void GetScores()
    {
        highScores.Clear();

        string sqlQuery = "SELECT * FROM HighScore";
        SQLiteQuery qr;
        qr = new SQLiteQuery(db, sqlQuery);
        while (qr.Step())
        {
            DateTime time;
            DateTime.TryParse(qr.GetString("Date"), out time);
            {
                highScores.Add(new HighScore(qr.GetString("Name"), qr.GetInteger("Count"), qr.GetInteger("Score"), time));
            }

        }

        highScores.Sort();
    }

    private void ShowScores()
    {
        GetScores();
        for (int i = 0; i < highScores.Count; i++)
        {
            GameObject tmpObject = Instantiate(scorePrefab);

            HighScore tmpScore = highScores[i];

            tmpObject.GetComponent<HighScoreScript>().SetScore(tmpScore.Name, tmpScore.Score.ToString(), "#" + (i + 1).ToString());
            tmpObject.transform.SetParent(scoreParent);
        }

        db.Close();
    }
}
