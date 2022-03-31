using Domain.Common;

namespace Domain.Entities;

public class RollHistory : Base
{
    public Guid RollHistoryId { get; set; }
    public string DiceNotation { get; set; }
    
    public List<DiceValue> DiceValues { get; set; }
}