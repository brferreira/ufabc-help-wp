using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace UFABC_Power_CR
{
    public partial class Professores : PhoneApplicationPage
    {
        List<banco_de_dados_local.Model.Professor> profes;
        public Professores()
        {
            InitializeComponent();
            if (App.db.Professores.Count() > 0)
            {
                profes = App.db.Professores.ToList();
                foreach (banco_de_dados_local.Model.Professor prof in profes.OrderBy(x=> x.Nome))
                {
                    lbProfessores.Items.Add(prof.Nome);
                }
            }
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (NavigationService.BackStack.First().Source.ToString().Equals("/addProf.xaml") || NavigationService.BackStack.First().Source.ToString().Equals("/Professor.xaml"))
                {
                    NavigationService.RemoveBackEntry();
                }
            }
            catch { }
        }

        private void lbProfessores_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (lbProfessores.SelectedIndex != -1)
            {
                App.professor = lbProfessores.SelectedItem.ToString();
                NavigationService.Navigate(new Uri("/Professor.xaml", UriKind.Relative));
            }
        }


        private void tbPesquisa_TextChanged(object sender, TextChangedEventArgs e)
        {
            lbProfessores.Items.Clear();
            foreach(banco_de_dados_local.Model.Professor prof in profes.Where(x=> x.Nome.ToLower().Contains(tbPesquisa.Text.ToLower())))
            {
                lbProfessores.Items.Add(prof.Nome);
            }
            if (tbPesquisa.Text == "")
            {
                tbTitulo.Visibility = System.Windows.Visibility.Visible;
                tbPesquisa.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private void btPesquisa_Click(object sender, EventArgs e)
        {
            tbTitulo.Visibility = System.Windows.Visibility.Collapsed;
            tbPesquisa.Visibility = Visibility.Visible;
            tbPesquisa.Focus();
        }

        private void tbPesquisa_LostFocus(object sender, RoutedEventArgs e)
        {
            if (tbPesquisa.Text == "")
            {
                tbPesquisa.Visibility = System.Windows.Visibility.Collapsed;
                tbTitulo.Visibility = System.Windows.Visibility.Visible;
            }
        }

    }
}