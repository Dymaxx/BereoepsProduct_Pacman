using System.ComponentModel;
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
    private bool isWhite = false;

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

        InvokeRepeating(nameof(ToggleFlash), Duration * 0.8f, 0.2f); // start flash na 80% en herhaal elke 0.2s
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
        CancelInvoke(nameof(ToggleFlash));
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

        CancelInvoke(nameof(ToggleFlash));
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
    private void ToggleFlash()
    {
        if (eaten) return;
        isWhite = !isWhite;

        blue.enabled = !isWhite;
        white.enabled = isWhite;
    }
}


