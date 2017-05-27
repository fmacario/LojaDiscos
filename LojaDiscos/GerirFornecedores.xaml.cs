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
    /// Interaction logic for GerirFornecedores.xaml
    /// </summary>
    public partial class GerirFornecedores : Page
    {
        public GerirFornecedores()
        {
            InitializeComponent();
            List();
        }

        private void List()
        {

            //SqlConnection con = ConnectionHelper.GetConnection();

            using (SqlConnection sc = ConnectionHelper.GetConnection())
            {
                sc.Open();
                string sql = "Select * FROM Pessoa As P JOIN Fornecedor As C ON P.nif = C.nif";
                SqlCommand com = new SqlCommand(sql, sc);


                using (SqlDataAdapter adapter = new SqlDataAdapter(com))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGrid.ItemsSource = dt.DefaultView;
                }



            }
        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = sender as DataGridRow;
            // Some operations with this row

            AlterarFornecedor alt = new AlterarFornecedor();
            this.NavigationService.Navigate(alt);


            DataRowView rowview = dataGrid.SelectedItem as DataRowView;
            alt.nif2.Text = rowview.Row[0].ToString();
            alt.nome2.Text = rowview.Row[1].ToString();
            alt.nTel2.Text = rowview.Row[2].ToString();
            alt.morada2.Text = rowview.Row[3].ToString();
            alt.email2.Text = rowview.Row[4].ToString(); ;



        }
        private void dataGrid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Editar.Visibility = Visibility.Visible;
        }

        private void Editar_Click(object sender, RoutedEventArgs e)
        {
            AlterarFornecedor alt = new AlterarFornecedor();
            this.NavigationService.Navigate(alt);
            DataRowView rowview = dataGrid.SelectedItem as DataRowView;
            alt.nif2.Text = rowview.Row[0].ToString();
            alt.nome2.Text = rowview.Row[1].ToString();
            alt.nTel2.Text = rowview.Row[2].ToString();
            alt.morada2.Text = rowview.Row[3].ToString();
            alt.email2.Text = rowview.Row[4].ToString();
        }

       

        private void criarFichaFornecedor_Click(object sender, RoutedEventArgs e)
        {
            CriarFichaFornecedor criarFichaFornecedor = new CriarFichaFornecedor();
            this.NavigationService.Navigate(criarFichaFornecedor);
        }

        private void pesquisar_Click(object sender, RoutedEventArgs e)
        {
            ListSearch();
        }

        private void ListSearch()
        {

            //SqlConnection con = ConnectionHelper.GetConnection();

            using (SqlConnection sc = ConnectionHelper.GetConnection())
            {
                sc.Open();
                string sql = "Select * FROM Pessoa As P JOIN Fornecedor As C ON P.nif = C.nif WHERE [nif]= @nif";
                SqlCommand com = new SqlCommand(sql, sc);
                com.Parameters.AddWithValue("@nif", nif_pesq.Text);

                using (SqlDataAdapter adapter = new SqlDataAdapter(com))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGrid.ItemsSource = dt.DefaultView;
                }

            }
        }

        private void dataGrid_SizeChanged(object sender, SizeChangedEventArgs e)
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

        private void vendaCliente_Click(object sender, RoutedEventArgs e)
        {
            Venda venda = new Venda();
            this.NavigationService.Navigate(venda);
        }

        private void reservaCliente_Click(object sender, RoutedEventArgs e)
        {
            Reserva reserva = new Reserva();
            this.NavigationService.Navigate(reserva);
        }

        private void gerirCliente_Click(object sender, RoutedEventArgs e)
        {
            GerirClientes gerirClientes = new GerirClientes();
            this.NavigationService.Navigate(gerirClientes);
        }

        private void gerirFornecedor_Click(object sender, RoutedEventArgs e)
        {
            GerirFornecedores gerirFornecedores = new GerirFornecedores();
            this.NavigationService.Navigate(gerirFornecedores);
        }

        private void discos_Click(object sender, RoutedEventArgs e)
        {
            GestaoDiscos gestaoDiscos = new GestaoDiscos();
            this.NavigationService.Navigate(gestaoDiscos);
        }

        private void encomendaFornecedor_Click(object sender, RoutedEventArgs e)
        {
            Encomenda encomenda = new Encomenda();
            this.NavigationService.Navigate(encomenda);
        }

        private void validarFornecedor_Click(object sender, RoutedEventArgs e)
        {
            ValidarEncomenda validarEncomenda = new ValidarEncomenda();
            this.NavigationService.Navigate(validarEncomenda);
        }

        private void adicionaDisco_Click(object sender, RoutedEventArgs e)
        {
            CriarDisco criarDisco = new CriarDisco();
            this.NavigationService.Navigate(criarDisco);
        }

      
    }
}
