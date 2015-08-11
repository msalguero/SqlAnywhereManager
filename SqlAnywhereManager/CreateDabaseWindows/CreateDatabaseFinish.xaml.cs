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
using SqlAnywhere;

namespace SqlAnywhereManager.CreateDabaseWindows
{
    /// <summary>
    /// Interaction logic for CreateDatabaseFinish.xaml
    /// </summary>
    public partial class CreateDatabaseFinish : Window
    {
        private CreateDatabaseModel _newDatabase;
        public CreateDatabaseFinish(CreateDatabaseModel newDatabase)
        {
            InitializeComponent();
            _newDatabase = newDatabase;
            var commandDdl = SqlCommandBuilder.GenerateDatabaseDdl(_newDatabase.DatabasePath, _newDatabase.LogPath,
                _newDatabase.Collation, _newDatabase.PageSize);
            DdlTextBox.Text = commandDdl;
            var commandInit = SqlCommandBuilder.GenerateDatabaseInit(_newDatabase.DatabasePath, _newDatabase.LogPath,
                _newDatabase.Collation, _newDatabase.PageSize);
            CommandTextBox.Text = commandInit;
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            var dialog = new CreateDatabasePage(_newDatabase);
            dialog.Show();
            this.Close();
        }

        private void Finish(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C " + CommandTextBox.Text;
            process.StartInfo = startInfo;
            process.Start();
            this.Close();
        }
    }
}
