using Microsoft.EntityFrameworkCore;
using Reisplanningssysteem_Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Reisplanningssysteem_Models;

namespace Reisplanningssysteem_DAL
{
    public class DatabaseOperations
    {


        public static List<Gebruiker> OphalenLijstGebruikers()
        {
            using (ReisplanningssysteemContext ctx = new ReisplanningssysteemContext())
            {
                return ctx.Gebruikers.
                Include(x => x.Gemeente).ToList();
            }
        }

        public static List<Gemeente> OphalenGemeentes()
        {
            using (ReisplanningssysteemContext ctx = new ReisplanningssysteemContext())
            {
                return ctx.Gemeenten.ToList();
            }
        }


        public static int VerwijderGebruiker(Gebruiker gebruiker)
        {
            try
            {
                using ReisplanningssysteemContext ctx = new();
                ctx.Entry(gebruiker).State = EntityState.Deleted;
                return ctx.SaveChanges();
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static int VoegGebruikerToe(Gebruiker gebruikerRecord)
        {
            try
            {
                using ReisplanningssysteemContext ctx = new();
                ctx.Gebruikers.Add(gebruikerRecord);
                return ctx.SaveChanges();
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static int UpdateGebruiker(Gebruiker gebruiker)
        {
            try
            {
                using ReisplanningssysteemContext ctx = new();
                ctx.Entry(gebruiker).State = EntityState.Modified;
                return ctx.SaveChanges();
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static Gebruiker ZoekGebruikerOpId(int gebruikerId)
        {
            using (ReisplanningssysteemContext ctx = new ReisplanningssysteemContext())
            {
                return ctx.Gebruikers
                    .Where(x => x.Id == gebruikerId).First();
            }
        }

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
                using (ReisplanningssysteemContext ctx = new())
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

        public static List<LeeftijdsCategorie> LeeftijdsCategorieënOphalen()
        {
            using (ReisplanningssysteemContext ctx = new())
            {
                return ctx.LeeftijdsCategorieën.ToList();
            }
        }

        public static int LeeftijdsCategorieToevoegen(LeeftijdsCategorie leeftijdsCategorie)
        {
            try
            {
                using (ReisplanningssysteemContext ctx = new())
                {
                    ctx.LeeftijdsCategorieën.Entry(leeftijdsCategorie).State = EntityState.Added;
                    return ctx.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static int LeeftijdsCategorieVerwijderen(LeeftijdsCategorie leeftijdsCategorie)
        {
            try
            {
                using (ReisplanningssysteemContext ctx = new())
                {
                    ctx.LeeftijdsCategorieën.Entry(leeftijdsCategorie).State = EntityState.Deleted;
                    return ctx.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static int LeeftijdsCategorieBewerken(LeeftijdsCategorie leeftijdsCategorie)
        {
            try
            {
                using (ReisplanningssysteemContext ctx = new())
                {
                    ctx.LeeftijdsCategorieën.Entry(leeftijdsCategorie).State = EntityState.Modified;
                    return ctx.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
