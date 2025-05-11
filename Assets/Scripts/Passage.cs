using UnityEngine;

/// <summary>
/// Zorgt ervoor dat je tussen de buizen kan teleporteren naar de andere kant van de map.
/// </summary>
public class NewMonoBehaviourScript : MonoBehaviour
{
    public Transform connection;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Vector3 position = other.transform.position;
        position.x = this.connection.position.x;
        position.y = this.connection.position.y;
        other.transform.position = position;
    }
}
