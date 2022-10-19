using MongoDB.Driver;
using System.Collections.Generic;
using WebAPImongoClient.Models;
using WebAPImongoClient.Utils;

namespace WebAPImongoClient.Services
{
    public class AddressService
    {
        private readonly IMongoCollection<Address> _address;

        public AddressService(IDatabaseSettings settings)
        {
            var address = new MongoClient(settings.ConnectionString);
            var database = address.GetDatabase(settings.DatabaseName);
            _address = database.GetCollection<Address>(settings.AddressCollectionName);
        }

        public Address Create (Address address)
        {
            _address.InsertOne(address);
            return address;
        }
        public List<Address> Get() => _address.Find(x => true).ToList();
        public Address Get(string id) => _address.Find(adress => adress.Id == id).FirstOrDefault();
        public void Update(string id, Address adressIn) => _address.ReplaceOne(address => adressIn.Id == id, adressIn);
        public void Remove(Address adressIn) => _address.DeleteOne(adress => adress.Id == adressIn.Id);
    }
}
