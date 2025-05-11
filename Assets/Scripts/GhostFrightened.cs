using UnityEngine;

/// <summary>
/// In deze klasse wordt bepaald hoe de geest reageert in frightened modus.
/// </summary>
public class GhostFrightened : GhostBehavior
{
    public SpriteRenderer body;
    public SpriteRenderer eyes;
    public SpriteRenderer blue;
    public SpriteRenderer white;

    private bool eaten;

    /// <summary>
    /// Wordt aangeroepen wanneer de geest de frightened-modus in gaat. 
    /// Verlaagt de snelheid van de geest, past de sprites aan, en start een timer om halverwege te gaan knipperen om aan te tonen dat de modus bijna is afgelopen.
    /// </summary>
    public override void OnEnter()
    {
        blue.GetComponent<AnimatedSprite>().Restart();
        Ghost.Movement.speedMultiplier = 0.5f;
        eaten = false;

        body.enabled = false;
        eyes.enabled = false;
        blue.enabled = true;
        white.enabled = false;

        Invoke(nameof(Flash), Duration / 2f);
    }

    /// <summary>
    /// Wordt aangeroepen wanneer de frightened-modus eindigt en Herstelt de originele snelheid en sprites van de geest.
    /// </summary>
    public override void OnExit()
    {
        Ghost.Movement.speedMultiplier = 1f;

        body.enabled = true;
        eyes.enabled = true;
        blue.enabled = false;
        white.enabled = false;
    }

    /// <summary>
    /// Wordt aangeroepen wanneer de geest opgegeten wordt door Pac-Man.
    /// </summary>
    public void Eaten()
    {
        eaten = true;

        body.enabled = false;
        eyes.enabled = true;
        blue.enabled = false;
        white.enabled = false;

        CancelInvoke();
    }

    /// <summary>
    /// Bepaalt de bewegingsrichting van de geest in frightened-modus.
    /// De geest kiest de richting die hem het verst van Pac-Man vandaan brengt.
    /// </summary>
    /// <param name="gameObject"></param>
    public override void Move(GameObject gameObject)
    {
        if (gameObject.TryGetComponent<Node>(out var node))
        {
            Vector2 direction = Vector2.zero;
            float maxDistance = float.MinValue;

            // Find the available direction that moves farthest from pacman
            foreach (Vector2 availableDirection in node.AvailableDirections)
            {
                Vector3 newPosition = transform.position + new Vector3(availableDirection.x, availableDirection.y);
                float distance = (Ghost.Target.position - newPosition).sqrMagnitude;

                if (distance > maxDistance)
                {
                    direction = availableDirection;
                    maxDistance = distance;
                }
            }

            Ghost.Movement.SetDirection(direction);
        }
    }

    private void Flash()
    {
        if (!eaten)
        {
            blue.enabled = false;
            white.enabled = true;
            white.GetComponent<AnimatedSprite>().Restart();
        }
    }
}
