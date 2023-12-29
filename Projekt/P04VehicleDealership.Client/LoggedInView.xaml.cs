using P04VehicleDealership.Client.ViewModels;
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

namespace P04VehicleDealership.Client
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoggedInView : Window
    {
        private readonly LoggedInViewModel _loginViewModel;
        public LoggedInView(LoggedInViewModel loginViewModel)
        {
            _loginViewModel = loginViewModel;
            DataContext = loginViewModel;
            InitializeComponent();
        }




    }
}
