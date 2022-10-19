﻿using Microsoft.AspNetCore.Http;
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
        private readonly AddressService _addressService;

        public ClientController(ClientService clientService,AddressService addressService)
        {
            _clientService = clientService;
            _addressService = addressService;
        }
        [HttpPost]
        public ActionResult<Client> Post(Client client)
        {
            Address address = _addressService.Create(client.Address);
            client.Address = address;   //insiro o endereço primeiro e já trago de volta

            _clientService.Create(client);//insiro o cliente com o endereço
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
        public ActionResult<Client> Put(string id, Client clientIn)
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
