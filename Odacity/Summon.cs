namespace Odacity;

using System;
using System.Collections.Generic;
using System.IO;

[Serializable]
public class Summon
{


    public Summon()
    {
        if (Menu.summonableCharacters.Count==0)
            Menu.summonableCharacters = GenerateSummonableCharacters();
    }

    public static void DisplaySummonScreen(int summoningCurrency, List<Character> collection)
    {
        Menu.currCurrency = "" + summoningCurrency;
        if (summoningCurrency > 9999)
            Menu.currCurrency = 9999 + "";
        else if (summoningCurrency<10)
        {
            Menu.currCurrency = "000" + summoningCurrency;
        }
        else if (summoningCurrency<100)
        {
            Menu.currCurrency = "00" + summoningCurrency;
        }
        else if (summoningCurrency<1000)
        {
            Menu.currCurrency = "0" + summoningCurrency;
        }
        Console.Clear();
        Console.WriteLine("/-------------------------------------------\\\n" +
                          "|                                 \u001b[33m"+Menu.currCurrency+"✇\u001b[0m     |\n" +
                          "|                                           |\n" +
                          "|                  \u001b[31mRECRUIT\u001b[0m                  |\n" +
                          "|                                           |\n" +
                          "|                                           |\n" +
                          "| \u001b[32mView(i)\u001b[0m                           \u001b[34mBack(b)\u001b[0m |\n" +
                          "|                 \u001b[31mSummon(s)\u001b[0m                 |\n" +
                          "\\-------------------------------------------/\n");

        ConsoleKeyInfo input = Console.ReadKey();
        switch (input.Key)
        {
            case ConsoleKey.I:
                Console.Clear();
                DisplaySummonableCharactersInfo(summoningCurrency, collection);
                break;
            case ConsoleKey.S:
                Console.Clear();
                PerformSummon(summoningCurrency, collection);
                break;
            case ConsoleKey.B:
                Console.Clear();
                Menu.ShowMenu();
                break;
            default:
                Console.WriteLine("Invalid input. Please try again.");
                DisplaySummonScreen(summoningCurrency, collection);
                break;
        }
    }

    private static void DisplaySummonableCharactersInfo(int summoningCurrency, List<Character> collection)
    {
        Console.Clear();
        Console.WriteLine("/-------------------------------------------\\\n" +
                          "|                                           |\n" +
                          "|       \u001b[32mSummonable Characters Info\u001b[0m          |\n" +
                          "|                                           |");
        
        // Display the summonable characters and their rates
        for(int i=Menu.summonableCharacters.Count-1; i>=0;i--)
        {
            Character character = Menu.summonableCharacters[i];
            int Rate;
            if (character.Grade == "L")
                Rate = 5;
            else if (character.Grade == "E")
                Rate = 10;
            else if (character.Grade == "R")
                Rate = 25;
            else //if Common rarity
                Rate = 60;

            float l = (float)Rate / character.totOfRarity;
            string s = $"|- ({character.Color}{character.Grade}\u001b[0m) {character.Name} - Rate: {l}%";
            while (s.Length<53)
            {
                s += " ";
            }
            Console.WriteLine(s+"|");
        }
        Console.WriteLine("\\-------------------------------------------/\n");
        Console.ReadKey();

        DisplaySummonScreen(summoningCurrency, collection);
    }

