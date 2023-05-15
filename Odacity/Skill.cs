namespace Odacity;

[Serializable]
public class Skill
{
    public int AttackMultiplier { get; set; }
    public string Description { get; set; }
    
    //Empty if False
    public string BuffEffect { get; set; } //
    public int buffPercentage { get; set; }
    public bool isAoeBuff { get; set; }
    public string DebuffEffect { get; set; } //stun, poison
    public bool isSingleTarget { get; set; }
    
    public int Cooldown { get; set; }
    
    public int CooldownLeft { get; set; }
}


