using System;
using System.Collections.Generic;
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
using System.Data;
using System.Data.SqlClient;
using static LojaDiscos.MainWindow;
using System.IO;

namespace LojaDiscos
{
    /// <summary>
    /// Interaction logic for GestaoDiscos.xaml
    /// </summary>
    public partial class GestaoDiscos : Page
    {
        public GestaoDiscos()
        {
            InitializeComponent();
            //carregarCds();
        }


        public void carregarCds()
        {
            for (int i = 0; i < 3; i++)
            {
                StackPanel sp = new StackPanel();
                sp.Orientation = Orientation.Vertical;

                Image Img = new Image();
                //Img.Source = new BitmapImage(new Uri("C:/Users/Filipe/Desktop/UA/3ano/ihc/projeto/LojaDiscos/LojaDiscos/Images/icon.png"));
                //Image resizedImage = new Bitmap(image, new Size(50, 50));
                TextBlock text = new TextBlock();
                text.Text = "TEste " + i + " !";

                //sp.Children.Add(Img);
                //sp.Children.Add(text);

                //this.wrapPanelDiscos.Children.Add(sp);
            }

            using (SqlConnection sc = ConnectionHelper.GetConnection())
            {
                sc.Open();
                int a = 11111;
                string sql = "Select titulo FROM Discos where "+a+" = id_disco";
                SqlCommand com = new SqlCommand(sql, sc);


                using (SqlDataAdapter adapter = new SqlDataAdapter(com))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    //dataGrid.ItemsSource = dt.DefaultView;
                    TextBlock text = new TextBlock();
                    text.Text = dt.DefaultView.ToString();
                    //this.wrapPanelDiscos.Children.Add(text);
                }



            }

        }

        private void pesquisar_Click(object sender, RoutedEventArgs e)
        {
            pesquisarCD();
        }

        private void wpd_MouseLeftButtonUp(object sender, RoutedEventArgs e)
        {
            // codigo se cliacado (editar cd)
            MessageBox.Show(sender.GetType().ToString());
        }

