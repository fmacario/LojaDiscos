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
using System.Collections.ObjectModel;
using System.Globalization;
using System.Data.SqlClient;

namespace LojaDiscos
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : NavigationWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public class ConnectionHelper
        {
            public static SqlConnection GetConnection()
            {
                string connectionStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Imagens_Discos_IHC\lojadiscos.mdf;Integrated Security=True;Connect Timeout=30";
                SqlConnection conn = new SqlConnection(connectionStr);
                return conn;
            }
        }


    }
}