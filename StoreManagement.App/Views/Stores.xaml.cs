using StoreManagement.App.ViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StoreManagement.App.Views
{
    /// <summary>
    /// Logique d'interaction pour Stores.xaml
    /// </summary>
    public partial class Stores : UserControl
    {
        public Stores()
        {
            InitializeComponent();
            DataContext = new StoresViewModel();
        }
    }
}
