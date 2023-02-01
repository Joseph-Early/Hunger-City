using System;
using UnityEngine;

public class FPS_Pause : MonoBehaviour
{
    public static bool paused = false;
    [SerializeField] GameObject PauseUI;

    // Awake method for deactivating the pause menu
    private void Awake()
    {
        // Disable the pause UI
        PauseUI.SetActive(false);
    }


    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        // Deactivate the pause menu
        PauseUI.SetActive(false);

        // Lock the cursor
        Cursor.lockState = CursorLockMode.Locked;

        // Resume time 
        Time.timeScale = 1f;

        // Set paused to false
        paused = false;
    }

    public void Pause()
    {
        // Acrivate the pause menu
        PauseUI.SetActive(true);

        // Lock the cursor
        Cursor.lockState = CursorLockMode.None;

        // Resume time 
        Time.timeScale = 0f;

        // Set pause to true
        paused = true;
    }
}
