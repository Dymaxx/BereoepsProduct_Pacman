using UnityEngine;

/// <summary>
/// Stuurt de richting van de ogen van de geest aan, zodat deze kijken in de richting waarin de geest beweegt.
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
public class GhostEyes : MonoBehaviour
{
    public Sprite up;
    public Sprite down;
    public Sprite left;
    public Sprite right;

    private SpriteRenderer spriteRenderer;
    private Movement movement;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        movement = GetComponentInParent<Movement>();
    }

    private void Update()
    {
        if (movement.direction == Vector2.up)
        {
            spriteRenderer.sprite = up;
        }
        else if (movement.direction == Vector2.down)
        {
            spriteRenderer.sprite = down;
        }
        else if (movement.direction == Vector2.left)
        {
            spriteRenderer.sprite = left;
        }
        else if (movement.direction == Vector2.right)
        {
            spriteRenderer.sprite = right;
        }
    }

}