using System.Collections.Generic;

namespace Odacity;

public class Enemies
{
    public string Name { get; set; }
    public string Nickname { get; set; }
    //public int awakening { get; set; }
    
    
    // Stats
    public int Level { get; set; }
    public int Accuracy { get; set; }
    public int Dodge { get; set; }
    public int Attack { get; set; }
    public int HP { get; set; }
    public int MaxHP { get; set; }
    
    public int buffedSpeed { get; set; }
    
    public int buffedAtk { get; set; }
    
    public int buffedDodge { get; set; }
    
    public int buffedAccuracy { get; set; }
    
    public bool IsAlive { get; set; }
    public int TurnPriority { get; set; }
    public int Speed { get; set; }
    public string Tag { get; set; }
    
    public static List<Enemies> towerEnemies = new List<Enemies>();
    public static List<Enemies> questEnemies = new List<Enemies>();
    
    public bool IsStuned { get; set; }
    
    public bool IsPoisonned { get; set; }
    
    public bool IsBuffed { get; set; }
    public int nbTurnsBuffed { get; set; }
    
    public Skill Skill1 { get; set; }
    public Skill UltSkill { get; set; }

    public static void InitializeTowerEnemies()
    {
        towerEnemies = new List<Enemies>();
        if (Dungeon.towerStageClear % 5 != 0)
        { 
            Enemies enemy1 = new Enemies();
            Enemies enemy2 = new Enemies();
            Enemies enemy3 = new Enemies();
            Enemies enemy4 = new Enemies();

            enemy1.Name = GenerateCharacters.GenerateEnemyName(); //ASSASSIN
            enemy2.Name = GenerateCharacters.GenerateEnemyName(); //WARRIOR
            enemy3.Name = GenerateCharacters.GenerateEnemyName(); //TANK
            enemy4.Name = GenerateCharacters.GenerateEnemyName(); //DPS

            enemy1.Nickname = GenerateCharacters.GenerateEnemyNickname();
            enemy2.Nickname = GenerateCharacters.GenerateEnemyNickname();
            enemy3.Nickname = GenerateCharacters.GenerateEnemyNickname();
            enemy4.Nickname = GenerateCharacters.GenerateEnemyNickname();

            enemy1.Tag = "ASSASSIN";
            enemy2.Tag = "WARIIOR";
            enemy3.Tag = "TANK";
            enemy4.Tag = "DPS";

            enemy1.Level = Dungeon.towerStageClear;
            enemy2.Level = Dungeon.towerStageClear;
            enemy3.Level = Dungeon.towerStageClear;
            enemy4.Level = Dungeon.towerStageClear;

            enemy1.Attack = 100 + 4*Dungeon.towerStageClear;
            enemy2.Attack = 80 + 3*Dungeon.towerStageClear; 
            enemy3.Attack = 50 + 2*Dungeon.towerStageClear;
            enemy4.Attack = 120 + 6*Dungeon.towerStageClear;
            
            enemy1.Accuracy = 11*Dungeon.towerStageClear + 15; 
            enemy2.Accuracy = 14*Dungeon.towerStageClear + 15; 
            enemy3.Accuracy = 20*Dungeon.towerStageClear + 5;
            enemy4.Accuracy = 13*Dungeon.towerStageClear + 20;
            
            enemy1.Dodge = 10*Dungeon.towerStageClear/ 14 + 15; 
            enemy2.Dodge = 10*Dungeon.towerStageClear/ 11 + 12; 
            enemy3.Dodge = 10*Dungeon.towerStageClear/ 20 + 10; 
            enemy4.Dodge = 10*Dungeon.towerStageClear/ 13 + 5;
            
            enemy1.Speed = 11*Dungeon.towerStageClear + 130; 
            enemy2.Speed = 16*Dungeon.towerStageClear + 90; 
            enemy3.Speed = 22*Dungeon.towerStageClear + 50; 
            enemy4.Speed = 22*Dungeon.towerStageClear + 60;
            
            enemy1.MaxHP = 150+Dungeon.towerStageClear*10; 
            enemy2.MaxHP = 300+Dungeon.towerStageClear*20; 
            enemy3.MaxHP = 400+Dungeon.towerStageClear*30; 
            enemy4.MaxHP = 120+Dungeon.towerStageClear*6;
            
            enemy1.Skill1 = GenerateCharacters.GenerateEnemyNormalSkill("C"); 
            enemy2.Skill1 = GenerateCharacters.GenerateEnemyNormalSkill("R"); 
            enemy3.Skill1 = GenerateCharacters.GenerateEnemyNormalSkill("C"); 
            enemy4.Skill1 = GenerateCharacters.GenerateEnemyNormalSkill("C");
            
            enemy1.UltSkill = GenerateCharacters.GenerateEnemyUltSkill("C"); 
            enemy2.UltSkill = GenerateCharacters.GenerateEnemyUltSkill("C"); 
            enemy3.UltSkill = GenerateCharacters.GenerateEnemyUltSkill("R"); 
            enemy4.UltSkill = GenerateCharacters.GenerateEnemyUltSkill("C");
            
            towerEnemies.Add(enemy1); 
            towerEnemies.Add(enemy2); 
            towerEnemies.Add(enemy3); 
            towerEnemies.Add(enemy4);
        }
        else // BOSS x4 exp drop
        {
            Enemies boss = new Enemies();
            boss.Name = "Dreadnought";
            boss.Nickname = "Ultimate Slayer";
            boss.Tag = "BOSS";
            
            boss.Level = Dungeon.towerStageClear;
            boss.Attack = 250 + 10*Dungeon.towerStageClear;
            boss.Accuracy = Dungeon.towerStageClear + 20;
            if (Dungeon.towerStageClear%10!=0)
            {
                boss.Dodge = Dungeon.towerStageClear + 20;
                boss.Skill1 = GenerateCharacters.GenerateEnemyNormalSkill("R");
                boss.UltSkill = GenerateCharacters.GenerateEnemyUltSkill("E");
            }
            else
            {
                boss.Dodge = 0;
                boss.Skill1 = GenerateCharacters.GenerateEnemyNormalSkill("E");
                boss.UltSkill = GenerateCharacters.GenerateEnemyUltSkill("L");
            }

            boss.Speed = Dungeon.towerStageClear*25 + 200;
            boss.MaxHP = 800+Dungeon.towerStageClear*35;
            towerEnemies.Add(boss);
        }
    }

