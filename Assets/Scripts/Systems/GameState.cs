using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Globals = HungerCity.Systems.Globals;

namespace HungerCity.Systems
{
    public class GameState : MonoBehaviour
    {
        public static GameState Instance;

        // Set the game state instance
        private void Awake() => Instance = this;

        // Time remaining
        public float timeRemaining;

        // Get the time remaining
        public void Start() => timeRemaining = Globals.Instance.timeToSurvive;

        // Dialogue is active for win condition
        public bool winDialogueActive = false;

        // Update time remaining and check if player has won
        private void Update()
        {
            // If dialogue is active, don't update the time remaining
            if (Globals.Instance.dialogueSystem.DialogueIsActive) return;

            // If win dialogue is active, restart the game
            if (winDialogueActive)
            {
                // Load the title screen
                SceneManager.LoadScene(0);
            }

            timeRemaining -= Time.deltaTime;

            if (timeRemaining <= 0)
            {
                // Dialogue to add
                string[] dialogue = {
                    "You managed to survive long enough for your city to to have enacted UBI and rent control so you can afford food!",
                    "Policies such as price caps, Universal Basic Income, and rent control are all policies that can help to reduce food insecurity.",
                    "These policies are not a silver bullet, but they can help to reduce food insecurity and hunger in your city even under capitalism.",
                    "The game will now restart."                    
                    };

                // Add messages to the dialogue system
                foreach (var message in dialogue)
                {
                    Globals.Instance.dialogueSystem.AddDialogue(message);
                }

                // Set the dialogue system to active
                Globals.Instance.dialogueSystem.SetDialogueActive();

                winDialogueActive = true;
            }
        }
    }
}
