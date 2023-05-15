namespace Odacity;

public class Combat
{
    public static int TurnCount;
    public static int Arrow; // 1, 2, 3 or 4;
    public static bool displayChars;
    public static void TowerFight(List<Character> characterCollection)
    {
        foreach (Character character in Dungeon.Team)
        {
            character.TurnPriority = character.Speed;
            character.IsAlive = true;
            character.MaxHP = character.HP;
            character.IsPoisonned = false;
            character.IsStuned = false;
            character.buffedSpeed = character.Speed;
            character.buffedAtk = character.Attack;
            character.buffedDodge = character.buffedDodge;
            character.buffedAccuracy = character.Accuracy;
            character.IsBuffed = false;
            character.Skill1.CooldownLeft = 0;
            character.Skill2.CooldownLeft = 0;
            character.UltSkill.CooldownLeft = character.UltSkill.Cooldown;
        }
        

        Enemies.InitializeTowerEnemies();

        foreach (Enemies enemy in Enemies.towerEnemies)
        {
            enemy.TurnPriority = enemy.Speed;
            enemy.IsAlive = true;
            enemy.HP = enemy.MaxHP;
            enemy.IsPoisonned = false;
            enemy.IsStuned = false;
            enemy.buffedSpeed = enemy.Speed;
            enemy.buffedAtk = enemy.Attack;
            enemy.buffedDodge = enemy.buffedDodge;
            enemy.buffedAccuracy = enemy.Accuracy;
            enemy.IsBuffed = false;
            enemy.Skill1.CooldownLeft = 0;
            enemy.UltSkill.CooldownLeft = enemy.UltSkill.Cooldown;
        }

        displayChars = true;
        Arrow = 1;
        TurnCount = 0;
        
        StartFight(characterCollection);
        bool dedEnemies = true;
        foreach (Enemies enemy in Enemies.towerEnemies)
        {
            if (enemy.IsAlive)
                dedEnemies = false;
        }

        if (dedEnemies)
        {
            Victory(characterCollection);
        }
        else
        {
            Defeat(characterCollection);
        }

    }

    public static void StartFight(List<Character> characterCollection)
    {
        Character character = FastestCharacter();
        Enemies enemy = FastestEnemy();
        foreach (Enemies enemys in Enemies.towerEnemies)
        {
            if (enemys.HP <= 0)
            {
                enemys.HP = 0;
                enemys.IsAlive = false;
            }
        }

        bool dedTeam = ADeadTeam(characterCollection);
        if (!dedTeam)
        {
            if (character.TurnPriority >= enemy.TurnPriority)
            {
                character.TurnPriority = 0;
                DisplayCharacterTurn(character,characterCollection);
            }
            else
            {
                enemy.TurnPriority = 0;
                DisplayEnemyTurn(enemy, characterCollection);
            }
            TurnCount+=1;
            UpdateTurnPriority();
            StartFight(characterCollection);
        }
        

    }

    public static bool ADeadTeam(List<Character> characterCollection)
    {
        bool dedTeam = true;
        
        foreach (Character character in Dungeon.Team)
        {
            if (character.IsAlive)
                dedTeam = false;
        }

        if (dedTeam)
        {
            return dedTeam;
        }

        bool dedEnemies = true;

        foreach (Enemies enemy in Enemies.towerEnemies)
        {
            if (enemy.IsAlive)
                dedEnemies = false;
        }
        
        if (dedEnemies)
        {
            return dedEnemies;
        }
        return false;
    }

    public static void Victory(List<Character> characterCollection)
    {
        Console.WriteLine("All \u001b[31menemies\u001b[0m have been defeated!");
        Console.ReadKey();
        Console.Clear();
        Console.WriteLine("/-------------------------------------------\\\n" +
                          "|                                           |\n" +
                          "|                                           |\n" +
                          "|                                           |\n" +
                          "|                   \u001b[33mVictory!\u001b[0m                |\n" +
                          "|                                           |\n" +
                          "|                                           |\n" +
                          "|                                           |\n" +
                          "|                                           |\n" +
                          "\\-------------------------------------------/\n");
        Dungeon.towerStageClear += 1;
        Console.ReadKey();
        Console.Clear();
        Dungeon.DisplayHighScores(characterCollection);
    }

