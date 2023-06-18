using FamWallet.Services.Campaign.Context;
using FamWallet.Services.Campaign.Models;
using FamWallet.Services.Campaign.Services;
using FamWallet.Shared.ControllerBases;
using FamWallet.Shared.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FamWallet.Services.Campaign.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DiscountController : CustomControllerBase
    {

        private readonly ICampaignService _service;
        private ISharedIdentityService identityService;

        public DiscountController(ICampaignService service,ISharedIdentityService sharedIdentityService)
        {
            _service = service;
            identityService = sharedIdentityService;
        }

        [HttpPost]
        public async Task<IActionResult> Add(DiscountModel model)
        {
            var data = await _service.AddCampaing(model);

            return CreateActionResultInstance(data);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DiscountModel model)
        {
            var data = await _service.DeleteCampaignAsync(model);

            return CreateActionResultInstance(data);
        }


        [HttpPost]
        public async Task<IActionResult> Update(DiscountModel model)
        {
            var data = await _service.UpdateCampaing(model);

            return CreateActionResultInstance(data);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetCampaign();

            return CreateActionResultInstance(data);
        }

        [HttpGet]
        public async Task<IActionResult> GetByUserId()
        {
            var data = await _service.GetCampaign(x=>x.UserId == identityService.GetUserId);

            return CreateActionResultInstance(data);
        }


        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _service.GetCampaign(x => x.Id == id);

            return CreateActionResultInstance(data);
        }


    }
}
