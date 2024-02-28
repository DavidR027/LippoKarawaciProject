using ProjectTagihan.Entities;

namespace ProjectTagihan.DTOs;

public class PayBillDTO
{
    public string? PaymentNo { get; set; }
    public DateTime? PaymentDate { get; set; }
    public decimal PaymentAmount { get; set; }

    public static implicit operator Bill(PayBillDTO billDTO)
    {
        return new Bill
        {
            PaymentNo = billDTO.PaymentNo,
            PaymentDate = billDTO.PaymentDate,
            PaymentAmount = billDTO.PaymentAmount,
        };
    }

    public static explicit operator PayBillDTO(Bill bill)
    {
        return new PayBillDTO
        {
            PaymentNo = bill.PaymentNo,
            PaymentDate = bill.PaymentDate,
            PaymentAmount = bill.PaymentAmount ?? 0,
        };
    }
}
