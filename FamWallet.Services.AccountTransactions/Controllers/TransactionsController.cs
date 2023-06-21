using FamWallet.Services.AccountTransactions.DTOs;
using FamWallet.Services.AccountTransactions.Services;
using FamWallet.Shared.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FamWallet.Services.AccountTransactions.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private ITransactionService transactionService;
        private ISharedIdentityService identityService;
        public TransactionsController(ITransactionService service,ISharedIdentityService sharedIdentityService)
        {
            transactionService = service;
            identityService = sharedIdentityService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> AddTransaction(TransactionDto transaction)
        {
            transactionService.AddTransaction(transaction);
            return Ok();
        }

        
        [HttpGet]
        public async Task<IActionResult> GetTransaction(int walletId)
        {
            var response = transactionService.GetTransaction(walletId.ToString());
            if (response.Result.IsSuccess) return Ok(response.Result);
            return BadRequest(response.Result);
        }
    }
}
