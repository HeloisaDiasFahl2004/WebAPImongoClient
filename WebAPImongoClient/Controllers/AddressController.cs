using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebAPImongoClient.Models;
using WebAPImongoClient.Services;

namespace WebAPImongoClient.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly AddressService _addressService;

        public AddressController(AddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpGet]
        public ActionResult<List<Address>> Get() => _addressService.Get();

        [HttpGet("{id:length(24)}", Name = "GetAddress")]
        public ActionResult<Address> Get(string id)
        {
            var address = _addressService.Get(id);
            if (address == null)
            {
                return NotFound();
            }
            return Ok(address);


        }

        [HttpPost]
        public ActionResult<Client> Create(Address address)
        {
            _addressService.Create(address);
            return CreatedAtRoute("GetAddress", new { id = address.Id.ToString() }, address);
        }

        [HttpPut]
        public ActionResult<Client> Put(Address addressIn, string id)
        {
            var address = _addressService.Get(id);
            if (address == null)
            {
                return NotFound("Não encontrado");
            }
            _addressService.Update(address.Id, addressIn);
            return NoContent();
        }

        [HttpDelete]
        public ActionResult<Client> Delete(string id)
        {
            Address address = _addressService.Get(id);
            if (address == null)
            {
                return NotFound();
            }
            _addressService.Remove(address);

            return NoContent();
        }
    }
}
