using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using TMPro;
using UnityEngine.SceneManagement;

public class FirebaseManager : MonoBehaviour
{
    [Header("Login")]
    public TMP_InputField loginEmail;
    public TMP_InputField loginPassword;

    [Header("Signup")]
    public TMP_InputField signupUsername;
    public TMP_InputField signupEmail;
    public TMP_InputField signupPassword;

    [Header("Panels")]
    public GameObject loginPanel;
    public GameObject signupPanel;

    private FirebaseAuth _auth;
    private DatabaseReference _dbRef;
    private bool _firebaseReady;

    void Awake()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                _auth = FirebaseAuth.DefaultInstance;
                _dbRef = FirebaseDatabase.DefaultInstance.RootReference;
                _firebaseReady = true;
            }
            else
            {
                Debug.LogError("Firebase not available: " + task.Result);
            }
        });
    }

    public void ShowSignup()
    {
        loginPanel.SetActive(false);
        signupPanel.SetActive(true);
    }

    public void ShowLogin()
    {
        signupPanel.SetActive(false);
        loginPanel.SetActive(true);
    }

    public void Signup()
    {
        if (!_firebaseReady) return;

        string username = signupUsername.text;
        string email = signupEmail.text;
        string password = signupPassword.text;

        _auth.CreateUserWithEmailAndPasswordAsync(email, password)
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted || task.IsCanceled)
                {
                    Debug.LogError("Signup failed: " + task.Exception);
                    return;
                }

                FirebaseUser newUser = task.Result.User;
                UserData userData = new UserData(username, email);
                string json = JsonUtility.ToJson(userData);

                _dbRef.Child("Users").Child(newUser.UserId).SetRawJsonValueAsync(json)
                    .ContinueWithOnMainThread(dbTask =>
                    {
                        if (dbTask.IsCompleted)
                            SceneManager.LoadScene(1);
                    });
            });
    }

    public void Login()
    {
        if (!_firebaseReady) return;

        string email = loginEmail.text;
        string password = loginPassword.text;

        _auth.SignInWithEmailAndPasswordAsync(email, password)
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted || task.IsCanceled)
                {
                    Debug.LogError("Login failed: " + task.Exception);
                    return;
                }

                SceneManager.LoadScene(1);
            });
    }
}