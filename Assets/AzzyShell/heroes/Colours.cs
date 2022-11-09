namespace Heroes {

// Enum of supported supported colours
public enum Colours
{
    Black,
    Blue,
    Cyan,
    DarkBlue,
    DarkCyan,
    DarkGray,
    DarkGreen,
    DarkMagenta,
    DarkRed,
    DarkYellow,
    Gray,
    Green,
    Magenta,
    Red,
    White,
    Yellow
}

public static class Colour
{
    // Convert the colour to a ConsoleColor
    public static Colours Convert(Colours colour)
    {
        // Return the colour
        return colour switch
        {
            // Colours.Black => ConsoleColor.Black,
            // Colours.Blue => ConsoleColor.Blue,
            // Colours.Cyan => ConsoleColor.Cyan,
            // Colours.DarkBlue => ConsoleColor.DarkBlue,
            // Colours.DarkCyan => ConsoleColor.DarkCyan,
            // Colours.DarkGray => ConsoleColor.DarkGray,
            // Colours.DarkGreen => ConsoleColor.DarkGreen,
            // Colours.DarkMagenta => ConsoleColor.DarkMagenta,
            // Colours.DarkRed => ConsoleColor.DarkRed,
            // Colours.DarkYellow => ConsoleColor.DarkYellow,
            // Colours.Gray => ConsoleColor.Gray,
            // Colours.Green => ConsoleColor.Green,
            // Colours.Magenta => ConsoleColor.Magenta,
            // Colours.Red => ConsoleColor.Red,
            // Colours.White => ConsoleColor.White,
            // Colours.Yellow => ConsoleColor.Yellow,
            // _ => ConsoleColor.White,
            Colours.Black => Colours.Black,
            Colours.Blue => Colours.Blue,
            Colours.Cyan => Colours.Cyan,
            Colours.DarkBlue => Colours.DarkBlue,
            Colours.DarkCyan => Colours.DarkCyan,
            Colours.DarkGray => Colours.DarkGray,
            Colours.DarkGreen => Colours.DarkGreen,
            Colours.DarkMagenta => Colours.DarkMagenta,
            Colours.DarkRed => Colours.DarkRed,
            Colours.DarkYellow => Colours.DarkYellow,
            Colours.Gray => Colours.Gray,
            Colours.Green => Colours.Green,
            Colours.Magenta => Colours.Magenta,
            Colours.Red => Colours.Red,
            Colours.White => Colours.White,
            Colours.Yellow => Colours.Yellow,
            _ => Colours.White,
        };
    }
}
}