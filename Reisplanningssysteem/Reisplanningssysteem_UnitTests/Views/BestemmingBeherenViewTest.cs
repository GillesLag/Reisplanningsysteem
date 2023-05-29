using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.UIA3;

namespace Reisplanningssysteem_UnitTests.Views
{
    [TestFixture]
    public class BestemmingBeherenViewTest
    {
        [Test]
        public void BestemmingToevoegen()
        {
            var app = Application.Launch(@"C:\Users\Dre\OneDrive\Desktop\tm\agile\Reisplanningsysteem\Reisplanningssysteem\Reisplanningssysteem_WPF\bin\Debug\net6.0-windows\Reisplanningssysteem_WPF.exe");
            using (var automation = new UIA3Automation())
            {
                //Arrange
                var window = app.GetMainWindow(automation);
                var bestemmingButton = window.FindFirstDescendant(x => x.ByAutomationId("Bestemmingen")).AsButton();
                bestemmingButton.Invoke();

                System.Threading.Thread.Sleep(5000);

                var secondWindow = app.GetAllTopLevelWindows(automation).First(x => x.Name == "BestemmingenView");

                var datagrid = secondWindow.FindFirstDescendant(x => x.ByAutomationId("Datagrid")).AsDataGridView();
                int count = datagrid.Rows.Count();
                var toevoegenButton = secondWindow.FindFirstDescendant(x => x.ByAutomationId("Toevoegen")).AsButton();
                toevoegenButton.Invoke();

                System.Threading.Thread.Sleep(2000);

                var thirdWindow = app.GetAllTopLevelWindows(automation).First(x => x.Name == "BestemmingBewerkenToevoegenView");

                var naam = thirdWindow.FindFirstDescendant(x => x.ByAutomationId("Naam")).AsTextBox();
                var land = thirdWindow.FindFirstDescendant(x => x.ByAutomationId("Land")).AsTextBox();
                var gemeente = thirdWindow.FindFirstDescendant(x => x.ByAutomationId("Gemeente")).AsComboBox();
                var straat = thirdWindow.FindFirstDescendant(x => x.ByAutomationId("Straat")).AsTextBox();
                var huisnummer = thirdWindow.FindFirstDescendant(x => x.ByAutomationId("Huisnummer")).AsTextBox();
                var bevestig = thirdWindow.FindFirstDescendant(x => x.ByAutomationId("Bevestig")).AsButton();

                //Act
                naam.Enter("Test");
                land.Enter("Test");
                gemeente.Items.First().Click();
                straat.Enter("test");
                huisnummer.Enter("12");
                bevestig.Invoke();

                System.Threading.Thread.Sleep(2000);

                //Assert
                Assert.That(count < datagrid.Rows.Count());

                thirdWindow.Close();
                secondWindow.Close();
                window.Close();
            }
        }
    }
}
