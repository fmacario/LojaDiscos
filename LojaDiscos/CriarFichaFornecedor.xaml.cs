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

            using (SqlCommand cmd = new SqlCommand("InserirFornecedor", conn))
            {
                int i;

                if (nif2.Text.Length == 0)
                    MessageBox.Show("Insira NIF.");
                else if (!Int32.TryParse(nif2.Text, out i))
                    MessageBox.Show("Formato de NIF inválido. Insira um NIF numérico válido.");
                else if (nome2.Text.Length == 0)
                    MessageBox.Show("Insira Nome.");
                else if (morada2.Text.Length == 0)
                    MessageBox.Show("Insira Morada.");
                else if (email2.Text.Length == 0)
                    MessageBox.Show("Insira E-mail.");
                else if (nTel2.Text.Length == 0)
                    MessageBox.Show("Insira Nº Telefone.");
                else if (!Int32.TryParse(nTel2.Text, out i))
                    MessageBox.Show("Formato de Nº Telefone inválido. Insira um Nº Telefone numérico válido.");
                else
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
                    CriarFichaFornecedor menu = new CriarFichaFornecedor();
                    this.NavigationService.Navigate(menu);
                }
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
