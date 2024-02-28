using ProjectTagihan.Entities;

namespace ProjectTagihan.DTOs;

public class NewBillDTO
{
    public string? Description { get; set; }
    public decimal? BillAmount { get; set; }
    public DateTime DueDate { get; set; }

    public static implicit operator Bill(NewBillDTO billDTO)
    {
        return new Bill
        {
            Description = billDTO.Description,
            BillAmount = billDTO.BillAmount,
            DueDate = billDTO.DueDate,
        };
    }

    public static explicit operator NewBillDTO(Bill bill)
    {
        return new NewBillDTO
        {
            Description = bill.Description,
            BillAmount = bill.BillAmount,
            DueDate = bill.DueDate,
        };
    }
}
