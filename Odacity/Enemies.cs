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
    
    public Skill Skill1 { get; set; }
    public Skill UltSkill { get; set; }

    public static void InitializeTowerEnemies()
    {
        Enemies enemy1 = new Enemies();
        Enemies enemy2 = new Enemies();
        Enemies enemy3 = new Enemies();
        Enemies enemy4 = new Enemies();

        //Needs random name generation
        enemy1.Name = "ASSASSIN";
        enemy2.Name = "WARRIOR";
        enemy3.Name = "TANK";
        enemy4.Name = "ARCHER";
        
        enemy1.Nickname = "Silent Fart";
        enemy2.Nickname = "Bloody Berserker";
        enemy3.Nickname = "Top Master";
        enemy4.Nickname = "Elf";

        enemy1.Level = Dungeon.towerStageClear; //ASSASSIN
        enemy2.Level = Dungeon.towerStageClear; //WARRIOR
        enemy3.Level = Dungeon.towerStageClear; //TANK
        enemy4.Level = Dungeon.towerStageClear; //DPS

        enemy1.Attack = 10 + Dungeon.towerStageClear;
        enemy2.Attack = 8 + Dungeon.towerStageClear;
        enemy3.Attack = 5 + Dungeon.towerStageClear;
        enemy4.Attack = 12 + Dungeon.towerStageClear;

        enemy1.Accuracy = Dungeon.towerStageClear / 11 + 1;
        enemy2.Accuracy = Dungeon.towerStageClear / 14 + 1;
        enemy3.Accuracy = Dungeon.towerStageClear / 20 + 1;
        enemy4.Accuracy = Dungeon.towerStageClear / 13 + 1;

        enemy1.Dodge = Dungeon.towerStageClear/ 14 + 1;
        enemy2.Dodge = Dungeon.towerStageClear/ 11 + 1;
        enemy3.Dodge = Dungeon.towerStageClear/ 20 + 1;
        enemy4.Dodge = Dungeon.towerStageClear/ 13 + 1;
        
        enemy1.Speed = Dungeon.towerStageClear/ 11 + 1;
        enemy2.Speed = Dungeon.towerStageClear/ 16 + 1;
        enemy3.Speed = Dungeon.towerStageClear/ 22 + 1;
        enemy4.Speed = Dungeon.towerStageClear/ 22 + 1;
        
        enemy1.MaxHP = 30+Dungeon.towerStageClear/ 2;
        enemy2.MaxHP = 30+Dungeon.towerStageClear*2;
        enemy3.MaxHP = 30+Dungeon.towerStageClear*3;
        enemy4.MaxHP = 30+Dungeon.towerStageClear;

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