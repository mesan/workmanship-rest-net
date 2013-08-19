using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using workmanship_rest_net.Models;

namespace workmanship_rest_net.Repositories
{
    public interface IBrukerRepository : IRepository<Bruker>
    {
        IEnumerable<Bruker> GetBrukereForProsjekt(int prosjektId);
    }
}
