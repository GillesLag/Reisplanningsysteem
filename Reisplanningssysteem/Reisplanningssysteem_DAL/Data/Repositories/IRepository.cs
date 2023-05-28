using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Reisplanningssysteem_DAL.Data.Repositories
{
    public interface IRepository<T>
        where T : class, new()
    {
        IEnumerable<T> Ophalen();
        int Toevoegen(T entity);
        int Bewerken(T entity);
        int Verwijderen(T entity);
        IEnumerable<T> Ophalen(Expression<Func<T, bool>> voorwaarden);
        IEnumerable<T> Ophalen(params Expression<Func<T, object>>[] includes);
        IEnumerable<T> Ophalen(Expression<Func<T, bool>> voorwaarden,
            params Expression<Func<T, object>>[] includes);
        T ZoekOpPK<TPrimaryKey>(TPrimaryKey id);
        int ToevoegenOfBewerken(T entity);
        int ToevoegenRange(IEnumerable<T> entities);
        int Verwijderen<TPrimaryKey>(TPrimaryKey id);
        int VerwijderenRange(IEnumerable<T> entities);
    }
}
