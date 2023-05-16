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
    
    public static List<Enemies> towerEnemies = new List<Enemies>();
    
    public bool IsStuned { get; set; }
    
    public bool IsPoisonned { get; set; }
    
    public bool IsBuffed { get; set; }
    public int nbTurnsBuffed { get; set; }
    
    public Skill Skill1 { get; set; }
    public Skill UltSkill { get; set; }

    public static void InitializeTowerEnemies()
    {
        Enemies enemy1 = new Enemies();
        Enemies enemy2 = new Enemies();
        Enemies enemy3 = new Enemies();
        Enemies enemy4 = new Enemies();

        //Needs random name generation
        enemy1.Name = "Cringe"; //ASSASSIN
        enemy2.Name = "Gutz"; //WARRIOR
        enemy3.Name = "Hodor"; //TANK
        enemy4.Name = "Lyndis"; //DPS
        
        enemy1.Nickname = "Silent Fart";
        enemy2.Nickname = "Bloody Berserker";
        enemy3.Nickname = "Top Master";
        enemy4.Nickname = "Elf";

        enemy1.Level = Dungeon.towerStageClear; //ASSASSIN
        enemy2.Level = Dungeon.towerStageClear; //WARRIOR
        enemy3.Level = Dungeon.towerStageClear; //TANK
        enemy4.Level = Dungeon.towerStageClear; //DPS

        enemy1.Attack = 100 + Dungeon.towerStageClear;
        enemy2.Attack = 80 + Dungeon.towerStageClear;
        enemy3.Attack = 50 + Dungeon.towerStageClear;
        enemy4.Attack = 120 + Dungeon.towerStageClear;

        enemy1.Accuracy = 10*Dungeon.towerStageClear / 11 + 15;
        enemy2.Accuracy = 10*Dungeon.towerStageClear / 14 + 15;
        enemy3.Accuracy = 10*Dungeon.towerStageClear / 20 + 5;
        enemy4.Accuracy = 10*Dungeon.towerStageClear / 13 + 20;

        enemy1.Dodge = 10*Dungeon.towerStageClear/ 14 + 15;
        enemy2.Dodge = 10*Dungeon.towerStageClear/ 11 + 12;
        enemy3.Dodge = 10*Dungeon.towerStageClear/ 20 + 10;
        enemy4.Dodge = 10*Dungeon.towerStageClear/ 13 + 5;
        
        enemy1.Speed = 10*Dungeon.towerStageClear/ 11 + 130;
        enemy2.Speed = 10*Dungeon.towerStageClear/ 16 + 90;
        enemy3.Speed = 10*Dungeon.towerStageClear/ 22 + 50;
        enemy4.Speed = 10*Dungeon.towerStageClear/ 22 + 60;
        
        enemy1.MaxHP = 150+Dungeon.towerStageClear*7;
        enemy2.MaxHP = 300+Dungeon.towerStageClear*20;
        enemy3.MaxHP = 400+Dungeon.towerStageClear*30;
        enemy4.MaxHP = 120+Dungeon.towerStageClear*5;

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
}