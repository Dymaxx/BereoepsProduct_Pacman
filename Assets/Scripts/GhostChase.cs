using UnityEngine;

/// <summary>
/// Bepaalt het gedrag van een geest in "chase"-modus, hier zullen de geesten de snelste route proberen te vinden naar de pacman.
/// </summary>
public class GhostChase : GhostBehavior
{
    /// <summary>
    /// De geest kiest de richting die hem het dichtst bij Pac-Man brengt, gebaseerd op alle beschikbare richtingen van de huidige node.
    /// </summary>
    /// <param name="gameObject"></param>
    public override void Move(GameObject gameObject)
    {
        if (gameObject.TryGetComponent<Node>(out var node))
        {
            Vector2 direction = Vector2.zero;
            float minDistance = float.MaxValue;

            // Find the available direction that moves closet to pacman
            foreach (Vector2 availableDirection in node.AvailableDirections)
            {
                // If the distance in this direction is less than the current min distance then this direction becomes the new closest
                Vector3 newPosition = transform.position + new Vector3(availableDirection.x, availableDirection.y);
                float distance = (Ghost.Target.position - newPosition).sqrMagnitude;

                if (distance < minDistance)
                {
                    direction = availableDirection;
                    minDistance = distance;
                }
            }

            Ghost.Movement.SetDirection(direction);
        }
    }
}