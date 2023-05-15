namespace Odacity;

public class Dungeon
{
    public static List<int> highScores = new List<int>();

    public static List<Character> Team = new List<Character>();

    public static int towerStageClear;
    public static void Enter(List<Character> characterCollection)
    {
        Console.Clear();
        Console.WriteLine("/-------------------------------------------\\\n" +
                          "|                                           |\n" +
                          "|                                           |\n" +
                          "|          \u001b[31mWelcome to the Dungeon\u001b[0m           |\n" +
                          "|                                           |\n" +
                          "|              \u001b[36mview scores(v)\u001b[0m               |\n" +
                          "|                                           |\n" +
                          "|\u001b[34mBack(b)\u001b[0m           \u001b[32mInfo(i)\u001b[0m        \u001b[35mDungeon(d)\u001b[0m|\n" +
                          "\\-------------------------------------------/\n");
        
        
        // Wait for user input
        ConsoleKeyInfo keyInfo = Console.ReadKey();

        // Process the user input
        switch (keyInfo.Key)
        {
            case ConsoleKey.B:
                Console.Clear();
                Menu.ShowMenu();
                break;
            case ConsoleKey.D:
                Console.Clear();
                EnterDungeon(characterCollection);
                return;
            case ConsoleKey.I:
                // Show detailed on game modes
                Console.Clear();
                DisplayInfo(characterCollection);
                break;
            case ConsoleKey.V:
                Console.Clear();
                DisplayHighScores(characterCollection);
                return;
            default:
                // Invalid input
                Console.Clear();
                Console.WriteLine("Invalid input. Please try again.");
                // Show the dungeon again
                Enter(characterCollection);
                break;
        }
    }

    private static void DisplayInfo(List<Character> characterCollection)
    {
        Console.WriteLine("/-------------------------------------------\\\n" +
                          "|                                           |\n" +
                          "|                   \u001b[32mINFO\u001b[0m                    |\n" +
                          "|                                           |\n" +
                          "|                                           |\n" +
                          "|                                           |\n" +
                          "|                                           |\n" +
                          "|                                           |\n" +
                          "\\-------------------------------------------/\n");
        Console.ReadKey();
        Enter(characterCollection);
    } //TODO
    
    public static void DisplayHighScores(List<Character> characterCollection)
    {
        //TODO 
        Console.WriteLine("/-------------------------------------------\\\n" +
                          "|                                           |\n" +
                          "|                 \u001b[36mHighScores\u001b[0m                |\n" +
                          "|                                           |\n" +
                          "|                                           |\n" +
                          "|                                           |\n" +
                          "|                                           |\n" +
                          "|                                           |\n" +
                          "\\-------------------------------------------/\n");
        Console.ReadKey();
        Enter(characterCollection);
    } //TODO

    public static void EnterDungeon(List<Character> characterCollection)
    {
        Console.WriteLine("     You have ventured into a Dungeon...\n"+ 
                          "/-------------------------------------------\\\n" +
                          "|                                           |\n" +
                          "|              \u001b[35mSelect a Trial\u001b[0m               |\n" +
                          "|                                           |\n" +
                          "|                                           |\n" +
                          "|\u001b[33mQuest(q)\u001b[0m                           \u001b[36mTower(t)\u001b[0m|\n" +
                          "|                                           |\n" +
                          "|\u001b[34mBack(b)\u001b[0m                          \u001b[35mSetTeam(s)\u001b[0m|\n" +
                          "|                                           |\n" +
                          "\\-------------------------------------------/\n");
        // Wait for user input
        ConsoleKeyInfo keyInfo = Console.ReadKey();

        // Process the user input
        switch (keyInfo.Key)
        {
            case ConsoleKey.B:
                Console.Clear();
                Enter(characterCollection);
                break;
            case ConsoleKey.Q:
                Console.Clear();
                if (Team.Count==0)
                {
                    Console.WriteLine("/-------------------------------------------\\\n" +
                                      "|                                           |\n" +
                                      "|                                           |\n" +
                                      "|                                           |\n" +
                                      "|    \u001b[31mYou Cannot enter with an empty Team\u001b[0m    |\n" +
                                      "|                                           |\n" +
                                      "|                                           |\n" +
                                      "|                                           |\n" +
                                      "\\-------------------------------------------/\n");
                    Console.ReadKey();
                    Console.Clear();
                    EnterDungeon(characterCollection);
                }
                else
                {
                    
                }
                //Quest
                return;
            case ConsoleKey.T:
                Console.Clear();
                if (Team.Count==0)
                {
                    Console.WriteLine("/-------------------------------------------\\\n" +
                                      "|                                           |\n" +
                                      "|                                           |\n" +
                                      "|                                           |\n" +
                                      "|    \u001b[31mYou Cannot enter with an empty Team\u001b[0m    |\n" +
                                      "|                                           |\n" +
                                      "|                                           |\n" +
                                      "|                                           |\n" +
                                      "\\-------------------------------------------/\n");
                    Console.ReadKey();
                    Console.Clear();
                    EnterDungeon(characterCollection);
                }
                else
                {
                    EnterTower(characterCollection);
                }
                break;
            case ConsoleKey.S:
                Console.Clear();
                SetTeam(characterCollection, 0);
                return;
            default:
                // Invalid input
                Console.Clear();
                Console.WriteLine("Invalid input. Please try again.");
                // Show the dungeon again
                EnterDungeon(characterCollection);
                break;
        }
        
    }

