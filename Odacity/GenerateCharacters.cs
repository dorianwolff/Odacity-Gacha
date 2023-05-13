namespace Odacity;

[Serializable]
public class GenerateCharacters
{
    public static string GenerateCharacterName()
    {
        string[] prefixes =
        {
            "Ald", "Bryn", "Cael", "Dyr", "Eir", "Fael", "Gwyn", "Hael", "Ith", "Jor", "Kel", "Nyx",
            "Ord", "Cal", "Krüg", "Ralph", "Mord", "Lyl", "O", "Ker"
            
        };
        string[] suffixes =
        {
            "gar", "wyn", "thor", "dor", "wynn", "nor", "dil", "fyr", "mir", "vyn",
            "dian", "für", "rumm"
        };

        string prefix = prefixes[new Random().Next(prefixes.Length)];
        string suffix = suffixes[new Random().Next(suffixes.Length)];

        return prefix + suffix;
    }
    
    public static string GenerateCharacterNickname()
    {
        string[] prefixes = { "Brave", "Swift", "Fierce", "Wise", "Noble", "Sly", "Mighty", "Shadow", "Silver", "Cunning",
            "Serious", "Average", "Heavenly", "Great", "Angelic", "Deceiving", "Ending", "Slow", "Treacherous", "Demonic",
            "Righteous", "Experienced", "Balanced", "Okay", "Undead", "Celestial", "Monstrous", "Mystical"
        };
        string[] suffixes =
        {
            "blade", "strike", "heart", "whisper", "arrow", "fang", "song", "wind", "thorn", "spear",
            "legend", "rider", "soldier", "tamer", "devourer", "sin", "sword", "rat", "coward", "fear bringer",
            "murderer", "protector", "ax", "champion", "gale", "shadow", "nova", "dusk", "dawn", "thunder","phoenix", "arcane"
        };

        string prefix = prefixes[new Random().Next(prefixes.Length)];
        string suffix = suffixes[new Random().Next(suffixes.Length)];

        return prefix + " " + suffix;
    }

    public static Skill GenerateCharacterUltSkill(string grade) //needs color
    {
        Skill ult = new Skill();
        ult.isAoeBuff = false;
        ult.BuffEffect = "";
        ult.DebuffEffect = "";
        ult.buffPercentage = 0;
        string[] Ultname =
        {
            "Oblivion", "Chaos", "Beginning of the end", "BONK!", "Begone", "Absolute order", "Get REKT",
            "Me>>You", "Shrekt", "Get dunked", "Purgatory", "Divinity's embrace", "Mortal slumber",
            "Thanks and bye", "Skill issue", "Muscle power"
            
        };
        string ultname = Ultname[new Random().Next(Ultname.Length)];

        //Determines if its a Buff, Debuff or Raw attack
        Random rd = new Random();
        int skillType = rd.Next(1, 4);
        if (skillType==1) //Raw Attack
        {
            string single = "aoe";
            int dmg = rd.Next(200, 500);
            if (dmg > 350)
            {
                ult.isSingleTarget = true;
                single = "single target";
            }
            if (grade == "R")
                dmg += 100;
            else if (grade == "E")
                dmg += 150;
            else if (grade == "L")
                dmg += 200;
            ult.AttackMultiplier = dmg;
            ult.Description = ultname + " : A very powerful \u001b[35m"+single+"\u001b[0m skill" +
                              "-with no special effect. It does \u001b[31m" + dmg+"%\u001b[0m of atk as damage.";
            ult.BuffEffect = "";
            ult.DebuffEffect = "";
        }
        
        else if (skillType==2) //Buff effect
        {
            string single = "aoe";
            string allAllies = "all allies";
            int buffPercentage = rd.Next(5, 30);
            int buffType = rd.Next(1, 4);
            string buffT = "atk";
            int dmg = rd.Next(100, 300);
            if (dmg > 200)
            {
                ult.isSingleTarget = true;
                single = "single target";
                allAllies = "this character";
            }
            else
            {
                buffPercentage /= 2;
                ult.isAoeBuff = true;
            }
            if (grade == "R")
            {
                dmg += 20;
                buffPercentage += 2;
            }
            else if (grade == "E")
            {
                dmg += 40;
                buffPercentage += 4;
            }
            else if (grade == "L")
            {
                dmg += 60;
                buffPercentage += 6;
            }

            if (buffType == 1)
            {
                buffT = "speed";
            }
            else if (buffType == 2)
            {
                buffT = "dodge";
            }
            
            ult.Description = ultname + " : A very powerful \u001b[35m"+single+"\u001b[0m skill" +
                              "-which buffs \u001b[34m"+allAllies+"\u001b[0m's \u001b[33m"+buffT+ "\u001b[0m by \u001b[31m"+buffPercentage+"%\u001b[0m. " + 
                              "-It does \u001b[31m" + dmg+"%\u001b[0m of atk as damage.";
            ult.BuffEffect = buffT;
            ult.AttackMultiplier = dmg;
            ult.DebuffEffect = "";
            ult.buffPercentage = buffPercentage;
        }
        
        else //Debuff effect
        {
            string single = "aoe";
            string allEnemies = "all enemies";
            int debuffType = rd.Next(1, 3);
            string debuffT = "poison";
            int dmg = rd.Next(150, 350);
            if (dmg > 250)
            {
                ult.isSingleTarget = true;
                single = "single target";
                allEnemies = "target enemy";
            }
            if (grade == "R")
            {
                dmg += 30;
            }
            else if (grade == "E")
            {
                dmg += 60;
            }
            else if (grade == "L")
            {
                dmg += 100;
            }

            if (debuffType == 1)
            {
                debuffT = "stun";
                ult.isSingleTarget = false;
                dmg /= 2;
            }
            ult.AttackMultiplier = dmg;
            ult.Description = ultname + " : A very powerful \u001b[35m"+single+"\u001b[0m skill " +
                              "-which does \u001b[31m" + dmg+"%\u001b[0m of atk as damage. " +
                              "It inflicts \u001b[32m"+debuffT+"\u001b[0m on " + "\u001b[34m"+ allEnemies+"\u001b[0m.";
            ult.BuffEffect = "";
            ult.DebuffEffect = debuffT;
        }
        
        return ult;
    }
    
