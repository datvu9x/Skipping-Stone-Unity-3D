using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DatabaseControl; // << Remember to add this reference to your scripts which use DatabaseControl
using System;

public class LoginMenu : MonoBehaviour {

    public GameObject loginParent;
    public GameObject registerParent;
    public GameObject loadingParent;

    //These are all the InputFields which we need in order to get the entered usernames, passwords, etc
    public InputField Login_UsernameField;
    public InputField Login_PasswordField;
    public InputField Register_UsernameField;
    public InputField Register_PasswordField;
    public InputField Register_ConfirmPasswordField;

    //These are the UI Texts which display errors
    public Text Login_ErrorText;
    public Text Register_ErrorText;

    //Called at the very start of the game
    void Awake()
    {
        ResetAllUIElements();
    }

    //Called by Button Pressed Methods to Reset UI Fields
    void ResetAllUIElements()
    {
        //This resets all of the UI elements. It clears all the strings in the input fields and any errors being displayed
        Login_UsernameField.text = "";
        Login_PasswordField.text = "";
        Register_UsernameField.text = "";
        Register_PasswordField.text = "";
        Register_ConfirmPasswordField.text = "";
        Login_ErrorText.text = "";
        Register_ErrorText.text = "";
    }

    //UI Button Pressed Methods
    public void Login_LoginButtonPressed()
    {
        //Called when player presses button to Login

        //Check the lengths of the username and password. (If they are wrong, we might as well show an error now instead of waiting for the request to the server)
        if (Login_UsernameField.text.Length > 3)
        {
            if (Login_PasswordField.text.Length > 5)
            {
                //Username and password seem reasonable. Change UI to 'Loading...'. Start the Coroutine which tries to log the player in.
                loginParent.gameObject.SetActive(false);
                loadingParent.gameObject.SetActive(true);
                registerParent.gameObject.SetActive(false);
                StartCoroutine(LoginUser(Login_UsernameField.text, Login_PasswordField.text));
            }
            else
            {
                //Password too short so it must be wrong
                Login_ErrorText.text = "Error: Password Incorrect";
            }
        }
        else
        {
            //Username too short so it must be wrong
            Login_ErrorText.text = "Error: Username Incorrect";
        }
    }

    IEnumerator LoginUser(string username, string password)
    {
        IEnumerator e = DCF.Login(username, password); // << Send request to login, providing username and password
        while (e.MoveNext())
        {
            yield return e.Current;
        }
        string response = e.Current as string; // << The returned string from the request

        if (response == "Success")
        {
            //Username and Password were correct. Stop showing 'Loading...' and show the LoggedIn UI. And set the text to display the username.
            ResetAllUIElements();
            loadingParent.gameObject.SetActive(false);
            Login_UsernameField.text = "";
            UserAccountManager.instance.LogIn(username, password);

        }
        else
        {
            //Something went wrong logging in. Stop showing 'Loading...' and go back to LoginUI
            loadingParent.gameObject.SetActive(false);
            loginParent.gameObject.SetActive(true);
            registerParent.gameObject.SetActive(false);
            if (response == "UserError")
            {
                //The Username was wrong so display relevent error message
                Login_ErrorText.text = "Error: Username not Found";
            }
            else
            {
                if (response == "PassError")
                {
                    //The Password was wrong so display relevent error message
                    Login_ErrorText.text = "Error: Password Incorrect";
                }
                else
                {
                    //There was another error. This error message should never appear, but is here just in case.
                    Login_ErrorText.text = "Error: Unknown Error. Please try again later.";
                }
            }
        }

        Login_PasswordField.text = "";
    }
    public void Login_RegisterButtonPressed()
    {
        //Called when the player hits register on the Login UI, so switches to the Register UI
        ResetAllUIElements();
        loginParent.gameObject.SetActive(false);
        registerParent.gameObject.SetActive(true);
        loadingParent.gameObject.SetActive(false);
    }
    public void Register_RegisterButtonPressed()
    {
        //Called when the player presses the button to register
        string confirmedPassword = Register_ConfirmPasswordField.text;

        //Make sure username and password are long enough
        if (Register_UsernameField.text.Length > 3)
        {
            if (Register_PasswordField.text.Length > 5)
            {
                //Check the two passwords entered match
                if (Register_PasswordField.text == confirmedPassword)
                {
                    //Username and passwords seem reasonable. Switch to 'Loading...' and start the coroutine to try and register an account on the server
                    registerParent.gameObject.SetActive(false);
                    loadingParent.gameObject.SetActive(true);
                    loginParent.gameObject.SetActive(false);
                    StartCoroutine(RegisterUser(Register_UsernameField.text, Register_PasswordField.text, "[COUNTS]0/[POINTS]0"));
                }
                else
                {
                    //Passwords don't match, show error
                    Register_ErrorText.text = "Error: Password's don't Match";
                }
            }
            else
            {
                //Password too short so show error
                Register_ErrorText.text = "Error: Password too Short";
            }
        }
        else
        {
            //Username too short so show error
            Register_ErrorText.text = "Error: Username too Short";
        }
    }

    IEnumerator RegisterUser(string username, string password, string data)
    {
        IEnumerator e = DCF.RegisterUser(username, password, data); // << Send request to register a new user, providing submitted username and password. It also provides an initial value for the data string on the account, which is "Hello World".
        while (e.MoveNext())
        {
            yield return e.Current;
        }
        string response = e.Current as string; // << The returned string from the request

        if (response == "Success")
        {
            //Username and Password were valid. Account has been created. Stop showing 'Loading...' and show the loggedIn UI and set text to display the username.
            ResetAllUIElements();
            loadingParent.gameObject.SetActive(false);

            Register_UsernameField.text = "";
            UserAccountManager.instance.LogIn(username, password);
        }
        else
        {
            //Something went wrong logging in. Stop showing 'Loading...' and go back to RegisterUI
            loadingParent.gameObject.SetActive(false);
            registerParent.gameObject.SetActive(true);
            loginParent.gameObject.SetActive(false);
            if (response == "UserError")
            {
                //The username has already been taken. Player needs to choose another. Shows error message.
                Register_ErrorText.text = "Error: Username Already Taken";
            }
            else
            {
                //There was another error. This error message should never appear, but is here just in case.
                Login_ErrorText.text = "Error: Unknown Error. Please try again later.";
            }
        }
        Register_PasswordField.text = "";
        Register_ConfirmPasswordField.text = "";
    }
    public void Register_BackButtonPressed()
    {
        //Called when the player presses the 'Back' button on the register UI. Switches back to the Login UI
        ResetAllUIElements();
        loginParent.gameObject.SetActive(true);
        registerParent.gameObject.SetActive(false);
        loadingParent.gameObject.SetActive(false);
    }
    public void LoggedIn_LogoutButtonPressed()
    {
        //Called when the player hits the 'Logout' button. Switches back to Login UI and forgets the player's username and password.
        //Note: Database Control doesn't use sessions, so no request to the server is needed here to end a session.
        ResetAllUIElements();
        UserAccountManager.instance.LogOut();
        loginParent.gameObject.SetActive(true);
        registerParent.gameObject.SetActive(false);
        loadingParent.gameObject.SetActive(false);
    }
}
