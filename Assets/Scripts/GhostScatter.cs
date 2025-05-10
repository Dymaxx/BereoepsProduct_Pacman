using UnityEngine;

public class GhostScatter : GhostBehavior
{
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