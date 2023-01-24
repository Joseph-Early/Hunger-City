#define GUI

using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : FPS_Health
{
    // When the player dies, reload the game scene
    internal override void Kill() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);


    // Kill the player when theyt fall below the scene
    void Update()
    {
        if (transform.position.y < -3 || health <= 0) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Update health
    void LateUpdate()
    {
        #if GUI
            FPS_GUI.Instance.textToDisplay += $"Health {Mathf.Round(health)}/100\n";
        #endif
    }
}
