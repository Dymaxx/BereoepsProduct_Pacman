using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class LevelManager : MonoBehaviour
{
    public int currentLevel = 1;
    public List<Sprite> levelIcons;        // Sleep hier je fruiticoontjes in, op volgorde
    public GameObject iconPrefab;          // Sleep hier je prefab in
    public Transform iconContainer;        // Sleep hier je LevelIconBar (de Horizontal Layout Group)

    void Start()
    {
        UpdateLevelIcons();
    }

    public void NextLevel()
    {
        currentLevel++;
        UpdateLevelIcons();
    }

    public void UpdateLevelIcons()
    {
        // Verwijder oude iconen
        foreach (Transform child in iconContainer)
        {
            Destroy(child.gameObject);
        }

        // Toon max 7 iconen, van huidige en voorgaande levels
        int startLevel = Mathf.Max(1, currentLevel - 6);

        for (int i = startLevel; i <= currentLevel; i++)
        {
            GameObject icon = Instantiate(iconPrefab, iconContainer);
            Image image = icon.GetComponent<Image>();

            if (i - 1 < levelIcons.Count)
            {
                image.sprite = levelIcons[i - 1];
            }
            else
            {
                image.sprite = levelIcons.Last(); // Gebruik laatste sprite (bv. sleutel) voor hoge levels
            }
        }
    }
}