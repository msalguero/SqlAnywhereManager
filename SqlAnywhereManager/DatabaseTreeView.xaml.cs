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
using SqlAnywhere;

namespace SqlAnywhereManager
{
    /// <summary>
    /// Interaction logic for DatabaseTreeView.xaml
    /// </summary>

    public partial class DatabaseTreeView : UserControl
    {
        private MainWindow _mainWindow ;
        public int Index;
        public string DataBaseName;
        public DatabaseTreeView(int index, string name)
        {
            InitializeComponent();

            DataBaseName = name;
            DbMenuItem.Tag = name;
            TreeDatabaseName.Content = name;
            Index = index;
            _mainWindow = (MainWindow)Window.GetWindow(this);

        }

        private void NewConnection_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow = (MainWindow)Window.GetWindow(this);
            _mainWindow.NewConnection_Click(sender,e);
        }

        private void EndConnection_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow = (MainWindow)Window.GetWindow(this);
            _mainWindow.EndConnection_Click(sender,e);
        }

        private void OnItemMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            _mainWindow = (MainWindow)Window.GetWindow(this);
            _mainWindow.OnItemMouseDoubleClick(sender,e);
        }

        private void OnDatabaseExpanded(object sender, RoutedEventArgs e)
        {
            _mainWindow = (MainWindow)Window.GetWindow(this);
            _mainWindow.OnDatabaseExpanded(sender,e);
        }

        public void ExpandSubtree()
        {
            DatabaseTreeViewItem.ExpandSubtree();
        }

        public void Collapse()
        {
            DatabaseTreeViewItem.IsExpanded = false;
        }
    }
}
