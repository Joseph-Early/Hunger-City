using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : FPS_Health
{
    // Return the player's health
    public float GetHealth() => health;

    // When the player dies, reload the game scene
    internal override void Kill() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);


    // Kill the player when they fall below the scene
    void Update()
    {
        if (transform.position.y < -3 || health <= 0) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
