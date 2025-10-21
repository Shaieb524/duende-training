using Duende.IdentityServer.Models;

public static class IdentityServerConfig
{
    public static IEnumerable<Client> GetClients()
    {
        return new List<Client>
        {
            // 1. Machine-to-Machine Client (Service-to-Service)
            new Client
            {
                ClientId = "machine-client",
                ClientName = "Machine to Machine Client",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets =
                {
                    new Secret("machine-secret".Sha256())
                },
                AllowedScopes = { "api1" }
            },

            // 2. Web Application Client (with user login)
            new Client
            {
                ClientId = "web-client",
                ClientName = "Web Application Client",
                AllowedGrantTypes = GrantTypes.Code,
                ClientSecrets =
                {
                    new Secret("web-secret".Sha256())
                },
                RedirectUris = { "https://localhost:5001/signin-oidc" },
                PostLogoutRedirectUris = { "https://localhost:5001/signout-callback-oidc" },
                AllowedScopes = { "openid", "profile", "api1" },
                RequireConsent = false
            },

            // 3. Single Page Application (SPA) Client
            new Client
            {
                ClientId = "spa-client",
                ClientName = "Single Page Application Client",
                AllowedGrantTypes = GrantTypes.Code,
                RequireClientSecret = false, // SPAs can't securely store secrets
                RequirePkce = true, // Security enhancement for SPAs
                RedirectUris = { "http://localhost:3000/callback" },
                PostLogoutRedirectUris = { "http://localhost:3000" },
                AllowedCorsOrigins = { "http://localhost:3000" },
                AllowedScopes = { "openid", "profile", "email", "api1" },
                RequireConsent = false,
                AllowAccessTokensViaBrowser = true
            }
        };
    }

    public static IEnumerable<ApiScope> GetApiScopes()
    {
        return new List<ApiScope>
        {
            new ApiScope("api1", "My API")
        };
    }

    public static IEnumerable<IdentityResource> GetIdentityResources()
    {
        return new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email()
        };
    }

    public static List<Duende.IdentityServer.Test.TestUser> GetTestUsers()
    {
        return new List<Duende.IdentityServer.Test.TestUser>
        {
            new Duende.IdentityServer.Test.TestUser
            {
                SubjectId = "1",
                Username = "alice",
                Password = "password"
            },
            new Duende.IdentityServer.Test.TestUser
            {
                SubjectId = "2",
                Username = "bob",
                Password = "password"
            }
        };
    }
}