namespace Odacity;

public class LevelUp
{
    public static void levelUpCharacter(Character character)
    {
        Console.Clear();
        int tempLevel = character.Level;
        string hp = character.MaxHP+" HP -> ";
        int hpA = 0;
        string atk = character.Attack+" ATK -> ";
        int atkA = 0;
        string dodge = character.Dodge+" DODGE -> ";
        int dodgeA = 0;
        string acc = character.Accuracy+" ACCURACY -> ";
        int accA = 0;
        string speed = character.Speed+" SPEED -> ";
        int speedA = 0;

        for (int i = 0; i < character.tempDefeatedEnemiesExp.Count; i++)
        {
            character.Experience += character.tempDefeatedEnemiesExp[i];
        }

        while (character.Experience > character.MaxExperience)
        {
            character.Level += 1;
            character.Experience -= character.MaxExperience;
            character.MaxExperience *= 2;
            Random lvBoost = new Random();
            int statPoint = lvBoost.Next(1, 4);
            if (statPoint == 1)
            {
                character.Dodge += 2;
                dodgeA += 2;
            }
            else if (statPoint == 2)
            {
                character.Accuracy += 2;
                accA += 2;
            }
            else
            {
                character.Speed += 10;
                speedA += 2;
            }

            character.HP += 20;
            character.Attack += 5;
            hpA += 2;
            atkA += 2;

        }
        if (tempLevel != character.Level)
        {
            //Here can add colors
            atk = $"|{character.Color}"+atk+atkA+character.Attack+"\u001b[0m ATK";
            hp = $"|{character.Color}"+hp+hpA+character.HP+"\u001b[0m HP";
            speed = $"|{character.Color}"+speed+speedA+character.Speed+"\u001b[0m Speed";
            acc = $"|{character.Color}"+acc+accA+character.Accuracy+"\u001b[0m Accuracy";
            dodge = $"|{character.Color}"+dodge+dodgeA+character.Dodge+"\u001b[0m Dodge";
            Combat.InsertsSpaces(atk, 50);
            Combat.InsertsSpaces(dodge, 50);
            Combat.InsertsSpaces(hp, 50);
            Combat.InsertsSpaces(speed, 50);
            Combat.InsertsSpaces(acc, 50);
            Console.WriteLine("/-----------------------------------------------\\\n" +
                              "|                                               |\n" +
                              "|                   \u001b[36mLevel Up !\u001b[0m                  |\n" +
                              "|                                               |");
            string s = $"|{character.Color}" + character.Name + "\u001b[0m the " + character.Nickname + " gained \u001b[31m" +
                       (character.Level - tempLevel) + "\u001b[0m level(s)!";
            Combat.InsertsSpaces(s, 50);
            Console.WriteLine(s+"|\n|                                               |");
            Console.WriteLine(hp+"|\n"+atk+"|\n"+speed+"|\n"+dodge+"|\n"+speed+"|");
            Console.WriteLine("|                                               |\n" + 
                              "\\-----------------------------------------------/\n");
            Console.ReadKey();
        }
    }
}