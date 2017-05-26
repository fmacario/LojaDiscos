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
    /// Interaction logic for CriarFichaFornecedor.xaml
    /// </summary>
    public partial class CriarFichaFornecedor : Page
    {
        public CriarFichaFornecedor()
        {
            InitializeComponent();
        }

        private void criarFornecedor_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection conn = ConnectionHelper.GetConnection();

            using (SqlCommand cmd = new SqlCommand("InserirCliente", conn))
            {

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@nif", SqlDbType.Int).Value = nif2.Text;
                cmd.Parameters.Add("@nome", SqlDbType.VarChar, 50).Value = nome2.Text;
                cmd.Parameters.Add("@morada", SqlDbType.VarChar, 50).Value = morada2.Text;
                cmd.Parameters.Add("@telefone", SqlDbType.Int).Value = nTel2.Text;
                cmd.Parameters.Add("@email", SqlDbType.VarChar, 30).Value = email2.Text;
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Fornecedor adicionado com sucesso", "Sucesso!");
            }

        }
    }
}
