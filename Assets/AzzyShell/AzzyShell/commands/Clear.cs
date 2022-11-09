using Heroes;

namespace App.Commands {

public class Clear : Command
{
    public override int Execute(string[] args)
    {
        // Check if args given
        if (CheckArgLength(args, 1) != 0) return 2;

        // Translate variables
        args = VariableTranslation(args);

        // Clear the console
        // Check for errors (shouldn't be any)
        // try
        // {
        //     // Console.Clear();
        // }
        // catch (Exception e)
        // {
        //     // Print the error
        //     Print($"Exception occurred while clearing the console", Colours.Red);
        //     Print(e.Message, Colours.Red);

        //     // Return error - 454 for no command
        //     return 454; // special code for dotnet command failure
        // }


        // Return success
        return 0;
    }
}
}