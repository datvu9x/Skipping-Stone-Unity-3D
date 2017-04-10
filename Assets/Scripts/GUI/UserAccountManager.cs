using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DatabaseControl;
using UnityEngine.SceneManagement;

public class UserAccountManager : MonoBehaviour {

    public static UserAccountManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this);
    }

    public static string playerUsername { get; protected set; }
    private static string playerPassword = "";

    public static string LoggedIn_Data { get; protected set; }
    public static bool IsLoggedIn{ get; protected set; }

    public string loggedInSceneName = "MainGame";
    public string loggedOutSceneName = "LoginGame";

    public delegate void OnDataReceivedCallback(string data);

    public void LogOut()
    {
        playerUsername = "";
        playerPassword = "";

        IsLoggedIn = false;

        Debug.Log("User logged out!");

        SceneManager.LoadScene(loggedOutSceneName);
    }

    public void LogIn(string username, string password)
    {
        playerUsername = username;
        playerPassword = password;

        IsLoggedIn = true;

        Debug.Log("Logged in as " + username);

        SceneManager.LoadScene(loggedInSceneName);
    }


    public void SendData(string data)
    {
        //Called when the player hits 'Set Data' to change the data string on their account. Switches UI to 'Loading...' and starts coroutine to set the players data string on the server
        if (IsLoggedIn)
        {
            StartCoroutine(SetData(playerUsername,playerPassword,data));
        }
       
    }

    IEnumerator SetData(string username, string password, string data)
    {
        IEnumerator e = DCF.SetUserData(username, password, data); // << Send request to set the player's data string. Provides the username, password and new data string
        while (e.MoveNext())
        {
            yield return e.Current;
        }
        string response = e.Current as string; // << The returned string from the request

        if (response == "Success")
        {
            //The data string was set correctly. Goes back to LoggedIn UI
            Debug.Log("Data upload success");
        }
        else
        {
            //There was another error. Automatically logs player out. This error message should never appear, but is here just in case.
            Debug.Log("Error: Unknown Error. Please try again later.");
        }
    }
    public void GetData(OnDataReceivedCallback onDataReceived)
    {
        //Called when the player hits 'Get Data' to retrieve the data string on their account. Switches UI to 'Loading...' and starts coroutine to get the players data string from the server
        if (IsLoggedIn)
        {
            StartCoroutine(sendGetData(playerUsername, playerPassword, onDataReceived));

        }
    }

    IEnumerator sendGetData(string username, string password, OnDataReceivedCallback onDataReceived)
    {

        string data = "ERROR";

        IEnumerator e = DCF.GetUserData(username, password); // << Send request to get the player's data string. Provides the username and password
        while (e.MoveNext())
        {
            yield return e.Current;
        }
        string response = e.Current as string; // << The returned string from the request

        if (response == "Error")
        {
            //There was another error. Automatically logs player out. This error message should never appear, but is here just in case.
            Debug.Log("Data Upload Error. Could be a server error. To check try again, if problem still occurs, contact us.");
        }
        else
        {
            //The player's data was retrieved. Goes back to loggedIn UI and displays the retrieved data in the InputField
            string DataRecieved = response;
            data = DataRecieved;
        }

        if (onDataReceived != null)
            onDataReceived.Invoke(data);
        //LoggedIn_Data = data;
    }
}
