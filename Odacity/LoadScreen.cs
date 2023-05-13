namespace Odacity;

using System;
using System.IO;
using System.Collections.Generic;

public class LoadScreen
{
    public static bool isNewAccount = false;
    private List<string> savedAccounts;
    public static Account acc;

    public LoadScreen()
    {
        savedAccounts = LoadSavedAccounts();
    }

    private List<string> LoadSavedAccounts()
    {
        // Load the list of saved account names from a file
        string filePath = "..//../../Accounts"; // Provide the appropriate file path
        try
        {
            // Get all file names in the directory
            string[] fileNames = Directory.GetFiles(filePath);
            List<string> filenames = new List<string>();
            // Display the file names
            foreach (string fileName in fileNames)
            {
                filenames.Add(fileName);
            }

            return filenames;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error listing files: {ex.Message}");
        }

        return new List<string>();
    }

    private void SaveAccount(string accountName)
    {
        // Save the account name to the list of saved accounts
        savedAccounts.Add(accountName);
    }

    [Obsolete("Obsolete")]
    public string ShowLoadScreen()
    {
        Console.Clear();
        Console.WriteLine("/-------------------------------------------\\\n" +
                          "|                                           |\n" +
                          "|                                           |\n" +
                          "|         \u001b[33mWelcome to the Load Screen!\u001b[0m       |\n" +
                          "|                                           |\n" +
                          "|                                           |\n" +
                          "|                                           |");

        // Display the saved account names
        for (int i = 0; i < savedAccounts.Count; i++)
        {
            string safe="";
            for (int j = 19; j < savedAccounts[i].Length-4; j++)
            {
                safe += savedAccounts[i][j];
            }
            while (safe.Length<38)
            {
                safe += " ";
            }
            Console.WriteLine($"|  {i + 1}. {safe}|");
        }
        Console.WriteLine("|                                           |\n" +
                          "|                                           |\n" +
                          "|                                           |\n" +
                          "\\-------------------------------------------/\n");

        while (true)
        {
            Console.Write("Enter the account \u001b[35mnumber\u001b[0m or '\u001b[36mnew\u001b[0m': ");
            string input = Console.ReadLine();

            if (input == "new")
            {
                Console.Write("Enter a new account name: ");
                string newAccountName = Console.ReadLine();
                string filePath = Path.Combine("..//../../Accounts/", newAccountName + ".dat");

                try
                {
                    // Create a new account instance
                    if (newAccountName != null && newAccountName.Length<=15)
                    {
                        acc = new Account(newAccountName,200,new List<Character>(),new List<Character>(),1);
                        acc.SummoningCurrency = 200;
                        acc.CharacterCollection = new List<Character>();
                        acc.SummonableCharacters = new List<Character>();

                        // Save the account data
                        Account.SaveAccountData(filePath, acc);
                    }

                    Console.WriteLine("Account created successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error creating account: {ex.Message}");
                }

                isNewAccount = true;
                SaveAccount(newAccountName);
                return filePath;
            }
            else if (int.TryParse(input, out int selectedIndex) && selectedIndex >= 1 && selectedIndex <= savedAccounts.Count)
            {
                // Return the selected account name
                return savedAccounts[selectedIndex - 1];
            }
            else
            {
                Console.WriteLine("Invalid input. Either length surpasses 15 characters," +
                                  " or it has an invalid character.\n" +
                                  "Please try again.");
            }
        }
    }
}
