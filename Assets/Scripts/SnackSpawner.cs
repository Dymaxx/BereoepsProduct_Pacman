using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// zorgt ervoor dat de alle snacks spawnen die zijn meegegeven aan het object 'SnackSpawner'.
    /// </summary>
    public class SnackSpawner : MonoBehaviour
    {
        [SerializeField]
        private OptionalSnack[] snackArray;

        private void Awake()
        {
            foreach (var snack in snackArray)
            {
                if (snack != null)
                {
                    StartCoroutine(snack.SpawnSnackAfterDelay());
                }
            }
        }
    }
}