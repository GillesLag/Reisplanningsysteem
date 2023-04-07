using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Reisplanningssysteem_Models;

namespace Reisplanningssysteem_DAL
{
    public static class DatabaseOperations
    {
        public static List<Bestemming> BestemmingenOphalen()
        {
            using (ReisplanningssysteemContext ctx = new())
            {
                return ctx.Bestemmingen.Include("Gemeente").ToList();
            }
        }

        public static int BestemmingVerwijderen(Bestemming bestemming)
        {
            try
            {
                using (ReisplanningssysteemContext ctx = new())
                {
                    ctx.Entry(bestemming).State = EntityState.Deleted;
                    return ctx.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static int BestemmingToevoegen(Bestemming bestemming)
        {
            try
            {
                using (ReisplanningssysteemContext ctx = new())
                {
                    ctx.Entry(bestemming).State = EntityState.Added;
                    return ctx.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static int BestemmingBewerken(Bestemming bestemming)
        {
            try
            {
                using(ReisplanningssysteemContext ctx = new())
                {
                    ctx.Entry(bestemming).State = EntityState.Modified;
                    return ctx.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static List<Gemeente> GemeentenOphalen()
        {
            using (ReisplanningssysteemContext ctx = new())
            {
                return ctx.Gemeenten.ToList();
            }
        }
    }
}
