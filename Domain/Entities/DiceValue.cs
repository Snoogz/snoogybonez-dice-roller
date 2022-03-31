namespace Domain.Entities;

public class DiceValue
{
    public Guid DiceValueId { get; set; }
    public int Pip { get; set; }
    
    public Guid RollHistoryId { get; set; }
    public RollHistory RollHistory { get; set; }
}