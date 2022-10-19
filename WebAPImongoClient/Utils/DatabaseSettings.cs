namespace WebAPImongoClient.Utils
{
    public class DatabaseSettings:IDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string ClientCollectionName { get; set; }    
        public string AddressCollectionName { get; set; }
    }
}
