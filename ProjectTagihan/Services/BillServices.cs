using ProjectTagihan.Contracts;
using ProjectTagihan.DTOs;
using ProjectTagihan.Entities;

namespace ProjectTagihan.Services;

public class BillServices
{
    private readonly IBillRepository _billRepository;

    public BillServices(IBillRepository billRepository)
    {
        _billRepository = billRepository;

    }

    public IEnumerable<Bill> GetAllBill()
    {
        var bills = _billRepository.GetAll().OrderBy(b => b.DueDate);

        if (!bills.Any()) return Enumerable.Empty<Bill>();

        var getbills = new List<Bill>();

        foreach (var bill in bills)
        {

            getbills.Add(new Bill
            {
                Guid = bill.Guid,
                Description = bill.Description,
                BillAmount = bill.BillAmount,
                DueDate = bill.DueDate,
                PaymentNo = bill.PaymentNo,
                PaymentDate = bill.PaymentDate,
                PaymentAmount = bill.PaymentAmount,
            });
        }

        return getbills;
    }

    public NewBillDTO Createbill(NewBillDTO billDTO)
    {
        int latestBillCounter = _billRepository.GetLatestBillCounter();
        var newBill = new Bill
        {
            Guid = Guid.NewGuid().ToString(),
            Description = "Tagihan#" + (latestBillCounter + 1),
            BillAmount = billDTO.BillAmount,
            DueDate = billDTO.DueDate,
        };

        _billRepository.Create(newBill);
        return billDTO;
    }

    public Bill GetBillByGuid(string guid)
    {
        var bill = _billRepository.GetByGuid(guid);
        if (bill == null) return null;

        var Bill = new Bill
        {
            Guid = bill.Guid,
            Description = bill.Description,
            BillAmount = bill.BillAmount,
            DueDate = bill.DueDate,
            PaymentNo = bill.PaymentNo,
            PaymentDate = bill.PaymentDate,
            PaymentAmount = bill.PaymentAmount,
        };

        return Bill;
    }

    public Bill Updatebill(Bill data)
    {
        var updateBill = _billRepository.GetByGuid(data.Guid);
        if (updateBill == null) return null;

        //Update
        updateBill.Guid = data.Guid;
        updateBill.Description = data.Description;
        updateBill.BillAmount = data.BillAmount;
        updateBill.DueDate = data.DueDate; 
        updateBill.PaymentNo = data.PaymentNo;
        updateBill.PaymentDate = data.PaymentDate;
        updateBill.PaymentAmount = data.PaymentAmount;

        _billRepository.Update(updateBill);
        return data;
    }

    public bool Deletebill(string guid)
    {
        var bill = _billRepository.GetByGuid(guid);
        if (bill == null) throw new Exception("Bill not found");

        return _billRepository.Delete(bill);
    }


    /* Query DB total undue & overdue pada tanggal 25 Mar 23
        SELECT
            COUNT(CASE WHEN due_date > '2023-03-25' THEN 1 END) AS Undue,
            COUNT(CASE WHEN due_date <= '2023-03-25' THEN 1 END) AS Overdue
        FROM bill;
    */
    public TotalDueDTO CheckDue(DateTime dateNow)
    {
        var allBills = _billRepository.GetAll();

        var undueBills = allBills.Where(bill => bill.DueDate > dateNow);
        var overdueBills = allBills.Where(bill => bill.DueDate <= dateNow);

        int totalUndue = undueBills.Count();
        int totalOverdue = overdueBills.Count();

        return new TotalDueDTO
        {
            TotalUndue = totalUndue,
            TotalOverdue = totalOverdue
        };
    }

    public PayBillDTO PayBill(PayBillDTO data)
    {
        var bills = _billRepository.GetAll()
            .OrderBy(b => b.DueDate)
            .ToList();

        var payment = data.PaymentAmount;

        int latestPaymentCounter = _billRepository.GetLatestPaymentCounter();

        foreach (var bill in bills)
        {
            if (data.PaymentAmount > 0 && bill.BillAmount.HasValue)
            {
                decimal remainingAmount = Math.Max(0, bill.BillAmount.Value - data.PaymentAmount);

                if (bill.PaymentAmount == bill.BillAmount)
                {
                    break;
                }

                if (remainingAmount == 0)
                {
                    bill.PaymentAmount = bill.BillAmount;
                }
                else
                {
                    bill.PaymentAmount = bill.BillAmount - remainingAmount;
                }

                data.PaymentAmount -= bill.PaymentAmount.Value;

                bill.PaymentNo = data.PaymentNo;

                bill.PaymentDate = data.PaymentDate;

                _billRepository.Update(bill);

            }
        }

        return new PayBillDTO
        {
            PaymentNo = data.PaymentNo,
            PaymentDate = data.PaymentDate,
            PaymentAmount = payment
        };
    }

    public List<PenatlyDTO> CheckPenalty(DateTime dateNow)
    {
        var Bills = _billRepository.GetAll();

        var penaltyNo = 0;
        var penalties = new List<PenatlyDTO>();

        foreach (var bill in Bills)
        {
            if (bill.DueDate > dateNow)
            {
                continue;
            }

            int lateTime = 0;

            if (bill.PaymentNo == null)
            {
                lateTime = _billRepository.CalculateLateTime(bill.DueDate, dateNow);
            }
            else
            {
                lateTime = _billRepository.CalculateLateTime(bill.DueDate, bill.PaymentDate);
            }

            decimal overdue = (bill.PaymentAmount.HasValue && bill.BillAmount.HasValue && bill.BillAmount > bill.PaymentAmount)
            ? (bill.PaymentAmount.Value >= bill.BillAmount.Value)
                ? 0
                : (bill.BillAmount.Value - bill.PaymentAmount.Value)
            : (bill.BillAmount.HasValue ? bill.BillAmount.Value : 0);

            decimal penaltyAmount = overdue * 0.002m * lateTime;

            penaltyNo++;

            var penalty = new PenatlyDTO
            {
                TagihanNo = bill.Description,
                PenaltyNo = penaltyNo,
                Overdue = overdue,
                LateTime = lateTime,
                PenaltyAmount = penaltyAmount
            };

            penalties.Add(penalty);
        }

        return penalties;
    }

}
