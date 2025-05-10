using UnityEngine;
using UnityEngine.UI;
public class LifeManager : MonoBehaviour
{
    public static LifeManager Instance { get; private set; }

    public Image[] LifeImages;

    [SerializeField]
    private int startingLives;
    public int Lives { get; set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateLives(startingLives);
    }

    public void UpdateLives(int lives)
    {
        Lives = lives;

        for (int i = 0; i < LifeImages.Length; i++)
        {
            if (i < lives)
                LifeImages[i].enabled = true; 
            else
                LifeImages[i].enabled = false;
        }
    }

    public void ResetLives()
    {
        Lives = startingLives;
    }
}
