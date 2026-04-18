using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance;
    public GameObject pausePanel;

    private bool _isPaused;

    void Awake()
    {
        Instance = this;
    }

    public void TogglePause()
    {
        if (GameManager.Instance.isGameOver) return;

        _isPaused = !_isPaused;

        if (_isPaused)
        {
            Time.timeScale = 0f;
            pausePanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            pausePanel.SetActive(false);
        }
    }

    public void Resume()
    {
        _isPaused = false;
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }

    public void Restart()
    {
        _isPaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(2);
    }

    public void GoToMenu()
    {
        _isPaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }
}