    private static void SetTeam(List<Character> characterCollection,int currentIndex) //The user's collection
    {
        Character character = characterCollection[currentIndex];
        UpdateTeam(Team.Count);
        string colorSelect = "";
        if (character.onTeam)
            colorSelect = "\u001b[32m";
        Console.WriteLine("              \u001b[33mSelect Character\u001b[0m\n"+ 
                          "/-------------------------------------------\\\n" +
                          "|            \u001b[35mCharacter Details\u001b[0m              |\n" +
                          "|                                           |\n" +
                          $"|Rarity : ({character.Color}{character.Grade}\u001b[0m)                               |");
        string s = $"|Name :{colorSelect}{character.Name}\u001b[0m";
        if (character.onTeam)
        {
            while (s.Length<53)
            {
                s += " ";
            }
        }
        else
        {
            while (s.Length<48)
            {
                s += " ";
            }
        }
        Console.WriteLine(s+"|");
        string z = $"|Nickname : {character.Nickname}";
        while (z.Length<44)
        {
            z += " ";
        }
        Console.WriteLine(z+"|");
        string lev = $"|Level : {character.Level}   Exp : {character.Experience}/{character.MaxExperience}";
        while (lev.Length<44)
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
                          "|                                           |\n" +
                          "|\u001b[31m[<-]\u001b[0m             " +
                          "\u001b[34mBack(b)\u001b[0m               \u001b[31m[->]\u001b[0m|\n" +
                          "\\-------------------------------------------/\n");
        


