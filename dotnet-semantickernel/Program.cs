using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

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

if (!string.IsNullOrEmpty(githubToken))
{
    // Create a Semantic Kernel builder
    var kernelBuilder = Kernel.CreateBuilder()
        .AddOpenAIChatCompletion(
            modelId: "openai/gpt-4o", 
            apiKey: githubToken, 
            endpoint: new Uri ("https://models.github.ai/inference")
        );

    // Build the Semantic Kernel instance
    var kernel = kernelBuilder.Build();

    Console.WriteLine("🤖 Semantic Kernel instance built successfully.");

    // Get the chat completion service
    var chatCompletion = kernel.GetRequiredService<IChatCompletionService>();

    // Create a ChatHistory object
    var chatHistory = new ChatHistory();

    // Add a user message
    chatHistory.AddUserMessage("What is 10 + (25 * 4)?");

    // Get a response from the AI
    var response = await chatCompletion.GetChatMessageContentAsync(chatHistory);

    // Print the result
    Console.WriteLine("🤖 AI Response: " + response.Content);
}
