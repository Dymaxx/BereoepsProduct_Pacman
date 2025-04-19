using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Ghost[] ghosts;
    public Pacman pacman;
    public Transform pellets;

    public int Score { get; private set; }
    public int Lives { get; private set; }

    
    private void Start(){
        NewGame();
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
        for(int i = 0; i < this.ghosts.Length; i++){
            this.ghosts[i].gameObject.SetActive(true);
        }

        this.pacman.gameObject.SetActive(true);
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
    }

    private void SetScore(int score){
        this.Score = score;
    }

    private void SetLives(int lives){
        this.Lives = lives;
    }

    public void GhostEaten(Ghost ghost){
        SetScore(this.Score + ghost.points);
    }

    public void PacManEaten(){
        this.pacman.gameObject.SetActive(false);
        SetLives(this.Lives - 1);

        if(this.Lives > 0){
            Invoke(nameof(ResetState), 3.0f);
        }else{
            GameOver();
        }
    }


}
