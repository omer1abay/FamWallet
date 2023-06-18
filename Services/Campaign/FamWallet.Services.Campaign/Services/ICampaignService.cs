using FamWallet.Services.Campaign.Models;
using FamWallet.Shared.DTOs;
using System.Linq.Expressions;

namespace FamWallet.Services.Campaign.Services
{
    public interface ICampaignService
    {
        public Task<ResponseDto<List<DiscountModel>>> GetCampaign(Expression<Func<DiscountModel,bool>> filter = null);
        public Task<ResponseDto<DiscountModel>> AddCampaing(DiscountModel model);
        public Task<ResponseDto<DiscountModel>> UpdateCampaing(DiscountModel model);
        public Task<ResponseDto<NoContent>> DeleteCampaignAsync(DiscountModel model);
    }
}
