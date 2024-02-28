namespace ProjectTagihan.Entities;

public class Bill
{
    public string Guid { get; set; } = null!;
    public string? Description { get; set; }
    public decimal? BillAmount { get; set; }
    public DateTime DueDate { get; set; }
    public string? PaymentNo { get; set; }
    public DateTime? PaymentDate { get; set; }
    public decimal? PaymentAmount { get; set; }
}