    private static void PerformSummon(int summoningCurrency, List<Character> collection)
    {
        Console.Clear();
        Console.WriteLine("/-------------------------------------------\\\n" +
                          "|                                 \u001b[33m"+Menu.currCurrency+"✇\u001b[0m     |\n" +
                          "|                                           |\n" +
                          "|                 \u001b[31mSUMMONING\u001b[0m                 |\n" +
                          "|                                           |\n" +
                          "|                                           |\n" +
                          "|              x1 summon = \u001b[33m10✇\u001b[0m              |\n" +
                          "|                                           |\n" +
                          "\\-------------------------------------------/\n");

        Console.WriteLine("Enter the number of summons you want to perform :");
        int numSummons;
        if (!int.TryParse(Console.ReadLine(), out numSummons)||numSummons==0)
        {
            Console.Clear();
            Console.WriteLine("/-------------------------------------------\\\n" +
                              "|                                           |\n" +
                              "|                                           |\n" +
                              "|            \u001b[31mAmount not recognized\u001b[0m          |\n" +
                              "|                                           |\n" +
                              "|          Please enter a \u001b[36mvalid number\u001b[0m      |\n" +
                              "|                                           |\n" +
                              "|                                           |\n" +
                              "\\-------------------------------------------/\n");
            PerformSummon(summoningCurrency, collection);
            return;
        }

        int totalCost = numSummons * 10; // Assuming each summon costs 10 summoning currency
        if (totalCost > summoningCurrency)
        {
            Console.Clear();
            Console.WriteLine("/-------------------------------------------\\\n" +
                              "|                                           |\n" +
                              "|                                           |\n" +
                              "|                                           |\n" +
                              "|      \u001b[31mInsufficient summoning currency.\u001b[0m     |\n" +
                              "|                                           |\n" +
                              "|                                           |\n" +
                              "|                                           |\n" +
                              "\\-------------------------------------------/\n");
            Console.ReadLine();
            DisplaySummonScreen(summoningCurrency, collection);
            return;
        }
        Console.Clear();
        // Deduct summoning currency
        Menu.summoningCurrency -= totalCost;
        summoningCurrency = Menu.summoningCurrency;

        Console.WriteLine("/---------------------------------------------------\\\n" +
                          "|                                                   |\n" +
                          "|               \u001b[31mRecruiting results :\u001b[0m                |\n" +
                          "|                                                   |");
        
        for (int i = 0; i < numSummons; i++)
        {
            Character summonedCharacter = PerformSingleSummon();
            bool hasChar = false;
            foreach (Character character in collection)
            {
                if (character.Name == summonedCharacter.Name && character.Nickname == summonedCharacter.Nickname)
                {
                    hasChar = true;
                    if (character.awakening<10) //here to put max awakening
                    {
                        character.awakening += 1;
                        ChangeAwakening(character);
                    }
                }
            }

            if (!hasChar)
            {
                string s = $"|- ({summonedCharacter.Color}{summonedCharacter.Grade}\u001b[0m)"+
                           $" {summonedCharacter.Name}"+
                           $" the {summonedCharacter.Nickname}" +
                           "  \u001b[33m(new!)\u001b[0m";
                while (s.Length<70)
                {
                    s += " ";
                }
                Console.WriteLine(s+"|");
            
                collection.Add(summonedCharacter);
            }
            else
            {
                string s = $"|- ({summonedCharacter.Color}{summonedCharacter.Grade}\u001b[0m)"+
                           $" {summonedCharacter.Name}"+
                           $" the {summonedCharacter.Nickname}" +
                           $" -> AWAKEN \u001b[35m{summonedCharacter.awakening}\u001b[0m";
                while (s.Length<70)
                {
                    s += " ";
                }
                Console.WriteLine(s+"|");
            }
        }

        for (int i = numSummons; i < 3; i++)
        {
            Console.WriteLine("|                                                   |");
        }
        
        Console.WriteLine("\\---------------------------------------------------/\n");
        

        Console.ReadLine();

        DisplaySummonScreen(summoningCurrency, collection);
    }

    private static void ChangeAwakening(Character character)
    {
        if (character.awakening%3 ==0)
        {
            if (character.Skill1.BuffEffect != "")
                character.Skill1.buffPercentage += 2;
            else
            {
                character.Skill1.AttackMultiplier += 2;
            }
        }
        else if (character.awakening%3 ==1)
        {
            if (character.Skill2.BuffEffect != "")
                character.Skill2.buffPercentage += 2;
            else
            {
                character.Skill2.AttackMultiplier += 2;
            }
        }
        else
        {
            if (character.UltSkill.isSingleTarget)
                character.UltSkill.isSingleTarget = false;
            else if (character.UltSkill.BuffEffect != "")
                character.UltSkill.buffPercentage += 6;
            else
            {
                character.UltSkill.AttackMultiplier += 6;
            }
        }
        
    }

    private static Character PerformSingleSummon()
    {
        int randomNum = new Random().Next(1, 101); // Generate a random number between 1 and 100

        // Determine the rarity of the summoned character based on the random number
        string rarity;
        if (randomNum <= 60)
        {
            rarity = "C";
        }
        else if (randomNum <= 85)
        {
            rarity = "R";
        }
        else if (randomNum <= 95)
        {
            rarity = "E";
        }
        else
        {
            rarity = "L";
        }

        // Filter summonable characters by the determined rarity
        List<Character> charactersByRarity = Menu.summonableCharacters.FindAll(c => c.Grade == rarity);

        // Random
        // Randomly select a character from the filtered list
        Character summonedCharacter = charactersByRarity[new Random().Next(charactersByRarity.Count)];

        // Add the summoned character to the player's collection

        return summonedCharacter;
    }
    
    private List<Character> GenerateSummonableCharacters()
    {
        // Generate amount
        int cNum = 16;
        int rNum = 10;
        int eNum = 8;
        int lNum = 5;
        
        //
        
        
        // Generate and return a list of summonable characters
        List<Character> summonableCharacters = new List<Character>();

        // Add characters with their respective rarities and rates

        for (int i = 0; i < cNum; i++)
        {
            summonableCharacters.Add(generateCcharacter(cNum));
        }

        for (int i = 0; i < rNum; i++)
        {
            summonableCharacters.Add(generateRcharacter(rNum));
        }
        for (int i = 0; i < eNum; i++)
        {
            summonableCharacters.Add(generateEcharacter(eNum));
        }
        
        for (int i = 0; i < lNum; i++)
        {
            summonableCharacters.Add(generateLcharacter(lNum));
        }
        Random rd = new Random();
        string directoryPath = "../../../../character_images";
        int fileCount = CountFilesInDirectory(directoryPath);
        foreach (Character character in summonableCharacters)
        {
            int n = rd.Next(1, fileCount); //One image for now
            character.ImagePath = "../../../../character_images/char"+n+".png";
            character.awakening = 1;
            character.Skill1 = GenerateCharacters.GenerateCharacterNormalSkill(character.Grade);
            character.Skill2 = GenerateCharacters.GenerateCharacterNormalSkill(character.Grade);
            character.UltSkill = GenerateCharacters.GenerateCharacterUltSkill(character.Grade);
        }
        return summonableCharacters;
    }

