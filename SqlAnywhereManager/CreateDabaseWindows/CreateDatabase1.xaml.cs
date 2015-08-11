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

namespace SqlAnywhereManager.CreateDabaseWindows
{
    /// <summary>
    /// Interaction logic for CreateDatabase1.xaml
    /// </summary>
    public partial class CreateDatabase1 : Window
    {
        public CreateDatabase1()
        {
            InitializeComponent();
        }

        private void Next(object sender, RoutedEventArgs e)
        {
            var dialog = new CreateDatabaseLocation();
            dialog.Show();
            this.Close();
        }
    }
}
