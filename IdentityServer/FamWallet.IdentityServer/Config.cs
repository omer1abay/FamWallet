// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;

namespace FamWallet.IdentityServer
{
    public static class Config
    {
        //Aud
        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
            new ApiResource("resource_moneytransfer"){ Scopes={ "moneytransfer_fullpermission" } },
            new ApiResource("resource_transaction"){ Scopes={ "transaction_fullpermission" } },
            new ApiResource(IdentityServerConstants.LocalApi.ScopeName),
            new ApiResource("resource_discount"){ Scopes={ "discount_fullpermission" } },
        };

        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
                       new IdentityResources.Email(),
                       new IdentityResources.OpenId(), //zorunlu openidconnect protokolünün zorunluluğu
                       new IdentityResources.Profile(),
                       new IdentityResource(){Name="roles",DisplayName="Roles",Description="Kullanıcı rolleri", UserClaims=new[]{"role"}}
                   };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("moneytransfer_fullpermission","Para Transferi API için full erişim"),
                new ApiScope("transaction_fullpermission","Hesap hareketleri API için full erişim"),
                new ApiScope(IdentityServerConstants.LocalApi.ScopeName),
                new ApiScope("discount_fullpermission","Discount API için full erişim"),
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
               {
                   ClientName = "Asp.Net Core Mvc",
                   ClientId = "WebMvcClient",
                   ClientSecrets = {new Secret("secret".Sha256())},
                   AllowedGrantTypes=GrantTypes.ClientCredentials,
                   AllowedScopes = { "discount_fullpermission", IdentityServerConstants.LocalApi.ScopeName }
               },
               new Client
               {
                   ClientName = "Asp.Net Core Mvc",
                   ClientId = "WebMvcClientForUser",
                   AllowOfflineAccess=true,
                   ClientSecrets = {new Secret("secret".Sha256())},
                   AllowedGrantTypes=GrantTypes.ResourceOwnerPassword, //refresh token
                   AllowedScopes = {"moneytransfer_fullpermission","transaction_fullpermission",IdentityServerConstants.StandardScopes.Email, IdentityServerConstants.StandardScopes.OpenId,
                       IdentityServerConstants.StandardScopes.Profile, IdentityServerConstants.StandardScopes.OfflineAccess, IdentityServerConstants.LocalApi.ScopeName, "roles" }, //OfflineAccess = refresh_token
                   AccessTokenLifetime=1*60*60, //1 saat accesstoken ömrü
                   RefreshTokenExpiration=TokenExpiration.Absolute,
                   AbsoluteRefreshTokenLifetime=(int)(DateTime.Now.AddDays(60) - DateTime.Now).TotalSeconds, //refresh_token ömrü 60 gün
                   RefreshTokenUsage = TokenUsage.ReUse //tekrar kullanılabilir
               }
            };
    }
}