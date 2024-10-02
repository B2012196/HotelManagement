using Duende.IdentityServer.Models;
using Duende.IdentityServer.Test;
using Duende.IdentityServer;
using IdentityModel;
using System.Security.Claims;

namespace IdentityManagement.API
{
    public class Config
    {
        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                   {
                        ClientId = "hotelmanagementClient",
                        AllowedGrantTypes = GrantTypes.ClientCredentials,
                        ClientSecrets =
                        {
                            new Secret("hotelsecret".Sha256())
                        },
                        AllowedScopes = { "hotelmanagementAPI" }
                   },
                new Client
                {
                    ClientId = "webapp_client",
                    AllowedGrantTypes = GrantTypes.Hybrid, 
                    RequirePkce = false,
                    AllowRememberConsent = false,
                    RedirectUris = new List<string>
                    {
                        "https://localhost:5057/signin-oidc"
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        "https://localhost:5057/signout-callback-oidc"
                    },
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256()) // Đảm bảo secret đã được băm đúng
                    },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "hotelmanagementAPI" // Đảm bảo scope này cũng đã được cấu hình
                    }
                }

            };

        public static IEnumerable<ApiScope> ApiScopes =>
           new ApiScope[]
           {
               new ApiScope("hotelmanagementAPI", "Hotel Management API")
           };

        public static IEnumerable<ApiResource> ApiResources =>
          new ApiResource[]
          {
               //new ApiResource("movieAPI", "Movie API")
          };

        public static IEnumerable<IdentityResource> IdentityResources =>
          new IdentityResource[]
          {
              new IdentityResources.OpenId(),
              new IdentityResources.Profile()

          };

        public static List<TestUser> TestUsers =>
            new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "5BE86359-073C-434B-AD2D-A3932222DABE",
                    Username = "admin",
                    Password = "admin",
                    Claims = new List<Claim>
                    {
                        new Claim(JwtClaimTypes.GivenName, "duc"),
                        new Claim(JwtClaimTypes.FamilyName, "lam")
                    }
                }
            };
    }
}
