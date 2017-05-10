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
            carregarCds();
        }


        public void carregarCds()
        {
            for (int i = 0; i < 3; i++)
            {
                StackPanel sp = new StackPanel();
                sp.Orientation = Orientation.Vertical;

                Image Img = new Image();
                Img.Source = new BitmapImage(new Uri("C:/Users/Filipe/Desktop/UA/3ano/ihc/projeto/LojaDiscos/LojaDiscos/Images/icon.png"));
                //Image resizedImage = new Bitmap(image, new Size(50, 50));
                TextBlock text = new TextBlock();
                text.Text = "TEste " + i + " !";

                sp.Children.Add(Img);
                sp.Children.Add(text);

                this.wrapPanelDiscos.Children.Add(sp);
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