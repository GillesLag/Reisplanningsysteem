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
    public class ThemaBeherenViewModelTest
    {
        ThemaBeherenViewModel viewModel = new ThemaBeherenViewModel();

        [Test]
        public void Toevoegen_SetsFoutmeldingNaam()
        {
            //Arrange
            Thema thema;

            //Act
            thema = null;

            viewModel.Thema = thema;
            viewModel.Toevoegen();

            //Assert
            Assert.That(viewModel.Foutmelding, Is.EqualTo("Er is iets mis gelopen!"));
        }
    }
}
