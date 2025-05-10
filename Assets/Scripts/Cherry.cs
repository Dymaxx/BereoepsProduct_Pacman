using UnityEngine;

namespace Assets.Scripts
{
    public class Cherry : MonoBehaviour, IEatable
    {
        [SerializeField]
        private int score = 100;

        public void Eaten()
        {
            gameObject.SetActive(false);
            ScoreManager.Instance.AddPoints(score);
        }
    }
}
