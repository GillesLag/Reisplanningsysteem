using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reisplanningssysteem_DAL.Data.Repositories;
using Reisplanningssysteem_Models;

namespace Reisplanningssysteem_DAL.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private IRepository<Bestemming> _bestellingRepo;
        private IRepository<Boeking> _boekingRepo;
        private IRepository<Cursus> _cursusRepo;
        private IRepository<Gebruiker> _gebruikerRepo;
        private IRepository<GebruikerCursus> _gebruikerCursuRepo;
        private IRepository<Gemeente> _gemeenteRepo;
        private IRepository<LeeftijdsCategorie> _leeftijdsCategorieRepo;
        private IRepository<Onkost> _onkostRepo;
        private IRepository<Reis> _reisRepo;
        private IRepository<Thema> _themaRepo;
        private ReisplanningssysteemContext Context { get; }

        #region repositories
        public IRepository<Bestemming> BestemmingRepo
        {
            get
            {
                if (_bestellingRepo == null)
                {
                    _bestellingRepo = new Repository<Bestemming>(Context);
                }
                return _bestellingRepo;
            }
        }

        public IRepository<Boeking> BoekingRepo
        {
            get
            {
                if (_boekingRepo == null)
                {
                    _boekingRepo = new Repository<Boeking>(Context);
                }
                return _boekingRepo;
            }
        }

        public IRepository<Cursus> CursusRepo
        {
            get
            {
                if (_cursusRepo == null)
                {
                    _cursusRepo = new Repository<Cursus>(Context);
                }
                return _cursusRepo;
            }
        }

        public IRepository<Gebruiker> GebruikerRepo
        {
            get
            {
                if (_gebruikerRepo == null)
                {
                    _gebruikerRepo = new Repository<Gebruiker>(Context);
                }
                return _gebruikerRepo;
            }
        }

        public IRepository<GebruikerCursus> GebruikersCursusRepo
        {
            get
            {
                if (_gebruikerCursuRepo == null)
                {
                    _gebruikerCursuRepo = new Repository<GebruikerCursus>(Context);
                }
                return _gebruikerCursuRepo;
            }
        }

        public IRepository<Gemeente> GemeenteRepo
        {
            get
            {
                if (_gemeenteRepo == null)
                {
                    _gemeenteRepo = new Repository<Gemeente>(Context);
                }
                return _gemeenteRepo;
            }
        }

        public IRepository<LeeftijdsCategorie> LeeftijdsCategorieRepo
        {
            get
            {
                if (_leeftijdsCategorieRepo == null)
                {
                    _leeftijdsCategorieRepo = new Repository<LeeftijdsCategorie>(Context);
                }
                return _leeftijdsCategorieRepo;
            }
        }

        public IRepository<Onkost> OnkostRepo
        {
            get
            {
                if (_onkostRepo == null)
                {
                    _onkostRepo = new Repository<Onkost>(Context);
                }
                return _onkostRepo;
            }
        }

        public IRepository<Reis> ReisRepo
        {
            get
            {
                if (_reisRepo == null)
                {
                    _reisRepo = new Repository<Reis>(Context);
                }
                return _reisRepo;
            }
        }

        public IRepository<Thema> ThemaRepo
        {
            get
            {
                if (_themaRepo == null)
                {
                    _themaRepo = new Repository<Thema>(Context);
                }
                return _themaRepo;
            }
        }
        #endregion

        public UnitOfWork(ReisplanningssysteemContext ctx)
        {
            Context = ctx;
        }
        public void Dispose()
        {
            Context.Dispose();
        }

        public int Save()
        {
            return Context.SaveChanges();
        }
    }
}
