using UnityEngine;

namespace Assets.Scripts
{
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