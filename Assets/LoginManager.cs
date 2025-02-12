using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    public UserDataBase userDatabase;
    [Header("UI Elements")]
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public Button loginButton;
    public Button registerButton;
    public TMP_Text statusText;

   // private string loginURL = "https://your-api.com/login";
    void Start()
    {

        loginButton.onClick.AddListener(HandleLogin);
       // registerButton.onClick.AddListener(HandleRegister);
    }

    public void HandleLogin()
    {
        string email = emailInput.text;
        string password = passwordInput.text;

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            statusText.text = "Please enter both email and password.";
            return;
        }

        UserData user = userDatabase.GetUser(email);
        if (user != null && user.password == password)
        {
            user.isLoggedIn = true;
            //userDatabase.SaveToDisk();

            statusText.text = "Login successful!";
            if (SceneManager.GetSceneByName("Login Scene").isLoaded)
            {
                SceneManager.UnloadSceneAsync("Login Scene");
            }
        }
        else
        {
            statusText.text = "Invalid email or password.";
        }
    }

    //private IEnumerator LoginRequest(string email, string password)
    //{
    //    // Create JSON payload
    //    string jsonData = $"{{\"email\":\"{email}\",\"password\":\"{password}\"}}";
    //    byte[] jsonToSend = Encoding.UTF8.GetBytes(jsonData);

    //    // Set up the request
    //    UnityWebRequest request = new UnityWebRequest(loginURL, "POST");
    //    request.uploadHandler = new UploadHandlerRaw(jsonToSend);
    //    request.downloadHandler = new DownloadHandlerBuffer();
    //    request.SetRequestHeader("Content-Type", "application/json");

    //    // Send request and wait for response
    //    yield return request.SendWebRequest();

    //    if (request.result == UnityWebRequest.Result.Success)
    //    {
    //        // Parse the response (assuming JSON format)
    //        string responseText = request.downloadHandler.text;
    //        Debug.Log("Login Response: " + responseText);

    //        // Handle successful login
    //        statusText.text = "Login successful!";

    //        if (SceneManager.GetSceneByName("Login Scene").isLoaded)
    //        {
    //            SceneManager.UnloadSceneAsync("Login Scene");
    //        }
    //    }
    //    else
    //    {
    //        // Handle error
    //        Debug.LogError("Login failed: " + request.error);
    //        statusText.text = "Invalid email or password.";
    //    }
    //}
}
