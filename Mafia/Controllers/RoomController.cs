using Microsoft.AspNetCore.Mvc;
using Mafia.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mafia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {

        [Route("CreateRoom")]
        [HttpPost]
        public void CreateRoom(RoomCreatingParams roomParams)
        {

        }


        //// GET: api/<RoomController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<RoomController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}


        //[Route("CreateB/{id}/{name}")]
        //[HttpGet]
        //public string CreateB(int id, string name)
        //{
        //    return "value";
        //}

        //// POST api/<RoomController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //[Route("CreateA")]
        //[HttpPost]
        //public void CreateA([FromBody] string value)
        //{

        //}

        //// PUT api/<RoomController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<RoomController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
