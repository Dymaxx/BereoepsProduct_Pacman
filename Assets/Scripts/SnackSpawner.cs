using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class SnackSpawner : MonoBehaviour
    {
        [SerializeField]
        private GameObject objectPrefab;

        [SerializeField]
        private Vector3 spawnPosition;

        [SerializeField]
        private float spawnDelay;

        private void Awake()
        {
            // Zet SpriteRenderer op prefab tijdelijk uit (alleen renderen uitschakelen)
            if (objectPrefab != null)
            {
                if (objectPrefab.TryGetComponent<SpriteRenderer>(out var prefabRenderer))
                {
                    prefabRenderer.enabled = false;
                }

                if (objectPrefab.TryGetComponent<Collider2D>(out var prefabCollider))
                {
                    prefabCollider.enabled = false;
                }

                StartCoroutine(SpawnSnackAfterDelay());
            }
        }

        private IEnumerator SpawnSnackAfterDelay()
        {
            yield return new WaitForSeconds(spawnDelay);

            GameObject newSnack = Instantiate(objectPrefab, spawnPosition, Quaternion.identity);

            // SpriteRenderer aanzetten
            SpriteRenderer newRenderer = newSnack.GetComponent<SpriteRenderer>();
            if (newRenderer != null)
            {
                newRenderer.enabled = true;
            }

            // Collider weer activeren
            if (newSnack.TryGetComponent<Collider2D>(out var newCollider))
            {
                newCollider.enabled = true;
            }
        }
    }
}