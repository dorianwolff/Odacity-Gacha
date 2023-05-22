using System;

namespace Odacity;

public static class ConsoleColorHelper
{
    public static ConsoleColor GetNearestConsoleColor(int red, int green, int blue)
    {
        ConsoleColor result = 0;
        double smallestDistance = double.MaxValue;
        
        foreach (ConsoleColor consoleColor in Enum.GetValues(typeof(ConsoleColor)))
        {
            var color = GetConsoleColorRGB(consoleColor);
            var distance = CalculateColorDistance(red, green, blue, color.Red, color.Green, color.Blue);

            if (distance < smallestDistance)
            {
                smallestDistance = distance;
                result = consoleColor;
            }
        }

        return result;
    }

    private static (int Red, int Green, int Blue) GetConsoleColorRGB(ConsoleColor consoleColor)
    {
        switch (consoleColor)
        {
            case ConsoleColor.Black:
                return (0, 0, 0);
            case ConsoleColor.DarkBlue:
                return (0, 0, 128);
            case ConsoleColor.DarkGreen:
                return (0, 128, 0);
            case ConsoleColor.DarkCyan:
                return (0, 128, 128);
            case ConsoleColor.DarkRed:
                return (128, 0, 0);
            case ConsoleColor.DarkMagenta:
                return (128, 0, 128);
            case ConsoleColor.DarkYellow:
                return (128, 128, 0);
            case ConsoleColor.Gray:
                return (192, 192, 192);
            case ConsoleColor.DarkGray:
                return (128, 128, 128);
            case ConsoleColor.Blue:
                return (0, 0, 255);
            case ConsoleColor.Green:
                return (0, 255, 0);
            case ConsoleColor.Cyan:
                return (0, 255, 255);
            case ConsoleColor.Red:
                return (255, 0, 0);
            case ConsoleColor.Magenta:
                return (255, 0, 255);
            case ConsoleColor.Yellow:
                return (255, 255, 0);
            case ConsoleColor.White:
                return (255, 255, 255);
            default:
                return (0, 0, 0);
        }
    }

    private static double CalculateColorDistance(int r1, int g1, int b1, int r2, int g2, int b2)
    {
        double deltaR = r1 - r2;
        double deltaG = g1 - g2;
        double deltaB = b1 - b2;

        return Math.Sqrt(deltaR * deltaR + deltaG * deltaG + deltaB * deltaB);
    }
}

