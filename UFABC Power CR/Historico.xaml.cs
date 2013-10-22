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
    public partial class Historico : PhoneApplicationPage
    {

        public Historico()
        {
            InitializeComponent();

            var query2 = from banco_de_dados_local.Model.Quadrimestre quadri in App.db.Quadrimestres select quadri;
            int i = 0;
            foreach (banco_de_dados_local.Model.Quadrimestre quadri in query2)
            {
                ListBoxItem a = new ListBoxItem();
                a.Content = quadri.Nome;
                lbQuadrimestres.Items.Add(a);
                TurnstileFeatherEffect.SetFeatheringIndex(a, i);
                i++;
            }
           
            
        }


        private void lbQuadrimestres_Tap_1(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (lbQuadrimestres.SelectedIndex != -1)
            {
                (App.Current as App).quadrimestre = lbQuadrimestres.SelectedIndex + 1;
                NavigationService.Navigate(new Uri("/Quadrimestre.xaml", UriKind.Relative));
            }
        }

        private void btNovoQuad_Click_1(object sender, EventArgs e)
        {

                if ((App.Current as App).quadriAtual < 21)
                {
                    var query = from banco_de_dados_local.Model.Aluno aluno in App.db.Aluno select aluno;
                    foreach (banco_de_dados_local.Model.Aluno aluno in query)
                    {
                        aluno.QuadriAtual += 1;
                        (App.Current as App).quadriAtual += 1;
                        App.db.Quadrimestres.InsertOnSubmit(new banco_de_dados_local.Model.Quadrimestre { Numero = (App.Current as App).quadriAtual, Nome = "quadrimestre " + (App.Current as App).quadriAtual.ToString() });
                        ListBoxItem a = new ListBoxItem();
                        a.Content = "quadrimestre " + aluno.QuadriAtual.ToString();
                        lbQuadrimestres.Items.Add(a);
                        App.db.SubmitChanges();
                        lbQuadrimestres.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Número máximo de quadrimestres atingido");
                }
                            
        }

        private void PhoneApplicationPage_Loaded_1(object sender, RoutedEventArgs e)
        {
            if (NavigationService.BackStack.First().Source.ToString().Equals("/Quadrimestre.xaml"))
            {
                NavigationService.RemoveBackEntry();
            }
        }

        private void lbQuadrimestres_Hold_1(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (lbQuadrimestres.SelectedIndex != -1)
            {
                (App.Current as App).quadrimestre = lbQuadrimestres.SelectedIndex + 1;
                NavigationService.Navigate(new Uri("/NomeQuadri.xaml", UriKind.Relative));
            }
        }

        private void itemApagaTudo_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja mesmo apagar o histórico?", "Apagar", MessageBoxButton.OKCancel).Equals(MessageBoxResult.OK))
            {

                var query = from Aluno aluno in App.db.Aluno select aluno;
                var query2 = from banco_de_dados_local.Model.Historico hist in App.db.Historicos select hist;
                var query3 = from banco_de_dados_local.Model.Quadrimestre quad in App.db.Quadrimestres select quad;
                foreach (Aluno aluno in query)
                {
                    aluno.QuadriAtual = 0;
                    (App.Current as App).quadriAtual = 0;
                }
                foreach (banco_de_dados_local.Model.Historico hist in query2)
                {
                    App.db.Historicos.DeleteOnSubmit(hist);
                }
                foreach (banco_de_dados_local.Model.Quadrimestre quad in query3)
                {
                    App.db.Quadrimestres.DeleteOnSubmit(quad);
                }
                App.db.Aluno.First().CA = 0;
                App.db.Aluno.First().CR = 0;
                App.db.Aluno.First().CP = 0;
                App.db.SubmitChanges();
                lbQuadrimestres.Items.Clear();
            }
            
        }

        private void itemProf_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Professores.xaml", UriKind.Relative));
        }

       

      
    }
}