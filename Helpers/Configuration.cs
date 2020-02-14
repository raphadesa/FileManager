
namespace FileManager.Helpers
{
    public class Configuration
    {        
        public static long buffferMaxConfig = 10 * 1024 * 1024; // 10MB
        public static long buffferMax => (long)(buffferMaxConfig * 0.7);
        public string urlBase { get; set; }
    }
}
