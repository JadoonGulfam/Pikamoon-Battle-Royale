using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIAuthController : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject[] loginPanel;

    [Header("UI References Login")]  
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public Toggle rememberMeToggle;
    public Button loginButton;
    public Button resetPasswordButton;
    public TextMeshProUGUI feedbackTextLogin;

    [Header("UI References Sign Up")]
    public TMP_InputField firstnameInput;
    public TMP_InputField lastnameInput;
    public TMP_InputField signupEmailInput;
    public TMP_InputField SignupPasswordInput;
    public TextMeshProUGUI feedbackTextSignUp;
    public Button signupButton;

    private async void Start()
    {
        // Auto-login if saved
        //string saved = PlayerPrefsEncryptor.LoadDecrypted("autoLogin");
        //if (!string.IsNullOrEmpty(saved))
        //{
        //    var parts = saved.Split('|');
        //    var (ok, msg) = await AuthManager.Instance.Login(parts[0], parts[1]);
        //    if (ok) LoadNextScene();
        //}
        feedbackTextSignUp.text = "";
        feedbackTextLogin.text = "";
        loginButton.onClick.AddListener(async () =>
        {
            feedbackTextLogin.text = "Logging in...";
            var (ok, msg) = await AuthManager.Instance.Login(emailInput.text, passwordInput.text);
            feedbackTextLogin.text = msg;
            if (ok) LoadNextScene();
        });

        signupButton.onClick.AddListener(async () =>
        {
            feedbackTextSignUp.text = "Creating account...";

            // Combine first name and last name for username
            string username = firstnameInput.text.Trim() + " " + lastnameInput.text.Trim();

            var (ok, msg) = await AuthManager.Instance.SignUp(username, signupEmailInput.text.Trim(), SignupPasswordInput.text.Trim());
            feedbackTextSignUp.text = msg;
            if (ok) Showpanels(0);
        });

        resetPasswordButton.onClick.AddListener(async () =>
        {
            feedbackTextLogin.text = "Resetting password...";
            var (ok, msg) = await AuthManager.Instance.ResetPassword(emailInput.text, passwordInput.text);
            feedbackTextLogin.text = msg;
        });
    }

    private void LoadNextScene()
    {
        LoadingManager.Instance.ActivateLoadingScene("lobby new", "Splash_Loading", true);
    }

    public void Showpanels(int _index) 
    {
        foreach (GameObject _panels in loginPanel) 
        {
            _panels.SetActive(false);
        }
        loginPanel[_index].SetActive(true);
    }
}
