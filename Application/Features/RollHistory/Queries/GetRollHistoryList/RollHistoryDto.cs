namespace Application.Features.RollHistory.Queries.GetRollHistoryList;

public class RollHistoryDto
{
    public Guid RollHistoryId { get; set; }
    public string DiceNotation { get; set; }
    
    public List<DiceValuesDto> DiceValues { get; set; }
}

public class DiceValuesDto
{
    public Guid DiceValueId { get; set; }
    public int Pip { get; set; }
}