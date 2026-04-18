using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public TextMeshProUGUI scoreText;

    private float _score;
    private bool _isGameOver;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (!_isGameOver)
        {
            _score += Time.deltaTime * 10f;
            scoreText.text = "SCORE: " + (int)_score;
        }
    }

    public void StopScore() { _isGameOver = true; }

    public int GetScore() { return (int)_score; }
}