using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    private int score = 0;
    private int highScore = 0;

    void Start()
    {
        // Laad high score van vorige sessie (optioneel)
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateUI();
    }

    public void AddPoints(int points)
    {
        score += points;
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("", highScore); // Sla op
        }
        UpdateUI();
    }

    private void UpdateUI()
    {
        scoreText.text = "" + score;
        highScoreText.text = "" + highScore;
    }

    public void ResetScore()
    {
        score = 0;
        UpdateUI();
    }
}