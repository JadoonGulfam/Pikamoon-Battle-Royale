using System.Text;
using UnityEngine;

public static class PlayerPrefsEncryptor
{
    public static void SaveEncrypted(string key, string value)
    {
        string encoded = System.Convert.ToBase64String(Encoding.UTF8.GetBytes(value));
        PlayerPrefs.SetString(key, encoded);
    }

    public static string LoadDecrypted(string key)
    {
        if (!PlayerPrefs.HasKey(key)) return null;
        string encoded = PlayerPrefs.GetString(key);
        return Encoding.UTF8.GetString(System.Convert.FromBase64String(encoded));
    }
}
