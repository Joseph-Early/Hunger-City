using UnityEngine;

namespace Heroes.Internal {
public class Entry : MonoBehaviour
{
    // Main loop
    public static GameLoop gameLoop = new GameLoop();

    // Init (Unity)
    private void Start() => Main(new string[2]);

    // Entry point
    public void Main(string[] args) => gameLoop.Enter();
}
}