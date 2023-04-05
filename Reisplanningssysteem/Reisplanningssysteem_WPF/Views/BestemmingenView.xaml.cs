using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Reisplanningssysteem_WPF.ViewModels;

namespace Reisplanningssysteem_WPF.Views
{
    /// <summary>
    /// Interaction logic for BestemmingenView.xaml
    /// </summary>
    public partial class BestemmingenView : Window
    {
        private readonly BestemmingenViewModel _vm = new BestemmingenViewModel();
        public BestemmingenView()
        {
            InitializeComponent();
            DataContext = _vm;
        }
    }
}
