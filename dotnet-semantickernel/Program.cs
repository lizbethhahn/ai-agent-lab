using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using SemanticKernelPlugin;

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
    // DO NOT MODIFY.
    // Required to avoid wrong overload and OpenAI default endpoint.
    var kernelBuilder = Kernel.CreateBuilder()
        .AddOpenAIChatCompletion(
            modelId: "openai/gpt-4o", 
            apiKey: githubToken, 
            endpoint: new Uri ("https://models.github.ai/inference")
        );
    
    // Register the MathPlugin
    kernelBuilder.Plugins.AddFromType<MathPlugin>();

    // Register the StringPlugin
    kernelBuilder.Plugins.AddFromType<StringPlugin>();

    // Register the TimePlugin
    kernelBuilder.Plugins.AddFromType<TimePlugin>();

    // Register the WeatherPlugin
    kernelBuilder.Plugins.AddFromType<WeatherPlugin>();

    // Build the Semantic Kernel instance
    var kernel = kernelBuilder.Build();

    Console.WriteLine("🤖 Semantic Kernel instance built successfully.");

    // Define test queries
    string[] testQueries =
    {
        "What time is it right now?",
        "What is 25 * 4 + 10?",
        "Reverse the string 'Hello World'",
        "What is the weather like today?"
    };

    // Get the chat completion service
    var chatCompletion = kernel.GetRequiredService<IChatCompletionService>();

    try
    {
        // Loop through each query
        foreach (var query in testQueries)
        {
            try
            {
                Console.WriteLine("==============================");
                Console.WriteLine($"📝 Query: {query}");

                // Create a ChatHistory object
                var chatHistory = new ChatHistory();

                // Add a system message
                chatHistory.AddSystemMessage("Please respond professionally and succinctly.");

                // Add the query to chat history
                chatHistory.AddUserMessage(query);

                // Create execution settings
                var executionSettings = new OpenAIPromptExecutionSettings
                {
                    ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
                };

                // Get a response from the AI with execution settings
                var response = await chatCompletion.GetChatMessageContentAsync(chatHistory, executionSettings, kernel);

                // Print the result
                Console.WriteLine($"🤖 AI Response: {response}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Error processing query: " + query);
                Console.WriteLine("💡 Exception: " + ex.Message);
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("❌ An error occurred during the query loop.");
        Console.WriteLine("💡 Exception: " + ex.Message);
    }

    Console.WriteLine("==============================");
}
