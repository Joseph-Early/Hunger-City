using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// List of all global variables
// Must use Singleton pattern as [SerializeField] variables are not accessible in static classes
namespace HungerCity.Systems
{
    public class Globals : MonoBehaviour
    {
        public static Globals Instance; // Global instance

        public GameObject EnemyPrefab; // Prefab for enemy

        public float timeToSurvive = 10f; // Time to survive in seconds

        public DialogueSystem dialogueSystem; // Dialogue system

        // Set the global instance
        private void Awake() => Instance = this;
    }

}