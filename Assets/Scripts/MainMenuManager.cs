using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase.Auth;

public class MainMenuManager : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(2);
    }

    public void Logout()
    {
        FirebaseAuth.DefaultInstance.SignOut();
        SceneManager.LoadScene(0);
    }
}