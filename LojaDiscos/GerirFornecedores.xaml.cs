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
    /// Interaction logic for GerirFornecedores.xaml
    /// </summary>
    public partial class GerirFornecedores : Page
    {
        public GerirFornecedores()
        {
            InitializeComponent();
        }

   
        private void criarFichaFornecedor_Click(object sender, RoutedEventArgs e)
        {
            CriarFichaFornecedor criarFichaFornecedor = new CriarFichaFornecedor();
            this.NavigationService.Navigate(criarFichaFornecedor);
        }

        private void pesquisar_Click(object sender, RoutedEventArgs e)
        {
            //ListSearch();
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
    }
}
