namespace Project9Api
{
    public class SettingsReader
    {
        public SettingsReader(IConfiguration configuration)
        {
            SiteUrl = configuration["MainSettings:SiteUrl"];
        }

        public string SiteUrl { get; set; }


    }
}
