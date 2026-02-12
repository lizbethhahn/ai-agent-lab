using System.ComponentModel;
using Microsoft.SemanticKernel;

namespace SemanticKernelPlugin
{
    public class WeatherPlugin
    {
        [KernelFunction, Description("Returns a mock weather report for the specified date.")]
        public string GetWeatherForDate([Description("The date to get the weather for.")] string date)
        {
           string dt = date; 
           string[] dateOnly = dt.Split(' ');
           string today = DateTime.Now.ToString("yyyy-MM-dd");
           if (today == dateOnly[0])
            {
                return $"Sunny, 72°F";
            }
            else
            {
                return $"Rainy, 55°F";
            }
        }
    }
}