    public static void Defeat(List<Character> characterCollection)
    {
        Console.WriteLine("\u001b[31mYou\u001b[0m have been defeated!");
        Console.ReadKey();
        Console.Clear();
        Console.WriteLine("/-------------------------------------------\\\n" +
                          "|                                           |\n" +
                          "|                                           |\n" +
                          "|                                           |\n" +
                          "|                 \u001b[31mAnnihilated\u001b[0m               |\n" +
                          "|                                           |\n" +
                          "|                                           |\n" +
                          "|                                           |\n" +
                          "|                                           |\n" +
                          "\\-------------------------------------------/\n");
        Console.ReadKey();
        Console.Clear();
        Dungeon.EnterDungeon(characterCollection);
    }


    public static void DisplayEnemyTurn(Enemies enemy,List<Character> characterCollection)
    {
        Console.Clear();
    }

    public static void DisplayCharacterTurn(Character character,List<Character> characterCollection)
    {
        Console.Clear();
        string isActive1 = "\u001b[0m";
        string isActive2 = "\u001b[0m";
        string isActive3 = "\u001b[0m";
        string arrowPointer = "\u001b[31m←\u001b[0m";

        string characterBuffsAndDebuffs1 = "  ";
        string characterBuffsAndDebuffs2 = "  ";
        string characterBuffsAndDebuffs3 = "  ";
        string characterBuffsAndDebuffs4 = "  ";
        
        string sc1 =CalculateCubes(Dungeon.Team[0].MaxHP,Dungeon.Team[0].HP,false);
        if (character == Dungeon.Team[0])
        {
            isActive1 = "\u001b[33m";
            if (Dungeon.Team[0].IsBuffed)
                characterBuffsAndDebuffs1 +="\u001b[34m⚔\u001b[0m ";
            if (Dungeon.Team[0].IsPoisonned)
                characterBuffsAndDebuffs1 += "\u001b[32m☠"+ "\u001b[0m ";
            if (Dungeon.Team[0].IsStuned)
                characterBuffsAndDebuffs1 += "\u001b[33m꩜"+ "\u001b[0m";

        }
        string sc2="";
        if (Dungeon.Team.Count>1)
        {
            sc2 = CalculateCubes(Dungeon.Team[1].MaxHP, Dungeon.Team[1].HP,false);
            if (character == Dungeon.Team[1])
                isActive2 = "\u001b[33m";
            sc2 += "   " + isActive2 + Dungeon.Team[1].Name + "\u001b[0m";
            if (Dungeon.Team[1].IsBuffed)
                characterBuffsAndDebuffs2 +="\u001b[34m⚔\u001b[0m ";
            if (Dungeon.Team[1].IsPoisonned)
                characterBuffsAndDebuffs2 += "\u001b[32m☠"+ "\u001b[0m ";
            if (Dungeon.Team[1].IsStuned)
                characterBuffsAndDebuffs2 += "\u001b[33m꩜"+ "\u001b[0m";
        }
        string sc3="";
        if (Dungeon.Team.Count>2)
        {
            sc3 = CalculateCubes(Dungeon.Team[2].MaxHP, Dungeon.Team[2].HP,false);
            if (character == Dungeon.Team[2])
                isActive3 = "\u001b[33m";
            sc3 += "   " + isActive3 + Dungeon.Team[2].Name + "\u001b[0m";
            if (Dungeon.Team[2].IsBuffed)
                characterBuffsAndDebuffs3 +="\u001b[34m⚔\u001b[0m ";
            if (Dungeon.Team[2].IsPoisonned)
                characterBuffsAndDebuffs3 += "\u001b[32m☠"+ "\u001b[0m ";
            if (Dungeon.Team[2].IsStuned)
                characterBuffsAndDebuffs3 += "\u001b[33m꩜"+ "\u001b[0m";
        }
        
        string characterHealthAndNames1 = sc1+"   "+isActive1+$"{Dungeon.Team[0].Name}\u001b[0m";
        string characterHealthAndNames2 = sc2+"\u001b[0m";
        string characterHealthAndNames3 = sc3+"\u001b[0m";
        characterHealthAndNames1 = InsertsSpaces(characterHealthAndNames1,40);
        characterHealthAndNames2 = InsertsSpaces(characterHealthAndNames2,40);
        characterHealthAndNames3 = InsertsSpaces(characterHealthAndNames3,40);
        string characterHealthAndNames4 = InsertsSpaces("  ",40);

        characterBuffsAndDebuffs1 = InsertsSpaces(characterBuffsAndDebuffs1, 40);
        characterBuffsAndDebuffs2 = InsertsSpaces(characterBuffsAndDebuffs2, 40);
        characterBuffsAndDebuffs3 = InsertsSpaces(characterBuffsAndDebuffs3, 40);
        characterBuffsAndDebuffs4 = InsertsSpaces(characterBuffsAndDebuffs4, 40);

        string enemyBuffsAndDebuffs1 = "  ";
        string se1 =CalculateCubes(Enemies.towerEnemies[0].MaxHP,Enemies.towerEnemies[0].HP,true);
        se1 += "   " + Enemies.towerEnemies[0].Name;
        if (Enemies.towerEnemies[0].IsBuffed)
            enemyBuffsAndDebuffs1 +="\u001b[34m⚔\u001b[0m ";
        if (Enemies.towerEnemies[0].IsPoisonned)
            enemyBuffsAndDebuffs1 += "\u001b[32m☠"+ "\u001b[0m ";
        if (Enemies.towerEnemies[0].IsStuned)
            enemyBuffsAndDebuffs1 += "\u001b[33m꩜"+ "\u001b[0m";
        string enemyBuffsAndDebuffs2 = "  ";
        string enemyBuffsAndDebuffs3 = "  ";
        string enemyBuffsAndDebuffs4 = "  ";
        
        string se2="";
        if (Enemies.towerEnemies.Count>1)
        {
            se2 = CalculateCubes(Enemies.towerEnemies[1].MaxHP, Enemies.towerEnemies[1].HP,true);
            se2 += "   " + Enemies.towerEnemies[1].Name;
            if (Enemies.towerEnemies[1].IsBuffed)
                enemyBuffsAndDebuffs2 +="\u001b[34m⚔\u001b[0m ";
            if (Enemies.towerEnemies[1].IsPoisonned)
                enemyBuffsAndDebuffs2 += "\u001b[32m☠"+ "\u001b[0m ";
            if (Enemies.towerEnemies[1].IsStuned)
                enemyBuffsAndDebuffs2 += "\u001b[33m꩜"+ "\u001b[0m";
            
        }
        string se3="";
        if (Enemies.towerEnemies.Count>2)
        {
            se3 = CalculateCubes(Enemies.towerEnemies[2].MaxHP, Enemies.towerEnemies[2].HP,true);
            se3 += "   " + Enemies.towerEnemies[2].Name;
            if (Enemies.towerEnemies[2].IsBuffed)
                enemyBuffsAndDebuffs3 +="\u001b[34m⚔\u001b[0m ";
            if (Enemies.towerEnemies[2].IsPoisonned)
                enemyBuffsAndDebuffs3 += "\u001b[32m☠"+ "\u001b[0m ";
            if (Enemies.towerEnemies[2].IsStuned)
                enemyBuffsAndDebuffs3 += "\u001b[33m꩜"+ "\u001b[0m";
        }
        string se4="";
        if (Enemies.towerEnemies.Count>3)
        {
            se4 = CalculateCubes(Enemies.towerEnemies[3].MaxHP, Enemies.towerEnemies[3].HP,true);
            se4 += "   " + Enemies.towerEnemies[3].Name;
            if (Enemies.towerEnemies[3].IsBuffed)
                enemyBuffsAndDebuffs4 +="\u001b[34m⚔\u001b[0m ";
            if (Enemies.towerEnemies[3].IsPoisonned)
                enemyBuffsAndDebuffs4 += "\u001b[32m☠"+ "\u001b[0m ";
            if (Enemies.towerEnemies[3].IsStuned)
                enemyBuffsAndDebuffs4 += "\u001b[33m꩜"+ "\u001b[0m";
        }
        
        se1 = InsertsSpaces(se1,23);
        se2 = InsertsSpaces(se2,23);
        se3 = InsertsSpaces(se3,23);
        se4 = InsertsSpaces(se4,23);
        

        enemyBuffsAndDebuffs1 = InsertsSpaces(enemyBuffsAndDebuffs1, 23);
        enemyBuffsAndDebuffs2 = InsertsSpaces(enemyBuffsAndDebuffs2, 23);
        enemyBuffsAndDebuffs3 = InsertsSpaces(enemyBuffsAndDebuffs3, 23);
        enemyBuffsAndDebuffs4 = InsertsSpaces(enemyBuffsAndDebuffs4, 23);
        
        se1 +="|";
        se2 +="|";
        se3 +="|";
        se4 +="|";
        if (Arrow == 1)
        {
            se1 += arrowPointer;
        }
        else if (Arrow == 2)
        {
            se2 += arrowPointer;
        }
        else if (Arrow == 3)
        {
            se3 += arrowPointer;
        }
        else
        {
            se4 += arrowPointer;
        }
        
        
        Console.WriteLine("\n" +
                          "                         \u001b[31mCombat\u001b[0m - \u001b[36mTower Level "+Dungeon.towerStageClear+"\u001b[0m\n");
        Console.WriteLine("| "+characterHealthAndNames1+se1);
        Console.WriteLine("| "+characterBuffsAndDebuffs1+enemyBuffsAndDebuffs1+"|");
        
        Console.WriteLine("| "+characterHealthAndNames2+se2);
        Console.WriteLine("| "+characterBuffsAndDebuffs2+enemyBuffsAndDebuffs2+"|");
        
        Console.WriteLine("| "+characterHealthAndNames3+se3);
        Console.WriteLine("| "+characterBuffsAndDebuffs3+enemyBuffsAndDebuffs3+"|");
        
        Console.WriteLine("| "+characterHealthAndNames4+se4);
        Console.WriteLine("| "+characterBuffsAndDebuffs4+enemyBuffsAndDebuffs4+"|");
        
        if (displayChars)
            DisplayCharacterInfo(character);
        else
        {
            DisplayEnemyInfo(Enemies.towerEnemies[Arrow-1]);
        }
        Console.WriteLine("|                                                                       |\n" +
                          "|\u001b[31m[<-]\u001b[0m                            \u001b[34mBack(b)\u001b[0m" +
                          "                            \u001b[31m[->]\u001b[0m|\n" +
                          "|                                                                       |\n" +
                          "\\-----------------------------------------------------------------------/\n");

        ConsoleKeyInfo keyInfo = Console.ReadKey(true);

        switch (keyInfo.Key)
        {
            case ConsoleKey.B:
                foreach (Character characterBis in Dungeon.Team)
                {
                    characterBis.IsAlive = false;
                }
                break;
            case ConsoleKey.Q:
                if (character.Skill1.CooldownLeft == 0)
                {
                    DoesCharacterCooldown();
                    character.Skill1.CooldownLeft = character.Skill1.Cooldown;
                    AttackEnemy(Enemies.towerEnemies[Arrow-1],character,1, characterCollection);
                    StartFight(characterCollection);
                }
                else
                {
                    DisplayCharacterTurn(character, characterCollection);
                }
                break;
            case ConsoleKey.W:
                if (character.Skill2.CooldownLeft == 0)
                {
                    DoesCharacterCooldown();
                    character.Skill2.CooldownLeft = character.Skill2.Cooldown;
                    AttackEnemy(Enemies.towerEnemies[Arrow-1],character,2, characterCollection);
                    StartFight(characterCollection);
                }
                else
                {
                    DisplayCharacterTurn(character, characterCollection);
                }
                break;
            case ConsoleKey.R:
                if (character.UltSkill.CooldownLeft == 0)
                {
                    DoesCharacterCooldown();
                    character.UltSkill.CooldownLeft = character.UltSkill.Cooldown;
                    AttackEnemy(Enemies.towerEnemies[Arrow-1],character,3, characterCollection);
                    StartFight(characterCollection);
                }
                else
                {
                    DisplayCharacterTurn(character, characterCollection);
                }
                break;
            case ConsoleKey.DownArrow:
                if (Arrow == 4)
                    Arrow = 1;
                else
                {
                    Arrow += 1;
                }
                DisplayCharacterTurn(character,characterCollection);
                break;
            case ConsoleKey.RightArrow:
                if (displayChars)
                    displayChars = false;
                else
                {
                    displayChars = true;
                }
                DisplayCharacterTurn(character, characterCollection);
                break;
            case ConsoleKey.LeftArrow:
                if (displayChars)
                    displayChars = false;
                else
                {
                    displayChars = true;
                }
                DisplayCharacterTurn(character, characterCollection);
                break;
            case ConsoleKey.UpArrow:
                if (Arrow == 1)
                    Arrow = 4;
                else
                {
                    Arrow -= 1;
                }
                DisplayCharacterTurn(character, characterCollection);
                break;
            default: //attack the selected enemy
                DisplayCharacterTurn(character, characterCollection);
                break;
        }
        
    }

