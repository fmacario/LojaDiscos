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
    /// Interaction logic for Encomenda.xaml
    /// </summary>
    /// 
    
    public partial class Encomenda : Page
    {
        int nif;
        int quantidade = 0;
        public Encomenda()
        {
            InitializeComponent();
            BindComboBox(comboBox);
        }
        public void BindComboBox(ComboBox GeneroCB)
         {
            SqlConnection conn = ConnectionHelper.GetConnection();
    
             SqlDataAdapter da = new SqlDataAdapter("Select * FROM Pessoa as P JOIN Fornecedor as F ON P.nif=F.nif", conn);
             DataSet ds = new DataSet();
             da.Fill(ds, "Pessoa");
             GeneroCB.ItemsSource = ds.Tables[0].DefaultView;
             GeneroCB.DisplayMemberPath = ds.Tables[0].Columns["nome"].ToString();

            SqlCommand cmd = new SqlCommand("Select * FROM Pessoa as P JOIN Fornecedor as F ON P.nif = F.nif", conn);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                
                nif = Convert.ToInt32(dr["nif"]);
            }
            conn.Close();

            Console.Write(nif);
            // nif = Int32.Parse(ds.Tables[0].Columns["nif"].ToString());

            
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

        private void criarFichaFornecedor_Click(object sender, RoutedEventArgs e)
        {
            CriarFichaFornecedor criarFichaFornecedor = new CriarFichaFornecedor();
            this.NavigationService.Navigate(criarFichaFornecedor);
        }

        private void criarFichaCliente_Click(object sender, RoutedEventArgs e)
        {
            CriarFichaCliente criarFichaCliente = new CriarFichaCliente();
            this.NavigationService.Navigate(criarFichaCliente);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection conn = ConnectionHelper.GetConnection();
            DateTime thisDay = DateTime.Today;
            using (SqlCommand cmd = new SqlCommand("InserirEncomenda", conn))
            {

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@id_disco", SqlDbType.Int).Value = textBox.Text;
                cmd.Parameters.Add("@data1", SqlDbType.Date).Value = thisDay.ToShortDateString();
                cmd.Parameters.Add("@nif_fornecedor", SqlDbType.Int).Value = nif;
               
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                quantidade = Int32.Parse(textBox_Copy.Text);
                MessageBox.Show("Encomenda adicionada com sucesso", "Sucesso!");
                Venda venda = new Venda();
                this.NavigationService.Navigate(venda);
            }
        }
        public int getQuatidade()
        {
            return quantidade;
        }
        
    }
}
