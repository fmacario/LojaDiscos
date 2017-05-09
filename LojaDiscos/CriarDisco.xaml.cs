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

        }

        private void adicionarDisco_Click(object sender, RoutedEventArgs e)
        {
            Add_disco();
        }
    }
}
