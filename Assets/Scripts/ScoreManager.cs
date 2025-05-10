using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public List<TextMeshProUGUI> scoreTexts;
    public List<TextMeshProUGUI> highScoreTexts;

    private int score = 0;
    private int highScore = 0;

    private void Awake()
    {
        // Zorg dat dit de enige instance is
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // voorkom duplicaten
        }
    }

    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateUI();
    }

    public void AddPoints(int points)
    {
        score += points;
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore); // <-- sleutel was leeg!
            PlayerPrefs.Save();
        }
        UpdateUI();
    }

    private void UpdateUI()
    {
        foreach (var text in scoreTexts)
        {
            if (text != null)
                text.text = score.ToString();
        }

        foreach (var text in highScoreTexts)
        {
            if (text != null)
                text.text = highScore.ToString();
        }
    }

    public void ResetScore()
    {
        score = 0;
        UpdateUI();
    }
}
