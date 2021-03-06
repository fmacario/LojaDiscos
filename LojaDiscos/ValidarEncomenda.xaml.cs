﻿using System;
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
    /// Interaction logic for ValidarEncomenda.xaml
    /// </summary>
    public partial class ValidarEncomenda : Page
    {
        public ValidarEncomenda()
        {
            InitializeComponent();
            List();
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

        private void pesquisa_Click(object sender, RoutedEventArgs e)
        {
            //SqlConnection con = ConnectionHelper.GetConnection();
            MessageBox.Show("Encomenda validada", "Sucesso!");
            Venda menu = new Venda();
            this.NavigationService.Navigate(menu);
        }
        private void List()
        {
            //SqlConnection con = ConnectionHelper.GetConnection();

            using (SqlConnection sc = ConnectionHelper.GetConnection())
            {
                sc.Open();
                string sql = "Select * FROM Encomendas as E JOIN Fornecedor as F ON E.nif_fornecedor=F.nif";
                SqlCommand com = new SqlCommand(sql, sc);

                using (SqlDataAdapter adapter = new SqlDataAdapter(com))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGrid.ItemsSource = dt.DefaultView;

            
                  
               

                    DataGridTextColumn qtd = new DataGridTextColumn();
                    Encomenda enc = new Encomenda();
                    int qtt = enc.getQuatidade();
                    qtd.Binding = new Binding(Convert.ToString(qtt));
                    qtd.Header = "Quantidade";
                    
                    dataGrid.Columns.Add(qtd);
                }

            }
        }

        private void dataGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;

            var workingWidth = dataGrid.ActualWidth - SystemParameters.VerticalScrollBarWidth; // take into account vertical scrollbar
            var col1 = 0.2;
            var col2 = 0.1;
            var col3 = 0.2;
            var col4 = 0.2;
            var col5 = 0.2;
            var col6 = 0.1;

            dataGrid.Columns[0].Width = workingWidth * col1;
            dataGrid.Columns[0].Header = "Todas as quantidades recebidas";
            dataGrid.Columns[1].Width = workingWidth * col2;
            dataGrid.Columns[1].Header = "Quantidade";
            dataGrid.Columns[2].Width = workingWidth * col3;
            dataGrid.Columns[2].Header = "ID Encomenda";
            dataGrid.Columns[3].Width = workingWidth * col4;
            dataGrid.Columns[3].Header = "Data Encomenda";
            dataGrid.Columns[4].Width = workingWidth * col5;
            dataGrid.Columns[4].Header = "NIF Fornecedor";
            dataGrid.Columns[5].Width = workingWidth * col6;
            dataGrid.Columns[5].Header = "ID Disco";
            dataGrid.Columns[6].Visibility = Visibility.Hidden;
        }
    }
}
