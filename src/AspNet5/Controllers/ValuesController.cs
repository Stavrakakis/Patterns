namespace AspNet5.Controllers
{
    using System.Collections.Generic;
    using Microsoft.AspNet.Mvc;
    using AspNet5.Services;
    using System;
    using Repositories;
    using Models;
    using System.Threading.Tasks;

    [Route("api/values")]
    public class ValuesController : Controller
    {
        private readonly IRepository<Thingy> thingyRepository;

        public ValuesController(IRepository<Thingy> thingyRepository)
        {
            if (thingyRepository == null)
            {
                throw new ArgumentNullException("thingyRepository");
            }
            
            this.thingyRepository = thingyRepository;
        }

        // GET: api/values
        [HttpGet]
        public async Task<IEnumerable<Thingy>> Get()
        {
            return await this.thingyRepository.GetAll();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
