using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HungerCity.Systems
{
    public class Globals : MonoBehaviour
    {
        public static Globals Instance;

        public GameObject EnemyPrefab;

        private void Awake()
        {
            Instance = this;
        }
    }

}