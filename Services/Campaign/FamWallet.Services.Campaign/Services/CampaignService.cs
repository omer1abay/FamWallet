using FamWallet.Services.Campaign.Context;
using FamWallet.Services.Campaign.Models;
using FamWallet.Shared.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FamWallet.Services.Campaign.Services
{
    public class CampaignService : ICampaignService
    {

        private readonly ApplicationDbContext _context;

        public CampaignService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseDto<DiscountModel>> AddCampaing(DiscountModel model)
        {
            var added = _context.Entry(model);
            added.State = EntityState.Added;
            await _context.SaveChangesAsync();
            return ResponseDto<DiscountModel>.Success(model,204); 
        }

        public async Task<ResponseDto<NoContent>> DeleteCampaignAsync(DiscountModel model)
        {
            var added = _context.Entry(model);
            added.State = EntityState.Deleted;
            await _context.SaveChangesAsync();
            return ResponseDto<NoContent>.Success(204);
        }

        public async Task<ResponseDto<List<DiscountModel>>> GetCampaign(Expression<Func<DiscountModel, bool>>? filter = null)
        {
            var data =  filter == null ? _context.Set<DiscountModel>().ToList() : _context.Set<DiscountModel>().Where(filter).ToList();
            return ResponseDto<List<DiscountModel>>.Success(data,200);
        }

        public async Task<ResponseDto<DiscountModel>> UpdateCampaing(DiscountModel model)
        {
            var added = _context.Entry(model);
            added.State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return ResponseDto<DiscountModel>.Success(model,204);
        }
    }
}
