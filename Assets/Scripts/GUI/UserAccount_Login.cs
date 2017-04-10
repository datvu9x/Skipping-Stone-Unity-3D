using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserAccount_Login : MonoBehaviour {

    public Text usernameText;

    private void Start()
    {
        if(UserAccountManager.IsLoggedIn)
            usernameText.text = UserAccountManager.playerUsername;
    }

    public void LogOut()
    {
        if (UserAccountManager.IsLoggedIn)
            UserAccountManager.instance.LogOut();
    }

}
