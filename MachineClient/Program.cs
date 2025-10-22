using System.Text;
using System.Text.Json;

Console.WriteLine("=== Machine-to-Machine Client ===");
Console.WriteLine("Testing Client Credentials Flow...\n");

// Create HTTP client
using var client = new HttpClient();

// Prepare token request
var tokenRequest = new Dictionary<string, string>
{
    ["grant_type"] = "client_credentials",
    ["client_id"] = "machine-client",
    ["client_secret"] = "machine-secret",
    ["scope"] = "api12"
};

var requestContent = new FormUrlEncodedContent(tokenRequest);

try
{
    // Request access token
    Console.WriteLine("Requesting access token from IdentityServer...");
    var response = await client.PostAsync("http://localhost:5295/connect/token", requestContent);
    
    if (response.IsSuccessStatusCode)
    {
        var json = await response.Content.ReadAsStringAsync();
        var tokenResponse = JsonSerializer.Deserialize<JsonElement>(json);
        
        var accessToken = tokenResponse.GetProperty("access_token").GetString();
        var tokenType = tokenResponse.GetProperty("token_type").GetString();
        var expiresIn = tokenResponse.GetProperty("expires_in").GetInt32();
        
        Console.WriteLine("✅ SUCCESS! Received access token:");
        Console.WriteLine($"Token Type: {tokenType}");
        Console.WriteLine($"Expires In: {expiresIn} seconds");
        Console.WriteLine($"Access Token: {accessToken?[..50]}...\n");
        
        Console.WriteLine("🎉 Machine-to-machine authentication successful!");
    }
    else
    {
        Console.WriteLine($"❌ ERROR: {response.StatusCode}");
        Console.WriteLine(await response.Content.ReadAsStringAsync());
    }
}
catch (Exception ex)
{
    Console.WriteLine($"❌ EXCEPTION: {ex.Message}");
}

Console.WriteLine("\nPress any key to exit...");
Console.ReadKey();
