using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Centrale plek voor het bijhouden van levens. Deze klasse zorgt ook voor het laten zien van de levens UI.
/// </summary>
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

    /// <summary>
    /// Functie voor het bijwerken van levens. Dit update ook de UI.
    /// </summary>
    /// <param name="lives"></param>
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

    /// <summary>
    /// Zet het aantal levens terug naar het oorspronkelijke aantal.
    /// </summary>
    public void ResetLives()
    {
        Lives = startingLives;
    }
}
