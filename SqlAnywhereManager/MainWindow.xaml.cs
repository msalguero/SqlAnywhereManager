using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SqlAnywhere;
using SqlAnywhereManager.CreateDabaseWindows;

namespace SqlAnywhereManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public SqlAnywhereConnectionManager ConnectionManager;
        
        public MainWindow()
        {
            ConnectionManager = null;
            InitializeComponent();
            DataGrid.AutoGenerateColumns = false;
            ResultsDataGrid.AutoGenerateColumns = false;

            var treeviewitem = new DatabaseTreeView(0,"Banco");
            var treeviewitem2 = new DatabaseTreeView(1,"Escuela");
            
            ObjectTreeView.Items.Add(treeviewitem);
            ObjectTreeView.Items.Add(treeviewitem2);
        }

        public void NewConnection_Click(object sender, RoutedEventArgs e)
        {
            if (ConnectionManager != null)
            {
                MessageBox.Show("You are already connected to a database");
                return;
            }
            NewConnection newConnectionWindow = new NewConnection(ConnectionManager);
            newConnectionWindow.NewConnectionEvent += Connection;
            newConnectionWindow.ShowDialog();
        }

        public void Connection(object sender, EventArgs e, SqlAnywhereConnectionManager newConnectionManager)
        {
            ((NewConnection)sender).Close();
            ConnectionManager = newConnectionManager;
            var item = ObjectTreeView.Items.GetItemAt(1) as DatabaseTreeView;
            item.ExpandSubtree();
            Image img = FindResource("connect") as Image;
            if (img != null) DatabaseIcon.Source = img.Source;
        }

        public void RefreshTreeView()
        {
            ObjectTreeView.Items.Clear();


        }

        public void ClearDatagrid()
        {
            DataGrid.Columns.Clear();
            DataGrid.ItemsSource = null;
            DataGrid.Items.Refresh();
        }

        public void OnItemMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ClearDatagrid();
            var objectName = getSenderName((TreeViewItem) sender);
            var objectTag = ((TreeViewItem) sender).Tag;
            var table = ConnectionManager.GetDataFromTable(objectTag.ToString());


            for (int i = 0; i < table.Columns.Count; i++)
            {
                var col = new DataGridTextColumn
                {
                    Header = table.Columns.ElementAt(i),
                    Binding = new Binding("[" + i + "]")
                };
                DataGrid.Columns.Add(col);
            }
            DataGrid.ItemsSource = table.Rows;
            DataGrid.Items.Refresh();
        }

        private string getSenderName(TreeViewItem sender)
        {
            StackPanel panel = (StackPanel) sender.Header;
            var lastOrDefault = (from object child in panel.Children select child.ToString()).LastOrDefault();
            if (lastOrDefault != null)
                return lastOrDefault.Split(':')[1];
            return "error";
        }

        public void OnDatabaseMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ConnectionManager != null)
                return;
            NewConnection_Click(sender, e);
            e.Handled = true;
        }

        public void OnDatabaseExpanded(object sender, RoutedEventArgs e)
        {
            var item = sender as TreeViewItem;
            if (ConnectionManager == null)
                item.IsExpanded = false;
        }

        public void EndConnection_Click(object sender, RoutedEventArgs e)
        {
            if (ConnectionManager == null)
            {
                MessageBox.Show("You are not connected to a database");
                return;
            }
    
            var dataBaseName = ((MenuItem) sender).Tag.ToString();
            ConnectionManager.DisconnectDatabase(dataBaseName);
            ConnectionManager = null;
            var item = ObjectTreeView.Items.GetItemAt(0) as TreeViewItem;
            item.IsExpanded = false;
            Image img = FindResource("disconnect") as Image;
            if (img != null) DatabaseIcon.Source = img.Source;
            ClearDatagrid();
        }

        private void RunSqlCommands(object sender, RoutedEventArgs e)
        {
            if (ConnectionManager == null)
            {
                NewConnection_Click(sender, e);
                return;
            }
            TextRange textRange = null;
            if (RichTextBox.Selection != null)
            {
                
                if (RichTextBox.Selection.IsEmpty)
                {
                    textRange = new TextRange(
                            
                    RichTextBox.Document.ContentStart,
                            
                    RichTextBox.Document.ContentEnd
            );
                }
                else
                {
                    textRange = new TextRange(RichTextBox.Selection.Start, RichTextBox.Selection.End);
                }
            }
            

            if (textRange != null)
            {
                var text = textRange.Text.Replace(Environment.NewLine, " ");
                ConnectionManager.ExecuteSqlCommand(text);
            }
        }

        private void RunSqlQueries(object sender, RoutedEventArgs e)
        {
            if (ConnectionManager == null)
            {
                NewConnection_Click(sender, e);
                return;
            }
            TextRange textRange = null;
            if (QueryTextBox.Selection != null)
            {

                if (QueryTextBox.Selection.IsEmpty)
                {
                    textRange = new TextRange(

                    QueryTextBox.Document.ContentStart,

                    QueryTextBox.Document.ContentEnd
            );
                }
                else
                {
                    textRange = new TextRange(QueryTextBox.Selection.Start, QueryTextBox.Selection.End);
                }
            }


            if (textRange != null)
            {
                var text = textRange.Text.Replace(Environment.NewLine, " ");
                ShowResults(ConnectionManager.ExecuteSqlQuery(text));
            }
        }

        private void ShowResults(SqlAnywhereTable table)
        {
            ResultsDataGrid.Columns.Clear();
            ResultsDataGrid.ItemsSource = null;
            ResultsDataGrid.Items.Refresh();
            for (int i = 0; i < table.Columns.Count; i++)
            {
                var col = new DataGridTextColumn
                {
                    Header = table.Columns.ElementAt(i),
                    Binding = new Binding("[" + i + "]")
                };
                ResultsDataGrid.Columns.Add(col);
            }
            ResultsDataGrid.ItemsSource = table.Rows;
            ResultsDataGrid.Items.Refresh();
        }

        private void ShowFilterDialog(object sender, RoutedEventArgs e)
        {
            Filters filtersDialog = new Filters();

            filtersDialog.ShowDialog();
        }

        private void CreateDatabase(object sender, RoutedEventArgs e)
        {
            CreateDatabase1 databaseCreation = new CreateDatabase1();

            databaseCreation.ShowDialog();
        }
    }
}
