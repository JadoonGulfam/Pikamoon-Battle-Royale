using System;
using System.IO;
using UnityEngine;

public static class AutoLoginManager
{
    private static string filePath = Path.Combine(Application.persistentDataPath, "autologin.json");

    [Serializable]
    private class AutoLoginData
    {
        public string login;      // email or username
        public string password;   // still hashed for safety
    }

    public static void Save(string login, string password)
    {
        // store hashed password just like LocalUserDatabase
        AutoLoginData data = new AutoLoginData
        {
            login = login,
            password = PasswordEncryptor.Hash(password)
        };

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(filePath, json);
    }

    public static (string login, string passwordHash)? Load()
    {
        if (!File.Exists(filePath)) return null;

        string json = File.ReadAllText(filePath);
        AutoLoginData data = JsonUtility.FromJson<AutoLoginData>(json);
        return (data.login, data.password);
    }

    public static void Clear()
    {
        if (File.Exists(filePath))
            File.Delete(filePath);
    }
}
