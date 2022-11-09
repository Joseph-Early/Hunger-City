using Heroes;

namespace App.Commands
{
    public class Log : Command
    {
        public override int Execute(string[] args)
        {
            // Check if args given
            if (CheckArgLength(args, 2) != 0) return 2;

            // Translate variables
            args = VariableTranslation(args);

            // Log the message
            Print(args[1]);

            // Return success
            return 0;
        }
    }
}