using StackExchange.Redis;

namespace FamWallet.Services.MoneyTransfer.Services
{
    public class RedisService
    {
        private readonly string _host;
        private int _port;

        //bağlantı için gerekli sınıf
        private ConnectionMultiplexer _connectionMultiplexer;
        public RedisService(string host, int port)
        {
            _host = host;
            _port = port;
        }

        //bağlantı kur
        public void Connect() => _connectionMultiplexer = ConnectionMultiplexer.Connect($"{_host}:{_port}");

        //1 numaralı veritabanımızı aldık
        public IDatabase GetDatabase(int sortNumber = 1) => _connectionMultiplexer.GetDatabase(sortNumber);

    }
}
