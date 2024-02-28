using Microsoft.AspNetCore.Mvc;
using ProjectTagihan.DTOs;
using ProjectTagihan.Entities;
using ProjectTagihan.Services;
using ProjectTagihan.Utilities;
using System.Net;

namespace ProjectTagihan.Controllers;

[ApiController]
[Route("bills")]
//[Authorize]
public class BillController : ControllerBase
{
    private readonly BillServices _billServices;
    public BillController(BillServices billServices)
    {
        _billServices = billServices;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        try
        {
            var entities = _billServices.GetAllBill();

            if (!entities.Any())
                return NotFound(new ResponseHandler<Bill>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data not found"
                });

            return Ok(new ResponseHandler<IEnumerable<Bill>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data found",
                Data = entities
            });
        }
        catch (Exception ex)
        {
            //if error
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseExceptionHandler
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = ex.Message
            });
        }
    }

    [HttpPost]
    public IActionResult Create(NewBillDTO billDTO)
    {
        try
        {
            var bill = _billServices.Createbill(billDTO);

            return Ok(new ResponseHandler<Bill>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Bill created successfully",
                Data = bill
            });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseExceptionHandler
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = ex.Message
            });
        }
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(string guid)
    {
        try
        {
            var bill = _billServices.GetBillByGuid(guid);

            if (bill == null)
                return NotFound(new ResponseHandler<Bill>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Bill not found"
                });

            return Ok(new ResponseHandler<Bill>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Bill found",
                Data = bill
            });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseExceptionHandler
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = ex.Message
            });
        }
    }

    [HttpGet("Total-due")]
    public IActionResult GetTotalDue(DateTime dateNow)
    {
        try
        {
            TotalDueDTO totalDue = _billServices.CheckDue(dateNow);
            return Ok(new ResponseHandler<TotalDueDTO>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Total Due",
                Data = totalDue
            });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseExceptionHandler
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = ex.Message
            });
        }
    }

    [HttpPost("Pay")]
    public IActionResult PayBill(PayBillDTO payBillDTO)
    {
        try
        {
            var result = _billServices.PayBill(payBillDTO);

            if (result == null)
            {
                return NotFound(new ResponseHandler<PayBillDTO>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Bill not found"
                });
            }

            return Ok(new ResponseHandler<PayBillDTO>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Bill payment processed successfully",
                Data = result
            });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseExceptionHandler
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = ex.Message
            });
        }
    }

    [HttpGet("Penalties")]
    public IActionResult GetPenalties(DateTime dateNow)
    {
        try
        {
            List<PenatlyDTO> penalties = _billServices.CheckPenalty(dateNow);
            return Ok(new ResponseHandler<List<PenatlyDTO>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Penalty List",
                Data = penalties
            });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseExceptionHandler
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = ex.Message
            });
        }
    }

    [HttpPut]
    public IActionResult Update(Bill updateBill)
    {
        try
        {

            var bill = _billServices.Updatebill(updateBill);

            if (bill == null)
            {
                return NotFound(new ResponseHandler<Bill>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Bill not found"
                });
            }

            return Ok(new ResponseHandler<Bill>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Bill updated successfully",
                Data = bill
            });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseExceptionHandler
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = ex.Message
            });
        }
    }

    [HttpDelete("{guid}")]
    public IActionResult Delete(string guid)
    {
        try
        {
            var result = _billServices.Deletebill(guid);

            if (!result)
                return NotFound(new ResponseHandler<Bill>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Bill not found"
                });

            return Ok(new ResponseHandler<Bill>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Bill deleted successfully"
            });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseExceptionHandler
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = ex.Message
            });
        }
    }

}