    private Character generateCcharacter(int tot)
    {
        Character c = new Character();
        c.Grade = "C";
        c.Level = 1;
        c.Nickname = GenerateCharacters.GenerateCharacterNickname();
        c.Name = GenerateCharacters.GenerateCharacterName();
        /*c.ImagePath = "   /\\_/\\ \n" +
                      "  ( o.o )\n" +
                      "   > ^ <";*/
        c.Experience = 0;
        c.totOfRarity = tot;
        c.MaxExperience = 5;
        c.onTeam = false;
        calculateStats(c.Grade,c);
        return c;
    }
    
    private Character generateRcharacter(int tot)
    {
        Character c = new Character();
        c.Grade = "R";
        c.Level = 1;
        c.Nickname = GenerateCharacters.GenerateCharacterNickname();
        c.Name = GenerateCharacters.GenerateCharacterName();
        /*c.ImagePath = "  /\\_/\\ \n" +
                      " (='.'=)\n" +
                      "  ( _ ) ";*/
        c.Experience = 0;
        c.MaxExperience = 5;
        c.totOfRarity = tot;
        c.onTeam = false;
        calculateStats(c.Grade,c);
        return c;
    }
    
    private Character generateEcharacter(int tot)
    {
        Character c = new Character();
        c.Grade = "E";
        c.Level = 1;
        c.Nickname = GenerateCharacters.GenerateCharacterNickname();
        c.Name = GenerateCharacters.GenerateCharacterName();
        /*c.ImagePath ="    /\\_/\\ \n"+
                     "   / o o \\ \n"+
                     "  (   \"   )\n"+
                     "   \\~(*)~/\n"+
                     "    \\_|_/\n";*/
        c.Experience = 0;
        c.MaxExperience = 5;
        c.totOfRarity = tot;
        c.onTeam = false;
        calculateStats(c.Grade,c);
        return c;
    }
    
    private Character generateLcharacter(int tot)
    {
        Character c = new Character();
        c.Grade = "L";
        c.Level = 1;
        c.Nickname = GenerateCharacters.GenerateCharacterNickname();
        c.Name = GenerateCharacters.GenerateCharacterName();
        /*c.ImagePath ="    /\\_/\\ \n"+
                     "  =( o.o )=\n"+
                     "   (\")_(\")\n";*/
        c.Experience = 0;
        c.MaxExperience = 5;
        c.totOfRarity = tot;
        c.onTeam = false;
        calculateStats(c.Grade,c);

        return c;
    }

    private void calculateStats(string grade,Character c)
    {
        Random rdA = new Random();
        int rdAtk = rdA.Next(10, 100);

        Random rdH = new Random();
        int rdHp = rdH.Next(100, 300);

        Random rdAc = new Random();
        int rdAcc = rdAc.Next(0, 20);

        Random rds = new Random();
        int rdSpeed = rds.Next(20, 100);
        
        switch (grade)
        {
            case "C":
                c.Attack = rdAtk;
                c.HP = rdHp;
                c.Accuracy = rdAcc;
                c.Dodge = 100 - rdAcc - 80;
                c.Speed = rdSpeed;
                c.Color = "\u001b[37m";
                break;
            case "R":
                c.Attack = rdAtk + 50;
                c.HP = rdHp+100;
                c.Accuracy = rdAcc;
                c.Dodge = 100 - rdAcc - 80;
                c.Speed = rdSpeed+100;
                c.Color = "\u001b[34m";
                break;
            case "E":
                c.Attack = rdAtk + 100;
                c.HP = rdHp+200;
                c.Accuracy = rdAcc+10;
                c.Dodge = 100 - rdAcc - 70;
                c.Speed = rdSpeed+150;
                c.Color = "\u001b[31m";
                break;
            default: //legendary
                c.Attack = rdAtk + 150;
                c.HP = rdHp+300;
                c.Accuracy = rdAcc+20;
                c.Dodge = 100 - rdAcc - 60;
                c.Speed = rdSpeed+200;
                c.Color = "\u001b[36m";
                break;
        }
    }
    
    public static int CountFilesInDirectory(string directoryPath)
    {
        try
        {
            string[] files = Directory.GetFiles(directoryPath);
            return files.Length;
        }
        catch (DirectoryNotFoundException)
        {
            Console.WriteLine("Directory not found.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }

        return 0;
    }
    
}