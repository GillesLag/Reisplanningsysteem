using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using Reisplanningssysteem_DAL.Data.UnitOfWork;
using System.Collections.ObjectModel;
using Reisplanningssysteem_Models;

namespace Reisplanningssysteem_UnitTests.Data.UnitOfWork
{
    [TestFixture]
    public class UnitOfWorkRepoTests
    {
        private IUnitOfWork _unitOfWork = A.Fake<IUnitOfWork>();

        [Test]
        public void Ophalen_ReturnsObservableCollectionOfTypeGebruiker()
        {
            //Arrange
            ObservableCollection<Gebruiker> gebruikers;

            //Act
            gebruikers = new ObservableCollection<Gebruiker>(_unitOfWork.GebruikerRepo.Ophalen(g => g.Gemeente));

            //Assert
            Assert.NotNull(gebruikers);
            Assert.IsInstanceOf<ObservableCollection<Gebruiker>>(gebruikers);
        }

        [Test]
        public void Ophalen_ReturnsObservableCollectionOfTypeReis()
        {
            //Arrange
            ObservableCollection<Reis> reizen;

            //Act
            reizen = new ObservableCollection<Reis>(_unitOfWork.ReisRepo.Ophalen(r => r.Onkosten,
                r => r.Bestemming,
                r => r.Thema,
                r => r.Boekingen,
                r => r.LeeftijdsCategorie));

            //Assert
            Assert.NotNull(reizen);
            Assert.IsInstanceOf<ObservableCollection<Reis>>(reizen);
        }

        [Test]
        public void Ophalen_ReturnsObservableCollectionOfTypeGemeente()
        {
            //Arrange
            ObservableCollection<Gemeente> gemeentes;

            //Act
            gemeentes = new ObservableCollection<Gemeente>(_unitOfWork.GemeenteRepo.Ophalen());

            //Assert
            Assert.NotNull(gemeentes);
            Assert.IsInstanceOf<ObservableCollection<Gemeente>>(gemeentes);
        }
        [Test]
        public void Ophalen_ReturnsObservableCollectionOfTypeThema()
        {
            //Arrange
            ObservableCollection<Thema> themas;

            //Act
            themas = new ObservableCollection<Thema>(_unitOfWork.ThemaRepo.Ophalen());

            //Assert
            Assert.NotNull(themas);
            Assert.IsInstanceOf<ObservableCollection<Thema>>(themas);
        }

        [Test]
        public void Ophalen_ReturnsObservableCollectionOfTypeLeeftijdsCategorie()
        {
            //Arrange
            ObservableCollection<LeeftijdsCategorie> leeftijdscategorieen;

            //Act
            leeftijdscategorieen = new ObservableCollection<LeeftijdsCategorie>(_unitOfWork.LeeftijdsCategorieRepo.Ophalen());

            //Assert
            Assert.NotNull(leeftijdscategorieen);
            Assert.IsInstanceOf<ObservableCollection<LeeftijdsCategorie>>(leeftijdscategorieen);
        }

        [Test]
        public void Ophalen_ReturnsObservableCollectionOfCursus()
        {
            //Arrange
            ObservableCollection<Cursus> cursussen;

            //Act
            cursussen = new ObservableCollection<Cursus>(_unitOfWork.CursusRepo.Ophalen());

            //Assert
            Assert.NotNull(cursussen);
            Assert.IsInstanceOf<ObservableCollection<Cursus>>(cursussen);
        }
    }
}
