using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Reisplanningssysteem_WPF.ViewModels;

namespace Reisplanningssysteem_WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            MainViewModel viewmodel = new MainViewModel();
            Views.MainView view = new Views.MainView();
            view.DataContext = viewmodel;
            view.Show();
        }
    }
}
