using Microsoft.EntityFrameworkCore;
using Reisplanningssysteem_Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reisplanningssysteem_Models;

namespace Reisplanningssysteem_DAL
{
    public class DatabaseOperations
    {
        public static List<Gebruiker> OphalenLijstGebruikers()
        {
            using (ReisplanningssysteemContext ctx = new ReisplanningssysteemContext())
            {
                return ctx.Gebruikers.Include(x => x.Gemeente).Include(x=> x.GebruikerCursussen).Include(x => x.Boekingen).OrderBy(g => g.Voornaam).ToList();
            }
        }

        public static List<Gebruiker> OphalenGefilterdeLijstGebruikers(string input)
        {
            using (ReisplanningssysteemContext ctx = new ReisplanningssysteemContext())
            {
                return ctx.Gebruikers
                    .Where(g => g.ToString().Contains(input))
                    .Include(x => x.Gemeente).ToList();
            }
        }

        public static List<Gemeente> OphalenGemeentes()
        {
            using (ReisplanningssysteemContext ctx = new ReisplanningssysteemContext())
            {
                return ctx.Gemeenten.OrderBy(g => g.Naam).ToList();
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

        public static int OnkostVerwijderen(Onkost geselecteerdeOnkost)
        {
            try
            {
                using (ReisplanningssysteemContext ctx = new())
                {
                    ctx.Entry(geselecteerdeOnkost).State = EntityState.Deleted;
                    return ctx.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static int OnkostBewerken(Onkost onkost)
        {
            try
            {
                using (ReisplanningssysteemContext ctx = new())
                {
                    ctx.Entry(onkost).State = EntityState.Modified;
                    return ctx.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static int ThemaToevoegen(Thema thema)
        {
            try
            {
                using (ReisplanningssysteemContext ctx = new())
                {
                    ctx.Entry(thema).State = EntityState.Added;
                    return ctx.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static int OnkostToevoegen(Onkost onkost)
        {
            try
            {
                using (ReisplanningssysteemContext ctx = new())
                {
                    ctx.Entry(onkost).State = EntityState.Added;
                    return ctx.SaveChanges();
                }
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

        public static GebruikerCursus ZoekGebruikerCursus(Gebruiker gebruiker, Cursus cursus)
        {
            using (ReisplanningssysteemContext ctx = new ReisplanningssysteemContext())
            {
                return ctx.GebruikersCursusen .Where(x => x.Gebruiker == gebruiker && x.Cursus == cursus).First();
            }
        }

        public static Boeking ZoekBoeking(Gebruiker gebruiker, Reis reis)
        {
            using (ReisplanningssysteemContext ctx = new ReisplanningssysteemContext())
            {
                return ctx.Boekingen.Where(x => x.Gebruiker == gebruiker && x.Reis == reis).First();
            }
        }

        public static int ThemaBewerken(Thema thema)
        {
            try
            {
                using (ReisplanningssysteemContext ctx = new())
                {
                    ctx.Entry(thema).State = EntityState.Modified;
                    return ctx.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static List<Bestemming> BestemmingenOphalen()
        {
            using (ReisplanningssysteemContext ctx = new())
            {
                return ctx.Bestemmingen.Include("Gemeente").OrderBy(b => b.Naam).ToList();
            }
        }

        public static int ThemaVerwijderen(Thema geselecteerdeThema)
        {
            try
            {
                using (ReisplanningssysteemContext ctx = new())
                {
                    ctx.Entry(geselecteerdeThema).State = EntityState.Deleted;
                    return ctx.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
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

        public static List<Gebruiker> HoofdmonitorenOphalen()
        {
            using (ReisplanningssysteemContext ctx = new ReisplanningssysteemContext())
            {
                return ctx.Gebruikers.
                    Where(g => g.HoofmonitorCursus).
                    Include(x => x.Gemeente).ToList();
            }
        }

        public static List<Thema> ThemasOphalen()
        {
            using (ReisplanningssysteemContext ctx = new ReisplanningssysteemContext())
            {
                return ctx.Themas.OrderBy(t => t.Naam).ToList();
            }
        }

        public static List<LeeftijdsCategorie> LeeftijdsCategorieŽnOphalen()
        {
            using (ReisplanningssysteemContext ctx = new())
            {
                return ctx.LeeftijdsCategorieŽn.OrderBy(l => l.Naam).ToList();
            }
        }

        public static int LeeftijdsCategorieToevoegen(LeeftijdsCategorie leeftijdsCategorie)
        {
            try
            {
                using (ReisplanningssysteemContext ctx = new())
                {
                    ctx.LeeftijdsCategorieŽn.Entry(leeftijdsCategorie).State = EntityState.Added;
                    return ctx.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static int GebruikerLinken(GebruikerCursus gebruikerCursus)
        {
            try
            {
                using (ReisplanningssysteemContext ctx = new())
                {
                    ctx.GebruikersCursusen.Entry(gebruikerCursus).State = EntityState.Added;
                    return ctx.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static int BoekingAanmaken(Boeking boeking)
        {
            try
            {
                using (ReisplanningssysteemContext ctx = new())
                {
                    ctx.Boekingen.Entry(boeking).State = EntityState.Added;
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
                    ctx.LeeftijdsCategorieŽn.Entry(leeftijdsCategorie).State = EntityState.Deleted;
                    return ctx.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static int GebruikercursusVerwijderen(GebruikerCursus gebruikerCursus)
        {
            try
            {
                using (ReisplanningssysteemContext ctx = new())
                {
                    ctx.GebruikersCursusen.Entry(gebruikerCursus).State = EntityState.Deleted;
                    return ctx.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static int BoekingVerwijderen(Boeking boeking)
        {
            try
            {
                using (ReisplanningssysteemContext ctx = new())
                {
                    ctx.Boekingen.Entry(boeking).State = EntityState.Deleted;
                    return ctx.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static int BoekingBewerken(Boeking boeking)
        {
            try
            {
                using (ReisplanningssysteemContext ctx = new())
                {
                    ctx.Boekingen.Entry(boeking).State = EntityState.Modified;
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
                    ctx.LeeftijdsCategorieŽn.Entry(leeftijdsCategorie).State = EntityState.Modified;
                    return ctx.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static List<Cursus> CursussenOphalen()
        {
            using (ReisplanningssysteemContext ctx = new())
            {
                return ctx.Cursussen.OrderBy(c => c.Naam).ToList();
            }
        }

        public static List<Reis> ReizenOphalen()
        {
            using (ReisplanningssysteemContext ctx = new())
            {
                return ctx.Reizen.
                    Include(r => r.Bestemming).
                    Include(r => r.Hoofdmonitor).
                    Include(r => r.Thema).
                    Include(r => r.LeeftijdsCategorie).
                    Include(r => r.Boekingen).
                    ThenInclude(b => b.Gebruiker).
                    Include(r => r.Onkosten).
                    OrderBy(r => r.Naam).
                    ToList();
            }
        }

        public static int ReisToevoegen(Reis reis)
        {
            try
            {
                using (ReisplanningssysteemContext ctx = new())
                {
                    ctx.Reizen.Entry(reis).State = EntityState.Added;
                    return ctx.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static int CursusToevoegen(Cursus cursus)
        {
            try
            {
                using (ReisplanningssysteemContext ctx = new())
                {
                    ctx.Cursussen.Entry(cursus).State = EntityState.Added;
                    return ctx.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static int ReisVerwijderen(Reis reis)
        {
            try
            {
                using (ReisplanningssysteemContext ctx = new())
                {
                    ctx.Reizen.Entry(reis).State = EntityState.Deleted;
                    return ctx.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static int CursusVerwijderen(Cursus cursus)
        {
            try
            {
                using (ReisplanningssysteemContext ctx = new())
                {
                    ctx.Cursussen.Entry(cursus).State = EntityState.Deleted;
                    return ctx.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static int ReisBewerken(Reis reis)
        {
            try
            {
                using (ReisplanningssysteemContext ctx = new())
                {
                    ctx.Reizen.Entry(reis).State = EntityState.Modified;
                    return ctx.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static int CursusBewerken(Cursus cursus)
        {
            try
            {
                using (ReisplanningssysteemContext ctx = new())
                {
                    ctx.Cursussen.Entry(cursus).State = EntityState.Modified;
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
