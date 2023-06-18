using FamWallet.IdentityServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace FamWallet.IdentityServer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TokenController : ControllerBase
    {

        public readonly IHttpClientFactory _httpClientFactory;

        public IEnumerable<KTIdentityServerResponseModel> Response { get; set; }

        public KeyValuePair<string, string>[] Request { get; set; }

        //constructor
        public TokenController(IHttpClientFactory httpClientFactory) => 
            _httpClientFactory = httpClientFactory; 

               
        
        [HttpGet]
        public async Task<IActionResult> KtToken()
        {
            Request = new[]{
                new KeyValuePair<string, string>("grant_type", Constants.GrantType),
                new KeyValuePair<string, string>("scopes", Constants.Scope),
                new KeyValuePair<string, string>("client_id", Constants.ClientId),
                new KeyValuePair<string, string>("client_secret", Constants.ClientSecret)
            };

            using (var encodedData = new FormUrlEncodedContent(Request))
            {
                var httpClient = _httpClientFactory.CreateClient("KuveytTurk");

                var httpResponseMessage = await httpClient.PostAsync("connect/token", encodedData);


                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    using var contentStram = await httpResponseMessage.Content.ReadAsStreamAsync();

                    var response = await JsonSerializer.DeserializeAsync<KTIdentityServerResponseModel>(contentStram);

                    return Ok(response);
                }
            }

            
            return BadRequest();
            
        }

    }
}