    public static void DoesCharacterCooldown()
    {
        foreach (var character in Dungeon.Team)
        {
            if (character.Skill1.CooldownLeft > 0)
                character.Skill1.CooldownLeft -= 1;
            if (character.Skill2.CooldownLeft > 0)
                character.Skill2.CooldownLeft -= 1;
            if (character.UltSkill.CooldownLeft > 0)
                character.UltSkill.CooldownLeft -= 1;
        }
    }

    public static void AttackEnemy(Enemies enemies, Character character, int skill, List<Character> characterCollection)
    {

        Random rd = new Random();
        int dodgeLuck = rd.Next(1, 11);
        Random rdv = new Random();
        int accuracyLuck = rdv.Next(1, 11);
        dodgeLuck += enemies.buffedDodge;
        accuracyLuck += character.buffedAccuracy;
        bool dodged = false;
        if (character.IsStuned)
        {
            Console.WriteLine("The character is stunned. It cannot attack until next turn.");
            Console.ReadKey();
            character.IsStuned = false;
        } //Check Stuned
        if (dodgeLuck>accuracyLuck)
        {
            Console.WriteLine("The enemy \u001b[31mdodged\u001b[0m!");
            Console.ReadKey();
        }
        else
        {
            if (skill == 1) // Skill 1 
            {
                if (character.Skill1.BuffEffect == "" && character.Skill1.DebuffEffect == "") //NORMAL ATK
                {
                    if (character.Skill1.isSingleTarget && !dodged) //SGT
                        enemies.HP -= character.buffedAtk*character.Skill1.AttackMultiplier/100;
                    else
                    {
                        foreach (Enemies enemy in Enemies.towerEnemies) //AOE
                        {
                            if (!dodged)
                                enemy.HP -= character.buffedAtk*character.Skill1.AttackMultiplier/100;
                        }
                    }
                    
                }
                else if (character.Skill1.DebuffEffect == "") //BUFF
                {
                    character.IsBuffed = true;
                    if (!character.Skill1.isAoeBuff) //SGT
                    {
                        if (character.Skill1.BuffEffect == "dodge")
                        {
                            character.buffedDodge +=character.buffedDodge*character.Skill1.buffPercentage/100;
                            
                        }
                        else if (character.Skill1.BuffEffect == "speed")
                        {
                            character.buffedSpeed +=character.buffedSpeed*character.Skill1.buffPercentage/100;
                        }
                        else if (character.Skill1.BuffEffect == "atk")
                        {
                            character.buffedAtk +=character.buffedAtk*character.Skill1.buffPercentage/100;
                        }
                        else //Accuracy
                        {
                            character.buffedAccuracy +=character.buffedAccuracy*character.Skill1.buffPercentage/100;
                        }
                        if (!dodged)
                            enemies.HP -= character.buffedAtk * character.Skill1.AttackMultiplier / 100;
                    }
                    else
                    {
                        if (character.Skill1.BuffEffect == "dodge")
                        {
                            foreach (Character charac in Dungeon.Team) //AOE
                            {
                                charac.buffedDodge +=charac.buffedDodge*charac.Skill1.buffPercentage/100;
                                charac.IsBuffed = true;
                            }

                        }
                        else if (character.Skill1.BuffEffect == "speed")
                        {
                            foreach (Character charac in Dungeon.Team) //AOE
                            {
                                charac.buffedSpeed +=charac.buffedSpeed*charac.Skill1.buffPercentage/100;
                                charac.IsBuffed = true;
                            }
                        }
                        else if (character.Skill1.BuffEffect == "atk")
                        {
                            foreach (Character charac in Dungeon.Team) //AOE
                            {
                                charac.buffedAtk +=charac.buffedAtk*charac.Skill1.buffPercentage/100;
                                charac.IsBuffed = true;
                            }
                        }
                        else //Accuracy
                        {
                            foreach (Character charac in Dungeon.Team) //AOE
                            {
                                charac.buffedAccuracy +=charac.buffedAccuracy*charac.Skill1.buffPercentage/100;
                                charac.IsBuffed = true;
                            }
                        }
                        foreach (Enemies enemy in Enemies.towerEnemies) //AOE
                        {
                            if (!dodged)
                                enemy.HP -= character.buffedAtk*character.Skill1.AttackMultiplier/100;
                        }
                    }
                    
                }
                else //DEBUFF
                {
                    if (character.Skill1.isSingleTarget) //SGT
                    {
                        if (character.Skill1.DebuffEffect == "stun")
                        {
                            enemies.IsStuned = true;
                        }
                        else //Poisonned
                        {
                            enemies.IsPoisonned = true;
                        }
                        if (!dodged)
                            enemies.HP -= character.buffedAtk * character.Skill1.AttackMultiplier / 100;
                    }
                    else
                    {
                        foreach (Enemies enemy in Enemies.towerEnemies) //AOE
                        {
                            if (character.Skill1.DebuffEffect == "stun")
                            {
                                enemy.IsStuned = true;
                            }
                            else //Poisonned
                            {
                                enemy.IsPoisonned = true;
                            }
                            if (!dodged)
                                enemy.HP -= character.buffedAtk*character.Skill1.AttackMultiplier/100;
                        }
                    }
                }
            }
            else if (skill == 2) // Skill 2
            {
                if (character.Skill2.BuffEffect == "" && character.Skill2.DebuffEffect == "") //NORMAL ATK
                {
                    if (character.Skill2.isSingleTarget && !dodged) //SGT
                        enemies.HP -= character.buffedAtk*character.Skill2.AttackMultiplier/100;
                    else
                    {
                        foreach (Enemies enemy in Enemies.towerEnemies) //AOE
                        {
                            if (!dodged)
                                enemy.HP -= character.buffedAtk*character.Skill2.AttackMultiplier/100;
                        }
                    }
                    
                }
                else if (character.Skill2.DebuffEffect == "") //BUFF
                {
                    character.IsBuffed = true;
                    if (!character.Skill2.isAoeBuff) //SGT
                    {
                        if (character.Skill2.BuffEffect == "dodge")
                        {
                            character.buffedDodge +=character.buffedDodge*character.Skill2.buffPercentage/100;
                            
                        }
                        else if (character.Skill2.BuffEffect == "speed")
                        {
                            character.buffedSpeed +=character.buffedSpeed*character.Skill2.buffPercentage/100;
                        }
                        else if (character.Skill2.BuffEffect == "atk")
                        {
                            character.buffedAtk +=character.buffedAtk*character.Skill2.buffPercentage/100;
                        }
                        else //Accuracy
                        {
                            character.buffedAccuracy +=character.buffedAccuracy*character.Skill2.buffPercentage/100;
                        }
                        if (!dodged)
                            enemies.HP -= character.buffedAtk * character.Skill2.AttackMultiplier / 100;
                    }
                    else
                    {
                        if (character.Skill2.BuffEffect == "dodge")
                        {
                            foreach (Character charac in Dungeon.Team) //AOE
                            {
                                charac.IsBuffed = true;
                                charac.buffedDodge +=charac.buffedDodge*charac.Skill2.buffPercentage/100;
                            }

                        }
                        else if (character.Skill2.BuffEffect == "speed")
                        {
                            foreach (Character charac in Dungeon.Team) //AOE
                            {
                                charac.IsBuffed = true;
                                charac.buffedSpeed +=charac.buffedSpeed*charac.Skill2.buffPercentage/100;
                            }
                        }
                        else if (character.Skill2.BuffEffect == "atk")
                        {
                            foreach (Character charac in Dungeon.Team) //AOE
                            {
                                charac.IsBuffed = true;
                                charac.buffedAtk +=charac.buffedAtk*charac.Skill2.buffPercentage/100;
                            }
                        }
                        else //Accuracy
                        {
                            foreach (Character charac in Dungeon.Team) //AOE
                            {
                                charac.IsBuffed = true;
                                charac.buffedAccuracy +=charac.buffedAccuracy*charac.Skill2.buffPercentage/100;
                            }
                        }
                        foreach (Enemies enemy in Enemies.towerEnemies) //AOE
                        {
                            if (!dodged)
                                enemy.HP -= character.buffedAtk*character.Skill2.AttackMultiplier/100;
                        }
                    }
                    
                }
                else //DEBUFF
                {
                    if (character.Skill2.isSingleTarget) //SGT
                    {
                        if (character.Skill2.DebuffEffect == "stun")
                        {
                            enemies.IsStuned = true;
                        }
                        else //Poisonned
                        {
                            enemies.IsPoisonned = true;
                        }
                        if (!dodged)
                            enemies.HP -= character.buffedAtk * character.Skill2.AttackMultiplier / 100;
                    }
                    else
                    {
                        foreach (Enemies enemy in Enemies.towerEnemies) //AOE
                        {
                            if (character.Skill2.DebuffEffect == "stun")
                            {
                                enemy.IsStuned = true;
                            }
                            else //Poisonned
                            {
                                enemy.IsPoisonned = true;
                            }
                            if (!dodged)
                                enemy.HP -= character.buffedAtk*character.Skill2.AttackMultiplier/100;
                        }
                    }
                }
            }
            else // Ultimate
            {
                if (character.UltSkill.BuffEffect == "" && character.UltSkill.DebuffEffect == "")
                {
                    if (character.UltSkill.isSingleTarget && !dodged) //SGT
                        enemies.HP -= character.buffedAtk*character.UltSkill.AttackMultiplier/100;
                    else
                    {
                        foreach (Enemies enemy in Enemies.towerEnemies)
                        {
                            if (!dodged)
                                enemy.HP -= character.buffedAtk*character.UltSkill.AttackMultiplier/100;
                        }
                    }
                }
                else if (character.UltSkill.DebuffEffect == "") //BUFF
                {
                    character.IsBuffed = true;
                    if (!character.UltSkill.isAoeBuff) //SGT
                    {
                        if (character.UltSkill.BuffEffect == "dodge")
                        {
                            character.buffedDodge +=character.buffedDodge*character.UltSkill.buffPercentage/100;
                            
                        }
                        else if (character.UltSkill.BuffEffect == "speed")
                        {
                            character.buffedSpeed +=character.buffedSpeed*character.UltSkill.buffPercentage/100;
                        }
                        else if (character.UltSkill.BuffEffect == "atk")
                        {
                            character.buffedAtk +=character.buffedAtk*character.UltSkill.buffPercentage/100;
                        }
                        else //Accuracy
                        {
                            character.buffedAccuracy +=character.buffedAccuracy*character.UltSkill.buffPercentage/100;
                        }
                        if (!dodged)
                            enemies.HP -= character.buffedAtk * character.UltSkill.AttackMultiplier / 100;
                    }
                    else
                    {
                        if (character.UltSkill.BuffEffect == "dodge")
                        {
                            foreach (Character charac in Dungeon.Team) //AOE
                            {
                                charac.IsBuffed = true;
                                charac.buffedDodge +=charac.buffedDodge*charac.UltSkill.buffPercentage/100;
                            }

                        }
                        else if (character.UltSkill.BuffEffect == "speed")
                        {
                            foreach (Character charac in Dungeon.Team) //AOE
                            {
                                charac.IsBuffed = true;
                                charac.buffedSpeed +=charac.buffedSpeed*charac.UltSkill.buffPercentage/100;
                            }
                        }
                        else if (character.UltSkill.BuffEffect == "atk")
                        {
                            foreach (Character charac in Dungeon.Team) //AOE
                            {
                                charac.IsBuffed = true;
                                charac.buffedAtk +=charac.buffedAtk*charac.UltSkill.buffPercentage/100;
                            }
                        }
                        else //Accuracy
                        {
                            foreach (Character charac in Dungeon.Team) //AOE
                            {
                                charac.IsBuffed = true;
                                charac.buffedAccuracy +=charac.buffedAccuracy*charac.UltSkill.buffPercentage/100;
                            }
                        }
                        foreach (Enemies enemy in Enemies.towerEnemies) //AOE
                        {
                            if (!dodged)
                                enemy.HP -= character.buffedAtk*character.UltSkill.AttackMultiplier/100;
                        }
                    }
                    
                }
                else //DEBUFF
                {
                    if (character.UltSkill.isSingleTarget) //SGT
                    {
                        if (character.UltSkill.DebuffEffect == "stun")
                        {
                            enemies.IsStuned = true;
                        }
                        else //Poisonned
                        {
                            enemies.IsPoisonned = true;
                        }
                        if (!dodged)
                            enemies.HP -= character.buffedAtk * character.UltSkill.AttackMultiplier / 100;
                    }
                    else
                    {
                        foreach (Enemies enemy in Enemies.towerEnemies) //AOE
                        {
                            if (character.UltSkill.DebuffEffect == "stun")
                            {
                                enemy.IsStuned = true;
                            }
                            else //Poisonned
                            {
                                enemy.IsPoisonned = true;
                            }
                            if (!dodged)
                                enemy.HP -= character.buffedAtk*character.UltSkill.AttackMultiplier/100;
                        }
                    }
                }
            }

        }
        if (character.IsPoisonned)
        {
            Console.WriteLine("The character is poisoned. It loses "+character.HP/20+ "HP.");
            character.IsPoisonned = false;
            Console.ReadKey();
            character.HP-=(character.HP/20);
        }
    }

