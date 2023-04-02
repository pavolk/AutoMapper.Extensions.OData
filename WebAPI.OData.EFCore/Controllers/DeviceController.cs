using AutoMapper;
using AutoMapper.AspNet.OData;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.OData.EFCore.Controllers
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("api/devices")]
    [ApiController]
    public class DeviceController : ODataController
    {
        List<Device> _devices = new List<Device>();

        IMapper _mapper;

        public DeviceController(IMapper mapper) 
        {
            _mapper = mapper;

            _devices.Add(new Device() { Id = "1", Modem = new Modem() { Name = "Modem-1" } });
            _devices.Add(new Device() { Id = "2" });

        }

        // GET: api/<DeviceController>
        [HttpGet]
        public async Task<IActionResult> Get(ODataQueryOptions<DeviceDto> options)
        {
            return Ok(await _devices.AsQueryable<Device>().GetQueryAsync(_mapper, options,
                            new QuerySettings { 
                                ODataSettings = new ODataSettings { HandleNullPropagation = HandleNullPropagationOption.False } }));

            // return Ok(_devices);
        }

        // GET api/<DeviceController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<DeviceController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<DeviceController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DeviceController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