    public static Skill GenerateCharacterNormalSkill(string grade) //needs color
    {
        Skill normal = new Skill();
        normal.isAoeBuff = false;
        normal.BuffEffect = "";
        normal.DebuffEffect = "";
        normal.buffPercentage = 0;
        string[] NormalNames =
        {
            "Normal punch", "Pistol shot", "Martial punch", "Average kick", "Magic mushrooms", "Idiotic acrobatics",
            "Confusing dance", "Gamer move", "Geek headbutt", "Mystic powers", "Decent swing", "Great kick series",
            "Tough attack", "Memorable moment"
            
        };
        string normalnames = NormalNames[new Random().Next(NormalNames.Length)];

        //Determines if its a Buff, Debuff or Raw attack
        Random rd = new Random();
        int skillType = rd.Next(1, 4);
        if (skillType==1) //Raw Attack
        {
            string single = "aoe";
            int dmg = rd.Next(90, 120);
            if (dmg > 100)
            {
                normal.isSingleTarget = true;
                single = "single target";
            }
            if (grade == "R")
                dmg += 10;
            else if (grade == "E")
                dmg += 15;
            else if (grade == "L")
                dmg += 20;
            normal.AttackMultiplier = dmg;
            normal.Description = normalnames + " : A normal \u001b[35m"+single+"\u001b[0m skill" +
                                 "-with no special effect. " + "It does \u001b[31m" + dmg+"%\u001b[0m of atk as damage.";
            normal.BuffEffect = "";
            normal.DebuffEffect = "";
        }
        
        else if (skillType==2) //Buff effect
        {
            string single = "aoe";
            string allAllies = "all allies";
            int buffPercentage = rd.Next(5, 10);
            int buffType = rd.Next(1, 5);
            string buffT = "atk";
            if (buffPercentage > 6)
            {
                normal.isSingleTarget = true;
                single = "single target";
                allAllies = "this character";
            }
            else
            {
                normal.isAoeBuff = true;
                normal.isAoeBuff = true;
            }
            if (grade == "R")
            {
                buffPercentage += 1;
            }
            else if (grade == "E")
            {
                buffPercentage += 2;
            }
            else if (grade == "L")
            {
                buffPercentage += 3;
            }

            if (buffType == 1)
            {
                buffT = "speed";
            }
            else if (buffType == 2)
            {
                buffT = "dodge";
            }
            else if (buffType == 3)
            {
                buffT = "accuracy";
            }
            
            normal.Description = normalnames + " : A useful \u001b[35m"+single+"\u001b[0m skill" +
                                 "-which buffs \u001b[34m"+ allAllies+"\u001b[0m's \u001b[33m"+buffT+"\u001b[0m by \u001b[31m"+
                                 buffPercentage+"%\u001b[0m";
            normal.BuffEffect = buffT;
            normal.AttackMultiplier = 0;
            normal.DebuffEffect = "";
            normal.buffPercentage = buffPercentage;
        }
        
        else //Debuff effect
        {
            string single = "aoe";
            string allEnemies = "all enemies";
            int debuffType = rd.Next(1, 3);
            string debuffT = "poison";
            int dmg = rd.Next(60, 80);
            if (dmg > 70)
            {
                normal.isSingleTarget = true;
                single = "single target";
                allEnemies = "target enemy";
            }
            if (grade == "R")
            {
                dmg += 5;
            }
            else if (grade == "E")
            {
                dmg += 10;
            }
            else if (grade == "L")
            {
                dmg += 15;
            }

            if (debuffType == 1)
            {
                debuffT = "stun";
                normal.isSingleTarget = false;
                dmg /= 2;
            }
            normal.AttackMultiplier = dmg;
            normal.Description = normalnames + " : An annoying \u001b[35m"+single+"\u001b[0m skill " +
                                 "-which does \u001b[31m" + dmg+"%\u001b[0m of atk as damage. " +
                                 "It inflicts \u001b[32m"+debuffT+ "\u001b[0m on \u001b[34m"+allEnemies+"\u001b[0m.";
            normal.BuffEffect = "";
            normal.DebuffEffect = debuffT;
        }
        
        return normal;
    }
    
