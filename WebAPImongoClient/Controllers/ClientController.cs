using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver.Core.Operations;
using System.Collections.Generic;
using WebAPImongoClient.Models;
using WebAPImongoClient.Services;

namespace WebAPImongoClient.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly ClientService _clientService;
        public ClientController(ClientService clientService)
        {
            _clientService = clientService;
        }
        [HttpPost]
        public ActionResult<Client> Create(Client client)
        {
            _clientService.Create(client);
            return CreatedAtRoute("GetClient",new {id=client.Id.ToString()},client);
        }
        [HttpGet]
        public ActionResult<List<Client>> Get() => _clientService.Get();
        [HttpGet("{id:length(24)}", Name = "GetClient")]
        public ActionResult<Client> Get(string id)
        {
            var client = _clientService.Get(id);
            if (client == null)
                return NotFound(); //404
            return Ok(client);
        }
        [HttpPut]
        public ActionResult<Client> Update(string id, Client clientIn)
        {
            var client = _clientService.Get(id);
            if (client == null) return NotFound();
            clientIn.Id = id;
            _clientService.Update(id, clientIn);
            return NoContent();//retorna só uma mensagem,não o objeto
        }
        [HttpDelete]
        public ActionResult Delete(string id)
        {
            var client = _clientService.Get(id);
            if (client == null)
                return NotFound();
            _clientService.Remove(client);
            return NoContent();
        }

    }
}
