var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentityServer()
    .AddInMemoryClients(IdentityServerConfig.GetClients())
    .AddInMemoryApiScopes(IdentityServerConfig.GetApiScopes())
    .AddInMemoryApiResources(IdentityServerConfig.GetApiResources())
    .AddInMemoryIdentityResources(IdentityServerConfig.GetIdentityResources())
    .AddTestUsers(IdentityServerConfig.GetTestUsers())
    .AddDeveloperSigningCredential();

var app = builder.Build();

app.UseIdentityServer();

app.Run();
