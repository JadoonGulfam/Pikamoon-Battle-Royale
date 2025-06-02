using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "UserData", menuName = "Scriptable Objects/UserData", order = 1)]
public class UserDataBase : ScriptableObject
{
    public List<UserData> users = new List<UserData>();
    public UserData GetUser(string email)
    {
        return users.Find(user => user.email == email);
    }
    public UserData GetLoggedInUser()
    {
        return users.FirstOrDefault(user => user.isLoggedIn);
    }

    public bool UserExists(string email)
    {
        return users.Exists(user => user.email == email);
    }
}
[Serializable]
public class UserData
{
    public string email;
    public string password;
    public bool isLoggedIn;

    public UserData(string email, string password)
    {
        this.email = email;
        this.password = password;
        this.isLoggedIn = false;
    }
}