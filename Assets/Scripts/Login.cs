using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
//using System.Data;
using System.IO;
//using Mono.Data.Sqlite;
using System.Text.RegularExpressions;

public class Login : MonoBehaviour
{
    private SQLiteDB db = null;
    public InputField username;
    public InputField password;
    public Text errortext;
    private string Username;
    private string Password;
    private String[] Lines;
    private string DecryptedPass;
    public Transform login, resgister;

    public GameObject canvasConnection;
    public GameObject canvasSelectLake;
    public GameObject panelServer;
    public Text Info;

    private NetworkMenu networkMenu;

    IEnumerator Download(WWW www)
    {
        yield return www;
    }
    private void Start()
    {
        username.text = "test";
        password.text = "123456";
        networkMenu = GetComponent<NetworkMenu>();


    }

    Player player;

    public void LoginButton()
    {
        db = new SQLiteDB();

        // a product persistant database path.
        string filename = Application.persistentDataPath + "/HighScore.sqlite";
        print(filename);
        // check if database already exists.

        if (!File.Exists(filename))
        {

            // ok , this is first time application start!
            // so lets copy prebuild dtabase from StreamingAssets and load store to persistancePath with Test2

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

                    //
                    // initialize database
                    //
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
            // it mean we already download prebuild data base and store into persistantPath
            // lest update, I will call Test

            try
            {
                //
                // initialize database
                //
                db.Open(filename);

            }
            catch (Exception e)
            {
                print(e.ToString());
            }

        }


        Debug.Log("LoginButton");
        bool UN = false;
        bool PW = false;
        string sqlQuery = String.Format("SELECT UserName, Password, Email FROM Player Where UserName = \"{0}\"", Username);
        SQLiteQuery qr;
        qr = new SQLiteQuery(db, sqlQuery);
        string us = "";
        string pa = "";

        if (Username != "" && Password != "")
        {
            while (qr.Step())
            {
                us = qr.GetString("UserName");
                if (us == Username)
                {
                    UN = true;
                    Player.UserName = Username;
                    NetworkMenu.name = Username;
                }
                else
                {
                    Debug.LogWarning("Username Invaild");
                    errortext.text = "Username Invaild";
                    throw new Exception("Username Invaild!");
                }

                pa = qr.GetString("Password");
                print(pa);
                print(Password);
                if (pa == Password)
                {
                    PW = true;
                    Player.Password = Password;
                }
                else
                {
                    Debug.LogWarning("Password Invaild");
                    errortext.text = "Password Invaild";
                    throw new Exception("Password Invaild");
                }
            }
            qr.Release();
        }
        else
        {
            Debug.LogWarning("Username Field Empty or Password Is invalid");
            errortext.text = "Username Field Empty or Password Is invalid";
        }
        if (UN == true && PW == true)
        {
            username.GetComponent<InputField>().text = "";
            password.GetComponent<InputField>().text = "";
            print("Login Sucessful");
            login.gameObject.SetActive(false);
            canvasConnection.gameObject.SetActive(false);
            canvasSelectLake.gameObject.SetActive(true);
            if (Network.isServer)
            {
                Info.text = "";
            }
            panelServer.gameObject.SetActive(false);
        }

        db.Close();
    }

    public void RegisterButton()
    {
        resgister.gameObject.SetActive(true);
        login.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (username.GetComponent<InputField>().isFocused)
            {
                password.GetComponent<InputField>().Select();
            }
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (Password != "" && Password != "")
            {
                LoginButton();
            }
        }
        Username = username.GetComponent<InputField>().text;
        Password = password.GetComponent<InputField>().text;
    }
}
