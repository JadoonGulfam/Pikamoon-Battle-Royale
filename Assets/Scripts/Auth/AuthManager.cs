using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;
public class AuthManager : MonoBehaviour
{
    public static AuthManager Instance;
    public UsersData LoggedInUser { get; private set; }
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public async Task<(bool, string)> SignUp(string username, string email, string password)
    {
        await Task.Delay(300); // simulate delay
                               // Check for empty fields
        if (string.IsNullOrWhiteSpace(username))
            return (false, "Username cannot be empty.");

        if (string.IsNullOrWhiteSpace(email))
            return (false, "Email cannot be empty.");

        if (!IsValidEmail(email))
            return (false, "Please enter a valid email address.");

        if (string.IsNullOrWhiteSpace(password))
            return (false, "Password cannot be empty.");

        if (LocalUserDatabase.UserNameExists(username))
            return (false, "Username already taken.");

        if (LocalUserDatabase.UserExists(email))
            return (false, "Email already registered.");

        LocalUserDatabase.AddUser(username, email, password);

        return (true, "Account created successfully!");
    }
    // --- Email format validation ---
    private bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        // Basic but robust regex for common email formats
        string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
    }
    public async Task<(bool, string)> Login(string email, string password, bool rememberMe = false)
    {
        await Task.Delay(300);
        bool valid = LocalUserDatabase.ValidateUser(email, password);
        if (!valid) return (false, "Invalid email or password.");

        if (rememberMe)
            AutoLoginManager.Save(email, password);
        else
            AutoLoginManager.Clear();

        return (true, "Login successful!");
    }

    public async Task<(bool, string)> ResetPassword(string email, string newPassword)
    {
        await Task.Delay(300);
        if (!LocalUserDatabase.UserExists(email))
            return (false, "No account found with that email.");

        LocalUserDatabase.ResetPassword(email, newPassword);
        return (true, "Password has been reset successfully!");
    }
    // Automatically tries to log in using autologin.json
    public async Task<(bool, string)> TryAutoLogin()
    {
        var saved = AutoLoginManager.Load();
        if (saved == null)
            return (false, "No saved login.");

        var (login, passwordHash) = saved.Value;
        var db = LocalUserDatabase.LoadDatabase();
        bool found = db.users.Exists(u =>
            (u.email.Equals(login, System.StringComparison.OrdinalIgnoreCase)
            || u.username.Equals(login, System.StringComparison.OrdinalIgnoreCase))
            && u.passwordHash == passwordHash);

        if (!found)
        {
            AutoLoginManager.Clear();
            return (false, "Saved login invalid.");
        }

        await Task.Delay(100);
        return (true, "Auto login successful!");
    }
    public async Task<(bool ok, string msg)> GuestLogin()
    {
        string guestID = "Guest_" + UnityEngine.Random.Range(10000, 99999);

        LoggedInUser = new UsersData
        {
            username = guestID,
            email = "",
            passwordHash = "",
            isGuest = true
        };

        // No auto-login saving for guests unless you want to add it.
        return (true, "Logged in as " + guestID);
    }
    public void Logout()
    {
        AutoLoginManager.Clear();
    }
}
