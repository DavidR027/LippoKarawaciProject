namespace ClientTagihan.Models;

public class NewBill
{
    public string? Description { get; set; }
    public decimal? BillAmount { get; set; }
    public DateTime DueDate { get; set; }
}
