namespace ClientTagihan.Models;

public class Penalty
{
    public string? TagihanNo { get; set; }
    public int? PenaltyNo { get; set; }
    public decimal? Overdue { get; set; }
    public int? LateTime { get; set; }
    public decimal? PenaltyAmount { get; set; }
}
