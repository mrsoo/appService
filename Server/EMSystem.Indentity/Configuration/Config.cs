using IdentityModel;
using IdentityServer4.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace EMSystem.Indentity.Configuration
{
    public class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
                {
                    new IdentityResources.OpenId(),
                    new IdentityResources.Email(),
                    new IdentityResources.Profile(),
                      new IdentityResource
                        {
                            Name = "rc.scope",
                            UserClaims =
                            {
                                "rc.garndma"
                            }
                        },
                      new IdentityResource
                      {
                          Name="role",
                          UserClaims= new List<string>{"role"}
                      }
                };
        }
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
        {
            new ApiResource("ApiClient") ,
            new ApiResource("ApiService") ,
            new ApiResource("ApiTest", new string[] { "rc.api.garndma"}),
            new ApiResource("ApiResx","My Resx", new string[] { "rc.api.garndma" }),
            new ApiResource
            {
                Name ="api",
                DisplayName="DeviceApi",
                Description="",
                UserClaims= new List<string>{"role"},
                ApiSecrets= new List<Secret> {
                new Secret("scopeSecret".Sha256()) },
                Scopes= new List<Scope>
                {
                    new Scope("defApi.read"),
                    new Scope("defApi.write")
                }
            }
        };
        }
        public static IEnumerable<Client> GetClients()
        {
            // client credentials client
            return new List<Client>
        {
            new Client
            {
                Enabled=true,

                ClientId = "client",
                ClientName="My BackEnd Client",
                AccessTokenType = AccessTokenType.Jwt,
                AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },
                AllowedScopes = new List<string> { "api", "openid", "ApiTest","ApiClient", "ApiResx","ApiService", "roles" },

                AlwaysSendClientClaims=true,
                UpdateAccessTokenClaimsOnRefresh=true,
                AlwaysIncludeUserClaimsInIdToken=true,
                AllowAccessTokensViaBrowser=true,
                IncludeJwtId=true,
                AllowOfflineAccess=true,
                AccessTokenLifetime=3600,
                RefreshTokenUsage=TokenUsage.OneTimeOnly,
                RefreshTokenExpiration=TokenExpiration.Sliding,
                AbsoluteRefreshTokenLifetime=360000,
                ClientClaimsPrefix=string.Empty,
                Claims =
                {
                    new Claim(JwtClaimTypes.Role,"Admin"),
                    new Claim(JwtClaimTypes.Role,"User")
                }

            }
        };
        }
    }
}