    //Enemies
    
    public static Skill GenerateEnemyUltSkill(string grade) //needs color
    {
        Skill ult = new Skill();
        ult.isAoeBuff = false;
        ult.BuffEffect = "";
        ult.DebuffEffect = "";
        ult.buffPercentage = 0;
        string[] Ultname =
        {
            "Oblivion", "Chaos", "The beginning of the end", "BONK!", "Begone", "Absolute order", "Get REKT",
            "Me>>You", "Shrekt", "Get dunked", "Purgatory", "Divinity's embrace", "Mortal slumber",
            "Thanks and bye", "Skill issue", "Muscle power"
            
        };
        string ultname = Ultname[new Random().Next(Ultname.Length)];

        //Determines if its a Buff, Debuff or Raw attack
        Random rd = new Random();
        int skillType = rd.Next(1, 4);
        if (skillType==1) //Raw Attack
        {
            string single = "aoe";
            int dmg = rd.Next(200, 500);
            if (dmg > 350)
            {
                ult.isSingleTarget = true;
                single = "single target";
            }
            if (grade == "R")
                dmg += 100;
            else if (grade == "E")
                dmg += 150;
            else if (grade == "L")
                dmg += 200;
            ult.AttackMultiplier = dmg;
            ult.Description = ultname + " : A very powerful \u001b[35m"+single+"\u001b[0m skill" +
                              "-with no special effect. It does \u001b[31m" + dmg+"%\u001b[0m of atk as damage.";
            ult.BuffEffect = "";
            ult.DebuffEffect = "";
        }
        
        else if (skillType==2) //Buff effect
        {
            string single = "aoe";
            string allAllies = "all allies";
            int buffPercentage = rd.Next(5, 30);
            int buffType = rd.Next(1, 4);
            string buffT = "atk";
            int dmg = rd.Next(100, 300);
            if (dmg > 200)
            {
                ult.isSingleTarget = true;
                single = "single target";
                allAllies = "this character";
            }
            else
            {
                buffPercentage /= 2;
                ult.isAoeBuff = true;
            }
            if (grade == "R")
            {
                dmg += 20;
                buffPercentage += 2;
            }
            else if (grade == "E")
            {
                dmg += 40;
                buffPercentage += 4;
            }
            else if (grade == "L")
            {
                dmg += 60;
                buffPercentage += 6;
            }

            if (buffType == 1)
            {
                buffT = "speed";
            }
            else if (buffType == 2)
            {
                buffT = "dodge";
            }
            
            ult.Description = ultname + " : A very powerful \u001b[35m"+single+"\u001b[0m skill" +
                              "-which buffs \u001b[34m"+allAllies+"\u001b[0m's \u001b[33m"+buffT+ "\u001b[0m by \u001b[31m"+buffPercentage+"%\u001b[0m. " + 
                              "-It does \u001b[31m" + dmg+"%\u001b[0m of atk as damage.";
            ult.BuffEffect = buffT;
            ult.AttackMultiplier = dmg;
            ult.DebuffEffect = "";
            ult.buffPercentage = buffPercentage;
        }
        
        else //Debuff effect
        {
            string single = "aoe";
            string allEnemies = "all enemies";
            int debuffType = rd.Next(1, 3);
            string debuffT = "poison";
            int dmg = rd.Next(150, 350);
            if (dmg > 250)
            {
                ult.isSingleTarget = true;
                single = "single target";
                allEnemies = "target enemy";
            }
            if (grade == "R")
            {
                dmg += 30;
            }
            else if (grade == "E")
            {
                dmg += 60;
            }
            else if (grade == "L")
            {
                dmg += 100;
            }

            if (debuffType == 1)
            {
                debuffT = "stun";
                ult.isSingleTarget = false;
                dmg /= 2;
            }
            ult.AttackMultiplier = dmg;
            ult.Description = ultname + " : A very powerful \u001b[35m"+single+"\u001b[0m skill " +
                              "-which does \u001b[31m" + dmg+"%\u001b[0m of atk as damage. " +
                              "It inflicts \u001b[32m"+debuffT+"\u001b[0m on " + "\u001b[34m"+ allEnemies+"\u001b[0m.";
            ult.BuffEffect = "";
            ult.DebuffEffect = debuffT;
        }
        
        return ult;
    }
    
