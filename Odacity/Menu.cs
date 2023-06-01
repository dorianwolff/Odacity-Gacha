using System.Collections.Generic;

namespace Odacity;

using System;

public class Menu
{
    public static int summoningCurrency;
    public static string currCurrency;
    private static Summon summon;
    public static List<Character> characterCollection = new List<Character>();
    public static string User;
    public static List<Character> summonableCharacters = new List<Character>();

    public static void ShowMenu()
    {
        if (summon == null)
            summon = new Summon();
        Console.Clear();
        currCurrency = "" + summoningCurrency;
        if (summoningCurrency > 9999)
            currCurrency = 9999 + "";
        else if (summoningCurrency<10)
        {
            currCurrency = "000" + summoningCurrency;
        }
        else if (summoningCurrency<100)
        {
            currCurrency = "00" + summoningCurrency;
        }
        else if (summoningCurrency<1000)
        {
            currCurrency = "0" + summoningCurrency;
        }
        string z = User;
        while (z.Length<31)
        {
            z += " ";
        }
        Console.WriteLine("/-------------------------------------------\\\n" +
                          "|  "+z+"\u001b[33m"+currCurrency+""+"✇\u001b[0m     |\n" +
                          "|                                           |\n" +
                          "|                 \u001b[36mODACITY\u001b[0m                   |\n" +
                          "|                                           |\n" +
                          "|                                           |\n" +
                          "| \u001b[32mCollection(c)\u001b[0m                  \u001b[35mDungeon(d)\u001b[0m |\n" +
                          "| \u001b[31mSummoning Gacha(s)\u001b[0m                \u001b[37mexit(e)\u001b[0m |\n" +
                          "\\-------------------------------------------/\n");

        // Wait for user input
        ConsoleKeyInfo keyInfo = Console.ReadKey();

        // Process the user input
        switch (keyInfo.Key)
        {
            case ConsoleKey.D:
                // Go to Dungeon
                if (characterCollection.Count >= 3)
                {
                    Console.Clear();
                    Dungeon.Enter(characterCollection);
                }
                else
                {
                    Console.WriteLine("\nYou must at least contain \u001b[31m3 characters\u001b[0m to unlock this feature!");
                    Console.ReadKey();
                    ShowMenu();
                }
                break;
            case ConsoleKey.E:
                Console.Clear();
                return;
            case ConsoleKey.C:
                // Go to Collection
                Console.Clear();
                DisplayCollection();
                break;
            case ConsoleKey.S:
                // Go to Summoning Gacha
                Console.Clear();
                Summon.DisplaySummonScreen(summoningCurrency, characterCollection);
                break;
            default:
                // Invalid input
                Console.Clear();
                Console.WriteLine("Invalid input. Please try again.");
                // Show the menu again
                ShowMenu();
                break;
        }
    }

    public static void DisplayCollection()
    {
        Collection collection = new Collection();
        collection.DisplayCollection(characterCollection);
    }
}
