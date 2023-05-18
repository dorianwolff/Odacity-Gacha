namespace Odacity;

public class Combat
{
    public static int TurnCount;
    public static int Arrow; // 1, 2, 3 or 4;
    public static bool displayChars;
    public static int damageTaken;
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
            character.buffedDodge = character.Dodge;
            character.buffedAccuracy = character.Accuracy;
            character.IsBuffed = false;
            character.Skill1.CooldownLeft = 0;
            character.Skill2.CooldownLeft = 0;
            character.UltSkill.CooldownLeft = character.UltSkill.Cooldown;
            character.nbTurnsBuffed = 0;
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
            enemy.buffedDodge = enemy.Dodge;
            enemy.buffedAccuracy = enemy.Accuracy;
            enemy.IsBuffed = false;
            enemy.Skill1.CooldownLeft = 0;
            enemy.UltSkill.CooldownLeft = enemy.UltSkill.Cooldown;
            enemy.nbTurnsBuffed = 0;
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
        foreach (Character chars in Dungeon.Team)
        {
            if (chars.HP <= 0)
            {
                chars.HP = 0;
                chars.IsAlive = false;
            }
        }

        bool dedTeam = ADeadTeam();
        if (character.TurnPriority >= enemy.TurnPriority &&!dedTeam)
        {
            character.TurnPriority = 0;
            DisplayCharacterTurn(character,characterCollection);
        }
        else if (!dedTeam)
        {
            enemy.TurnPriority = 0;
            DisplayEnemyTurn(enemy);
            TurnCount+=1;
            UpdateTurnPriority();
            StartFight(characterCollection);
        }
        

    }

    public static bool ADeadTeam()
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


    public static void DisplayEnemyTurn(Enemies enemy)
    {
        Character characterTargeted = Dungeon.Team[0];
        foreach (Character character in Dungeon.Team)
        {
            if (character.IsAlive && character.HP < characterTargeted.HP)
                characterTargeted = character;
        }
        if (ADeadTeam())
            return;
        Console.Clear();
        Console.WriteLine();
        Console.WriteLine("\u001b[31mEnemy\u001b[0m turn");
        
        string isActive1 = "\u001b[0m";
        string isActive2 = "\u001b[0m";
        string isActive3 = "\u001b[0m";

        string characterBuffsAndDebuffs1 = "  ";
        string characterBuffsAndDebuffs2 = "  ";
        string characterBuffsAndDebuffs3 = "  ";
        string characterBuffsAndDebuffs4 = "  ";
        
        string sc1 =CalculateCubes(Dungeon.Team[0].MaxHP,Dungeon.Team[0].HP,false);
        if ( characterTargeted == Dungeon.Team[0])
        {
            isActive1 = "\u001b[33m";
            if (!Dungeon.Team[0].IsAlive)
                isActive1 = "\u001b[31m";
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
            if (characterTargeted == Dungeon.Team[1])
                isActive2 = "\u001b[33m";
            if (!Dungeon.Team[1].IsAlive)
                isActive2 = "\u001b[31m";
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
            if (characterTargeted == Dungeon.Team[2])
                isActive3 = "\u001b[33m";
            if (!Dungeon.Team[2].IsAlive)
                isActive3 = "\u001b[31m";
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
        if (!Enemies.towerEnemies[0].IsAlive)
            se1 += "   \u001b[31m" + Enemies.towerEnemies[0].Name+"\u001b[0m";
        else if (Enemies.towerEnemies[0]==enemy) //active
        {
            se1 += "   \u001b[32m" + Enemies.towerEnemies[0].Name+"\u001b[0m";
        }
        else
        {
            se1 += "   " + Enemies.towerEnemies[0].Name;
        }
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
            if (!Enemies.towerEnemies[1].IsAlive)
                se2 += "   \u001b[31m" + Enemies.towerEnemies[1].Name+"\u001b[0m";
            else if (Enemies.towerEnemies[1]==enemy) //active
            {
                se2 += "   \u001b[32m" + Enemies.towerEnemies[1].Name+"\u001b[0m";
            }
            else
            {
                se2 += "   " + Enemies.towerEnemies[1].Name;
            }
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
            if (!Enemies.towerEnemies[2].IsAlive)
                se3 += "   \u001b[31m" + Enemies.towerEnemies[2].Name+"\u001b[0m";
            else if (Enemies.towerEnemies[2]==enemy) //active
            {
                se3 += "   \u001b[32m" + Enemies.towerEnemies[2].Name+"\u001b[0m";
            }
            else
            {
                se3 += "   " + Enemies.towerEnemies[2].Name;
            }
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
            if (!Enemies.towerEnemies[3].IsAlive)
                se4 += "   \u001b[31m" + Enemies.towerEnemies[3].Name+"\u001b[0m";
            else if (Enemies.towerEnemies[3]==enemy) //active
            {
                se4 += "   \u001b[32m" + Enemies.towerEnemies[3].Name+"\u001b[0m";
            }
            else
            {
                se4 += "   " + Enemies.towerEnemies[3].Name;
            }
            if (Enemies.towerEnemies[3].IsBuffed)
                enemyBuffsAndDebuffs4 +="\u001b[34m⚔\u001b[0m ";
            if (Enemies.towerEnemies[3].IsPoisonned)
                enemyBuffsAndDebuffs4 += "\u001b[32m☠"+ "\u001b[0m ";
            if (Enemies.towerEnemies[3].IsStuned)
                enemyBuffsAndDebuffs4 += "\u001b[33m꩜"+ "\u001b[0m";
        }
        
        se1 = InsertsSpaces(se1,30);
        se2 = InsertsSpaces(se2,30);
        se3 = InsertsSpaces(se3,30);
        se4 = InsertsSpaces(se4,30);
        

        enemyBuffsAndDebuffs1 = InsertsSpaces(enemyBuffsAndDebuffs1, 30);
        enemyBuffsAndDebuffs2 = InsertsSpaces(enemyBuffsAndDebuffs2, 30);
        enemyBuffsAndDebuffs3 = InsertsSpaces(enemyBuffsAndDebuffs3, 30);
        enemyBuffsAndDebuffs4 = InsertsSpaces(enemyBuffsAndDebuffs4, 30);
        
        se1 +="|";
        se2 +="|";
        se3 +="|";
        se4 +="|";


        Console.WriteLine("\n" +
                          "                         \u001b[31mCombat\u001b[0m - \u001b[36mTower Level "+Dungeon.towerStageClear+"\u001b[0m\n");
        Console.WriteLine("/-----------------------------------------------------------------------\\");
        Console.WriteLine("| "+characterHealthAndNames1+se1);
        Console.WriteLine("| "+characterBuffsAndDebuffs1+enemyBuffsAndDebuffs1+"|");
        
        Console.WriteLine("| "+characterHealthAndNames2+se2);
        Console.WriteLine("| "+characterBuffsAndDebuffs2+enemyBuffsAndDebuffs2+"|");
        
        Console.WriteLine("| "+characterHealthAndNames3+se3);
        Console.WriteLine("| "+characterBuffsAndDebuffs3+enemyBuffsAndDebuffs3+"|");
        
        Console.WriteLine("| "+characterHealthAndNames4+se4);
        Console.WriteLine("| "+characterBuffsAndDebuffs4+enemyBuffsAndDebuffs4+"|");
        
        DisplayEnemyInfo(enemy,enemy.UltSkill.Cooldown);
        
        Console.WriteLine("|                                                                       |\n" +
                          "|                                                                       |\n" +
                          "|                                                                       |\n" +
                          "\\-----------------------------------------------------------------------/\n");
        
        if (enemy.IsPoisonned) //Check if poisonned
        {
            Console.WriteLine("The enemy is poisoned. It loses "+enemy.HP/20+ "HP.");
            enemy.IsPoisonned = false;
            enemy.HP-=(enemy.HP/20);
        }

        if (enemy.IsBuffed)
        {
            enemy.nbTurnsBuffed -= 1;
            if (enemy.nbTurnsBuffed == 0)
                enemy.IsBuffed = false;
            if (enemy.IsBuffed == false)
            {
                enemy.buffedAccuracy = enemy.Accuracy;
                enemy.buffedAtk = enemy.Attack;
                enemy.buffedDodge = enemy.Dodge;
                enemy.buffedSpeed = enemy.Speed;
            }
        }
        
        Random rd = new Random();
        int dodgeLuck = rd.Next(1, 11);
        Random rdv = new Random();
        int accuracyLuck = rdv.Next(1, 11);
        dodgeLuck += characterTargeted.buffedDodge;
        accuracyLuck += enemy.buffedAccuracy;
        if (enemy.IsStuned) // Check if stunned
        {
            Console.WriteLine("The enemy is stunned. It cannot attack until next turn.");
            enemy.IsStuned = false;
        }
        else
        {
            Console.WriteLine();
            if (enemy.UltSkill.CooldownLeft == 0) //Ult skill
            {
                DoesEnemyCooldown(enemy);
                enemy.UltSkill.CooldownLeft = enemy.UltSkill.Cooldown;
                if (dodgeLuck > accuracyLuck)
            {
                Console.WriteLine("\u001b[33m"+characterTargeted.Name+"\u001b[0m has \u001b[31mdodged\u001b[0m \u001b[35m"+enemy.Name+"\u001b[0m's ultimate attack!");
            }
                else
            {
                if (enemy.UltSkill.BuffEffect == "" && enemy.UltSkill.DebuffEffect == "") //Normal attack
                {
                    if (enemy.UltSkill.isSingleTarget) //SGT
                    {
                        characterTargeted.HP -= enemy.buffedAtk*enemy.UltSkill.AttackMultiplier/100;
                        Console.WriteLine("\u001b[35m"+enemy.Name + "\u001b[0m attacked \u001b[33m"+characterTargeted.Name+"\u001b[0m with his ultimate skill and dealt "+enemy.buffedAtk*enemy.UltSkill.AttackMultiplier/100+" \u001b[31mdamage\u001b[0m.");
                    }
                    else
                    {
                        foreach (Enemies character in Enemies.towerEnemies) //AOE
                        {
                            character.HP -= enemy.buffedAtk*enemy.UltSkill.AttackMultiplier/100;
                        }
                        Console.WriteLine("\u001b[35m"+enemy.Name + "\u001b[0m attacked \u001b[33mall allies\u001b[0m with his ultimate skill and dealt "+enemy.buffedAtk*enemy.UltSkill.AttackMultiplier/100+" \u001b[31mdamage\u001b[0m to all your characters.");
                    }
                }
                else if (enemy.UltSkill.DebuffEffect == "") //Buff attack
                {
                    enemy.IsBuffed = true;
                    if (!enemy.UltSkill.isAoeBuff) //SGT
                    {
                        if (enemy.UltSkill.BuffEffect == "dodge")
                        {
                            enemy.buffedDodge +=enemy.buffedDodge*enemy.UltSkill.buffPercentage/100;
                            
                        }
                        else if (enemy.UltSkill.BuffEffect == "speed")
                        {
                            enemy.buffedSpeed +=enemy.buffedSpeed*enemy.UltSkill.buffPercentage/100;
                        }
                        else if (enemy.UltSkill.BuffEffect == "atk")
                        {
                            enemy.buffedAtk +=enemy.buffedAtk*enemy.UltSkill.buffPercentage/100;
                        }
                        else //Accuracy
                        {
                            enemy.buffedAccuracy +=enemy.buffedAccuracy*enemy.UltSkill.buffPercentage/100;
                        }
                        Console.WriteLine("\u001b[35m"+enemy.Name + "\u001b[0m attacked \u001b[33mall allies\u001b with his ultimate skill and dealt \u001b[31m"+enemy.buffedAtk*enemy.UltSkill.AttackMultiplier/100+
                                          " \u001b[0mdamage, and buffed himself with a \u001b[34m"+enemy.UltSkill.buffPercentage+"\u001b[0m% "+enemy.UltSkill.BuffEffect+" boost.");
                        characterTargeted.HP -= enemy.buffedAtk * enemy.UltSkill.AttackMultiplier / 100;
                    }
                    else
                    {
                        if (enemy.UltSkill.BuffEffect == "dodge")
                        {
                            foreach (Enemies enem in Enemies.towerEnemies) //AOE
                            {
                                enem.buffedDodge +=enem.buffedDodge*enem.UltSkill.buffPercentage/100;
                                enem.IsBuffed = true;
                            }

                        }
                        else if (enemy.UltSkill.BuffEffect == "speed")
                        {
                            foreach (Enemies enem in Enemies.towerEnemies) //AOE
                            {
                                enem.buffedSpeed +=enem.buffedSpeed*enem.UltSkill.buffPercentage/100;
                                enem.IsBuffed = true;
                            }
                        }
                        else if (enemy.UltSkill.BuffEffect == "atk")
                        {
                            foreach (Enemies enem in Enemies.towerEnemies) //AOE
                            {
                                enem.buffedAtk +=enem.buffedAtk*enem.UltSkill.buffPercentage/100;
                                enem.IsBuffed = true;
                            }
                        }
                        else //Accuracy
                        {
                            foreach (Enemies enem in Enemies.towerEnemies) //AOE
                            {
                                enem.buffedAccuracy +=enem.buffedAccuracy*enem.UltSkill.buffPercentage/100;
                                enem.IsBuffed = true;
                            }
                        }
                        Console.WriteLine("\u001b[35m"+enemy.Name + "\u001b[0m attacked \u001b[33mall allies\u001b[0m with his ultimate skill and dealt \u001b[31m"+enemy.buffedAtk*enemy.UltSkill.AttackMultiplier/100+
                                          " \u001b[0mdamage to all your characters, and buffed his team with a \u001b[34m"+enemy.UltSkill.buffPercentage+"\u001b[0m% "+enemy.UltSkill.BuffEffect+" boost.");
                        foreach (Character charac in Dungeon.Team) //AOE
                        {
                            charac.HP -= enemy.buffedAtk*enemy.UltSkill.AttackMultiplier/100;
                        }
                    }
                }
                else //DEBUFF
                {
                    if (enemy.UltSkill.isSingleTarget) //SGT
                    {
                        if (enemy.UltSkill.DebuffEffect == "stun")
                        {
                            characterTargeted.IsStuned = true;
                        }
                        else //Poisonned
                        {
                            characterTargeted.IsPoisonned = true;
                        }
                        characterTargeted.HP -= enemy.buffedAtk * enemy.UltSkill.AttackMultiplier / 100;
                        Console.WriteLine("\u001b[33m"+characterTargeted.Name+"\u001b[0m was inflicted with \u001b[32m"+enemy.UltSkill.DebuffEffect+"\u001b[0m debuff and took \u001b[31m"+
                                          enemy.buffedAtk * enemy.UltSkill.AttackMultiplier / 100+
                                          " \u001b[0mdamage from \u001b[35m"+enemy.Name+"\u001b[0m's ultimate skill!");
                    }
                    else
                    {
                        foreach (Character charac in Dungeon.Team) //AOE
                        {
                            if (charac.UltSkill.DebuffEffect == "stun")
                            {
                                charac.IsStuned = true;
                            }
                            else //Poisonned
                            {
                                charac.IsPoisonned = true;
                            }
                            charac.HP -= enemy.buffedAtk*enemy.UltSkill.AttackMultiplier/100;
                        }
                        Console.WriteLine("\u001b[33mAll allies\u001b[0m were inflicted with \u001b[32m"+enemy.UltSkill.DebuffEffect+"\u001b[0m debuff and took \u001b[31m"+
                                         enemy.buffedAtk * enemy.UltSkill.AttackMultiplier / 100+
                                          "\u001b[0m damage from \u001b[35m"+enemy.Name+"\u001b[0m's ultimate skill!");
                    }
                }
            }
            }
            else
        {
            DoesEnemyCooldown(enemy);
            enemy.UltSkill.CooldownLeft = enemy.UltSkill.Cooldown;
            if (dodgeLuck > accuracyLuck)
            {
                Console.WriteLine("\u001b[33m"+characterTargeted.Name+"\u001b[0m has \u001b[31mdodged\u001b[0m \u001b[35m"+enemy.Name+"\u001b[0m's attack!");
            }
            else
            {
                if (enemy.Skill1.BuffEffect == "" && enemy.Skill1.DebuffEffect == "") //Normal attack
                {
                    if (enemy.Skill1.isSingleTarget) //SGT
                    {
                        characterTargeted.HP -= enemy.buffedAtk*enemy.Skill1.AttackMultiplier/100;
                        Console.WriteLine("\u001b[35m"+enemy.Name + "\u001b[0m attacked \u001b[33m"+characterTargeted.Name+
                                          "\u001b[0m and dealt \u001b[31m"+enemy.buffedAtk*enemy.Skill1.AttackMultiplier/100+"\u001b[0m damage.");
                    }
                    else
                    {
                        foreach (Enemies character in Enemies.towerEnemies) //AOE
                        {
                            character.HP -= enemy.buffedAtk*enemy.Skill1.AttackMultiplier/100;
                        }
                        Console.WriteLine("\u001b[35m"+enemy.Name + "\u001b[0m attacked \u001b[33mall allies\u001b[0m and dealt \u001b[31m"+enemy.buffedAtk*enemy.Skill1.AttackMultiplier/100+
                                          "\u001b[0m damage to all your characters.");
                    }
                }
                else if (enemy.Skill1.DebuffEffect == "") //Buff attack
                {
                    enemy.IsBuffed = true;
                    if (!enemy.Skill1.isAoeBuff) //SGT
                    {
                        if (enemy.Skill1.BuffEffect == "dodge")
                        {
                            enemy.buffedDodge +=enemy.buffedDodge*enemy.Skill1.buffPercentage/100;
                            
                        }
                        else if (enemy.Skill1.BuffEffect == "speed")
                        {
                            enemy.buffedSpeed +=enemy.buffedSpeed*enemy.Skill1.buffPercentage/100;
                        }
                        else if (enemy.Skill1.BuffEffect == "atk")
                        {
                            enemy.buffedAtk +=enemy.buffedAtk*enemy.Skill1.buffPercentage/100;
                        }
                        else //Accuracy
                        {
                            enemy.buffedAccuracy +=enemy.buffedAccuracy*enemy.Skill1.buffPercentage/100;
                        }
                        Console.WriteLine("\u001b[35m"+enemy.Name + "\u001b[0m attacked \u001b[33m"+characterTargeted.Name+"and dealt \u001b[31m"+enemy.buffedAtk*enemy.Skill1.AttackMultiplier/100+
                                          " \u001b[0mdamage, and buffed himself with a \u001b[34m"+enemy.Skill1.buffPercentage+"\u001b[0m% "+enemy.Skill1.BuffEffect+" boost.");
                        characterTargeted.HP -= enemy.buffedAtk * enemy.Skill1.AttackMultiplier / 100;
                    }
                    else
                    {
                        if (enemy.Skill1.BuffEffect == "dodge")
                        {
                            foreach (Enemies enem in Enemies.towerEnemies) //AOE
                            {
                                enem.buffedDodge +=enem.buffedDodge*enem.Skill1.buffPercentage/100;
                                enem.IsBuffed = true;
                            }

                        }
                        else if (enemy.Skill1.BuffEffect == "speed")
                        {
                            foreach (Enemies enem in Enemies.towerEnemies) //AOE
                            {
                                enem.buffedSpeed +=enem.buffedSpeed*enem.Skill1.buffPercentage/100;
                                enem.IsBuffed = true;
                            }
                        }
                        else if (enemy.Skill1.BuffEffect == "atk")
                        {
                            foreach (Enemies enem in Enemies.towerEnemies) //AOE
                            {
                                enem.buffedAtk +=enem.buffedAtk*enem.Skill1.buffPercentage/100;
                                enem.IsBuffed = true;
                            }
                        }
                        else //Accuracy
                        {
                            foreach (Enemies enem in Enemies.towerEnemies) //AOE
                            {
                                enem.buffedAccuracy +=enem.buffedAccuracy*enem.Skill1.buffPercentage/100;
                                enem.IsBuffed = true;
                            }
                        }
                        Console.WriteLine("\u001b[35m"+enemy.Name + "\u001b[0m attacked you and dealt \u001b[31m"+enemy.buffedAtk*enemy.Skill1.AttackMultiplier/100+
                                          "\u001b[0m damage to \u001b[33mall your characters\u001b[0m, and buffed his team with a \u001b[34m"+enemy.Skill1.buffPercentage+"\u001b[0m% "+enemy.Skill1.BuffEffect+" boost.");
                        foreach (Character charac in Dungeon.Team) //AOE
                        {
                            charac.HP -= enemy.buffedAtk*enemy.Skill1.AttackMultiplier/100;
                        }
                    }
                }
                else //DEBUFF
                {
                    if (enemy.Skill1.isSingleTarget) //SGT
                    {
                        if (enemy.Skill1.DebuffEffect == "stun")
                        {
                            characterTargeted.IsStuned = true;
                        }
                        else //Poisonned
                        {
                            characterTargeted.IsPoisonned = true;
                        }
                        characterTargeted.HP -= enemy.buffedAtk * enemy.Skill1.AttackMultiplier / 100;
                        Console.WriteLine("\u001b[33m"+characterTargeted.Name+"\u001b[0m was inflicted with \u001b[32m"+enemy.Skill1.DebuffEffect+"\u001b[0m debuff and took \u001b[31m"+
                                          enemy.buffedAtk * enemy.Skill1.AttackMultiplier / 100+
                                          "\u001b[0m damage from \u001b[35m"+enemy.Name+"\u001b[0m!");
                    }
                    else
                    {
                        foreach (Character charac in Dungeon.Team) //AOE
                        {
                            if (charac.Skill1.DebuffEffect == "stun")
                            {
                                charac.IsStuned = true;
                            }
                            else //Poisonned
                            {
                                charac.IsPoisonned = true;
                            }
                            charac.HP -= enemy.buffedAtk*enemy.Skill1.AttackMultiplier/100;
                        }
                        Console.WriteLine("\u001b[33mAll your characters\u001b[0m were inflicted with \u001b[32m"+enemy.Skill1.DebuffEffect+"\u001b[0m debuff and took \u001b[31m"+
                                          enemy.buffedAtk * enemy.Skill1.AttackMultiplier / 100+
                                          "\u001b[0m damage from \u001b[35m"+enemy.Name+"\u001b[0m!");
                    }
                }
            }
        }
        }
        
        Console.ReadKey();
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

        foreach (Character ca in Dungeon.Team)
        {
            if (ca.HP <= 0)
            {
                ca.IsAlive = false;
                ca.HP = 0;
            }
        }

        foreach (Enemies enem in Enemies.towerEnemies)
        {
            if (enem.HP <= 0)
            {
                enem.IsAlive = false;
                enem.HP = 0;
            }
        }
        if (ADeadTeam())
            return;
        
        
        string sc1 =CalculateCubes(Dungeon.Team[0].MaxHP,Dungeon.Team[0].HP,false);
        if (character == Dungeon.Team[0])
        {
            isActive1 = "\u001b[33m";
            if (!Dungeon.Team[0].IsAlive)
                isActive1 = "\u001b[31m";
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
            if (!Dungeon.Team[1].IsAlive)
                isActive2 = "\u001b[31m";
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
            if (!Dungeon.Team[2].IsAlive)
                isActive3 = "\u001b[31m";
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
        if (!Enemies.towerEnemies[0].IsAlive)
            se1 += "   \u001b[31m" + Enemies.towerEnemies[0].Name+"\u001b[0m";
        else
        {
            se1 += "   " + Enemies.towerEnemies[0].Name;
        }
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
            if (!Enemies.towerEnemies[1].IsAlive)
                se2 += "   \u001b[31m" + Enemies.towerEnemies[1].Name+"\u001b[0m";
            else
            {
                se2 += "   " + Enemies.towerEnemies[1].Name;
            }
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
            if (!Enemies.towerEnemies[2].IsAlive)
                se3 += "   \u001b[31m" + Enemies.towerEnemies[2].Name+"\u001b[0m";
            else
            {
                se3 += "   " + Enemies.towerEnemies[2].Name;
            }
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
            if (!Enemies.towerEnemies[3].IsAlive)
                se4 += "   \u001b[31m" + Enemies.towerEnemies[3].Name+"\u001b[0m";
            else
            {
                se4 += "   " + Enemies.towerEnemies[3].Name;
            }
            if (Enemies.towerEnemies[3].IsBuffed)
                enemyBuffsAndDebuffs4 +="\u001b[34m⚔\u001b[0m ";
            if (Enemies.towerEnemies[3].IsPoisonned)
                enemyBuffsAndDebuffs4 += "\u001b[32m☠"+ "\u001b[0m ";
            if (Enemies.towerEnemies[3].IsStuned)
                enemyBuffsAndDebuffs4 += "\u001b[33m꩜"+ "\u001b[0m";
        }
        
        se1 = InsertsSpaces(se1,30);
        se2 = InsertsSpaces(se2,30);
        se3 = InsertsSpaces(se3,30);
        se4 = InsertsSpaces(se4,30);
        

        enemyBuffsAndDebuffs1 = InsertsSpaces(enemyBuffsAndDebuffs1, 30);
        enemyBuffsAndDebuffs2 = InsertsSpaces(enemyBuffsAndDebuffs2, 30);
        enemyBuffsAndDebuffs3 = InsertsSpaces(enemyBuffsAndDebuffs3, 30);
        enemyBuffsAndDebuffs4 = InsertsSpaces(enemyBuffsAndDebuffs4, 30);
        
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
        Console.WriteLine("/-----------------------------------------------------------------------\\");
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
            DisplayEnemyInfo(Enemies.towerEnemies[Arrow-1],0);
        }
        Console.WriteLine("|                                                                       |\n" +
                          "|\u001b[31m[<-]\u001b[0m                \u001b[34mBack(b)\u001b[0m" +
                          "                \u001b[36mStats(s)\u001b[0m                \u001b[31m[->]\u001b[0m|\n" +
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
            case ConsoleKey.S:
                DisplayStats(character, Enemies.towerEnemies[Arrow-1]);
                DisplayCharacterTurn(character, characterCollection);
                break;
            case ConsoleKey.Q:
                if (character.Skill1.CooldownLeft == 0 && Enemies.towerEnemies[Arrow-1].IsAlive)
                {
                    DoesCharacterCooldown(character);
                    character.Skill1.CooldownLeft = character.Skill1.Cooldown;
                    AttackEnemy(Enemies.towerEnemies[Arrow-1],character,1, characterCollection);
                }
                else
                {
                    DisplayCharacterTurn(character, characterCollection);
                }
                break;
            case ConsoleKey.W:
                if (character.Skill2.CooldownLeft == 0 && Enemies.towerEnemies[Arrow-1].IsAlive)
                {
                    DoesCharacterCooldown(character);
                    character.Skill2.CooldownLeft = character.Skill2.Cooldown;
                    AttackEnemy(Enemies.towerEnemies[Arrow-1],character,2, characterCollection);
                }
                else
                {
                    DisplayCharacterTurn(character, characterCollection);
                }
                break;
            case ConsoleKey.R:
                if (character.UltSkill.CooldownLeft == 0 && Enemies.towerEnemies[Arrow-1].IsAlive)
                {
                    DoesCharacterCooldown(character);
                    character.UltSkill.CooldownLeft = character.UltSkill.Cooldown;
                    AttackEnemy(Enemies.towerEnemies[Arrow-1],character,3, characterCollection);
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
        foreach (Character ca in Dungeon.Team)
        {
            if (ca.HP <= 0)
            {
                ca.IsAlive = false;
                ca.HP = 0;
            }
        }

        foreach (Enemies enem in Enemies.towerEnemies)
        {
            if (enem.HP <= 0)
            {
                enem.IsAlive = false;
                enem.HP = 0;
            }
        }
        StartFight(characterCollection);
    }

    public static void DisplayStats(Character character, Enemies enemy)
    {
        Console.Clear();
        
        string s = $"|Name :{character.Name}";
        while (s.Length<44)
        {
            s += " ";
        }
        s+= $"| |Name :{enemy.Name}";
        while (s.Length<90)
        {
            s += " ";
        }
        string z = $"|Nickname : {character.Nickname}";
        while (z.Length<44)
        {
            z += " ";
        }
        z+= $"| |Nickname : {enemy.Nickname}";
        while (z.Length<90)
        {
            z += " ";
        }
        string lev = $"|Level : \u001b[31m{character.Level}\u001b[0m      Exp : {character.Experience}/{character.MaxExperience}" +
                     $"       AWAKEN : \u001b[35m{character.awakening}\u001b[0m";
        while (lev.Length<62)
        {
            lev += " ";
        }
        lev += "| |";
        while (lev.Length<108)
        {
            lev += " ";
        }
        string y = $"|Attack : {character.Attack} \u001b[34m(+{character.buffedAtk-character.Attack})\u001b[0m";
        while (y.Length<53)
        {
            y += " ";
        }
        y+= $"| |Attack : {enemy.Attack} \u001b[34m(+{enemy.buffedAtk-enemy.Attack})\u001b[0m";
        while (y.Length<108)
        {
            y += " ";
        }
        string w = $"|HP : {character.HP}/{character.MaxHP} ";
        while (w.Length<44)
        {
            w += " ";
        }
        w+= $"| |HP : {enemy.HP}/{enemy.MaxHP} ";
        while (w.Length<90)
        {
            w += " ";
        }
        string x = $"|Speed : {character.Speed} \u001b[34m(+{character.buffedSpeed-character.Speed})\u001b[0m";
        while (x.Length<53)
        {
            x += " ";
        }
        x+= $"| |Speed : {enemy.Speed} \u001b[34m(+{enemy.buffedSpeed-enemy.Speed})\u001b[0m";
        while (x.Length<108)
        {
            x += " ";
        }
        string v = $"|Dodge : {character.Dodge} \u001b[34m(+{character.buffedDodge-character.Dodge})\u001b[0m";
        while (v.Length<53)
        {
            v += " ";
        }
        v+= $"| |Dodge : {enemy.Dodge} \u001b[34m(+{enemy.buffedDodge-enemy.Dodge})\u001b[0m";
        while (v.Length<108)
        {
            v += " ";
        }
        string u = $"|Accuracy : {character.Accuracy} \u001b[34m(+{character.buffedAccuracy-character.Accuracy})\u001b[0m";
        while (u.Length<53)
        {
            u += " ";
        }
        u+= $"| |Accuracy : {enemy.Accuracy} \u001b[34m(+{enemy.buffedAccuracy-enemy.Accuracy})\u001b[0m";
        while (u.Length<108)
        {
            u += " ";
        }
        Console.WriteLine("/-------------------------------------------\\" +" /-------------------------------------------\\\n"+
                          "|            \u001b[33mCharacter Details\u001b[0m              |" +" |              \u001b[35mEnemy Details\u001b[0m                |\n"+
                          "|                                           |" +" |                                           |\n" +
                          $"|Rarity : ({character.Color}{character.Grade}\u001b[0m)                               |"+
                          " |                                           |");
        Console.WriteLine(s+"|");
        Console.WriteLine(z+"|");
        Console.WriteLine(lev+"|");
        Console.WriteLine("|                                           |"+" |                                           |\n"
                          +"|             \u001b[34mCharacter Stats\u001b[0m               |"+" |               \u001b[34mEnemy Stats\u001b[0m                 |");
        Console.WriteLine(y+"|");
        Console.WriteLine(w+"|");
        Console.WriteLine(x+"|");
        Console.WriteLine(v+"|");
        Console.WriteLine(u+"|");

        Console.WriteLine("|                                           |" +" |                                           |\n"+
                          "\\-------------------------------------------/"+" \\-------------------------------------------/\n");

        Console.ReadKey();
    } 

    public static void DoesCharacterCooldown(Character character)
    {
        if (character.Skill1.CooldownLeft > 0)
            character.Skill1.CooldownLeft -= 1;
        if (character.Skill2.CooldownLeft > 0)
            character.Skill2.CooldownLeft -= 1;
        if (character.UltSkill.CooldownLeft > 0)
            character.UltSkill.CooldownLeft -= 1;
    }

    public static void DoesEnemyCooldown(Enemies enemy)
    {
        if (enemy.Skill1.CooldownLeft > 0)
            enemy.Skill1.CooldownLeft -= 1;
        if (enemy.UltSkill.CooldownLeft > 0)
            enemy.UltSkill.CooldownLeft -= 1;
    }

    public static void AttackEnemy(Enemies enemies, Character character, int skill, List<Character> characterCollection)
    {
        
        Random rd = new Random();
        int dodgeLuck = rd.Next(1, 11);
        Random rdv = new Random();
        int accuracyLuck = rdv.Next(1, 11);
        dodgeLuck += enemies.buffedDodge;
        accuracyLuck += character.buffedAccuracy;
        if (character.IsBuffed)
        {
            character.nbTurnsBuffed -= 1;
            if (character.nbTurnsBuffed == 0)
                character.IsBuffed = false;
            if (character.IsBuffed == false)
            {
                character.buffedAccuracy = character.Accuracy;
                character.buffedAtk = character.Attack;
                character.buffedDodge = character.Dodge;
                character.buffedSpeed = character.Speed;
            }
        }
        if (character.IsStuned)
        {
            Console.WriteLine(character.Name+" is \u001b[32mstunned\u001b[0m. It cannot attack until next turn.");
            character.IsStuned = false;
        } //Check Stuned
        if (character.IsPoisonned)
        {
            Console.WriteLine(character.Name+" is \u001b[32mpoisoned\u001b[0m. It loses "+character.HP/20+ "HP.");
            character.IsPoisonned = false;
            character.HP-=(character.HP/20);
        }
        if (dodgeLuck>accuracyLuck)
        {
            Console.WriteLine("The enemy \u001b[31mdodged\u001b[0m "+character.Name+"'s attack!");
            Console.ReadKey();
        }
        else if (!character.IsStuned)
        {
            if (skill == 1) // Skill 1 
            {
                if (character.Skill1.BuffEffect == "" && character.Skill1.DebuffEffect == "") //NORMAL ATK
                {
                    if (character.Skill1.isSingleTarget) //SGT
                    {
                        enemies.HP -= character.buffedAtk * character.Skill1.AttackMultiplier / 100;
                        Console.WriteLine("\u001b[33m"+character.Name+" has dealt \u001b[31m"+character.buffedAtk * character.Skill1.AttackMultiplier / 100+
                                          "\u001b[0m damage to \u001b[35m"+enemies.Name+"\u001b[0m!");
                    }
                    else
                    {
                        foreach (Enemies enemy in Enemies.towerEnemies) //AOE
                        {
                            enemy.HP -= character.buffedAtk*character.Skill1.AttackMultiplier/100;
                        }
                        Console.WriteLine("\u001b[33m"+character.Name+"\u001b[0m has dealt \u001b[31m"+character.buffedAtk * character.Skill1.AttackMultiplier / 100+
                                          "\u001b[0m damage to \u001b[35mall enemies\u001b[0m!");
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
                        Console.WriteLine("\u001b[33m"+character.Name+"\u001b[0m has buffed himself by \u001b[34m"+character.Skill1.buffPercentage+
                                          "\u001b[0m% of "+character.Skill1.BuffEffect+" and has dealt \u001b[31m"+character.buffedAtk * character.Skill1.AttackMultiplier / 100+
                                          "\u001b[0m damage to \u001b[35m" + enemies.Name+"\u001b[0m!");
                        enemies.HP -= character.buffedAtk * character.Skill1.AttackMultiplier / 100;
                        character.nbTurnsBuffed = 2;
                    }
                    else
                    {
                        if (character.Skill1.BuffEffect == "dodge")
                        {
                            foreach (Character charac in Dungeon.Team) //AOE
                            {
                                charac.buffedDodge +=charac.buffedDodge*charac.Skill1.buffPercentage/100;
                                charac.IsBuffed = true;
                                charac.nbTurnsBuffed = 2;
                            }

                        }
                        else if (character.Skill1.BuffEffect == "speed")
                        {
                            foreach (Character charac in Dungeon.Team) //AOE
                            {
                                charac.buffedSpeed +=charac.buffedSpeed*charac.Skill1.buffPercentage/100;
                                charac.IsBuffed = true;
                                charac.nbTurnsBuffed = 2;
                            }
                        }
                        else if (character.Skill1.BuffEffect == "atk")
                        {
                            foreach (Character charac in Dungeon.Team) //AOE
                            {
                                charac.buffedAtk +=charac.buffedAtk*charac.Skill1.buffPercentage/100;
                                charac.IsBuffed = true;
                                charac.nbTurnsBuffed = 2;
                            }
                        }
                        else //Accuracy
                        {
                            foreach (Character charac in Dungeon.Team) //AOE
                            {
                                charac.buffedAccuracy +=charac.buffedAccuracy*charac.Skill1.buffPercentage/100;
                                charac.IsBuffed = true;
                                charac.nbTurnsBuffed = 2;
                            }
                        }
                        foreach (Enemies enemy in Enemies.towerEnemies) //AOE
                        {
                            enemy.HP -= character.buffedAtk*character.Skill1.AttackMultiplier/100;
                        }
                        Console.WriteLine("\u001b[33m"+character.Name+"\u001b[0m has buffed your team by \u001b[34m"+character.Skill1.buffPercentage+
                                          "\u001b[0m% of "+character.Skill1.BuffEffect+" and has dealt \u001b[31m"+character.buffedAtk * character.Skill1.AttackMultiplier / 100+
                                          "\u001b[0m damage to \u001b[35mall enemies\u001b[0m!");
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
                        enemies.HP -= character.buffedAtk * character.Skill1.AttackMultiplier / 100;
                        Console.WriteLine("\u001b[33m"+character.Name+"\u001b[0m has inflicted \u001b[32m"+character.Skill1.DebuffEffect+"\u001b[0m on "+
                                          enemies.Name+"\u001b[0m and has dealt \u001b[31m"+character.buffedAtk * character.Skill1.AttackMultiplier / 100+
                                          "\u001b[0m damage!");
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
                            enemy.HP -= character.buffedAtk*character.Skill1.AttackMultiplier/100;
                        }
                        Console.WriteLine("\u001b[33m"+character.Name+"\u001b[0m has inflicted \u001b[32m"+character.Skill1.DebuffEffect+"\u001b[0m on \u001b[35mall enemies\u001b[0m and has dealt \u001b[31m"
                                          +character.buffedAtk * character.Skill1.AttackMultiplier / 100+ "\u001b[0m damage!");
                    }
                }
            }
            else if (skill == 2) // Skill 2
            {
                if (character.Skill2.BuffEffect == "" && character.Skill2.DebuffEffect == "") //NORMAL ATK
                {
                    if (character.Skill2.isSingleTarget) //SGT
                    {
                        enemies.HP -= character.buffedAtk * character.Skill2.AttackMultiplier / 100;
                        Console.WriteLine("\u001b[33m"+character.Name+"\u001b[0m has dealt \u001b[31m"+character.buffedAtk * character.Skill2.AttackMultiplier / 100+
                                          "\u001b[0m damage to \u001b[35m"+enemies.Name+"\u001b[0m!");
                    }
                    else
                    {
                        foreach (Enemies enemy in Enemies.towerEnemies) //AOE
                        {
                            enemy.HP -= character.buffedAtk*character.Skill2.AttackMultiplier/100;
                        }
                        Console.WriteLine("\u001b[33m"+character.Name+"\u001b[0m has dealt \u001b[31m"+character.buffedAtk * character.Skill2.AttackMultiplier / 100+
                                          "\u001b[0m damage to \u001b[35mall enemies\u001b[0m!");
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
                        enemies.HP -= character.buffedAtk * character.Skill2.AttackMultiplier / 100;
                        Console.WriteLine("\u001b[33m"+character.Name+"\u001b[0m has buffed himself by \u001b[34m"+character.Skill2.buffPercentage+
                                          "\u001b[0m% of "+character.Skill2.BuffEffect+" and has dealt \u001b[31m"+character.buffedAtk * character.Skill2.AttackMultiplier / 100+
                                          "\u001b[0m damage to \u001b[35m" + enemies.Name+"\u001b[0m!");
                        character.nbTurnsBuffed = 2;
                    }
                    else
                    {
                        if (character.Skill2.BuffEffect == "dodge")
                        {
                            foreach (Character charac in Dungeon.Team) //AOE
                            {
                                charac.IsBuffed = true;
                                charac.buffedDodge +=charac.buffedDodge*charac.Skill2.buffPercentage/100;
                                charac.nbTurnsBuffed = 2;
                            }

                        }
                        else if (character.Skill2.BuffEffect == "speed")
                        {
                            foreach (Character charac in Dungeon.Team) //AOE
                            {
                                charac.IsBuffed = true;
                                charac.buffedSpeed +=charac.buffedSpeed*charac.Skill2.buffPercentage/100;
                                charac.nbTurnsBuffed = 2;
                            }
                        }
                        else if (character.Skill2.BuffEffect == "atk")
                        {
                            foreach (Character charac in Dungeon.Team) //AOE
                            {
                                charac.IsBuffed = true;
                                charac.buffedAtk +=charac.buffedAtk*charac.Skill2.buffPercentage/100;
                                charac.nbTurnsBuffed = 2;
                            }
                        }
                        else //Accuracy
                        {
                            foreach (Character charac in Dungeon.Team) //AOE
                            {
                                charac.IsBuffed = true;
                                charac.buffedAccuracy +=charac.buffedAccuracy*charac.Skill2.buffPercentage/100;
                                charac.nbTurnsBuffed = 2;
                            }
                        }
                        foreach (Enemies enemy in Enemies.towerEnemies) //AOE
                        {
                            enemy.HP -= character.buffedAtk*character.Skill2.AttackMultiplier/100;
                        }
                        Console.WriteLine("\u001b[33m"+character.Name+"\u001b[0m has buffed your team by \u001b[34m"+character.Skill2.buffPercentage+
                                          "\u001b[0m% of "+character.Skill2.BuffEffect+" and has dealt \u001b[31m"+character.buffedAtk * character.Skill2.AttackMultiplier / 100+
                                          "\u001b[0m damage to \u001b[35mall enemies\u001b[0m!");
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
                        enemies.HP -= character.buffedAtk * character.Skill2.AttackMultiplier / 100;
                        Console.WriteLine("\u001b[33m"+character.Name+"\u001b[0m has inflicted \u001b[32m"+character.Skill2.DebuffEffect+"\u001b[0m on \u001b[35m"+
                                          enemies.Name+"\u001b[0m and has dealt \u001b[31m"+character.buffedAtk * character.Skill2.AttackMultiplier / 100+
                                          "\u001b[0m damage!");
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
                            enemy.HP -= character.buffedAtk*character.Skill2.AttackMultiplier/100;
                        }
                        Console.WriteLine("\u001b[33m"+character.Name+"\u001b[0m has inflicted \u001b[32m"+character.Skill2.DebuffEffect+"\u001b[0m on \u001b[35mall enemies\u001b[0m and has dealt \u001b[31m"
                                          +character.buffedAtk * character.Skill2.AttackMultiplier / 100+ "\u001b[0m damage!");
                    }
                }
            }
            else // Ultimate
            {
                if (character.UltSkill.BuffEffect == "" && character.UltSkill.DebuffEffect == "")
                {
                    if (character.UltSkill.isSingleTarget) //SGT
                    {
                        enemies.HP -= character.buffedAtk * character.UltSkill.AttackMultiplier / 100;
                        Console.WriteLine("\u001b[33m"+character.Name+"\u001b[0m has dealt \u001b[31m"+character.buffedAtk * character.UltSkill.AttackMultiplier / 100+
                                          "\u001b[0m damage to \u001b[35m"+enemies.Name+"\u001b[0m!");
                    }
                    else
                    {
                        foreach (Enemies enemy in Enemies.towerEnemies)
                        {
                            enemy.HP -= character.buffedAtk*character.UltSkill.AttackMultiplier/100;
                        }
                        Console.WriteLine("\u001b[33m"+character.Name+"\u001b[0m has dealt \u001b[31m"+character.buffedAtk * character.UltSkill.AttackMultiplier / 100+
                                          "\u001b[0m damage to \u001b[35mall enemies\u001b[0m!");
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
                        enemies.HP -= character.buffedAtk * character.UltSkill.AttackMultiplier / 100;
                        Console.WriteLine("\u001b[33m"+character.Name+"\u001b[0m has buffed himself by \u001b[34m"+character.UltSkill.buffPercentage+
                                          "\u001b[0m% of "+character.UltSkill.BuffEffect+" and has dealt \u001b[31m"+character.buffedAtk * character.UltSkill.AttackMultiplier / 100+
                                          "\u001b[0m damage to \u001b[35m" + enemies.Name+"\u001b[0m!");
                        character.nbTurnsBuffed = 2;
                    }
                    else
                    {
                        if (character.UltSkill.BuffEffect == "dodge")
                        {
                            foreach (Character charac in Dungeon.Team) //AOE
                            {
                                charac.IsBuffed = true;
                                charac.buffedDodge +=charac.buffedDodge*charac.UltSkill.buffPercentage/100;
                                charac.nbTurnsBuffed = 2;
                            }

                        }
                        else if (character.UltSkill.BuffEffect == "speed")
                        {
                            foreach (Character charac in Dungeon.Team) //AOE
                            {
                                charac.IsBuffed = true;
                                charac.buffedSpeed +=charac.buffedSpeed*charac.UltSkill.buffPercentage/100;
                                charac.nbTurnsBuffed = 2;
                            }
                        }
                        else if (character.UltSkill.BuffEffect == "atk")
                        {
                            foreach (Character charac in Dungeon.Team) //AOE
                            {
                                charac.IsBuffed = true;
                                charac.buffedAtk +=charac.buffedAtk*charac.UltSkill.buffPercentage/100;
                                charac.nbTurnsBuffed = 2;
                            }
                            
                        }
                        else //Accuracy
                        {
                            foreach (Character charac in Dungeon.Team) //AOE
                            {
                                charac.IsBuffed = true;
                                charac.buffedAccuracy +=charac.buffedAccuracy*charac.UltSkill.buffPercentage/100;
                                charac.nbTurnsBuffed = 2;
                            }
                        }
                        foreach (Enemies enemy in Enemies.towerEnemies) //AOE
                        {
                            enemy.HP -= character.buffedAtk*character.UltSkill.AttackMultiplier/100;
                        }
                        Console.WriteLine("\u001b[33m"+character.Name+"\u001b[0m has buffed your team by \u001b[34m"+character.UltSkill.buffPercentage+
                                          "\u001b[0m% of "+character.UltSkill.BuffEffect+" and has dealt \u001b[31m"+character.buffedAtk * character.UltSkill.AttackMultiplier / 100+
                                          "\u001b[0m damage to all enemies!");
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
                        enemies.HP -= character.buffedAtk * character.UltSkill.AttackMultiplier / 100;
                        Console.WriteLine("\u001b[33m"+character.Name+"\u001b[0m has inflicted \u001b[32m"+character.UltSkill.DebuffEffect+"\u001b[0m on \u001b[35m"+
                                          enemies.Name+"\u001b[0m and has dealt \u001b[31m"+character.buffedAtk * character.UltSkill.AttackMultiplier / 100+
                                          "\u001b[0m damage!");
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
                            enemy.HP -= character.buffedAtk*character.UltSkill.AttackMultiplier/100;
                        }
                        Console.WriteLine("\u001b[33m"+character.Name+"\u001b[0m has inflicted \u001b[32m"+character.UltSkill.DebuffEffect+"\u001b[0m on \u001b[35mall enemies\u001b[0m and has dealt \u001b[31m"
                                          +character.buffedAtk * character.UltSkill.AttackMultiplier / 100+ "\u001b[0m damage!");
                    }
                }
            }
            Console.ReadKey();
        }
    }

    public static void DisplayEnemyInfo(Enemies enemies, int skillToUse)
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

            string temp = res + "|";
            if (skillToUse!=0)
                temp+="\u001b[31m←\u001b[0m";
            Console.WriteLine(temp);
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

            string temp = res + "|";
            if (skillToUse==0)
                temp+="\u001b[31m←\u001b[0m";
            Console.WriteLine(temp);
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
                
                else
                {
                    res += " " + character.Skill1.CooldownLeft + "T";
                    
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
                else
                {
                    res += " " + character.Skill2.CooldownLeft + "T";
                    
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
                else
                {
                    res += " " + character.UltSkill.CooldownLeft + "T";
                    
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
            if (character.TurnPriority > tempFastest.TurnPriority && character.IsAlive)
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
            if (enemy.TurnPriority > tempFastest.TurnPriority && enemy.IsAlive)
                tempFastest = enemy;
        }

        return tempFastest;
    }
    
}
