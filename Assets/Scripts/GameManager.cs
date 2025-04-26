using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Ghost[] ghosts;
    public Pacman pacman;
    public Transform pellets;

    public int ghostMultiplier { get; private set; } = 1;

    public int Score { get; private set; }
    public int Lives { get; private set; }

    
    private void Start(){
        NewGame();
        pacman.PlayEatPelletSound();
    }

    private void Update(){
        if(this.Lives <= 0 && Input.anyKeyDown){
            NewGame();
        }
    }

    private void NewRound(){
        foreach (Transform pellet in this.pellets){
            pellet.gameObject.SetActive(true);
        }

        ResetState();
        
    }

    private void ResetState(){
        ResetGhostMultiplier();

        for(int i = 0; i < this.ghosts.Length; i++){
            this.ghosts[i].ResetState();
        }

        this.pacman.ResetState();
        pacman.PlayEatPelletSound();
    }

    private void GameOver(){
       for(int i = 0; i < this.ghosts.Length; i++){
            this.ghosts[i].gameObject.SetActive(false);
        }

        this.pacman.gameObject.SetActive(false); 
    }

    private void NewGame(){
        SetScore(0);
        SetLives(3);
        NewRound();
    }

    private void SetScore(int score){
        this.Score = score;
    }

    private void SetLives(int lives){
        this.Lives = lives;
    }

    public void GhostEaten(Ghost ghost){
        int points = (ghost.points * this.ghostMultiplier);
        SetScore(this.Score + points);
        this.ghostMultiplier++;
    }

    public void PacManEaten()
    {
        // Stop alle beweging
        pacman.StopMovement();
        StopAllGhosts();

        // Speel dood geluid af
        pacman.PlayDeathSound();

        // Pacman wordt gedeactiveerd pas na het geluid
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

        // Trek leven af
        SetLives(this.Lives - 1);
    }

    private void DeactivatePacman()
    {
        pacman.gameObject.SetActive(false);
    }

    private void StopAllGhosts()
    {
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].Movement.SetDirection(Vector2.zero); // Zet richting naar 0 (stil)
            this.ghosts[i].enabled = false; // Optioneel: helemaal Ghost script pauzeren
        }
    }
    private void HidePacman()
    {
        pacman.gameObject.SetActive(false);
    }

    public void PelletEaten(Pellet pellet)
    {
        pellet.gameObject.SetActive(false);
        SetScore(this.Score +  pellet.points);

        if (!HasRemainingPellets())
        {
            this.pacman.gameObject.SetActive(false); 
            Invoke(nameof(NewRound), 3.0f);
        }

    }

    public void PowerPelletEaten(PowerPellet pellet) {
        // TODO: Change ghost state
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].Frightened.Enable(pellet.duration);
        }

        PelletEaten(pellet);
        CancelInvoke();
        Invoke(nameof(ResetGhostMultiplier), pellet.duration);
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
