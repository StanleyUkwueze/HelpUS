using HelpUs.API.Services.TransactionServices;
using HelpUs.Service.DataTransferObjects.Requests;
using Microsoft.AspNetCore.Mvc;

namespace HelpUs.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonationController(ITransactionService transactionService) : ControllerBase
    {
        [HttpPost("donate")]
        public async Task<IActionResult> InitiatePayment([FromForm]TransactionDto transaction)
        {
            var response = await transactionService.InitiatePayment(transaction!);

             return Ok(response);
        }

        [HttpGet("verify-payment")]
        public async Task<IActionResult> VerifyPayment(string trxref)
        {
            var response = await transactionService.VerifyPayment(trxref);
            return Ok(response);
        }
    }
}
