using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Controllers
{
    //http:localhosr:5000/api/values
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly DataContext _context;

        public ValuesController(DataContext context)
        {
            _context = context;
        }
        // GET api/values
        [HttpGet]
        // here the IEnumerable use to retrieve set of things. In this case, they are strings.
        // public ActionResult<IEnumerable<string>> Get()
        // {
            
        //     return new string[] { "value1", "value3" };
        // }
        //IActionResult returns an ok.

        //synchronous(lock this method until the first request is done)
        // public IActionResult GetValues()
        // {
        //    var values=_context.Values.ToList();
        //    return Ok(values);
        // }

        //asynchronous(will be able to handle lots of rquests at the same time)
         public async Task<IActionResult> GetValues()
        {
           var values =await _context.Values.ToListAsync();
           return Ok(values);
        }


        // GET api/values/5
        [HttpGet("{id}")]
        // public ActionResult<string> Get(int id)
        // {
        //     return "value";
        // }

        // here the x represents the value of _context.values.
        // here better to use FirstOrDefault method. because if there are no records, it'll return null rather than giving exceptions.
        // synchronous
        // public IActionResult GetValue(int id)
        // {
        //    var value=_context.Values.FirstOrDefault(x =>x.Id==id);
        //    return Ok(value);
        // }

        //asynchronous
        public async Task<IActionResult> GetValue(int id)
        {
           var value =await _context.Values.FirstOrDefaultAsync(x =>x.Id==id);
           return Ok(value);
        }
        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
