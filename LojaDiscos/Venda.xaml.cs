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
using System.Windows.Shapes;
using static LojaDiscos.MainWindow;

namespace LojaDiscos
{
    /// <summary>
    /// Interaction logic for Venda.xaml
    /// </summary>
    public partial class Venda : Page
    {
        Int32 nif_cliente = 0;
        public Venda()
        {
            InitializeComponent();
        }

        /*private void dataGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {

            DataGrid dataGrid = sender as DataGrid;

            var workingWidth = dataGrid.ActualWidth - SystemParameters.VerticalScrollBarWidth; // take into account vertical scrollbar
            var col1 = 0.1;
            var col2 = 0.3;
            var col3 = 0.1;
            var col4 = 0.3;
            var col5 = 0.2;
            var col6 = 0;

            dataGrid.Columns[0].Width = workingWidth * col1;
            dataGrid.Columns[1].Width = workingWidth * col2;
            dataGrid.Columns[2].Width = workingWidth * col3;
            dataGrid.Columns[3].Width = workingWidth * col4;
            dataGrid.Columns[4].Width = workingWidth * col5;
            dataGrid.Columns[5].Width = workingWidth * col6;
        }
        */


        private void getCliente()
        {
            

            SqlConnection conn = ConnectionHelper.GetConnection();

            using (SqlCommand cmd = new SqlCommand("pesquisaClienteVenda", conn))
            {

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@pesquisa", cliente.Text);

                nif_cliente = Int32.Parse(cmd.Parameters["@pesquisa"].Value.ToString());

                cmd.Parameters.Add("@nome", SqlDbType.VarChar, 50);
                cmd.Parameters["@nome"].Direction = ParameterDirection.Output;
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                cliente.Text = cmd.Parameters["@nome"].Value.ToString();


            }
        }

        private void pesquisa_Click(object sender, RoutedEventArgs e)
        {
            getCliente();
        }

        private void concluirVenda_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection conn = ConnectionHelper.GetConnection();
            DateTime thisDay = DateTime.Today;
            using (SqlCommand cmd = new SqlCommand("RegistrarVenda", conn))
            {

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@data_venda", SqlDbType.Date).Value = thisDay.ToShortDateString();
                cmd.Parameters.Add("@nif_funcionario", SqlDbType.Int).Value = 98372344;
                cmd.Parameters.Add("@id_disco", SqlDbType.Int).Value = 99999;
                cmd.Parameters.Add("@nif_cliente", SqlDbType.Int).Value = nif_cliente;

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Venda Efectuada", "Sucesso!");
                Menu menu = new Menu();
                this.NavigationService.Navigate(menu);
            }

        }
        private void showDisco()
        {
            //SqlConnection con = ConnectionHelper.GetConnection();

            using (SqlConnection sc = ConnectionHelper.GetConnection())
            {
                sc.Open();
                string sql = "Select * FROM Discos WHERE [id_disco] = @id_disco";
                SqlCommand com = new SqlCommand(sql, sc);
                com.Parameters.AddWithValue("@id_disco", 99999);

                using (SqlDataAdapter adapter = new SqlDataAdapter(com))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridVenda.ItemsSource = dt.DefaultView;
                  
                }
            }


        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);
            window.KeyDown += HandleKeyPress;
        }

        private void HandleKeyPress(object sender, KeyEventArgs e)
        {
            showDisco();
        }

    }
}