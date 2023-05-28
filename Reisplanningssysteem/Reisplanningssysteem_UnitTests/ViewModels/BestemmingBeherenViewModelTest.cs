using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reisplanningssysteem_WPF.ViewModels;
using Reisplanningssysteem_Models;
using FakeItEasy;

namespace Reisplanningssysteem_UnitTests.ViewModels
{
    [TestFixture]
    public class BestemmingBeherenViewModelTest
    {
        BestemmingBeherenViewModel viewModel = new BestemmingBeherenViewModel();

        [Test]
        public void Toevoegen_SetsFoutmeldingToEmptyString()
        {
            //Arrange
            Bestemming bestemming;

            //Act
            bestemming = new Bestemming()
            {
                Naam = "test",
                Land = "België",
                Straat = "TestStraat",
                Huisnummer = "12b",
            };

            viewModel.Bestemming = bestemming;
            viewModel.Toevoegen();

            //Assert
            Assert.IsNotEmpty(viewModel.Foutmelding);
        }
    }
}
