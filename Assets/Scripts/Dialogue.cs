/*
 *   Copyright (c) 2023 Az Foxxo
 *   All rights reserved.
 */
using System.Collections.Generic;
using UnityEngine;
using TMPro;


namespace UI.Dialogue
{
    public class Dialogue : MonoBehaviour
    {
        // Unity uses a really old version of C# so new() won't work
        private List<string> dialogueList = new List<string>(); // Dialogue text
        private bool dialogueActive = false; // Is the dialogue system active
        private float timeLast; // Last time
        private uint currentPosition = 0; // The current position of the message currently being displayed
        private uint currentMessage = 0; // The message to display


        [Header("Dialogue variables")]
        [SerializeField] private TMP_Text textComponent; // TMP text (component)
        [SerializeField] private GameObject dialogueSystemObject; // Used to enable/disable based on if the dialogue is active or not
        [SerializeField][Range(.01f, 2f)] private float timeDelayForEachCharacter; // Time between each character

        // Store a reference to the text component on the TMP
        private void Awake()
        {
            // Check if component is null
            if (textComponent == null) { Debug.LogError("Dialogue TMP_Text not found on gameobject"); return; }

            // Set text to an empty string
            textComponent.text = "";

            // TODO: Remove this (testing only)
            // Add some example dialogue to the test the system
            AddDialogue("This is an example message.");
            AddDialogue("The dialogue system should now hide itself...");
            AddDialogue("now");
            SetDialogueActive();
        }

        // Set the last time to the current time
        private void Start() => timeLast = Time.time;

        private void Update()
        {
            // Show/hide dialogue
            if (!dialogueActive)
            {
                // Hide the dialogue
                if (dialogueSystemObject.activeSelf) dialogueSystemObject.SetActive(false);

                // Return
                return;
            }
            else
            {
                // Show the dialogue
                if (!dialogueSystemObject.activeSelf) dialogueSystemObject.SetActive(true);
            }

            // Show the next character
            if (timeLast + timeDelayForEachCharacter >= Time.time)
            {
                if (currentPosition != dialogueList[(int)currentMessage].Length)
                {
                    // Add the next character
                    var toAdd = dialogueList[(int)currentMessage].ToCharArray();

                    Debug.Log(toAdd[currentPosition]);

                    textComponent.text += toAdd[currentPosition];

                    // Increment current position
                    currentPosition++;

                    // Reset last time
                    timeLast = Time.time;
                }
            }

            // If space bar pressed, advance the dialogue if at the end of the line, else display all the text at once
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (currentPosition == dialogueList[(int)currentMessage].Length)
                {
                    // Advance the dialogue to the next set of lines

                    // Increase current message
                    currentMessage++;

                    // Reset the current position
                    currentPosition = 0;

                    // Reset the dialogue to an empty string
                    textComponent.text = "";

                    // If no dialogue is left, set active to false
                    if (currentMessage > dialogueList.Count)
                    {
                        // Clear the list
                        dialogueList.Clear();

                        // Reset the position and messages
                        currentMessage = 0;
                        currentPosition = 0;

                        // Hide the dialogue system
                        dialogueActive = false;
                    }

                    Debug.Log($"Advanced! Current message ID {currentMessage} with the value {dialogueList[(int)currentMessage]}");
                }
            }
        }

        /// <summary> Add a piece of dialogue to the dialogue system </summary>
        public void AddDialogue(string newDialogue = "No message set!") => dialogueList.Add(newDialogue);


        /// <summary> Activate/deactivate the dialogue system </summary>
        public void SetDialogueActive(bool state = true) => dialogueActive = state;
    }
}