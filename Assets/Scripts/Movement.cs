using UnityEngine;

/// <summary>
/// De movement klasse bepaald de snelheid, richting, hitbox en obstakellaag.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    public float speed = 8f;
    public float speedMultiplier = 1f;
    public Vector2 initialDirection;
    public LayerMask obstacleLayer;

    public Rigidbody2D Rigidbody { get; private set; }
    public Vector2 direction { get; private set; }
    public Vector2 nextDirection { get; private set; }
    public Vector3 startingPosition { get; private set; }

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        startingPosition = transform.position;
    }

    private void Start()
    {
        ResetState();
    }
    
    /// <summary>
    /// Wijzigt alle waardes terug naar originele staat.
    /// </summary>
    public void ResetState()
    {
        speedMultiplier = 1f;
        direction = initialDirection;
        nextDirection = Vector2.zero;
        transform.position = startingPosition;
        enabled = true;
    }

    private void Update()
    {
        // Try to move in the next direction while it's queued to make movements
        // more responsive
        if (nextDirection != Vector2.zero)
        {
            SetDirection(nextDirection);
        }
    }

    private void FixedUpdate()
    {
        Vector2 position = Rigidbody.position;
        Vector2 translation = speed * speedMultiplier * Time.fixedDeltaTime * direction;

        Rigidbody.MovePosition(position + translation);
    }

    /// <summary>
    /// Wijzig richting van een GameObject
    /// </summary>
    /// <param name="direction">Nieuwe richting</param>
    /// <param name="forced">Wijzigt richting zonder rekening te houden met obstakels</param>
    public void SetDirection(Vector2 direction, bool forced = false)
    {
        // Only set the direction if the tile in that direction is available
        // otherwise we set it as the next direction so it'll automatically be
        // set when it does become available
        if (forced || !Occupied(direction))
        {
            this.direction = direction;
            nextDirection = Vector2.zero;
        }
        else
        {
            nextDirection = direction;
        }
    }

    /// <summary>
    /// Controleert of een GameObject bijna tegen een muur aan loopt
    /// </summary>
    /// <param name="direction">Richting om te controleren of daar een muur is</param>
    /// <returns>true als er geen obstakels in de aangegeven richting is</returns>
    public bool Occupied(Vector2 direction)
    {
        // If no collider is hit then there is no obstacle in that direction
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.75f, 0f, direction, 0.5f, obstacleLayer);
        return hit.collider != null;
    }

}