using FamWallet.Services.MoneyTransfer.Models;
using FamWallet.Services.MoneyTransfer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FamWallet.Services.MoneyTransfer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {

        private ITransactionService _transactionService;
        private IMoneyTransferService _moneyTransferService;
        private RedisService _redisService;

        public PaymentController(ITransactionService transactionService, RedisService redisService,IMoneyTransferService moneyTransferService)
        {
            _transactionService = transactionService;
            _redisService = redisService;
            _moneyTransferService = moneyTransferService;
        }


        [HttpPost]
        public async Task<IActionResult> DoMoneyTransfer(MoneyTransferModel model)
        {
            var result = _moneyTransferService.DoMoneyTransfer(model.ReceiverWalletNumber,model.Balance,model.SenderWalletNumber);

            if (result != 0)
            {
                return Ok();
            }

            return BadRequest();
        }

    }
}
