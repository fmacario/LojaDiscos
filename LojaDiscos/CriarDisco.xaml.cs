using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using static LojaDiscos.MainWindow;

namespace LojaDiscos
{
    /// <summary>
    /// Interaction logic for CriarDisco.xaml
    /// </summary>
    public partial class CriarDisco : Page
    {
        public CriarDisco()
        {
            InitializeComponent();

            BindComboBox(GeneroCB);
        }
        public void BindComboBox(ComboBox GeneroCB)
        {
            SqlConnection conn = ConnectionHelper.GetConnection();

            SqlDataAdapter da = new SqlDataAdapter("Select * FROM Genero", conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "Genero");
            GeneroCB.ItemsSource = ds.Tables[0].DefaultView;
            GeneroCB.DisplayMemberPath = ds.Tables[0].Columns["genero"].ToString();
           
        }

    }
}
