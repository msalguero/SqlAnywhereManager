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

namespace SqlAnywhereManager
{
    /// <summary>
    /// Interaction logic for NewConnection.xaml
    /// </summary>

    public delegate void NewConnectionEventHandler(object sender, EventArgs e, SqlAnywhereConnectionManager newConnectionManager );

    public partial class NewConnection : Window
    {
        public SqlAnywhereConnectionManager ConnectionManager;
        public event NewConnectionEventHandler NewConnectionEvent;

        public NewConnection(SqlAnywhereConnectionManager connectionManager)
        {
            ConnectionManager = connectionManager;
            InitializeComponent();
        }

        private void Test_Click(object sender, RoutedEventArgs e)
        {
            var connectionManager = new SqlAnywhereConnectionManager();
            if (connectionManager.ConnectToDatabase(DatabaseBox.Text, UserIdBox.Text, PasswordBox.Password))
            {
                connectionManager.DisconnectDatabase(DatabaseBox.Text);
                MessageLabel.Content = "Test Succesfull";
                
            }
            else
            {
                MessageLabel.Content = "Test Failed";
            }
        }

        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            
            if (ConnectionManager.ConnectToDatabase(DatabaseBox.Text,UserIdBox.Text,PasswordBox.Password))
            {
                
                MessageLabel.Content = "Test Succesfull";
                if (NewConnectionEvent != null) NewConnectionEvent(this, e, ConnectionManager);
            }
            else
            {
                MessageLabel.Content = "Connection Failed";
               
            }
        }
    }
}
