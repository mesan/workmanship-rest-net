using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace workmanship_rest_net.Repositories
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAlle();
        T Get(int id);
        bool LeggTil(T entity);
        bool Oppdater(T entity);
        bool Slett(T entity);
    }
}
