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
    public partial class Quadrimestre : PhoneApplicationPage
    {
        List<banco_de_dados_local.Model.Historico> hists = new List<banco_de_dados_local.Model.Historico>();
        public Quadrimestre()
        {
            InitializeComponent();
            var query = from banco_de_dados_local.Model.Historico hist in App.db.Historicos where hist.Quadrimestre == (App.Current as App).quadrimestre select hist;
            var query2 = from banco_de_dados_local.Model.Quadrimestre quadri in App.db.Quadrimestres where quadri.Numero == (App.Current as App).quadrimestre select quadri;
            foreach (banco_de_dados_local.Model.Historico hist in query)
            {
                TextBlock tb = new TextBlock();
                tb.Text = hist.Materia;
                tb.TextWrapping = TextWrapping.Wrap;
                lbMaterias.Items.Add(tb);
                hists.Add(hist);
            }
            foreach (banco_de_dados_local.Model.Quadrimestre quadri in query2)
            {
                lbQuadri.Text = quadri.Nome;
                tbCR.Text = "CR do quadrimestre: " + quadri.CR.ToString();
            }
            
            
        }

        private void btAddMateria_Click_1(object sender, EventArgs e)
        {
            (App.Current as App).edita = false;
            NavigationService.Navigate(new Uri("/addMateria.xaml",UriKind.Relative));
               
        }

        private void lbMaterias_Tap_1(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (lbMaterias.SelectedIndex >= 0)
            {
                (App.Current as App).Hist = hists.ElementAt(lbMaterias.SelectedIndex);
                NavigationService.Navigate(new Uri("/Materia.xaml", UriKind.Relative));
            }
        }

        private void PhoneApplicationPage_Loaded_1(object sender, RoutedEventArgs e)
        {
            if (NavigationService.BackStack.First().Source.ToString().Equals("/addMateria.xaml") || NavigationService.BackStack.First().Source.ToString().Equals("/NomeQuadri.xaml") || NavigationService.BackStack.First().Source.ToString().Equals("/Materia.xaml"))
            {
                NavigationService.RemoveBackEntry();
            }

            if ((App.Current as App).quadriAtual == (App.Current as App).quadrimestre)
            {
                ApplicationBarIconButton b = (ApplicationBarIconButton)ApplicationBar.Buttons[2];
                b.IsEnabled = true;
            }
            
        }

        private void btDelQuad_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja mesmo apagar o quadrimestre?", "Aviso", MessageBoxButton.OKCancel).Equals(MessageBoxResult.OK))
            {
                var query = from Aluno aluno in App.db.Aluno select aluno;
                var query2 = from banco_de_dados_local.Model.Historico hist in App.db.Historicos where hist.Quadrimestre == (App.Current as App).quadrimestre select hist;
                var query3 = from banco_de_dados_local.Model.Quadrimestre quad in App.db.Quadrimestres where quad.Numero == (App.Current as App).quadrimestre select quad;
                foreach (Aluno aluno in query)
                {
                    aluno.QuadriAtual -= 1;
                    (App.Current as App).quadriAtual -= 1;
                }
                foreach (banco_de_dados_local.Model.Historico hist in query2)
                {
                    App.db.Historicos.DeleteOnSubmit(hist);
                }
                foreach (banco_de_dados_local.Model.Quadrimestre quad in query3)
                {
                    App.db.Quadrimestres.DeleteOnSubmit(quad);
                }
                App.db.SubmitChanges();
                banco_de_dados_local.ViewModel.ToDoViewModel a = new banco_de_dados_local.ViewModel.ToDoViewModel();
                CR ca = new CR(a.CarregaMateriasMaiorConceito());
                ca.salvaCA();
                CR cr = new CR(a.CarregaMaterias(true));
                cr.salvaCR(0);
                CR crQuadri = new CR(a.CarregaMaterias((App.Current as App).quadrimestre));
                crQuadri.salvaCR((App.Current as App).quadrimestre);
                CP cp = new CP(a.CarregaMaterias(false), App.aluno.Curso, App.aluno.AnoIngresso);
                NavigationService.RemoveBackEntry();
                NavigationService.Navigate(new Uri("/Historico.xaml", UriKind.Relative));

            }
        }

        private void btEdita_Click_1(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/NomeQuadri.xaml", UriKind.Relative));
        }


















    }
}