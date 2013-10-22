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
    public partial class Disciplinas : PhoneApplicationPage
    {
        List<banco_de_dados_local.Model.Materia> disciplinas;
        public Disciplinas()
        {
            InitializeComponent();
            if (App.db.Materias.Count() > 0)
            {
                disciplinas = App.db.Materias.ToList();
                foreach (banco_de_dados_local.Model.Materia disc in disciplinas.OrderBy(x=> x.Nome) )
                {
                    lbDisciplinas.Items.Add(disc.Nome);
                }
            }
        }

        private void lbDisciplinas_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (lbDisciplinas.SelectedIndex != -1)
            {
                App.disciplina = lbDisciplinas.SelectedItem.ToString();
                NavigationService.Navigate(new Uri("/Disciplina.xaml", UriKind.Relative));
            }
        }

        private void tbPesquisa_TextChanged(object sender, TextChangedEventArgs e)
        {
            lbDisciplinas.Items.Clear();
            foreach (banco_de_dados_local.Model.Materia disc in disciplinas.Where(x => x.Nome.ToLower().Contains(tbPesquisa.Text.ToLower())))
            {
                lbDisciplinas.Items.Add(disc.Nome);
            }
            if (tbPesquisa.Text == "")
            {
                tbTitulo.Visibility = System.Windows.Visibility.Visible;
                tbPesquisa.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            
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