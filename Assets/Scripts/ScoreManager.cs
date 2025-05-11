using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Centrale plek om score bij te houden. Dit regelt ook de score en high score UI.
/// </summary>
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public List<TextMeshProUGUI> scoreTexts;
    public List<TextMeshProUGUI> highScoreTexts;

    private int score = 0;
    private int highScore = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateUI();
    }

    /// <summary>
    /// Voeg bepaald aantal punten toe aan de totale score. Werkt highscore bij als de score hoger is dan de huidige highscore.
    /// </summary>
    /// <param name="points">Aantal punten toe te voegen.</param>
    public void AddPoints(int points)
    {
        score += points;
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
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

    /// <summary>
    /// Zet score terug naar 0.
    /// </summary>
    public void ResetScore()
    {
        score = 0;
        UpdateUI();
    }
}
