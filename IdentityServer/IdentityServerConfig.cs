using Duende.IdentityServer.Models;

public static class IdentityServerConfig
{
    public static IEnumerable<Client> GetClients()
    {
        return new List<Client>
        {
            new Client
            {
                ClientId = "client",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },
                AllowedScopes = { "api1" }
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
            new IdentityResources.Profile()
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