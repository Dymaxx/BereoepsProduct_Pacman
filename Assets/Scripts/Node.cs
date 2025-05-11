using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// kijkt welke richting je op kunt lopen bij een intersectie vanaf een bepaald punt.
/// </summary>
public class Node : MonoBehaviour
{
    public LayerMask obstacleLayer;
    public List<Vector2> AvailableDirections {  get; private set; }

    private void Start()
    {
        this.AvailableDirections = new List<Vector2>();
        CheckAvailableDirection(Vector2.up);
        CheckAvailableDirection(Vector2.down);
        CheckAvailableDirection(Vector2.left);
        CheckAvailableDirection(Vector2.right);
    }

    private void CheckAvailableDirection(Vector2 direction)
    {
        // If no collider is hit then there is no obstacle in that direction
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.5f, 0f, direction, 1.0f, obstacleLayer);

        if(hit.collider == null)
        {
            this.AvailableDirections.Add(direction);
        }
    }

}
