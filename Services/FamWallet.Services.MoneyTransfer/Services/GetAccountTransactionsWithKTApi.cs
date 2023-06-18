using FamWallet.Services.MoneyTransfer.Models;
using FamWallet.Shared.Constants;
using FamWallet.Shared.DTOs;
using FamWallet.Shared.Hashing;
using FamWallet.Shared.Models;
using System.Net;
using System.Text;
using System.Text.Json;

namespace FamWallet.Services.MoneyTransfer.Services
{
    public class GetAccountTransactionsWithKTApi : ITransactionService
    {
        #region Properties
        private IHttpClientFactory _httpClient;

        private readonly string _accountTransactionsUrl = "https://prep-gateway.kuveytturk.com.tr/v3/accounts/5/transactions";

        private readonly string _tokenUrl = "https://localhost:5051/api/token/kttoken";

        private KTIdentityServerResponseModel _tokenResponse;

        private readonly RedisService _redisService;

        private KTAccountTransactionsResponseModel? _transactionResponse;
        #endregion

        public GetAccountTransactionsWithKTApi(IHttpClientFactory httpClientFactory, RedisService redisService)
        {
            _redisService = redisService;
            _httpClient = httpClientFactory;
        }

        public async Task<ResponseDto<KTAccountTransactionsResponseModel>> GetTransactions()
        {
            //DeleteCache();
            var accessToken = JsonSerializer.Deserialize<KTIdentityServerResponseModel>(GetAccessTokenFromRedis() ?? GetToken());

            string data = accessToken!.AccessToken;

            if (accessToken is not null)
            {
                var signature = SignatureHelper.CreateSignature(data, Constants.PrivateKey); //signed data with encoded

                var httpRequestMessage = new HttpRequestMessage(
                        HttpMethod.Get,
                        _accountTransactionsUrl
                    )
                {
                    Headers =
                    {
                        {"Authorization","Bearer " + data },
                        {"Signature", signature }
                    }
                };

                var httpClient = _httpClient.CreateClient("KuveytTurk");

                var httpResponse = await httpClient.SendAsync(httpRequestMessage);

                if (httpResponse.IsSuccessStatusCode)
                {
                    using var stream = httpResponse.Content.ReadAsStreamAsync();
                    _transactionResponse = JsonSerializer.Deserialize<KTAccountTransactionsResponseModel>(stream.Result);
                    return ResponseDto<KTAccountTransactionsResponseModel>.Success(_transactionResponse!, 204);
                }
                else
                {
                    DeleteCache();
                }
            }

            return ResponseDto<KTAccountTransactionsResponseModel>.Failure("Access token may be null", 400);

        }

        public async void DeleteCache()
        {
            await _redisService.GetDatabase().KeyDeleteAsync("token");
            await GetTransactions();
        }

        public string GetToken()
        {
            //get-token
            var httpRequest = _httpClient.CreateClient("KuveytTurk");

            var httpResponse = httpRequest.GetAsync(_tokenUrl);

            if (httpResponse.Result.IsSuccessStatusCode)
            {
                using var getToken = httpResponse.Result.Content.ReadAsStreamAsync();
                _tokenResponse = JsonSerializer.Deserialize<KTIdentityServerResponseModel>(getToken.Result);
            }

            _redisService.GetDatabase().StringSet("token", JsonSerializer.Serialize(_tokenResponse));

            return GetAccessTokenFromRedis();

        }

        #region Internal Methods

        private string GetAccessTokenFromRedis()
        {
            var accessTokenData = _redisService.GetDatabase().StringGet("token");
            return accessTokenData;
        }

        #endregion

    }
}
