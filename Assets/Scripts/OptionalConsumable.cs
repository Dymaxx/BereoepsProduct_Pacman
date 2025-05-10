using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class OptionalConsumable : MonoBehaviour, IEatable
    {
        [SerializeField]
        private int score = 100;

        public void Eaten()
        {
            ScoreManager.Instance.AddPoints(score);
        }
    }
}
