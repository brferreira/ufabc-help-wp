using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using banco_de_dados_local.Model;

namespace UFABC_Power_CR
{
    public partial class NomeQuadri : PhoneApplicationPage
    {
        public NomeQuadri()
        {
            InitializeComponent();
            tbTitulo.Text = "quadrimestre " + (App.Current as App).quadrimestre.ToString();
        }

        private void btSalvar_Click_1(object sender, EventArgs e)
        {
            if (tboxNome.Text != "")
            {
                var query1 = from banco_de_dados_local.Model.Quadrimestre quadri in App.db.Quadrimestres where quadri.Numero == (App.Current as App).quadrimestre select quadri;
                foreach (banco_de_dados_local.Model.Quadrimestre quadri in query1)
                {
                    quadri.Nome = tboxNome.Text;
                }
                App.db.SubmitChanges();
                NavigationService.RemoveBackEntry();
                NavigationService.RemoveBackEntry();
                NavigationService.Navigate(new Uri("/Historico.xaml", UriKind.Relative));
            }
            else
            {
                MessageBox.Show("Digite um nome");
            }
        }

        private void btCancelar_Click_1(object sender, EventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}