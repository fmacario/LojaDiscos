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
        int quantidade;
        string nif = "0";

        public Venda()
        {
            InitializeComponent();
            quantidade = 0;
        }

        public Venda(string nif)
        {
            this.nif = nif;
            InitializeComponent();
            quantidade = 0;
            getCliente();
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

                

                if (nif.Equals("0"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@pesquisa", cliente.Text);
                    nif_cliente = Int32.Parse(cmd.Parameters["@pesquisa"].Value.ToString());
                }
                else {
                    nif_cliente = Int32.Parse(nif);
                    cliente.Text = nif;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@pesquisa", cliente.Text);
                }


                cmd.Parameters.Add("@nome", SqlDbType.VarChar, 50);
                cmd.Parameters["@nome"].Direction = ParameterDirection.Output;
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                if (cmd.Parameters["@nome"].Value.ToString() != "")
                {
                    cliente.Text = cmd.Parameters["@nome"].Value.ToString();
                }
                /*else
                {
                    if (MessageBox.Show("NIF não encontrado. Deseja adicionar novo cliente?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                    {
                        //do no stuff
                        Venda contin = new Venda();
                        this.NavigationService.Navigate(contin);

                    }
                    else
                    {
                        //do yes stuff
                        CriarFichaCliente newcliente = new CriarFichaCliente();
                        this.NavigationService.Navigate(newcliente);
                    }

                }*/
            }
        }
        private void pesquisa_Click(object sender, RoutedEventArgs e)
        {
            getCliente();
        }

        private void addCliente_Click(object sender, RoutedEventArgs e)
        {
            CriarFichaCliente newcliente = new CriarFichaCliente("venda");
            this.NavigationService.Navigate(newcliente);
        }

        private void concluirVenda_Click(object sender, RoutedEventArgs e)
        {

            int codigo = 0;
            foreach (DataRowView row in dataGridVenda.Items)
            {
                codigo = Int32.Parse(row.Row.ItemArray[0].ToString());
            }

            SqlConnection conn = ConnectionHelper.GetConnection();
            DateTime thisDay = DateTime.Today;
            using (SqlCommand cmd = new SqlCommand("RegistrarVenda", conn))
            {

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@data_venda", SqlDbType.Date).Value = thisDay.ToShortDateString();
                cmd.Parameters.Add("@nif_funcionario", SqlDbType.Int).Value = 98372344;
                cmd.Parameters.Add("@id_disco", SqlDbType.Int).Value = codigo;
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
                //string sql = "Select * FROM Discos WHERE [id_disco] = @id_disco";
                string sql = "SELECT TOP 1 * FROM Discos ORDER BY NEWID()";
                SqlCommand com = new SqlCommand(sql, sc);
                //com.Parameters.AddWithValue("@id_disco", 99999);

                using (SqlDataAdapter adapter = new SqlDataAdapter(com))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridVenda.ItemsSource = dt.DefaultView;
                    
                    
                        var workingWidth = dataGridVenda.ActualWidth - SystemParameters.VerticalScrollBarWidth; // take into account vertical scrollbar
                        var col0 = 0.1;
                        var col1 = 0.4;
                        var col2 = 0.15;
                        var col3 = 0.15;
                        var col4 = 0.2;

                        dataGridVenda.Columns[0].Width = workingWidth * col0;
                        dataGridVenda.Columns[0].Header = "Código";
                        dataGridVenda.Columns[0].DisplayIndex = 0;

                        dataGridVenda.Columns[1].Width = workingWidth * col2;
                        dataGridVenda.Columns[1].Header = "Preço";
                        dataGridVenda.Columns[1].DisplayIndex = 2;

                        dataGridVenda.Columns[2].Visibility = Visibility.Hidden;

                        dataGridVenda.Columns[3].Width = workingWidth * col1;
                        dataGridVenda.Columns[3].Header = "Designação";
                        dataGridVenda.Columns[3].DisplayIndex = 1;

                        dataGridVenda.Columns[4].Visibility = Visibility.Hidden;
                        dataGridVenda.Columns[5].Visibility = Visibility.Hidden;
                        dataGridVenda.Columns[6].Visibility = Visibility.Hidden;
                        dataGridVenda.Columns[7].Visibility = Visibility.Hidden;
                        dataGridVenda.Columns[8].Visibility = Visibility.Hidden;

                    if (quantidade == 1)
                    {
                        DataGridTextColumn qtd = new DataGridTextColumn();
                        qtd.Header = "Quantidade";
                        qtd.IsReadOnly = false;
                        qtd.Width = workingWidth * col3;
                        dataGridVenda.Columns.Add(qtd);
                        qtd.IsReadOnly = false;
                  

                        DataGridTextColumn tot = new DataGridTextColumn();
                        tot.Header = "Preço Total";
                        tot.Width = workingWidth * col4;
                        dataGridVenda.Columns.Add(tot);
                    }

                }
                
                foreach (DataRowView row in dataGridVenda.Items)
                {
                    totalE.Text = row.Row.ItemArray[1].ToString();
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
            quantidade++;

            if (e.Key == Key.F1)
            {
                showDisco();
            }
        
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