using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamWallet.Shared.Services
{
    public class SharedIdentityService: ISharedIdentityService
    {
        //jwt'den gelen data'yı oku claim verilerini okuyacağız httpcontext nesnesinden okuyaccağız

        //startup tarafında servis olarak eklememiz lazım
        private IHttpContextAccessor _httpContextAccessor;

        public SharedIdentityService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserId => _httpContextAccessor.HttpContext.User.FindFirst("sub").Value;
    }
}