    public static Skill GenerateEnemyNormalSkill(string grade) //needs color
    {
        Skill normal = new Skill();
        normal.isAoeBuff = false;
        normal.BuffEffect = "";
        normal.DebuffEffect = "";
        normal.buffPercentage = 0;
        string[] NormalNames =
        {
            "Normal punch", "Pistol shot", "Martial punch", "Average kick", "Magic mushrooms", "Idiotic acrobatics",
            "Confusing dance", "Gamer move", "Geek headbutt", "Mystic powers", "Decent swing", "Great kick series",
            "Tough attack", "Memorable moment"
            
        };
        string normalnames = NormalNames[new Random().Next(NormalNames.Length)];

        //Determines if its a Buff, Debuff or Raw attack
        Random rd = new Random();
        int skillType = rd.Next(1, 4);
        if (skillType==1) //Raw Attack
        {
            string single = "aoe";
            int dmg = rd.Next(90, 120);
            if (dmg > 100)
            {
                normal.isSingleTarget = true;
                single = "single target";
            }
            if (grade == "R")
                dmg += 10;
            else if (grade == "E")
                dmg += 15;
            else if (grade == "L")
                dmg += 20;
            normal.AttackMultiplier = dmg;
            normal.Description = normalnames + " : A normal \u001b[35m"+single+"\u001b[0m skill" +
                                 "-with no special effect. " + "It does \u001b[31m" + dmg+"%\u001b[0m of atk as damage.";
            normal.BuffEffect = "";
            normal.DebuffEffect = "";
        }
        
        else if (skillType==2) //Buff effect
        {
            string single = "aoe";
            string allAllies = "all allies";
            int buffPercentage = rd.Next(5, 10);
            int buffType = rd.Next(1, 5);
            string buffT = "atk";
            if (buffPercentage > 6)
            {
                normal.isSingleTarget = true;
                single = "single target";
                allAllies = "this character";
            }
            else
            {
                normal.isAoeBuff = true;
                normal.isAoeBuff = true;
            }
            if (grade == "R")
            {
                buffPercentage += 1;
            }
            else if (grade == "E")
            {
                buffPercentage += 2;
            }
            else if (grade == "L")
            {
                buffPercentage += 3;
            }

            if (buffType == 1)
            {
                buffT = "speed";
            }
            else if (buffType == 2)
            {
                buffT = "dodge";
            }
            else if (buffType == 3)
            {
                buffT = "accuracy";
            }
            
            normal.Description = normalnames + " : A useful \u001b[35m"+single+"\u001b[0m skill" +
                                 "-which buffs \u001b[34m"+ allAllies+"\u001b[0m's \u001b[33m"+buffT+"\u001b[0m by \u001b[31m"+
                                 buffPercentage+"%\u001b[0m";
            normal.BuffEffect = buffT;
            normal.AttackMultiplier = 0;
            normal.DebuffEffect = "";
            normal.buffPercentage = buffPercentage;
        }
        
        else //Debuff effect
        {
            string single = "aoe";
            string allEnemies = "all enemies";
            int debuffType = rd.Next(1, 3);
            string debuffT = "poison";
            int dmg = rd.Next(60, 80);
            if (dmg > 70)
            {
                normal.isSingleTarget = true;
                single = "single target";
                allEnemies = "target enemy";
            }
            if (grade == "R")
            {
                dmg += 5;
            }
            else if (grade == "E")
            {
                dmg += 10;
            }
            else if (grade == "L")
            {
                dmg += 15;
            }

            if (debuffType == 1)
            {
                debuffT = "stun";
                normal.isSingleTarget = false;
                dmg /= 2;
            }
            normal.AttackMultiplier = dmg;
            normal.Description = normalnames + " : An annoying \u001b[35m"+single+"\u001b[0m skill " +
                                 "-which does \u001b[31m" + dmg+"%\u001b[0m of atk as damage. " +
                                 "It inflicts \u001b[32m"+debuffT+ "\u001b[0m on \u001b[34m"+allEnemies+"\u001b[0m.";
            normal.BuffEffect = "";
            normal.DebuffEffect = debuffT;
        }
        
        return normal;
    }

}