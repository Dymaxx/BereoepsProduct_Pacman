using UnityEngine;

public class PowerPellet : Pellet
{
    public float Duration = 8.0f;

    public override void Eaten()
    {
        base.Eaten();
        FindFirstObjectByType<GameManager>().PowerPelletEaten(this);
    }
}