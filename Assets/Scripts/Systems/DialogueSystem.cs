using TMPro;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    public bool DialogueIsActive {get; private set; } = false; // Is the dialogue system active
    private float timeLast;

    private uint positionInMessage, messageInMessages;

    [Header("Dialogue variables")]
    [SerializeField] private TMP_Text textComponent; // TMP text (component)
    [SerializeField] private GameObject dialogueSystemToHide; // Used to enable/disable based on if the dialogue is active or not
    [SerializeField][Range(.01f, .1f)] private float timeDelayForEachCharacter; // Time between each character
    [SerializeField] private bool startEnabled; // Whether to start enabled or not
    [SerializeField] private string[] messages; // Dialogue text
    [SerializeField] private GameObject[] ObjectsToHide; // Objects to hide when dialogue is active

    // Start is called before the first frame update
    void Awake()
    {
        // Check if component is null
        if (textComponent == null) { Debug.LogError("Dialogue TMP_Text not found on game object!"); return; }

        // Check of dialogue system is null
        if (dialogueSystemToHide == null) { Debug.LogError("Dialogue system not set!"); return; }

        // Set text to an empty string
        textComponent.text = "";

        // Set the initial time
        timeLast = Time.time;

        if (startEnabled) SetDialogueActive();
    }

    public void SetDialogueActive(bool MakeActive = true) {
        if (messages.Length != 0) DialogueIsActive = MakeActive;

        // Set objects to hide, if dialogue is active, else set them to active
        if (ObjectsToHide != null)
        {
            foreach (var obj in ObjectsToHide)
            {
                obj.gameObject.SetActive(!MakeActive);
            }
        }
    }

    public void AddDialogue(string message)
    {
        if (messages == null)
        {
            messages = new string[1];
            messages[0] = message;
        }
        else
        {
            // Increase array size by one and add dialogue
            var newMessages = new string[messages.Length + 1];

            // Copy over old values
            int i = 0;
            foreach (var msg in messages)
            {
                newMessages[i] = msg;
                i++;
            }

            // Add the new value
            newMessages[i] = message;

            // Set messages to new messages
            messages = newMessages;
        }

        // Print all the messages
        foreach (var msg in messages)
        {
            print($"Message {msg}");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Activate
        if (DialogueIsActive)
        {
            dialogueSystemToHide.SetActive(true);
        }
        else
        {
            // Reset position and current display
            positionInMessage = 0;
            textComponent.text = "";

            // De-activate
            dialogueSystemToHide.SetActive(false);

            return;
        }


        // Check if the message is not complete, add the next char every timeDelayForEachCharacter
        if (timeLast + timeDelayForEachCharacter < Time.time)
        {
            if (positionInMessage < messages[messageInMessages].Length)
            {
                // Add the character
                var charArray = messages[messageInMessages].ToCharArray();
                textComponent.text += charArray[positionInMessage];
                positionInMessage++;
            }

            // Reset the last time
            timeLast = Time.time;
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            if (positionInMessage < messages[messageInMessages].Length)
            {
                // Display full message
                textComponent.text = messages[messageInMessages];
                positionInMessage = (uint)messages[messageInMessages].Length;
            }
            else
            {
                // Advance to next message
                textComponent.text = "";
                positionInMessage = 0;
                messageInMessages++;

                // If dialogue has completed, delete all dialogue and hide the system
                if (messageInMessages >= messages.Length)
                {
                    // Hide the dialogue
                    SetDialogueActive(false);

                    // Reset variables
                    textComponent.text = "";
                    positionInMessage = 0;
                    messageInMessages = 0;

                    // Set array to null
                    messages = null;
                }
            }
        }
    }
}
