using Heroes;
namespace App.Commands {

public class Help : Command
{
    public override int Execute(string[] args)
    {
        // Check if args given
        if (CheckArgLength(args, 1) != 0) return 2;

        // Translate variables
        args = VariableTranslation(args);

        // Print list of commands
        Print("Commands:");
        Print("quit - Quit the app");
        Print("help - Show this help message");
        Print("clear - Clear the console");
        Print("log - Log a message to the console");
        Print("fizzbuzz - Print the FizzBuzz sequence up to a given number");
        Print("set - Set a variable");
        Print("vars - List all variables");
        Print("logo - Print the Azzy logo");

        // Return success
        return 0;
    }
}

}