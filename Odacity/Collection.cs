namespace Odacity;

using System;
using System.Collections.Generic;
using SkiaSharp;

[Serializable]
public class Collection
{
    private int currentIndex;

    public Collection()
    {
        currentIndex = 0;
    }

    public void DisplayCollection(List<Character> collection)
    {
        Console.Clear();
        List<Character> l = new List<Character>();
        List<Character> e = new List<Character>();
        List<Character> r = new List<Character>();
        List<Character> c = new List<Character>();
        foreach (Character character in collection)
        {
            if (character.Grade == "L")
            {
                l.Add(character);
            }
            else if (character.Grade == "E")
            {
                e.Add(character);
            }
            else if (character.Grade == "R")
            {
                r.Add(character);
            }
            else
            {
                c.Add(character);
            }
        }

        foreach (Character character in e)
        {
            l.Add(character);
        }
        foreach (Character character in r)
        {
            l.Add(character);
        }
        foreach (Character character in c)
        {
            l.Add(character);
        }

        collection = l;
        Console.WriteLine("/-------------------------------------------\\\n" +
                          "|                                           |\n" +
                          "|                \u001b[32mCollection\u001b[0m                 |\n" +
                          "|                                           |");
        
        // Display each character in the collection
        if (collection.Count > 0)
        {
            for (int i = 0; i < collection.Count; i++)
            {
                Character character = collection[i];
                string s = $"|{character.Color}{character.Grade}\u001b[0m - {character.Name} ({character.Nickname})";
                while (s.Length<53)
                {
                    s += " ";
                }
                Console.WriteLine(s+"|");
            }
            Console.WriteLine("|                                    \u001b[34mBack(b)\u001b[0m|");
        }
        else
        {
            Console.WriteLine("|                                           |\n" +
                              "|                                           |\n" +
                              "|                                           |\n" +
                              "|                                    \u001b[34mBack(b)\u001b[0m|");
        }
        
        Console.WriteLine("\\-------------------------------------------/\n");

        Console.WriteLine("Type the beginning of a character's \u001b[31mname\u001b[0m to view details");

        string input = Console.ReadLine();
        if (input.ToLower() == "b")
        {
            // Go back to the menu
            Menu.ShowMenu();
        }
        else
        {
            Character selectedCharacter = FindCharacterByName(input, collection);
            if (selectedCharacter != null)
            {
                DisplayCharacterDetails(selectedCharacter, collection);
            }
            else
            {
                Console.WriteLine("Character \u001b[31not found\u001b[0m. Please try again.");
                DisplayCollection(collection);
            }
        }
    }

    private Character FindCharacterByName(string nameBeginning, List<Character> collection)
    {
        // Search for a character by the beginning of their name
        foreach (Character character in collection)
        {
            if (character.Name.StartsWith(nameBeginning, StringComparison.OrdinalIgnoreCase))
            {
                return character;
            }
        }

        return null;
    }

    public void DisplayCharacterDetails(Character character, List<Character> collection)
    {
        Console.Clear();

        Console.WriteLine("/-------------------------------------------\\\n" +
                          "|            \u001b[35mCharacter Details\u001b[0m              |\n" +
                          "|                                           |\n" +
                          $"|Rarity : ({character.Color}{character.Grade}\u001b[0m)                               |");
        string s = $"|Name :{character.Name}";
        while (s.Length<44)
        {
            s += " ";
        }
        Console.WriteLine(s+"|");
        string z = $"|Nickname : {character.Nickname}";
        while (z.Length<44)
        {
            z += " ";
        }
        Console.WriteLine(z+"|");
        string lev = $"|Level : \u001b[31m{character.Level}\u001b[0m      Exp : {character.Experience}/{character.MaxExperience}" +
                     $"       AWAKEN : \u001b[35m{character.awakening}\u001b[0m";
        while (lev.Length<62)
        {
            lev += " ";
        }
        Console.WriteLine(lev+"|");
        Console.WriteLine("|                                           |\n"
                          +"|             \u001b[34mCharacter Stats\u001b[0m               |");
        string y = $"|Attack : {character.Attack}";
        while (y.Length<44)
        {
            y += " ";
        }
        Console.WriteLine(y+"|");
        string w = $"|HP : {character.HP}";
        while (w.Length<44)
        {
            w += " ";
        }
        Console.WriteLine(w+"|");
        string x = $"|Speed : {character.Speed}";
        while (x.Length<44)
        {
            x += " ";
        }
        Console.WriteLine(x+"|");
        string v = $"|Dodge : {character.Dodge}";
        while (v.Length<44)
        {
            v += " ";
        }
        Console.WriteLine(v+"|");
        string u = $"|Accuracy : {character.Accuracy}";
        while (u.Length<44)
        {
            u += " ";
        }
        Console.WriteLine(u+"|");
        Console.WriteLine("|                                           |\n" +
                          "|                  \u001b[36mSkill(s)\u001b[0m                 |\n" +
                          "|                                           |\n" +
                          "|\u001b[31m[<-]\u001b[0m      \u001b[34mBack(b)\u001b[0m      " +
                          "\u001b[33mDisplay(d)\u001b[0m      \u001b[31m[->]\u001b[0m|\n" +
                          "\\-------------------------------------------/\n");

        ConsoleKeyInfo keyInfo = Console.ReadKey(true);

        switch (keyInfo.Key)
        {
            case ConsoleKey.D:
                // Display the character's image
                Console.Clear();
                DisplayImage(character.ImagePath);
                Console.ReadKey();
                DisplayCharacterDetails(character, collection);
                break;
            case ConsoleKey.LeftArrow:
                currentIndex = (currentIndex - 1 + collection.Count) % collection.Count;
                DisplayCharacterDetails(collection[currentIndex], collection);
                break;
            case ConsoleKey.RightArrow:
                currentIndex = (currentIndex + 1) % collection.Count;
                DisplayCharacterDetails(collection[currentIndex], collection);
                break;
            case ConsoleKey.B:
                DisplayCollection(collection);
                break;  // Exit the method and return to the menu
            case ConsoleKey.S:
                DisplaySkills(character, collection);
                break;
            default:
                Console.Clear();
                Console.WriteLine("Please enter a valid character name.");
                DisplayCharacterDetails(character, collection);
                break;
        }
    }
    
