namespace WebAPImongoClient.Utils
{
    public interface IDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string ClientCollectionName { get; set; }
        string AddressCollectionName { get; set; }
    }
}
