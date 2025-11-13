using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class UsersData
{
    public string username;
    public string email;
    public string passwordHash; // stored as hashed version
}

[Serializable]
public class UserDatabase
{
    public List<UsersData> users = new List<UsersData>();
}

public static class LocalUserDatabase
{
    private static string filePath = Path.Combine(Application.persistentDataPath, "users.json");

    public static UserDatabase LoadDatabase()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, JsonUtility.ToJson(new UserDatabase()));
        }
        string json = File.ReadAllText(filePath);
        UserDatabase loadedList = JsonUtility.FromJson<UserDatabase>(json);
        return loadedList;
    }

    public static void SaveDatabase(UserDatabase db)
    {
        string json = JsonUtility.ToJson(db, true);
        File.WriteAllText(filePath, json);
    }
    public static bool UserNameExists(string username)
    {
        var db = LoadDatabase();
        return db.users.Exists(u => u.username.Equals(username, StringComparison.OrdinalIgnoreCase));
    }
    public static bool UserExists(string email)
    {
        var db = LoadDatabase();
        return db.users.Exists(u => u.email.Equals(email, StringComparison.OrdinalIgnoreCase));
    }

    public static bool ValidateUser(string email, string password)
    {
        var db = LoadDatabase();
        string hashed = PasswordEncryptor.Hash(password);
        return db.users.Exists(u => (u.email.Equals(email, StringComparison.OrdinalIgnoreCase) || u.username.Equals(email, StringComparison.OrdinalIgnoreCase))
            && u.passwordHash == hashed);
    }

    public static void AddUser(string username, string email, string password)
    {
        var db = LoadDatabase();
        db.users.Add(new UsersData { username = username, email = email, passwordHash = PasswordEncryptor.Hash(password) });
        SaveDatabase(db);
    }

    public static bool ResetPassword(string email, string newPassword)
    {
        var db = LoadDatabase();
        var user = db.users.Find(u => u.email.Equals(email, StringComparison.OrdinalIgnoreCase));
        if (user == null) return false;

        user.passwordHash = PasswordEncryptor.Hash(newPassword);
        SaveDatabase(db);
        return true;
    }
}