    public static void DisplayImage(string imagePath)
    {
        int plusEqy = 3;
        int plusEqx = 2;
        if (imagePath == "../../../../character_images/char1.png")
        {
            plusEqy = 4;
            plusEqx = 3;
        }

        const string grayscaleChars = "@%#*+=-:. ";
        const char darkCharacter = ' ';

        using (SKBitmap image = SKBitmap.Decode(imagePath))
        {
            for (int y = 0; y < image.Height; y += plusEqy)
            {
                for (int x = 0; x < image.Width; x += plusEqx)
                {
                    SKColor pixel = image.GetPixel(x, y);
                    int brightness = (pixel.Red + pixel.Green + pixel.Blue) / 3;
                    var charIndex = (int)(brightness / 255.0 * (grayscaleChars.Length - 1));
                    char character = (brightness > 0) ? grayscaleChars[charIndex] : darkCharacter;

                    Console.ForegroundColor = ConsoleColorHelper.GetNearestConsoleColor(pixel.Red, pixel.Green, pixel.Blue);
                    Console.Write(character);
                    if (x <= image.Width / 4 || x >= 3 * image.Width / 4)
                        x += 1;
                }
                Console.WriteLine();
            }
        }
    }

    public void DisplaySkills(Character character, List<Character> collection)
    {
        Console.Clear();

        Console.WriteLine("/-----------------------------------------------------------------------\\\n" +
                          "|                                                                       |\n" +
                          "|                                \u001b[36mSkill(s)\u001b[0m                               |\n"+
                          "|                                                                       |");
        string s = $"\u001b[33mSkill 1\u001b[0m Details: {character.Skill1.Description}";
        int jj;
        for (int i = 1; i < s.Length; i += jj)
        {
            
            string res = "|";
            int len = 0;
            bool insideColorCode = false;
            for (int j = i-1; j < s.Length; j++)
            {
                if (s[j] == '-')
                {
                    break;
                }
                else if (s[j] == '\u001b')
                {
                    insideColorCode = true;
                    res += s[j];
                    len -= 1;
                }
                else if (insideColorCode)
                {
                    res += s[j];
                    if (s[j] == 'm')
                    {
                        insideColorCode = false;
                    }
                    len -= 1;
                }
                else
                {
                    res += s[j];
                }

                len += 1;
            }
            jj = res.Length;
            while (len < 71)
            {
                res += " ";
                len += 1;
            }
            Console.WriteLine(res + "|");
        }
        Console.WriteLine("|                                                                       |");
        s = $"\u001b[33mSkill 2\u001b[0m Details: {character.Skill2.Description}";
        
        for (int i = 1; i < s.Length; i += jj)
        {
            
            string res = "|";
            int len = 0;
            bool insideColorCode = false;
            for (int j = i-1; j < s.Length; j++)
            {
                if (s[j] == '-')
                {
                    break;
                }
                else if (s[j] == '\u001b')
                {
                    insideColorCode = true;
                    res += s[j];
                    len -= 1;
                }
                else if (insideColorCode)
                {
                    res += s[j];
                    if (s[j] == 'm')
                    {
                        insideColorCode = false;
                    }
                    len -= 1;
                }
                else
                {
                    res += s[j];
                }

                len += 1;
            }
            jj = res.Length;
            while (len < 71)
            {
                res += " ";
                len += 1;
            }
            Console.WriteLine(res + "|");
        }
        Console.WriteLine("|                                                                       |");
        s = $"\u001b[36mUltimate\u001b[0m : {character.UltSkill.Description}";
        
        for (int i = 1; i < s.Length; i += jj)
        {
            
            string res = "|";
            int len = 0;
            bool insideColorCode = false;
            for (int j = i-1; j < s.Length; j++)
            {
                if (s[j] == '-')
                {
                    break;
                }
                else if (s[j] == '\u001b')
                {
                    insideColorCode = true;
                    res += s[j];
                    len -= 1;
                }
                else if (insideColorCode)
                {
                    res += s[j];
                    if (s[j] == 'm')
                    {
                        insideColorCode = false;
                    }
                    len -= 1;
                }
                else
                {
                    res += s[j];
                }

                len += 1;
            }
            jj = res.Length;
            while (len < 71)
            {
                res += " ";
                len += 1;
            }
            Console.WriteLine(res + "|");
        }
        Console.WriteLine("|                                                                       |\n" +
                          "|                                                                       |\n" +
                          "\\-----------------------------------------------------------------------/\n"); 
        Console.ReadKey();
        DisplayCharacterDetails(character, collection);



    }




    
}
