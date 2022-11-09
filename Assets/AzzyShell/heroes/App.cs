using Heroes.Internal;

namespace Heroes {

// Application class
public static class Application
{
    ///<summary>Quit the application.</summary>
    public static void Quit() => GameLoop.running = false;

    ///<summary>Restart the application.</summary>
    public static void Restart() => Entry.gameLoop.Restart();

}
}