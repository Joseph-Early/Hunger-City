using Heroes;
using System.Collections.Generic;

namespace App.Commands {

public class Set : Command
{
    public override int Execute(string[] args)
    {
        // Check if args given
        if (CheckArgLength(args, 3) != 0) return 2;

        // Translate variables
        args = VariableTranslation(args);

        // Flags for type, value and name
        string name = args[1];
        string value = args[2];
        string type = "";

        // Check if the variable name exists
        bool found = false;
        foreach (Variables variable in AzzyShell.GetInstance().variables) {
            if (variable.name == name) {
                found = true;
                break;
            }
        }

        // Check the variable is a number
        if (int.TryParse(value, out int intValue)) {
            type = "Int";
        }

        // Check the variable is a double
        else if (double.TryParse(value, out double doubleValue)) {
            type = "Double";
        }
        // Check the variable is a boolean
        else if (bool.TryParse(value, out bool boolValue)) {
            type = "Bool";
        }
        // Else it's a string
        else {
            type = "String";
        }

        // If the variable name doesn't exist, append the variable to the variables list
        if (!found) {
            AzzyShell.GetInstance().variables.Add(new Variables(name, value, type));
            return 0;
        }

        // Create an array copy of the variables list
        Variables[] variablesArrayCopy = AzzyShell.GetInstance().variables.ToArray();

        // Get the index of the variable to be reassigned
        int index = 0;
        for (int i = 0; i < variablesArrayCopy.Length; i++) {
            if (variablesArrayCopy[i].name == name) {
                index = i;
                break;
            }
        }

        // Reassign the variable
        variablesArrayCopy[index] = new Variables(name, value, type);

        // Set the variables list to the array copy
        AzzyShell.GetInstance().variables = new List<Variables>(variablesArrayCopy);

        // Return 0 if successful
        return 0;
        
    }
}
}