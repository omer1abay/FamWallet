using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FamWallet.Shared.Models
{
    public class UserModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("walletNumber")]
        public int WalletNumber { get; set; }

        [JsonPropertyName("budget")]
        public decimal Budget { get; set; }

        [JsonPropertyName("userName")]
        public string UserName { get; set; }

        [JsonPropertyName("normalizedUserName")]
        public string NormalizedUserName { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("normalizedEmail")]
        public string NormalizedEmail { get; set; }

        [JsonPropertyName("emailConfirmed")]
        public bool EmailConfirmed { get; set; }

        [JsonPropertyName("passwordHash")]
        public string PasswordHash { get; set; }

        [JsonPropertyName("securityStamp")]
        public string SecurityStamp { get; set; }

        [JsonPropertyName("concurrencyStamp")]
        public string ConcurrencyStamp { get; set; }

        [JsonPropertyName("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonPropertyName("phoneNumberConfirmed")]
        public bool PhoneNumberConfirmed { get; set; }

        [JsonPropertyName("twoFactorEnabled")]
        public bool TwoFactorEnabled { get; set; }

        [JsonPropertyName("lockoutEnd")]
        public string LockoutEnd { get; set; }

        [JsonPropertyName("lockoutEnabled")]
        public bool LockoutEnabled { get; set; }

        [JsonPropertyName("accessFailedCount")]
        public int AccessFailedCount { get; set; }
    }
}