    public static void InitializeQuestEnemies()
    {
        questEnemies = new List<Enemies>();
        
        if (Dungeon.towerStageClear % 5 != 0) //Change for quest
        { 
            Enemies enemy1 = new Enemies();
            Enemies enemy2 = new Enemies();
            Enemies enemy3 = new Enemies();
            Enemies enemy4 = new Enemies();
            Enemies enemy5 = new Enemies();

            enemy1.Name = GenerateCharacters.GenerateEnemyName();
            enemy2.Name = GenerateCharacters.GenerateEnemyName();
            enemy3.Name = GenerateCharacters.GenerateEnemyName();
            enemy4.Name = GenerateCharacters.GenerateEnemyName();
            enemy5.Name = GenerateCharacters.GenerateEnemyName();

            enemy1.Nickname = GenerateCharacters.GenerateEnemyNickname();
            enemy2.Nickname = GenerateCharacters.GenerateEnemyNickname();
            enemy3.Nickname = GenerateCharacters.GenerateEnemyNickname();
            enemy4.Nickname = GenerateCharacters.GenerateEnemyNickname();
            enemy5.Nickname = GenerateCharacters.GenerateEnemyNickname();

            enemy1.Tag = "ASSASSIN";
            enemy2.Tag = "WARIIOR";
            enemy3.Tag = "TANK";
            enemy4.Tag = "DPS";
            enemy5.Tag = "MINER";

            enemy1.Level = Dungeon.questStageClear;
            enemy2.Level = Dungeon.questStageClear;
            enemy3.Level = Dungeon.questStageClear;
            enemy4.Level = Dungeon.questStageClear;
            enemy5.Level = Dungeon.questStageClear;

            enemy1.Attack = 100 + 5*Dungeon.questStageClear;
            enemy2.Attack = 80 + 4*Dungeon.questStageClear; 
            enemy3.Attack = 50 + 3*Dungeon.questStageClear;
            enemy4.Attack = 120 + 7*Dungeon.questStageClear;
            enemy5.Attack = 120 + 6*Dungeon.questStageClear;
            
            enemy1.Accuracy = 13*Dungeon.questStageClear + 15; 
            enemy2.Accuracy = 16*Dungeon.questStageClear + 15; 
            enemy3.Accuracy = 22*Dungeon.questStageClear + 5;
            enemy4.Accuracy = 15*Dungeon.questStageClear + 20;
            enemy5.Accuracy = 13*Dungeon.questStageClear + 20;
            
            enemy1.Dodge = 10*Dungeon.questStageClear/ 14 + 15; 
            enemy2.Dodge = 10*Dungeon.questStageClear/ 11 + 12; 
            enemy3.Dodge = 10*Dungeon.questStageClear/ 20 + 10; 
            enemy4.Dodge = 10*Dungeon.questStageClear/ 13 + 5;
            enemy5.Dodge = 10*Dungeon.questStageClear/ 13 + 5;
            
            enemy1.Speed = 13*Dungeon.questStageClear + 130; 
            enemy2.Speed = 18*Dungeon.questStageClear + 90; 
            enemy3.Speed = 24*Dungeon.questStageClear + 50; 
            enemy4.Speed = 24*Dungeon.questStageClear + 60;
            enemy5.Speed = 24*Dungeon.questStageClear + 60;
            
            enemy1.MaxHP = 150+Dungeon.questStageClear*12; 
            enemy2.MaxHP = 300+Dungeon.questStageClear*22; 
            enemy3.MaxHP = 400+Dungeon.questStageClear*32; 
            enemy4.MaxHP = 120+Dungeon.questStageClear*7;
            enemy5.MaxHP = 120+Dungeon.questStageClear*7;
            
            enemy1.Skill1 = GenerateCharacters.GenerateEnemyNormalSkill("C"); 
            enemy2.Skill1 = GenerateCharacters.GenerateEnemyNormalSkill("R"); 
            enemy3.Skill1 = GenerateCharacters.GenerateEnemyNormalSkill("C"); 
            enemy4.Skill1 = GenerateCharacters.GenerateEnemyNormalSkill("C");
            enemy5.Skill1 = GenerateCharacters.GenerateEnemyNormalSkill("C");
            
            enemy1.UltSkill = GenerateCharacters.GenerateEnemyUltSkill("C"); 
            enemy2.UltSkill = GenerateCharacters.GenerateEnemyUltSkill("C"); 
            enemy3.UltSkill = GenerateCharacters.GenerateEnemyUltSkill("R"); 
            enemy4.UltSkill = GenerateCharacters.GenerateEnemyUltSkill("C");
            enemy5.UltSkill = GenerateCharacters.GenerateEnemyUltSkill("C");
            
            questEnemies.Add(enemy1); 
            questEnemies.Add(enemy2); 
            questEnemies.Add(enemy3); 
            questEnemies.Add(enemy4);
            //questEnemies.Add(enemy5);
        }
        else // BOSS x4 exp drop
        {
            Enemies boss = new Enemies();
            boss.Name = "Anubis";
            boss.Nickname = "Corrupted God";
            boss.Tag = "BOSS";
            
            boss.Level = Dungeon.questStageClear;
            boss.Attack = 250 + 12*Dungeon.questStageClear;
            boss.Accuracy = Dungeon.questStageClear + 21;
            if (Dungeon.questStageClear%10!=0)
            {
                boss.Dodge = Dungeon.questStageClear + 21;
                boss.Skill1 = GenerateCharacters.GenerateEnemyNormalSkill("R");
                boss.UltSkill = GenerateCharacters.GenerateEnemyUltSkill("E");
            }
            else
            {
                boss.Dodge = 0;
                boss.Skill1 = GenerateCharacters.GenerateEnemyNormalSkill("E");
                boss.UltSkill = GenerateCharacters.GenerateEnemyUltSkill("L");
            }

            boss.Speed = Dungeon.questStageClear*27 + 200;
            boss.MaxHP = 800+Dungeon.questStageClear*37;
            questEnemies.Add(boss);
        }
    }
}