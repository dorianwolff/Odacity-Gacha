using System;
using System.Threading;
using System.Threading.Tasks;

public class Loading
{
    public static void ShowLoadingScreen()
    {
        ClearConsoleInput();
        Console.Clear();

        int periodCount = 0;
        bool increasing = true;
        const int MaxPeriodCount = 3;

        DateTime endTime = DateTime.Now.AddSeconds(5);

        while (DateTime.Now < endTime)
        {
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write($"{"Loading", -10}");

            for (int i = 0; i < MaxPeriodCount; i++)
            {
                if (i < periodCount)
                    Console.ForegroundColor = ConsoleColor.Yellow;
                else
                    Console.ForegroundColor = ConsoleColor.DarkGray;

                Console.Write(".");
            }

            Thread.Sleep(500);

            if (increasing)
            {
                periodCount++;

                if (periodCount > MaxPeriodCount)
                {
                    periodCount = MaxPeriodCount;
                    increasing = false;
                }
            }
            else
            {
                periodCount--;

                if (periodCount < 0)
                {
                    periodCount = 0;
                    increasing = true;
                }
            }
        }

        Console.SetCursorPosition(0, Console.CursorTop);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Loading complete!");
        Console.ResetColor();
    }

    private static void ClearConsoleInput()
    {
        while (Console.KeyAvailable)
        {
            Console.ReadKey(true);
        }
    }
}