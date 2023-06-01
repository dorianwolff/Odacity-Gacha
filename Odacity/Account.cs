namespace Odacity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
public class Account
{
    public Account(string username, int summoningCurrency, List<Character> characterCollection,List<Character> summonableCharacters,
        int towerStageClear, int questStageClear)
    {
        Username = username;
        SummoningCurrency = summoningCurrency;
        CharacterCollection = characterCollection;
        SummonableCharacters = summonableCharacters;
        TowerStageClear = towerStageClear;
        QuestStageClear = questStageClear;

    }

    public string Username { get; set; }
    public int SummoningCurrency { get; set; }
    
    public int TowerStageClear { get; set; }
    public int QuestStageClear { get; set; }
    public List<Character> CharacterCollection { get; set; }
    public List<Character> SummonableCharacters { get; set; }

    // Save account data to a file
    [Obsolete("Obsolete")]
    public static void SaveAccountData(string filePath, Account account)
    {
        try
        {
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(fileStream, account);
            }
            Console.WriteLine("Account data saved successfully.");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Failed to save account data: {e.Message}");
        }
    }


    // Load account data from a file
    [Obsolete("Obsolete")]
    public static Account LoadAccountData(string filePath)
    {
        try
        {
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                return (Account)binaryFormatter.Deserialize(fileStream);
            }

        }
        catch (Exception e)
        {
            Console.WriteLine($"Failed to load account data: {e.Message}");
            return null;
        }
    }
    
    public static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e, 
        string filepath, Account account)
    {
        account.SummoningCurrency = Menu.summoningCurrency;
        account.SummonableCharacters = Menu.summonableCharacters;
        account.TowerStageClear = Dungeon.towerStageClear;
        account.QuestStageClear = Dungeon.questStageClear;
        foreach (Character character in account.CharacterCollection)
        {
            character.HP = character.MaxHP;
        }
        // Save the account data before the program exits
        SaveAccountData(filepath,account);
    }
}
