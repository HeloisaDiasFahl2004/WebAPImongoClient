using WebAPImongoClient.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPImongoClient.Models;
using System.Collections.Generic;

namespace WebAPImongoClient.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly ClientService _clientService;
        private readonly AddressService _addressService;

        public ClientController(ClientService clientService, AddressService addressService)
        {
            _clientService = clientService;
            _addressService = addressService;
        }

        [HttpGet]
        public ActionResult<List<Client>> Get() => _clientService.Get();

        [HttpGet("{Id:length(24)}", Name = "GetClient")]
        public ActionResult<Client> Get(string id)
        {
            var client = _clientService.Get(id);
            if (client == null) return NotFound();

            return Ok(client);
        }
        //[HttpGet("{name}")]
        //public ActionResult<Client> GetName(string name)
        //{
        //    var client = _clientService.Get(name);
        //    if (client == null) return NotFound();
        //    var c = client.Where(x => x.Name == name).FirstOrDefault();
        //    return Ok(c);
        //}

        [HttpPost]
        public ActionResult<Client> Post(Client client)
        {
            Address address = _addressService.Create(client.Address);//pega o objeto address
            client.Address = address;//insere o address e já traz de volta
            _clientService.Create(client);
            return CreatedAtRoute("GetClient", new { id = client.Id.ToString() }, client);
        }

        [HttpPut]
        public ActionResult<Client> Put(Client clientIn, string id)
        {
            var client = _clientService.Get(id);
            if (client == null) return NotFound("Não encontrado");
            _clientService.Update(client.Id, clientIn);
            return NoContent();
        }

        [HttpDelete]
        public ActionResult<Client> Delete(string id)
        {
            Client client = _clientService.Get(id);
            if (client == null) return NotFound();
            _clientService.Remove(client);
            return NoContent();
        }
    }
}
