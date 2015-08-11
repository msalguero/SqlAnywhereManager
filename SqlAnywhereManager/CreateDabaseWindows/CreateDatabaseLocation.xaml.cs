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
    /// Interaction logic for CreateDatabaseLocation.xaml
    /// </summary>
    public partial class CreateDatabaseLocation : Window
    {
        private CreateDatabaseModel _newDatabase;
        public CreateDatabaseLocation()
        {
            InitializeComponent();
            _newDatabase = new CreateDatabaseModel();
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            var dialog = new CreateDatabase1();
            dialog.Show();
            this.Close();
        }

        private void Next(object sender, RoutedEventArgs e)
        {
            _newDatabase.DatabasePath = FileTextBox.Text;
            var dialog = new CreateDatabaseLog(_newDatabase);
            dialog.Show();
            this.Close();
        }

        private void Browse(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog()
            {
                DefaultExt = ".db",
                Filter =
                    "Database Files (*.db)|*.db"
            };

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;
                FileTextBox.Text = filename;
            }
        }
    }
}
