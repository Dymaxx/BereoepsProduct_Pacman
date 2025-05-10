using UnityEngine;

public class GhostChase : GhostBehavior
{
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