        private void pesquisarCD()
        {

            //pesquisa por codigo disco
            if (comboBox.Text == "Código")
            {
                SqlConnection conn = ConnectionHelper.GetConnection();
                WrapPanel wpd = new WrapPanel();
                wpd.Height = 300;
                wpd.Width = 300;
                Thickness margin = wpd.Margin;
                margin.Left = 30;
                wpd.Margin = margin;
                using (SqlCommand cmd = new SqlCommand("pesquisaDiscos", conn))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@pesquisa", disco_pesq.Text);

                    cmd.Parameters.Add("@preço", SqlDbType.Money);
                    cmd.Parameters["@preço"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@titulo", SqlDbType.VarChar, 30);
                    cmd.Parameters["@titulo"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Ano", SqlDbType.Int);
                    cmd.Parameters["@Ano"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@artista", SqlDbType.VarChar, 30);
                    cmd.Parameters["@artista"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@genero", SqlDbType.VarChar, 30);
                    cmd.Parameters["@genero"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@stock", SqlDbType.Int);
                    cmd.Parameters["@stock"].Direction = ParameterDirection.Output;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    /*
                    artista.Content = "Artista: " + cmd.Parameters["@artista"].Value.ToString();
                    titulo.Content = "Título: " + cmd.Parameters["@titulo"].Value.ToString();
                    ano.Content = "Ano: " + cmd.Parameters["@ano"].Value.ToString();
                    genero.Content = "Género: " + cmd.Parameters["@genero"].Value.ToString();
                    preço.Content = "Preço: " + cmd.Parameters["@preço"].Value.ToString() + "€";
                    stock.Content = "Stock: " + cmd.Parameters["@stock"].Value.ToString();
                    */

                    Label artista = new Label();
                    Label titulo = new Label();
                    Label ano = new Label();
                    Label genero = new Label();
                    Label preço = new Label();
                    Label stock = new Label();

                    artista.Content = cmd.Parameters["@artista"].Value.ToString() + ":";
                    titulo.Content = cmd.Parameters["@titulo"].Value.ToString();
                    ano.Content = "Ano: " + cmd.Parameters["@ano"].Value.ToString();
                    genero.Content = "Género: " + cmd.Parameters["@genero"].Value.ToString();
                    preço.Content = "Preço: " + cmd.Parameters["@preço"].Value.ToString() + "€";
                    stock.Content = "Stock: " + cmd.Parameters["@stock"].Value.ToString();

                    wpd.Children.Add(artista);
                    wpd.Children.Add(titulo);
                    wpd.Children.Add(ano);
                    wpd.Children.Add(genero);
                    wpd.Children.Add(preço);
                    wpd.Children.Add(stock);
                    
                }
                //IMAGEM DISCO

                conn.Open();
                string myquery = ("SELECT imagemDisco, id_disco FROM Discos WHERE id_disco= '" + disco_pesq.Text + "';");
                SqlDataAdapter dataAdapter = new SqlDataAdapter(new SqlCommand(myquery, conn));
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);
                if (dataSet.Tables[0].Rows.Count == 1)
                {
                    Byte[] data = new Byte[0];
                    data = (Byte[])(dataSet.Tables[0].Rows[0]["imagemDisco"]);
                    MemoryStream mem = new MemoryStream(data);
                    var imageSource = new BitmapImage();
                    imageSource.BeginInit();
                    imageSource.StreamSource = mem;
                    imageSource.EndInit();
                    Image image = new Image();
                    image.Source = imageSource;
                    wpd.Children.Add(image);

                }
                this.wrapPanelDiscos.Children.Add(wpd);
            }
            //pesquisa por ano
            else if (comboBox.Text == "Ano")    // tem de se acrescentar um for. a cada iteração adiciona um novo wpd
            {
                WrapPanel wpd = new WrapPanel();
                wpd.Height = 300;
                wpd.Width = 300;
                Thickness margin = wpd.Margin;
                margin.Left = 30;
                wpd.Margin = margin;
                SqlConnection conn = ConnectionHelper.GetConnection();

                using (SqlCommand cmd = new SqlCommand("pesquisaDiscosAno", conn))
                {
                    
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@pesquisa", disco_pesq.Text);

                    cmd.Parameters.Add("@preço", SqlDbType.Money);
                    cmd.Parameters["@preço"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@titulo", SqlDbType.VarChar, 30);
                    cmd.Parameters["@titulo"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Ano", SqlDbType.Int);
                    cmd.Parameters["@Ano"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@artista", SqlDbType.VarChar, 30);
                    cmd.Parameters["@artista"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@genero", SqlDbType.VarChar, 30);
                    cmd.Parameters["@genero"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@stock", SqlDbType.Int);
                    cmd.Parameters["@stock"].Direction = ParameterDirection.Output;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    /*
                    artista.Content = "Artista: " + cmd.Parameters["@artista"].Value.ToString();
                    titulo.Content = "Título: " + cmd.Parameters["@titulo"].Value.ToString();
                    ano.Content = "Ano: " + cmd.Parameters["@ano"].Value.ToString();
                    genero.Content = "Género: " + cmd.Parameters["@genero"].Value.ToString();
                    preço.Content = "Preço: " + cmd.Parameters["@preço"].Value.ToString() + "€";
                    stock.Content = "Stock: " + cmd.Parameters["@stock"].Value.ToString();
                    */

                    Label artista = new Label();
                    Label titulo = new Label();
                    Label ano = new Label();
                    Label genero = new Label();
                    Label preço = new Label();
                    Label stock = new Label();

                    artista.Content = cmd.Parameters["@artista"].Value.ToString() + ":";
                    titulo.Content = cmd.Parameters["@titulo"].Value.ToString();
                    ano.Content = "Ano: " + cmd.Parameters["@ano"].Value.ToString();
                    genero.Content = "Género: " + cmd.Parameters["@genero"].Value.ToString();
                    preço.Content = "Preço: " + cmd.Parameters["@preço"].Value.ToString() + "€";
                    stock.Content = "Stock: " + cmd.Parameters["@stock"].Value.ToString();

                   

                    wpd.Children.Add(artista);
                    wpd.Children.Add(titulo);
                    wpd.Children.Add(ano);
                    wpd.Children.Add(genero);
                    wpd.Children.Add(preço);
                    wpd.Children.Add(stock); 

                }
                //IMAGEM DISCO

                conn.Open();
                string myquery = ("SELECT imagemDisco, ano FROM Discos WHERE ano= '" + disco_pesq.Text + "';");
                SqlDataAdapter dataAdapter = new SqlDataAdapter(new SqlCommand(myquery, conn));
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);
                if (dataSet.Tables[0].Rows.Count == 1) ////////////////////////////
                {
                    Byte[] data = new Byte[0];
                    data = (Byte[])(dataSet.Tables[0].Rows[0]["imagemDisco"]);
                    MemoryStream mem = new MemoryStream(data);
                    var imageSource = new BitmapImage();
                    imageSource.BeginInit();
                    imageSource.StreamSource = mem;
                    imageSource.EndInit();
                    Image image = new Image();
                    image.Source = imageSource;
                    
                    wpd.Children.Add(image);

                }
                wpd.MouseLeftButtonUp += wpd_MouseLeftButtonUp;
                this.wrapPanelDiscos.Children.Add(wpd);
            }
        }

        
    }

}


/*using (SqlConnection sc = ConnectionHelper.GetConnection())
            {
                sc.Open();
                string sql = "Select * FROM Pessoa As P JOIN Cliente As C ON P.nif = C.nif";
                SqlCommand com = new SqlCommand(sql, sc);
            

                using (SqlDataAdapter adapter = new SqlDataAdapter(com))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGrid.ItemsSource = dt.DefaultView;
                }



            }
*/