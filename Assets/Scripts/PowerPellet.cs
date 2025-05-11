using UnityEngine;

/// <summary>
/// De powerpellet geeft punten zoals een normale pellet, maar dit activeert ook een powerup.
/// </summary>
public class PowerPellet : Pellet
{
    public float Duration = 8.0f;

    /// <summary>
    /// Voer standaard Eaten() functie uit en activeert de powerup.
    /// </summary>
    public override void Eaten()
    {
        base.Eaten();
        FindFirstObjectByType<GameManager>().PowerPelletEaten(this);
    }
}