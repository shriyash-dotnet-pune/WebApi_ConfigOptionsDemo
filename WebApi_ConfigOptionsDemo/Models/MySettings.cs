namespace WebApi_ConfigOptionsDemo.Models
{
    public class MySettings
    {
        public string ApplicationName { get; set; } = string.Empty;
        public string Version { get; set; } = string.Empty;
        public int MaxItems { get; set; }
    }
}
