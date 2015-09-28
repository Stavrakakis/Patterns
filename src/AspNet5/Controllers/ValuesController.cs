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
        private readonly IProductRepository productRepository;

        private readonly IRepository<Thingy> thingyRepository;

        public ValuesController(IProductRepository productRepository, IRepository<Thingy> thingyRepository)
        {
            if (productRepository == null)
            {
                throw new ArgumentNullException("productRepository");
            }

            if (thingyRepository == null)
            {
                throw new ArgumentNullException("thingyRepository");
            }

            this.productRepository = productRepository;
            this.thingyRepository = thingyRepository;
        }

        // GET: api/values
        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            var thingies = await this.thingyRepository.GetAll();

            var products = await this.productRepository.GetAll();

            return products;
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
