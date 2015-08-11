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
    /// Interaction logic for CreateDatabaseLog.xaml
    /// </summary>
    public partial class CreateDatabaseLog : Window
    {
        private CreateDatabaseModel _newDatabase;
        public CreateDatabaseLog(CreateDatabaseModel model)
        {
            InitializeComponent();
            _newDatabase = model;
        }

        private void Browse(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog()
            {
                DefaultExt = ".log",
                Filter =
                    "Log Files (*.log)|*.log"
            };

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;
                FileTextBox.Text = filename;
            }
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            var dialog = new CreateDatabaseLocation();
            dialog.Show();
            this.Close();
        }

        private void Next(object sender, RoutedEventArgs e)
        {
            _newDatabase.LogPath = FileTextBox.Text;
            var dialog = new CreateDatabasePage(_newDatabase);
            dialog.Show();
            this.Close();
        }
    }
}
