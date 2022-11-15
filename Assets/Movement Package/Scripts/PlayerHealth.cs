using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : FPS_Health
{
    // When the player dies, reload the game scene
    internal override void Kill() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
}
