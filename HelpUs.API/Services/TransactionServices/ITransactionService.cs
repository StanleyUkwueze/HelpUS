using HelpUs.DataAccess.DataTransferObjects.Responses;
using HelpUs.Service.DataTransferObjects.Requests;
using HelpUs.Service.DataTransferObjects.Responses;
using PayStack.Net;

namespace HelpUs.API.Services.TransactionServices
{
    public interface ITransactionService
    {
        Task<PaymentVerificationResponse> VerifyPayment(string trxref);
        Task<APIResponse<TransactionInitializeResponse>> InitiatePayment(TransactionDto transactionDto);
    }
}