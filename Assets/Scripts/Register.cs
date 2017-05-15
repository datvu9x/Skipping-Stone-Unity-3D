using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;
//using System.Data;
//using Mono.Data.Sqlite;
using System.Text.RegularExpressions;

public class Register : MonoBehaviour
{
    public Transform login, resgister;
    public GameObject username;
    public GameObject email;
    public GameObject password;
    public GameObject confPassword;
    public Text errortext;
    private string Username;
    private string Email;
    private string Password;
    private string ConfPassword;
    private string form;
    private SQLiteDB db = null;

    private bool EmailValid = false;
    private string[] Characters = {"a","b","c","d","e","f","g","h","i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z",
                                   "A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z",
                                   "1","2","3","4","5","6","7","8","9","0","_","-"};

    public void RegisterButton()
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


        bool UN = false;
        bool EM = false;
        bool PW = false;
        bool CPW = false;

        string sqlQuery = String.Format("SELECT UserName FROM Player");
        SQLiteQuery qr;
        qr = new SQLiteQuery(db, sqlQuery);
        string ten = "";

        if (Username != "")
        {
            while (qr.Step())
            {
                ten = qr.GetString("UserName");
                print(ten);
                if (ten != Username)
                {
                    UN = true;
                }
                else
                {
                    Debug.LogWarning("Username Taken");
                    errortext.text = "Username Taken";
                }
            }
            qr.Release();
        }
        else
        {
            Debug.LogWarning("Username field Empty");
            errortext.text = "Username field Empty";
        }
        if (Email != "")
        {
            EmailValidation();
            if (EmailValid)
            {
                if (Email.Contains("@"))
                {
                    if (Email.Contains("."))
                    {
                        EM = true;
                    }
                    else
                    {
                        Debug.LogWarning("Email is Incorrect");
                        errortext.text = "Email is Incorrect";
                    }
                }
                else
                {
                    Debug.LogWarning("Email is Incorrect");
                    errortext.text = "Email is Incorrect";
                }
            }
            else
            {
                Debug.LogWarning("Email is Incorrect");
                errortext.text = "Email is Incorrect";
            }
        }
        else
        {
            Debug.LogWarning("Email Field Empty");
            errortext.text = "Email Field Empty";
        }
        if (Password != "")
        {
            if (Password.Length > 5)
            {
                PW = true;
            }
            else
            {
                Debug.LogWarning("Password Must Be atleast 6 Characters long");
                errortext.text = "Password Must Be atleast 6 Characters long";
            }
        }
        else
        {
            Debug.LogWarning("Password Field Empty");
            errortext.text = "Password Field Empty";
        }
        if (ConfPassword != "")
        {
            if (ConfPassword == Password)
            {
                CPW = true;
            }
            else
            {
                Debug.LogWarning("Passwords Don't Match");
                errortext.text = "Passwords Don't Match";
            }
        }
        else
        {
            Debug.LogWarning("Confirm Password Field Empty");
            errortext.text = "Confirm Password Field Empty";
        }
        string sqlQueryInsert = "INSERT INTO Player(UserName,Password,Email) VALUES(?,?,?)";


        if (UN == true && EM == true && PW == true && CPW == true)
        {
            qr = new SQLiteQuery(db, sqlQueryInsert);
            qr.Bind(Username);
            qr.Bind(Password);
            qr.Bind(Email);
            qr.Step();
            qr.Release();
            username.GetComponent<InputField>().text = "";
            email.GetComponent<InputField>().text = "";
            password.GetComponent<InputField>().text = "";
            confPassword.GetComponent<InputField>().text = "";
            print("Registration Complete");
            login.gameObject.SetActive(true);
            resgister.gameObject.SetActive(false);
            errortext.text = "";
        }

        db.Close();
    }

    IEnumerator Download(WWW www)
    {
        yield return www;
    }
    private void Start()
    {
       
    }

    public void BackButton()
    {
        login.gameObject.SetActive(true);
        resgister.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (username.GetComponent<InputField>().isFocused)
            {
                email.GetComponent<InputField>().Select();
            }
            if (email.GetComponent<InputField>().isFocused)
            {
                password.GetComponent<InputField>().Select();
            }
            if (password.GetComponent<InputField>().isFocused)
            {
                confPassword.GetComponent<InputField>().Select();
            }
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (Password != "" && Email != "" && Password != "" && ConfPassword != "")
            {
                RegisterButton();
            }
        }
        Username = username.GetComponent<InputField>().text;
        Email = email.GetComponent<InputField>().text;
        Password = password.GetComponent<InputField>().text;
        ConfPassword = confPassword.GetComponent<InputField>().text;
    }

    void EmailValidation()
    {
        bool SW = false;
        bool EW = false;
        for (int i = 0; i < Characters.Length; i++)
        {
            if (Email.StartsWith(Characters[i]))
            {
                SW = true;
            }
        }
        for (int i = 0; i < Characters.Length; i++)
        {
            if (Email.EndsWith(Characters[i]))
            {
                EW = true;
            }
        }
        if (SW == true && EW == true)
        {
            EmailValid = true;
        }
        else
        {
            EmailValid = false;
        }

    }
}
