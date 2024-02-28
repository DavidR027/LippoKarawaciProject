using ProjectTagihan.Contexts;
using ProjectTagihan.Contracts;
using ProjectTagihan.Entities;

namespace ProjectTagihan.Repositories;

public class BillRepository : GeneralRepository<Bill>, IBillRepository
{
    public BillRepository(BillDbContext context) : base(context)
    {
    }

    public int GetLatestBillCounter()
    {
        var latestBillNo = GetAll()
            .Where(b => b.Description != null)
            .OrderByDescending(b => b.Description)
            .Select(b => b.Description)
            .FirstOrDefault();

        if (int.TryParse(latestBillNo?.Replace("Tagihan#", ""), out int latestBill))
        {
            return latestBill;
        }

        return 0;
    }

    public int GetLatestPaymentCounter()
    {
        var latestPaymentNo = GetAll()
            .Where(b => b.PaymentNo != null)
            .OrderByDescending(b => b.PaymentNo)
            .Select(b => b.PaymentNo)
            .FirstOrDefault();

        if (int.TryParse(latestPaymentNo?.Replace("Payment#", ""), out int latestPaymet))
        {
            return latestPaymet;
        }

        return 0;
    }

    public int CalculateLateTime(DateTime dueDate, DateTime? paymentDate)
    {
        if (paymentDate.HasValue)
        {
            int lateTime = (int)Math.Max(0, (paymentDate.Value - dueDate).TotalDays);
            return lateTime;
        }

        return 0;
    }

}
