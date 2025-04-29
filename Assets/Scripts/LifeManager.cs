using UnityEngine;
using UnityEngine.UI;
public class LifeManager : MonoBehaviour
{
    public Image[] lifeImages; // Array van Image-componenten voor levens
    private int lives;

    // Update de UI met het huidige aantal levens
    public void UpdateLives(int lives)
    {
        this.lives = lives;

        // Zet alle levens uit
        for (int i = 0; i < lifeImages.Length; i++)
        {
            if (i < lives)
                lifeImages[i].enabled = true;  // Leven zichtbaar
            else
                lifeImages[i].enabled = false;  // Leven niet zichtbaar
        }
    }
}
