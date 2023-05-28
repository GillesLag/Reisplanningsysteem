using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Reisplanningssysteem_DAL.Data.Repositories
{
    public class Repository<T> : IRepository<T>
        where T : class, new()
    {
        protected DbContext Context { get; }

        public Repository(DbContext context)
        {
            this.Context = context;
        }

        public IEnumerable<T> Ophalen()
        {
            return Context.Set<T>().ToList();
        }

        public int Toevoegen(T entity)
        {
            Context.Set<T>().Add(entity);
            return Context.SaveChanges();
        }

        public int Bewerken(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            return Context.SaveChanges();
        }

        public int Verwijderen(T entity)
        {
            Context.Entry(entity).State = EntityState.Deleted;
            return Context.SaveChanges();
        }

        //uitbreiding

        public IEnumerable<T> Ophalen(Expression<Func<T, bool>> voorwaarden,
           params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = Context.Set<T>();
            if (includes != null)
            {
                foreach (var item in includes)
                {
                    query = query.Include(item);
                }
            }
            if (voorwaarden != null)
            {
                query = query.Where(voorwaarden);
            }
            return query.ToList();
        }

        public IEnumerable<T> Ophalen(Expression<Func<T, bool>> voorwaarden)
        {
            return Ophalen(voorwaarden, null).ToList();
        }

        public IEnumerable<T> Ophalen(params Expression<Func<T, object>>[] includes)
        {
            return Ophalen(null, includes).ToList();
        }

        //handige functies
        public T ZoekOpPK<TPrimaryKey>(TPrimaryKey id)
        {
            return Context.Set<T>().Find(id);
        }

        public int ToevoegenOfBewerken(T entity)
        {
            // Wanneer de PK = 0, zal er worden toegevoegd
            // Wanneer de PK > 0, zal er worden geupdatet
            Context.Set<T>().Update(entity);
            return Context.SaveChanges();
        }

        public int ToevoegenRange(IEnumerable<T> entities)
        {
            Context.Set<T>().AddRange(entities);
            return Context.SaveChanges();
        }

        public int Verwijderen<TPrimaryKey>(TPrimaryKey id)
        {
            var entity = ZoekOpPK(id);
            Context.Set<T>().Remove(entity);
            return Context.SaveChanges();
        }

        public int VerwijderenRange(IEnumerable<T> entities)
        {
            Context.Set<T>().RemoveRange(entities);
            return Context.SaveChanges();
        }
    }
}
