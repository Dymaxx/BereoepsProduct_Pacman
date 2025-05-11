using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// Zorgt ervoor dat een snack kan worden opgegeten voor punten en op een later moment te spawnen in het spel.
    /// </summary>
    public class OptionalSnack : MonoBehaviour, IEatable
    {
        [SerializeField]
        private int score;

        [SerializeField]
        private Vector3 spawnPosition;

        [SerializeField]
        private float spawnInterval;

        private void Awake()
        {
            // Zet SpriteRenderer & collider uit
            if (gameObject != null)
            {
                if (gameObject.TryGetComponent<SpriteRenderer>(out var prefabRenderer))
                {
                    prefabRenderer.enabled = false;
                }

                if (gameObject.TryGetComponent<Collider2D>(out var prefabCollider))
                {
                    prefabCollider.enabled = false;
                }

                StartCoroutine(SpawnSnackAfterDelay());
            }
        }

        /// <summary>
        /// Regelt de acties als de snack is opgegeten. Je krijgt hier punten voor en de snack wordt weggehaald.
        /// </summary>
        public void Eaten()
        {
            gameObject.SetActive(false);
            ScoreManager.Instance.AddPoints(score);
        }

        /// <summary>
        /// activeert de sprite en de collision met de pacman.
        /// </summary>
        /// <returns></returns>
        public IEnumerator SpawnSnackAfterDelay()
        {
            yield return new WaitForSeconds(spawnInterval);

            GameObject newSnack = Instantiate(gameObject, spawnPosition, Quaternion.identity);

            // SpriteRenderer aanzetten
            if (newSnack.TryGetComponent<SpriteRenderer>(out var newRenderer))
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
 