namespace Domain.Common;

public class Base
{
    public Guid CreatedBy { get; set; }
    public DateTime CreateAt { get; set; }
    public Guid ChangedBy { get; set; }
    public DateTime ChangedAt { get; set; }
}