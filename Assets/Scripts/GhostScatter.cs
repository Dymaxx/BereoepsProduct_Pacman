using UnityEngine;

/// <summary>
/// hier wordt bepaald hoe de geest reageert in scatter modus.
/// </summary>
public class GhostScatter : GhostBehavior
{
    /// <summary>
    /// bepaald de richting waar de geest heen moet lopen in scatter modus.
    /// </summary>
    /// <param name="gameObject"></param>
    public override void Move(GameObject gameObject)
    {
        if (gameObject.TryGetComponent<Node>(out var node))
        {
            int index = Random.Range(0, node.AvailableDirections.Count);

            // Prefer not to go back the same direction so increment the index to the next available direction
            if (node.AvailableDirections.Count > 1 && node.AvailableDirections[index] == -Ghost.Movement.direction)
            {
                index++;

                // Wrap the index back around if overflowed
                if (index >= node.AvailableDirections.Count)
                {
                    index = 0;
                }
            }

            Ghost.Movement.SetDirection(node.AvailableDirections[index]);
        }
    }
}