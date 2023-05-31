using Reisplanningssysteem_Models;
using Reisplanningssysteem_WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reisplanningssysteem_UnitTests.ViewModels
{
    [TestFixture]
    public class LeeftijdsCategorieBeherenViewModelTest
    {
        LeeftijdsCategorieBeherenViewModel viewModel = new LeeftijdsCategorieBeherenViewModel();

        [Test]
        public void Toevoegen_SetsFoutmeldingLeeftijdenNietGelijk()
        {
            //Arrange
            LeeftijdsCategorie leeftijdsCategorie;

            //Act
            leeftijdsCategorie = new LeeftijdsCategorie
            {
                Naam = "Jongeren",
                LeeftijdMinimum=12,
                LeeftijdMaximum=12,
            };

            viewModel.Categorie = leeftijdsCategorie;
            viewModel.Toevoegen();

            //Assert
            Assert.True(viewModel.Foutmelding.Contains("Minimum en maximum leeftijd moet verschillend zijn."));
        }
    }
}
