using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject gameOverPanel;
    public TextMeshProUGUI highScoreText;
    public GameObject explosionPrefab;
    public ObstacleSpawner obstacleSpawner;
    public ScrollingBackground scrollingBackground;
    public GameObject pauseButton;

    public bool isGameOver;

    void Awake()
    {
        Instance = this;
        Application.targetFrameRate = 90;
    }

    public void TriggerGameOver(Vector3 position)
    {
        if (isGameOver) return;
        isGameOver = true;

        ScoreManager.Instance.StopScore();

        if (obstacleSpawner != null) obstacleSpawner.enabled = false;
        if (scrollingBackground != null) scrollingBackground.enabled = false;
        if (pauseButton != null) pauseButton.SetActive(false);

        if (explosionPrefab != null)
            Instantiate(explosionPrefab, position, Quaternion.identity);

        if (PlayerController.Instance != null)
            PlayerController.Instance.gameObject.SetActive(false);

        StartCoroutine(GameOverRoutine());
    }

    private IEnumerator GameOverRoutine()
    {
        yield return new WaitForSecondsRealtime(0.8f);
        int finalScore = ScoreManager.Instance.GetScore();
        gameOverPanel.SetActive(true);
        highScoreText.text = "HIGHSCORE: ...";
        SaveAndShowHighScore(finalScore);
    }

    void SaveAndShowHighScore(int score)
    {
        FirebaseUser user = FirebaseAuth.DefaultInstance.CurrentUser;
        if (user == null)
        {
            highScoreText.text = "HIGHSCORE: " + score;
            return;
        }

        DatabaseReference dbRef = FirebaseDatabase.DefaultInstance.RootReference;
        string userId = user.UserId;

        dbRef.Child("Users").Child(userId).Child("highscore").GetValueAsync()
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    int existingHigh = 0;

                    if (snapshot.Exists)
                        existingHigh = int.Parse(snapshot.Value.ToString());

                    if (score > existingHigh)
                    {
                        existingHigh = score;
                        dbRef.Child("Users").Child(userId).Child("highscore")
                            .SetValueAsync(existingHigh);
                    }

                    highScoreText.text = "HIGHSCORE: " + existingHigh;
                }
            });
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(2);
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }
}