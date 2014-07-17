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
using Microsoft.Phone.Tasks;


namespace UFABC_Power_CR
{
    public partial class Materia : PhoneApplicationPage
    {
        public Materia()
        {
            InitializeComponent();                
            tbNome.Text = (App.Current as App).Hist.Materia;
            tbCreditos.Text = "Créditos: " + (App.Current as App).Hist.Credito.ToString();
            tbConceito.Text = "Conceito: " + (App.Current as App).Hist.Conceito.ToString();
            tbProfessor.Text = (App.Current as App).Hist.Professor;
            tbSite.Text = (App.Current as App).Hist.Site;
            tbSite.TextDecorations = TextDecorations.Underline;
            if((App.Current as App).edita == true)
            {
                (App.Current as App).edita = false;
            }
           
        }

        

        private void btApagar_Click_1(object sender, EventArgs e)
        {
            var query = from banco_de_dados_local.Model.Historico hist in App.db.Historicos where hist.Quadrimestre == (App.Current as App).quadrimestre select hist;
            foreach (banco_de_dados_local.Model.Historico hist in query)
            {
                if (hist.HistId == (App.Current as App).Hist.HistId)
                {
                    App.db.Historicos.DeleteOnSubmit(hist);
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
                    NavigationService.Navigate(new Uri("/Quadrimestre.xaml", UriKind.Relative));
                }
                    
            }
        }

        private void btEditar_Click_1(object sender, EventArgs e)
        {
            (App.Current as App).edita = true;
            NavigationService.Navigate(new Uri("/addMateria.xaml", UriKind.Relative));
        }


        private void tbSite_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (tbSite.Text != "")
            {
                WebBrowserTask webBrowserTask = new WebBrowserTask();
                if (tbSite.Text.StartsWith("http://") == true)
                {
                    webBrowserTask.Uri = new Uri(tbSite.Text, UriKind.Absolute);
                }
                else
                {
                    webBrowserTask.Uri = new Uri("http://"+tbSite.Text, UriKind.Absolute);
                }
                webBrowserTask.Show();
            }
        }

        private void tbProfessor_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (tbProfessor.Text != "")
            {
                if (App.db.Professores.Any(x => x.Nome == tbProfessor.Text) == true)
                {
                    if (App.db.Aluno.First().Tipo_Usuario == 1)
                    {
                        App.professor = tbProfessor.Text;
                        NavigationService.Navigate(new Uri("/Professor.xaml", UriKind.Relative));
                    }
                    else
                    {
                        MessageBox.Show("Você não tem permissão para acessar este conteúdo.");
                    }
                }
                else
                {
                    MessageBox.Show("Não existe um professor com esse nome no banco de dados", "Erro", MessageBoxButton.OK);
                }
            }
        }

        private void tbNome_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (App.db.Aluno.First().Tipo_Usuario == 1)
            {
                App.disciplina = tbNome.Text;
                NavigationService.Navigate(new Uri("/Disciplina.xaml", UriKind.Relative));
            }
            else
            {
                MessageBox.Show("Você não tem permissão para acessar este conteúdo.");
            }
        }
    }
}