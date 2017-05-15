using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {
    public static string UserName { set; get; }

    public static string Password { set; get; }

    public static string Email { set; get; }

    public Player(string userName, string password, string email)
    {
        UserName = userName;
        Password = password;
        Email = email;
    }
}
