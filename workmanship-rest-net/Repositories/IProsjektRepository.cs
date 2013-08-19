using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using workmanship_rest_net.Models;

namespace workmanship_rest_net.Repositories
{
    public interface IProsjektRepository : IRepository<Prosjekt>
    {
        IEnumerable<Prosjekt> GetProsjekterForBruker(int brukerId);
    }
}
