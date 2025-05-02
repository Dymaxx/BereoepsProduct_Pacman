using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Ghost[] ghosts;
    public Pacman pacman;
    public Transform pellets;
    public ScoreManager scoreManager;
    public LifeManager lifeManager;  // Voeg deze lijn toe voor de LifeManager
    public int ghostMultiplier { get; private set; } = 1;

    public int Score { get; private set; }
    public int Lives { get; private set; }

    private void Start()
    {
        NewGame();
    }

    private void Update()
    {
        if (this.Lives <= 0 && Input.anyKeyDown)
        {
            NewGame();
        }
    }

    private void NewRound()
    {
        foreach (Transform pellet in this.pellets)
        {
            pellet.gameObject.SetActive(true);
        }
        // Start het ghost-geluid als het spel begint
        GhostAudioManager.Instance.SetMode("scatter");    // Voor de scatter-modus


        ResetState();
    }

    private void ResetState()
    {
        ResetGhostMultiplier();

        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].ResetState();
        }

        this.pacman.ResetState();
        pacman.PlayEatPelletSound();
    }

    private void GameOver()
    {
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].gameObject.SetActive(false);
        }

        this.pacman.gameObject.SetActive(false);
    }

    private void NewGame()
    {
        SetScore(0);
        scoreManager.ResetScore(); // Reset ook UI
        SetLives(3);  // Zet het aantal levens op 3
        NewRound();
    }

    private void SetScore(int score)
    {
        int difference = score - this.Score;
        this.Score = score;

        if (scoreManager != null && difference > 0)
        {
            scoreManager.AddPoints(difference);
        }
    }

    private void SetLives(int lives)
    {
        this.Lives = lives;

        // Controleer of lifeManager niet null is en update de UI
        if (lifeManager != null)
        {
            lifeManager.UpdateLives(Lives);  // Update de levens in de UI
        }
    }
    public void GhostEaten(Ghost ghost)
    {
        int points = (ghost.points * this.ghostMultiplier);
        SetScore(this.Score + points);
        this.ghostMultiplier++;
    }

    public void PacManEaten()
    {
        pacman.StopMovement();
        StopAllGhosts();

        pacman.PlayDeathSound();
        pacman.PlayDeathAnimation();

        float soundDuration = pacman.deathSound.length;
        Invoke(nameof(DeactivatePacman), soundDuration);

        if (this.Lives > 0)
        {
            Invoke(nameof(ResetState), soundDuration + 0.5f);
        }
        else
        {
            Invoke(nameof(GameOver), soundDuration + 0.5f);
        }

        SetLives(this.Lives - 1);  // Trek een leven af

    }

    private void DeactivatePacman()
    {
        pacman.gameObject.SetActive(false);
    }

    private void StopAllGhosts()
    {
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].Movement.SetDirection(Vector2.zero);
            this.ghosts[i].enabled = false;
        }
    }

    private void HidePacman()
    {
        pacman.gameObject.SetActive(false);
    }

    public void PelletEaten(Pellet pellet)
    {
        pellet.gameObject.SetActive(false);
        SetScore(this.Score + pellet.points);

        if (!HasRemainingPellets())
        {
            this.pacman.gameObject.SetActive(false);
            Invoke(nameof(NewRound), 3.0f);
        }
    }

    public void PowerPelletEaten(PowerPellet pellet)
    {
        // Zet de frightened modus voor alle ghosts aan
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].Frightened.Enable(pellet.duration);
        }

        // Speel de frightened geluid af
        GhostAudioManager.Instance.SetMode("frightened");

        // Zet een timer om terug naar scatter modus te gaan na de duur van de pellet
        CancelInvoke(); // Zorg ervoor dat je geen eerdere invokes hebt
        Invoke(nameof(ResetToScatter), pellet.duration); // Zet een Invoke aan voor de reset
        PelletEaten(pellet); // Het eten van de pellet verwerken
    }
    private void ResetToScatter()
    {
        

        // Stop het frightened geluid en start scatter geluid
        GhostAudioManager.Instance.SetMode("scatter");
    }

    private bool HasRemainingPellets()
    {
        foreach (Transform pellet in this.pellets)
        {
            if (pellet.gameObject.activeSelf)
            {
                return true;
            }
        }
        return false;
    }

    private void ResetGhostMultiplier()
    {
        this.ghostMultiplier = 1;
    }
}
