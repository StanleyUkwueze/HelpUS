using AutoMapper;
using HelpUs.API.Common;
using HelpUs.API.DataAccess;
using HelpUs.API.Entity.Entities;
using HelpUs.API.Models;
using HelpUs.DataAccess.DataTransferObjects.Responses;
using HelpUs.DataAccess.UserRepository;
using HelpUs.Service.DataTransferObjects.Requests;
using HelpUs.Service.DataTransferObjects.Responses;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PayStack.Net;

namespace HelpUs.API.Services.TransactionServices
{
    public class TransactionService : ITransactionService
    {
        private readonly HttpClient _httpClient;
        private readonly AppDbContext _context;
        private readonly HelperMethods _helperMethods;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly string token;
        private readonly string baseUrl;

        private PayStackApi Paystack { get; set; }
        public TransactionService
            (
            HttpClient httpClient,
            IUserRepository userRepository,
            IMapper mapper,
            HelperMethods helperMethods,
            IConfiguration configuration,
            AppDbContext context
            )
        {
            _httpClient = httpClient;
            _context = context;
            _mapper = mapper;   
            _helperMethods = helperMethods;
            _configuration = configuration;
            _userRepository = userRepository;
            token = _configuration["Payment:PaystackSK"]!;
            baseUrl = _configuration.GetSection("BaseUrl").Value!;
            Paystack = new PayStackApi(token);
        }

        public async Task<APIResponse<TransactionInitializeResponse>> InitiatePayment(TransactionDto transactionDto)
        {
           
            TransactionInitializeRequest request = new()
            {
                AmountInKobo = ((int)(transactionDto.Amount * 100)),
                Email = transactionDto.Email,
                Reference = ReferenceGenerator().ToString(),
                Currency = "NGN",
                CallbackUrl = $"{baseUrl}/api/Donation/verify-payment"
            };

            var loggedInUser = _helperMethods.GetUserId();
            if (loggedInUser.Item1 is null || loggedInUser.Item2 is null)
            {
                return new APIResponse<TransactionInitializeResponse>
                {
                    Message = $"No logged in user record found",
                    IsSuccessful = false
                };
            }
            var user = await _userRepository.GetByIDAsync(loggedInUser.Item1);
            if (user is null)
            {
                return new APIResponse<TransactionInitializeResponse>
                {
                    Message = $"No user record found",
                    IsSuccessful = false
                };
            }
            var project = await _context.Projects.FindAsync(transactionDto.ProjectId);

            if (project is null)
            {
                return new APIResponse<TransactionInitializeResponse>
                {
                    Message = $"No project record found",
                    IsSuccessful = false
                };
            }
            var donation = new Donation
            {
                UserId = loggedInUser.Item1,
                DonorName = transactionDto.Name!,
                ProjectId = transactionDto.ProjectId!,
                Amount = transactionDto.Amount,
                User = user,
                Project = project!
            };

            _context.Donations.Add(donation);
            await _context.SaveChangesAsync();

            var response = Paystack.Transactions.Initialize(request);
            response.RawJson = null;
            if (response.Status)
            {

                var transaction = new Transaction()
                {
                    Amount = (int)transactionDto.Amount,
                    Email = transactionDto.Email!,
                    TrxRef = request.Reference,
                    CustomerName = transactionDto.Name!,
                    ProjectId = transactionDto.ProjectId!
                };

                await _context.Transactions.AddAsync(transaction);
                var isSaved = await _context.SaveChangesAsync();

                if (isSaved > 0)
                {
                    return new APIResponse<TransactionInitializeResponse>
                    {
                        Message = response.Message,
                        IsSuccessful = response.Status,
                        Data = response
                    };
                }

                return new APIResponse<TransactionInitializeResponse>
                {
                    Message = "Transaction/payment records not saved successfully",
                    IsSuccessful = false
                };
            }

            return new APIResponse<TransactionInitializeResponse>
            {
                Message = "Transaction failed",
                IsSuccessful = false
            };
        }

        public async Task<PaymentVerificationResponse> VerifyPayment(string trxref)
        {
            TransactionVerifyResponse response = Paystack.Transactions.Verify(trxref);
            var result = new PaymentVerificationResponse();

            if (response.Status)
            {
                var userId = _helperMethods.GetUserId();
                var transaction = _context.Transactions.Where(x => x.TrxRef == trxref).FirstOrDefault();
                if (transaction != null)
                {
                    var donation = await _context.Donations
                       .FirstOrDefaultAsync(x => x.UserId == userId.Item1
                       && !x.IsPaid);

                    if (donation is not null)
                    {
                        donation.IsPaid = true;
                        _context.Update(donation);
                    }

                     transaction.Status = true;
                    _context.Transactions.Update(transaction);
                    await _context.SaveChangesAsync();

                    var jsonResponese = JsonConvert.SerializeObject(response.Data);
                    result = JsonConvert.DeserializeObject<PaymentVerificationResponse>(jsonResponese);

                    return result!;
                }
            }

            return result;
        }


        public static long ReferenceGenerator()
        {
            Random rand = new Random((int)DateTime.Now.Ticks);
            return rand.Next(100000000, 999999999);
        }
    }
}
