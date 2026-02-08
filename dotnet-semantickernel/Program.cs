using Microsoft.Extensions.Configuration;

Console.WriteLine("Welcome to the Semantic Kernel AI Agent!");

// Build configuration
var configuration = new ConfigurationBuilder()
    .AddEnvironmentVariables()
    .AddUserSecrets<Program>()
    .Build();

// Retrieve GitHub token from configuration
string? githubToken = configuration["GITHUB_TOKEN"];

if (string.IsNullOrEmpty(githubToken))
{
    Console.WriteLine("❌ Error: GITHUB_TOKEN not found. Please set it as an environment variable or in user secrets.");
}
else
{
    Console.WriteLine("✅ GITHUB_TOKEN loaded successfully!");
}
