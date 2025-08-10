namespace SmartCarePatientPortal.Models
{
    internal class Constants
    {
        public const string OpenAI_API = "https://api.openai.com/v1/chat/completions";
        public const string OpenAI_Key = "sk-api-xxx";
    }

    public class OpenAISettings
    {
        public string ApiKey { get; set; }
    }

}