    public static void DisplayEnemyInfo(Enemies enemies)
    {
        Console.WriteLine("/-----------------------------------------------------------------------\\\n" +
                          "|                                                                       |\n" +
                          "|                             \u001b[31mEnemy Skill(s)\u001b[0m                            |\n"+
                          "|                                                                       |");
        string s = $"\u001b[31mSkill 1\u001b[0m Details: {enemies.Skill1.Description}";
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
        s = $"\u001b[36mUltimate\u001b[0m : {enemies.UltSkill.Description}";
        
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
    }
    public static void DisplayCharacterInfo(Character character)
    {
        Console.WriteLine("/-----------------------------------------------------------------------\\\n" +
                          "|                                                                       |\n" +
                          "|                             \u001b[36mAlly Skill(s)\u001b[0m                             |\n"+
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
            if (i+jj >= s.Length - 1)
            {
                while (len < 67)
                {
                    res += " ";
                    len += 1;
                }

                if (character.Skill1.CooldownLeft == 0)
                {
                    res += "[Q]";

                    len += 3;
                }
            }
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
            if (i+jj >= s.Length - 1)
            {
                while (len < 67)
                {
                    res += " ";
                    len += 1;
                }

                if (character.Skill2.CooldownLeft == 0)
                {
                    res += "[W]";

                    len += 3;
                }
            }
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
            if (i+jj >= s.Length - 1)
            {
                while (len < 67)
                {
                    res += " ";
                    len += 1;
                }
                if (character.UltSkill.CooldownLeft==0)
                {
                    res += "[R]";

                    len += 3;
                }
            }
            while (len < 71)
            {
                res += " ";
                len += 1;
            }
            Console.WriteLine(res + "|");
        }
    }
    public static string InsertsSpaces(string s, int size) // Needs at least a string of len 2
    {
        int jj;
        string result = "";
        for (int i = 1; i < s.Length; i += jj)
        {
            
            string res = "";
            int len = 0;
            bool insideColorCode = false;
            for (int j = i-1; j < s.Length; j++)
            {
                if (s[j] == '\u001b')
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
            while (len < size)
            {
                res += " ";
                len += 1;
            }

            result += res;
        }

        return result;
    }

    public static string CalculateCubes(int maxHp, int hp, bool isEnemy)
    {
        int variable = maxHp;
        string greenCubes = "\u001b[32m";
        string redCubes = "\u001b[31m";
        if (isEnemy)
            greenCubes = "\u001b[35m";
        for (int i = 0; i <hp*10; i+=variable)
        {
            greenCubes += "█";
        }
        for (int i = hp*10; i <maxHp*10; i+=variable)
        {
            redCubes += "█";
        }
        return greenCubes+"\u001b[0m"+redCubes+"\u001b[0m";
    }

    public static void UpdateTurnPriority()
    {
        foreach (Character character in Dungeon.Team)
        {
            character.TurnPriority += character.buffedSpeed;
        }

        foreach (Enemies enemy in Enemies.towerEnemies)
        {
            enemy.TurnPriority += enemy.buffedSpeed;
        }
    }

    public static Character FastestCharacter() //returns the character with the highest turn priority
    {
        Character tempFastest = new Character();
        tempFastest.TurnPriority = 0;
        foreach (Character character in Dungeon.Team)
        {
            if (character.TurnPriority > tempFastest.TurnPriority)
                tempFastest = character;
        }

        return tempFastest;
    }
    
    public static Enemies FastestEnemy() //returns the enemy with the highest turn priority
    {
        Enemies tempFastest = new Enemies();
        tempFastest.TurnPriority = 0;
        foreach (Enemies enemy in Enemies.towerEnemies)
        {
            if (enemy.TurnPriority > tempFastest.TurnPriority)
                tempFastest = enemy;
        }

        return tempFastest;
    }
    
}
