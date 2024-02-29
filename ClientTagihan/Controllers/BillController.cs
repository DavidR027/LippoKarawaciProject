using ClientTagihan.Models;
using ClientTagihan.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ClientTagihan.Controllers;

public class BillController : Controller
{
    private readonly IBillRepository repository;
    public BillController(IBillRepository repository)
    {
        this.repository = repository;
    }

    public async Task<IActionResult> Index()
    {
        var result = await repository.Get();
        var bills = new List<Bill>();

        if (result.Data != null)
        {
            bills = result.Data.Select(e => new Bill
            {
                Guid = e.Guid,
                Description = e.Description,
                BillAmount = e.BillAmount,
                DueDate = e.DueDate,
                PaymentNo = e.PaymentNo,
                PaymentDate = e.PaymentDate,
                PaymentAmount = e.PaymentAmount,

            }).ToList();
        }

        return View(bills);
    }

    [HttpGet]
    public async Task<IActionResult> PayBills()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> PayBills(PayBill data)
    {
        var result = await repository.PayBills(data);

        if (result != null)
        {
            if (result.Code == 200)
            {
                return RedirectToAction(nameof(Index));
            }
            else if (result.Code == 409)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View();
            }
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Error processing payment. Please try again.");
            return View();
        }

        return View();
    }

    [HttpGet]
    public async Task<IActionResult> GetPenalties(DateTime dateNow)
    {
        var result = await repository.Penalties(dateNow);
        var penalties = new List<Penalty>();

        if (result != null && result.Data != null)
        {
            penalties = result.Data.Select(e => new Penalty
            {
                TagihanNo = e.TagihanNo,
                PenaltyNo = e.PenaltyNo,
                Overdue = e.Overdue,
                LateTime = e.LateTime,
                PenaltyAmount = e.PenaltyAmount,
            }).ToList();
        }

        return View(penalties);
    }


}
