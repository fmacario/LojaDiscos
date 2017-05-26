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
        Boolean teste = false;
        double totalDouble = 0;
        DataTable dt = new DataTable();
        int count = 0;
        DataGridTextColumn[] dataGridColumn = new DataGridTextColumn[11];

        public Venda()
        {
            InitializeComponent();
            quantidade = 0;
            //criarTabela();
        }

        public Venda(string nif)
        {
            this.nif = nif;
            InitializeComponent();
            quantidade = 0;
            getCliente();
        }

        private void getCliente()
        {

            SqlConnection conn = ConnectionHelper.GetConnection();

            using (SqlCommand cmd = new SqlCommand("pesquisaClienteVenda", conn))
            {

                

                if (nif.Equals("0"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@pesquisa", cliente.Text);
                    teste = Int32.TryParse(cmd.Parameters["@pesquisa"].Value.ToString(), out nif_cliente);
                }
                else {
                    nif_cliente = Int32.Parse(nif);
                    cliente.Text = nif;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@pesquisa", cliente.Text);
                    teste = true;
                }

                if (teste) { 
                    cmd.Parameters.Add("@nome", SqlDbType.VarChar, 50);
                    cmd.Parameters["@nome"].Direction = ParameterDirection.Output;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    if (cmd.Parameters["@nome"].Value.ToString() != "")
                    {
                        MessageBox.Show(nomeDoCliente.Text);
                        nomeDoCliente.Text = cmd.Parameters["@nome"].Value.ToString();
                        MessageBox.Show(nomeDoCliente.Text);
                        nomeDoCliente.Visibility = Visibility.Visible;
                        cliente.Visibility = Visibility.Hidden;
                        clienteNome.Text = "Nome: ";
                        novaPesquisa.Visibility = Visibility.Visible;
                        addCliente.Visibility = Visibility.Hidden;
                    }
                    else
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
                            CriarFichaCliente newcliente = new CriarFichaCliente("Venda", nif_cliente.ToString());
                            this.NavigationService.Navigate(newcliente);
                        }

                    }
                    
                }
                else if (!teste)
                {
                    MessageBox.Show("Formato inválido. Insira um NIF válido");
                }
               
            }
        }

        private void novaPesquisa_Click(object sender, RoutedEventArgs e)
        {
            novaPesquisa.Visibility = Visibility.Hidden;
            cliente.Visibility = Visibility.Visible;
            pesquisa.Visibility = Visibility.Visible;
            clienteNome.Text = "NIF Cliente: ";
            nomeDoCliente.Visibility = Visibility.Hidden;
            addCliente.Visibility = Visibility.Visible;

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
                Venda venda = new Venda();
                this.NavigationService.Navigate(venda);
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
                    //DataTable dt = new DataTable();
                    MessageBox.Show(adapter.ToString());
                    adapter.Fill(dt);
                    dataGridVenda.ItemsSource = dt.DefaultView;
                    
                    if (count == 0)
                        apagarTitulos(dataGridVenda);

                    var workingWidth = dataGridVenda.ActualWidth - SystemParameters.VerticalScrollBarWidth; // take into account vertical scrollbar
                    var col0 = 0.1;
                    var col1 = 0.4;
                    var col2 = 0.15;
                    var col3 = 0.15;
                    var col4 = 0.2;


                    dataGridVenda.Columns[0].Width = workingWidth * col0;
                    dataGridVenda.Columns[0].Header = "Código";
                    dataGridVenda.Columns[0].DisplayIndex = 0;
                    dataGridVenda.Columns[0].IsReadOnly = true;

                    

                    

                    dataGridVenda.Columns[1].Width = workingWidth * col2;
                    dataGridVenda.Columns[1].Header = "Preço";
                    dataGridVenda.Columns[1].DisplayIndex = 2;
                    dataGridVenda.Columns[1].IsReadOnly = true;

                    dataGridVenda.Columns[2].Visibility = Visibility.Hidden;

                    dataGridVenda.Columns[3].Width = workingWidth * col1;
                    dataGridVenda.Columns[3].Header = "Designação";
                    dataGridVenda.Columns[3].DisplayIndex = 1;
                    dataGridVenda.Columns[3].IsReadOnly = true;

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
                        
                        DataGridTextColumn tot = new DataGridTextColumn();
                        tot.Header = "Preço Total";
                        tot.Width = workingWidth * col4;
                        dataGridVenda.Columns[5].IsReadOnly = true;
                        dataGridVenda.Columns.Add(tot);
                    }

                     
                }
                totalDouble = 0;
                if (count > 50) // não sei porque dá erro
                foreach (DataRowView row in dataGridVenda.Items)
                {
                        totalDouble += Double.Parse(row.Row.ItemArray[1].ToString());
                    if (totalDouble.ToString().Length >= 5)
                        totalE.Text = totalDouble.ToString().Substring(0, 5) + " €";
                    else if (totalDouble.ToString().Length == 4)
                        totalE.Text = totalDouble.ToString().Substring(0, 4) + "0 €";
                    else if (totalDouble.ToString().Length == 3)
                        totalE.Text = totalDouble.ToString().Substring(0, 3) + "00 €";
                    else if (totalDouble.ToString().Length == 2)
                        totalE.Text = totalDouble.ToString().Substring(0, 2) + ",00 €";
                    else if (totalDouble.ToString().Equals("0"))
                        totalE.Text = totalDouble.ToString().Substring(0, 1) + "0,00 €";
                    else if (totalDouble.ToString().Length == 1)
                        totalE.Text = totalDouble.ToString().Substring(0, 1) + ",00 €";

                }
            }
            count++;
        }

        
        private void apagarTitulos(DataGrid dataGridVenda)
        {
            for (int i = 0; i < dataGridColumn.Length; i++)
            {
                dataGridVenda.Columns.Remove(dataGridColumn[i]);
            }
        }

        private void criarTabela()
        {
            var workingWidth = 1300;// dataGridVenda.ActualWidth - SystemParameters.VerticalScrollBarWidth; // take into account vertical scrollbar
            var col0 = 0.1;
            var col1 = 0.4;
            var col2 = 0.15;
            var col3 = 0.15;
            var col4 = 0.2;
            
            

            for (int i = 0; i < dataGridColumn.Length; i++)
            {
                dataGridColumn[i] = new DataGridTextColumn();
            }

            dataGridColumn[0].Width = workingWidth * col0;
            dataGridColumn[0].Header = "Código";
            dataGridColumn[0].DisplayIndex = 0;
            dataGridColumn[0].IsReadOnly = true;

            dataGridColumn[1].Width = workingWidth * col2;
            dataGridColumn[1].Header = "Preço";
            dataGridColumn[1].DisplayIndex = 2;
            dataGridColumn[1].IsReadOnly = true;

            dataGridColumn[2].Visibility = Visibility.Hidden;

            dataGridColumn[3].Width = workingWidth * col1;
            dataGridColumn[3].Header = "Designação";
            dataGridColumn[3].DisplayIndex = 1;
            dataGridColumn[3].IsReadOnly = true;

            dataGridColumn[4].Visibility = Visibility.Hidden;
            dataGridColumn[5].Visibility = Visibility.Hidden;
            dataGridColumn[6].Visibility = Visibility.Hidden;
            dataGridColumn[7].Visibility = Visibility.Hidden;
            dataGridColumn[8].Visibility = Visibility.Hidden;

            dataGridColumn[9].Width = workingWidth * col3;
            dataGridColumn[9].Header = "Quantidade";
            dataGridColumn[9].DisplayIndex = 3;
            dataGridColumn[9].IsReadOnly = false;

            dataGridColumn[10].Width = workingWidth * col4;
            dataGridColumn[10].Header = "Total";
            dataGridColumn[10].DisplayIndex = 4;
            dataGridColumn[10].IsReadOnly = true;

            for (int i = 0; i < dataGridColumn.Length; i++)
            {
                dataGridVenda.Columns.Add(dataGridColumn[i]);
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

        private void cliente_TextChanged(object sender, TextChangedEventArgs e)
        {
            pesquisa.Visibility = Visibility.Visible;
        }


        private void cliente_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                //concluirVenda_Click(sender, e);
            }
        }

        private void dataGridVenda_Initialized(object sender, EventArgs e)
        {
            criarTabela();
        }

        private void dataGridVenda_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            MessageBox.Show(dataGridVenda.Items.GetItemAt(0).ToString());
               // quantidade não está a funcionar
                //Cells[1].Text = "text";
        }
    }
}