        ConsoleKeyInfo keyInfo = Console.ReadKey(true);
        switch (keyInfo.Key)
        {
            case ConsoleKey.LeftArrow:
                Console.Clear();
                currentIndex = (currentIndex - 1 + characterCollection.Count) % characterCollection.Count;
                SetTeam(characterCollection, currentIndex);
                break;
            case ConsoleKey.RightArrow:
                Console.Clear();
                currentIndex = (currentIndex + 1) % characterCollection.Count;
                SetTeam(characterCollection, currentIndex);
                break;
            case ConsoleKey.Enter:
                Console.Clear();
                if (!character.onTeam && Team.Count < 3)
                {
                    character.onTeam = true;
                    Team.Add(character);
                }
                else if (character.onTeam)
                {
                    character.onTeam = false;
                    Team.Remove(character);
                }
                SetTeam(characterCollection, currentIndex);
                break;
            case ConsoleKey.B:
                Console.Clear();
                EnterDungeon(characterCollection);
                break;
            default:
                Console.Clear();
                SetTeam(characterCollection, currentIndex);
                break;
        }

    }

    private static void UpdateTeam(int nbSelected)
    {
        if (nbSelected == 0)
        {
            Console.WriteLine("                  \u001b[33mCurrent Team\u001b[0m                  \n"+ 
                              "/-----------------------------------------------\\\n" +
                              "|               |               |               |\n" +
                              "|               |               |               |\n" +
                              "|               |               |               |\n" +
                              "|               |               |               |\n" +
                              "|               |               |               |\n" +
                              "|               |               |               |\n" +
                              "|               |               |               |\n" +
                              "\\-----------------------------------------------/\n");
            Console.WriteLine("       \u001b[31m^\u001b[0m");
        }
        else if (nbSelected == 1)
        {
            string name1 = Team[0].Name;
            for (int i = name1.Length; i < 15; i++)
            {
                if (name1.Length==14)
                    name1 += " ";
                else
                {
                    name1 = " "+name1+" ";
                    i += 1;
                }
            }
            string attack = "atk:"+Team[0].Attack;
            string hp = "hp:"+Team[0].HP;
            for (int i = attack.Length+hp.Length; i < 15; i++)
            {
                attack += " ";
            }
            string level = "lvl:"+Team[0].Level;
            for (int i = level.Length; i < 10; i++)
            {
                level = " "+level;
            }
            Console.WriteLine("                  \u001b[33mCurrent Team\u001b[0m                  \n"+ 
                              "/-----------------------------------------------\\\n" +
                              $"|     ({Team[0].Color}{Team[0].Grade}\u001b[0m)       |               |               |\n" +
                              $"|{name1}|               |               |\n" +
                              "|               |               |               |\n" +
                              $"|{level}     |               |               |\n" +
                              "|               |               |               |\n" +
                              $"|{attack+hp}|               |               |\n" +
                              "|               |               |               |\n" +
                              "\\-----------------------------------------------/\n");
            Console.WriteLine("                       \u001b[31m^\u001b[0m");
        }
        else if (nbSelected == 2)
        {
            string name1 = Team[0].Name;
            for (int i = name1.Length; i < 15; i++)
            {
                if (name1.Length==14)
                    name1 += " ";
                else
                {
                    name1 = " "+name1+" ";
                    i += 1;
                }
            }
            string attack = "atk:"+Team[0].Attack;
            string hp = "hp:"+Team[0].HP;
            for (int i = attack.Length+hp.Length; i < 15; i++)
            {
                attack += " ";
            }
            string level = "lvl:"+Team[0].Level;
            for (int i = level.Length; i < 10; i++)
            {
                level = " "+level;
            }
            string name2 = Team[1].Name;
            for (int i = name2.Length; i < 15; i++)
            {
                if (name2.Length==14)
                    name2 += " ";
                else
                {
                    name2 = " "+name2+" ";
                    i += 1;
                }
            }
            string attack2 = "atk:"+Team[1].Attack;
            string hp2 = "hp:"+Team[1].HP;
            for (int i = attack2.Length+hp2.Length; i < 15; i++)
            {
                attack2 += " ";
            }
            string level2 = "lvl:"+Team[1].Level;
            for (int i = level2.Length; i < 10; i++)
            {
                level2 = " "+level2;
            }
            Console.WriteLine("                  \u001b[33mCurrent Team\u001b[0m                  \n"+ 
                              "/-----------------------------------------------\\\n" +
                              $"|     ({Team[0].Color}{Team[0].Grade}\u001b[0m)       " +
                              $"|     ({Team[1].Color}{Team[1].Grade}\u001b[0m)       |               |\n" +
                              $"|{name1}|{name2}|               |\n" +
                              "|               |               |               |\n" +
                              $"|{level}     |{level2}     |               |\n" +
                              "|               |               |               |\n" +
                              $"|{attack+hp}|{attack2+hp2}|               |\n" +
                              "|               |               |               |\n" +
                              "\\-----------------------------------------------/\n");
            Console.WriteLine("                                       \u001b[31m^\u001b[0m");
        }
        else
        {
            string name1 = Team[0].Name;
            for (int i = name1.Length; i < 15; i++)
            {
                if (name1.Length==14)
                    name1 += " ";
                else
                {
                    name1 = " "+name1+" ";
                    i += 1;
                }
            }
            string attack = "atk:"+Team[0].Attack;
            string hp = "hp:"+Team[0].HP;
            for (int i = attack.Length+hp.Length; i < 15; i++)
            {
                attack += " ";
            }
            string level = "lvl:"+Team[0].Level;
            for (int i = level.Length; i < 10; i++)
            {
                level = " "+level;
            }
            string name2 = Team[1].Name;
            for (int i = name2.Length; i < 15; i++)
            {
                if (name2.Length==14)
                    name2 += " ";
                else
                {
                    name2 = " "+name2+" ";
                    i += 1;
                }
            }
            string attack2 = "atk:"+Team[1].Attack;
            string hp2 = "hp:"+Team[1].HP;
            for (int i = attack2.Length+hp2.Length; i < 15; i++)
            {
                attack2 += " ";
            }
            string level2 = "lvl:"+Team[1].Level;
            for (int i = level2.Length; i < 10; i++)
            {
                level2 = " "+level2;
            }
            string name3 = Team[2].Name;
            for (int i = name3.Length; i < 15; i++)
            {
                if (name3.Length==14)
                    name3 += " ";
                else
                {
                    name3 = " "+name3+" ";
                    i += 1;
                }
            }
            string attack3 = "atk:"+Team[2].Attack;
            string hp3 = "hp:"+Team[2].HP;
            for (int i = attack3.Length+hp3.Length; i < 15; i++)
            {
                attack3 += " ";
            }
            string level3 = "lvl:"+Team[2].Level;
            for (int i = level3.Length; i < 10; i++)
            {
                level3 = " "+level3;
            }
            Console.WriteLine("                  \u001b[33mCurrent Team\u001b[0m                  \n"+ 
                              "/-----------------------------------------------\\\n" +
                              $"|     ({Team[0].Color}{Team[0].Grade}\u001b[0m)       " +
                              $"|     ({Team[1].Color}{Team[1].Grade}\u001b[0m)       " +
                              $"|     ({Team[2].Color}{Team[2].Grade}\u001b[0m)       |\n" +
                              $"|{name1}|{name2}|{name3}|\n" +
                              "|               |               |               |\n" +
                              $"|{level}     |{level2}     |{level3}     |\n" +
                              "|               |               |               |\n" +
                              $"|{attack+hp}|{attack2+hp2}|{attack3+hp3}|\n" +
                              "|               |               |               |\n" +
                              "\\-----------------------------------------------/\n");
        }
        
    }

    private static void EnterTower(List<Character> characterCollection)
    {
        string towerStage = towerStageClear+"";
        if (towerStageClear < 10)
        {
            towerStage += "  ";
        }
        if (towerStageClear < 100)
        {
            towerStage += " ";
        }
        else if (towerStageClear==101)
        {
            Console.WriteLine("/-------------------------------------------\\\n" +
                              "|                                           |\n" +
                              "|                   \u001b[36mTower\u001b[0m                   |\n" +
                              "|                                           |\n" +
                              "|                                           |\n" +
                              "|\u001b[33mCongratulations on clearing the final 100th\u001b[0m|\n" +
                              "|                   \u001b[33mfloor!\u001b[0m                  |\n" +
                              "|                                           |\n" +
                              "|                  \u001b[34mBack(b)\u001b[0m                  |\n" +
                              "\\-------------------------------------------/\n");
            Console.ReadKey();
            EnterDungeon(characterCollection);
            return;
        }
        Console.WriteLine("/-------------------------------------------\\\n" +
                          "|                                           |\n" +
                          "|                   \u001b[36mTower\u001b[0m                   |\n" +
                          "|                                           |\n" +
                          "|                                           |\n" +
                          "|  Please press \u001b[36m(t)\u001b[0m to proceed to stage \u001b[31m"+towerStage+"\u001b[0m|\n" +
                          "|                                           |\n" +
                          "|                                           |\n" +
                          "|                  \u001b[34mBack(b)\u001b[0m                  |\n" +
                          "\\-------------------------------------------/\n");
        // Wait for user input
        ConsoleKeyInfo keyInfo = Console.ReadKey();

        // Process the user input
        switch (keyInfo.Key)
        {
            case ConsoleKey.B:
                Console.Clear();
                EnterDungeon(characterCollection);
                break;
            case ConsoleKey.T:
                Console.Clear();
                Combat.TowerFight(characterCollection);
                break;
            default:
                // Invalid input
                Console.Clear();
                Console.WriteLine("Invalid input. Please try again.");
                // Show the dungeon again
                EnterTower(characterCollection);
                break;
        }
    }
    
}