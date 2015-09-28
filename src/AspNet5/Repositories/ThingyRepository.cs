namespace AspNet5.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AspNet5.Models;
    using System.Linq;

    public class ThingyRepository : IRepository<Thingy>
    {
        [Cache(minutes: 20)]
        public Task<IEnumerable<Thingy>> GetAll()
        {
            var thingies = new List<Thingy> { new Thingy { Name = "A Normal Thingy" } };

            return Task.FromResult(thingies.AsEnumerable());
        }
    }
}
