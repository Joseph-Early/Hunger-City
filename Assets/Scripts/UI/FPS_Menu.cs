using UnityEngine;
using UnityEngine.SceneManagement;

public class FPS_Menu : MonoBehaviour
{
    public static void StartGame() => SceneManager.LoadScene("GameLevel");
    public static void QuitGame() => Application.Quit();
}
