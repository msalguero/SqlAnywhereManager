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
    /// Interaction logic for CreateDatabasePage.xaml
    /// </summary>
    public partial class CreateDatabasePage : Window
    {
        private CreateDatabaseModel _newDatabase;
        public CreateDatabasePage(CreateDatabaseModel newDatabase)
        {
            InitializeComponent();
            _newDatabase = newDatabase;
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            var dialog = new CreateDatabaseLog(_newDatabase);
            dialog.Show();
            this.Close();
        }

        private void Next(object sender, RoutedEventArgs e)
        {
            var checkedButton = container.Children.OfType<RadioButton>()
                                      .FirstOrDefault(r => r.IsChecked != null && (bool)r.IsChecked);
            if (checkedButton != null) _newDatabase.PageSize = checkedButton.Content.ToString();
            var dialog = new CreateDatabaseFinish(_newDatabase);
            dialog.Show();
            this.Close();
        }
    }
}
