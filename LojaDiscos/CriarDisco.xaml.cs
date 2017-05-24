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
using Microsoft.Win32;

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
            BindComboBoxTipo(TipoCB);
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
        public void BindComboBoxTipo(ComboBox TipoCB)
        {
            SqlConnection conn = ConnectionHelper.GetConnection();

            SqlDataAdapter da = new SqlDataAdapter("Select * FROM Tipos_Discos", conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "Tipos_Discos");
            TipoCB.ItemsSource = ds.Tables[0].DefaultView;
            TipoCB.DisplayMemberPath = ds.Tables[0].Columns["tipo"].ToString();

        }
        public void Add_disco() {
            SqlConnection conn = ConnectionHelper.GetConnection();
            
            using (SqlCommand cmd = new SqlCommand("InserirDisco", conn))
            {
                int i;
                double d;

                if (codigo2.Text.Length == 0)
                    MessageBox.Show("Insira Código.");
                else if (!Int32.TryParse(codigo2.Text, out i))
                    MessageBox.Show("Formato de Código inválido. Insira um Código numérico válido.");
                else if (album2.Text.Length == 0)
                    MessageBox.Show("Insira nome do Álbum.");
                else if (preço1.Text.Length == 0)
                    MessageBox.Show("Insira Preço.");
                else if (!Double.TryParse(preço1.Text, out d))
                    MessageBox.Show("Formato de Preço inválido. Insira um Preço válido (xx,yy).");
                else if (ano2.Text.Length == 0)
                    MessageBox.Show("Insira Ano.");
                else if (!Int32.TryParse(ano2.Text, out i))
                    MessageBox.Show("Formato de Ano inválido. Insira um Ano válido.");
                else if (GeneroCB.Text != null)
                    MessageBox.Show("Escolha o Género.");
                else if (TipoCB.Text != null)
                    MessageBox.Show("Escolha o Tipo.");
                else if (artista2.Text.Length == 0)
                    MessageBox.Show("Insira nome do Artista.");
                else if (unidades2.Text.Length == 0)
                    MessageBox.Show("Insira Unidades.");
                else if (!Int32.TryParse(unidades2.Text, out i))
                    MessageBox.Show("Formato de Unidades inválido. Insira Unidades numéricas válidas.");
                else { 
                    
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@id_disco", SqlDbType.Int).Value = codigo2.Text;
                    cmd.Parameters.Add("@titulo", SqlDbType.VarChar, 30).Value = album2.Text;
                    cmd.Parameters.Add("@preço", SqlDbType.Money).Value = preço1.Text;
                    cmd.Parameters.Add("@ano", SqlDbType.Int).Value = ano2.Text;
                    cmd.Parameters.Add("@stock", SqlDbType.Int).Value = unidades2.Text;
                    cmd.Parameters.Add("@genero", SqlDbType.VarChar, 30).Value = GeneroCB.Text;
                    cmd.Parameters.Add("@tipo", SqlDbType.VarChar, 30).Value = TipoCB.Text;
                    cmd.Parameters.Add("@artista", SqlDbType.VarChar, 30).Value = artista2.Text;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();


                    //actualizar  stocks
                    SqlCommand c = new SqlCommand("atualizarStock", conn);
                    c.CommandType = CommandType.StoredProcedure;
                    c.Parameters.AddWithValue("@id_disco", codigo2.Text);
                    c.Parameters.AddWithValue("@update", unidades2.Text);
                    conn.Open();
                    c.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Disco inserido com sucesso", "Sucesso!");
                    Menu menu = new Menu();
                    this.NavigationService.Navigate(menu);
                }

            }


            /*
            string query = "INSERT INTO Discos (id_disco, preço, stock, titulo, ano,  id_artista, id_genero, id_tipo, imagemDisco)";
            query += " VALUES (@id_disco, @preço, @stock, @titulo, @ano, @id_artista, @id_genero, @id_tipo, @imagemDisco)";

            SqlCommand myCommand = new SqlCommand(query, conn);
            myCommand.Parameters.AddWithValue("@id_disco", codigo2.Text);
            myCommand.Parameters.AddWithValue("@preço", preço1.Text);
            myCommand.Parameters.AddWithValue("@titulo", preço1.Text);
            myCommand.Parameters.AddWithValue("@stock", 1);
            myCommand.Parameters.AddWithValue("@ano", ano2.Text);
            myCommand.Parameters.AddWithValue("@id_artista", );
            myCommand.Parameters.AddWithValue("@id_genero", );
            myCommand.Parameters.AddWithValue("@id_tipo", );
            myCommand.Parameters.AddWithValue("@imagemDisco", imgPhoto);
            myCommand.ExecuteNonQuery();*/
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Selecione uma imagem.";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                imgPhoto.Source = new BitmapImage(new Uri(op.FileName));
            }

            btnLoad.Content = "Alterar Capa";
        }

        private void adicionarDisco_Click(object sender, RoutedEventArgs e)
        {
            Add_disco();
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
