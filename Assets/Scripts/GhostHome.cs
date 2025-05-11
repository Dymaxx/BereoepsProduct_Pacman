using System.Collections;
using UnityEngine;

/// <summary>
/// Bepaalt het gedrag van een geest terwijl deze zich in het home gebied bevindt, de geest stuitert hier binnen de muren totdat hij wordt vrijgelaten.
/// Bevat ook de animatie om de geest vrij te laten vanuit het home gebied.
/// </summary>
public class GhostHome : GhostBehavior
{
    public Transform inside;
    public Transform outside;

    /// <summary>
    /// Wordt aangeroepen wanneer de geest naar huis gaat.
    /// </summary>
    public override void OnEnter()
    {
        StopAllCoroutines();
    }

    /// <summary>
    /// Wordt aangeroepen wanneer de geest het huis verlaat.
    /// </summary>
    public override void OnExit()
    {
        // Check for active self to prevent error when object is destroyed
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(ExitTransition());
        }
    }

    /// <summary>
    /// Bepaalt het gedrag van de geest terwijl hij binnen het huis is.
    /// Als hij een obstakel raakt, keert hij om, dit creëert het stuiter-effect.
    /// </summary>
    /// <param name="gameObject"></param>
    public override void Move(GameObject gameObject)
    {
        // Reverse direction everytime the ghost hits a wall to create the effect of the ghost bouncing around the home
        if (gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {   
            Ghost.Movement.SetDirection(-Ghost.Movement.direction);
        }
    }

    private IEnumerator ExitTransition()
    {
        // Turn off movement while we manually animate the position
        Ghost.Movement.SetDirection(Vector2.up, true);
        Ghost.Movement.Rigidbody.isKinematic = true;
        Ghost.Movement.enabled = false;

        Vector3 position = transform.position;

        float duration = 0.5f;
        float elapsed = 0f;

        // Animate to the starting point
        while (elapsed < duration)
        {
            Ghost.SetPosition(Vector3.Lerp(position, inside.position, elapsed / duration));
            elapsed += Time.deltaTime;
            yield return null;
        }

        elapsed = 0f;

        // Animate exiting the ghost home
        while (elapsed < duration)
        {
            Ghost.SetPosition(Vector3.Lerp(inside.position, outside.position, elapsed / duration));
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Pick a random direction left or right and re-enable movement
        Ghost.Movement.SetDirection(new Vector2(Random.value < 0.5f ? -1f : 1f, 0f), true);
        Ghost.Movement.Rigidbody.isKinematic = false;
        Ghost.Movement.enabled = true;
    }
}