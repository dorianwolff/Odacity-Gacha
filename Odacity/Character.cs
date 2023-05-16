namespace Odacity;

// Character data class - It is kind of like the Player's
[Serializable]
public class Character
{
    public string Name { get; set; }
    public string Nickname { get; set; }
    public string Grade { get; set; }
    public string Color { get; set; }
    public string ImagePath { get; set; }
    public int totOfRarity { get; set; }
    public bool onTeam { get; set; }
    public int awakening { get; set; }


    // Stats
    public int Level { get; set; }
    public int Experience { get; set; }
    public int MaxExperience { get; set; }
    public int Accuracy { get; set; }
    public int Dodge { get; set; }
    public int Attack { get; set; }
    public int HP { get; set; }
    public int MaxHP { get; set; }
    public int Speed { get; set; }
    
    public int buffedSpeed { get; set; }
    
    public int buffedAtk { get; set; }
    
    public int buffedDodge { get; set; }
    
    public int buffedAccuracy { get; set; }
    
    public bool IsBuffed { get; set; }
    public int nbTurnsBuffed { get; set; }
    public bool IsStuned { get; set; }
    
    public bool IsPoisonned { get; set; }
    public int TurnPriority { get; set; }
    
    public bool IsAlive { get; set; }

    //Abilities
    
    public Skill Skill1 { get; set; }
    public Skill Skill2 { get; set; }
    public Skill UltSkill { get; set; }
    
    
}

// Collection management class
[Serializable]
public class CharacterCollection
{
    private List<Character> characters;

    public CharacterCollection()
    {
        characters = new List<Character>();
    }

    public void AddCharacter(Character character)
    {
        characters.Add(character);
    }

    public void RemoveCharacter(Character character)
    {
        characters.Remove(character);
    }

    public List<Character> GetCharacters()
    {
        return characters;
    }
    
    public void SetCharacterStats(Character character, int level, int experience, int accuracy, int dodge, int attack,
        int hp, int speed,int maxExperience, Skill skill1, Skill skill2, Skill ultSkill, int turnPriority, bool isAlive,
         int maxHp, bool isStuned, bool isPoisonned)
    {
        character.Level = level;
        character.Experience = experience;
        character.Accuracy = accuracy;
        character.Dodge = dodge;
        character.Attack = attack;
        character.HP = hp;
        character.Speed = speed;
        character.MaxExperience = maxExperience;
        character.Skill1 = skill1;
        character.Skill2 = skill2;
        character.UltSkill = ultSkill;
        character.TurnPriority = turnPriority;
        character.IsAlive = isAlive;
        character.MaxHP = maxHp;
        character.IsPoisonned = isPoisonned;
        character.IsStuned = isStuned;
    }
}

