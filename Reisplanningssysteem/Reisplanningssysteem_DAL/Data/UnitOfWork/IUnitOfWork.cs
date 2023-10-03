using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reisplanningssysteem_DAL.Data.Repositories;
using Reisplanningssysteem_Models;

namespace Reisplanningssysteem_DAL.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Bestemming> BestemmingRepo { get; }
        IRepository<Boeking> BoekingRepo { get; }
        IRepository<Cursus> CursusRepo { get; }
        IRepository<Gebruiker> GebruikerRepo { get; }
        IRepository<GebruikerCursus> GebruikersCursusRepo { get; }
        IRepository<Gemeente> GemeenteRepo { get; }
        IRepository<LeeftijdsCategorie> LeeftijdsCategorieRepo { get; }
        IRepository<Onkost> OnkostRepo { get; }
        IRepository<Reis> ReisRepo { get; }
        IRepository<Thema> ThemaRepo { get; }

        Task<int> Save();
    }
}
