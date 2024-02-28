using ProjectTagihan.Entities;

namespace ProjectTagihan.Contracts;

public interface IBillRepository : IGeneralRepository<Bill>
{
    int GetLatestBillCounter();
    int GetLatestPaymentCounter();
    int CalculateLateTime(DateTime dueDate, DateTime? paymentDate);
}
