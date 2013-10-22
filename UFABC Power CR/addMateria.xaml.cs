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
    public partial class addMateria : PhoneApplicationPage
    {
        List<banco_de_dados_local.Model.Materia> materias;
        List<string> nomes;
        public addMateria()
        {
            InitializeComponent();
            materias = App.db.Materias.ToList();
            List<string> profsNomes = new List<string>();
            List<banco_de_dados_local.Model.Professor> profs = App.db.Professores.ToList();
            foreach (banco_de_dados_local.Model.Professor prof in profs)
            {
                profsNomes.Add(prof.Nome);
            }
            tboxProf.ItemsSource = profsNomes;
            if ((App.Current as App).edita == true)
            {
                tboxMateria.IsEnabled = false;
                tbTitulo.Text = "editar disciplina cursada";
                tboxCreditos.Text = (App.Current as App).Hist.Credito.ToString();
                tboxConceito.Text = (App.Current as App).Hist.Conceito;
                listTipo.SelectedIndex = (App.Current as App).Hist.Tipo;
                tboxMateria.Text = (App.Current as App).Hist.Materia;
                tboxProf.Text = (App.Current as App).Hist.Professor;
                tboxSite.Text = (App.Current as App).Hist.Site;
            }
            else
            {
               
                nomes = new List<string>();
                int aux = 0;
                foreach (banco_de_dados_local.Model.Materia materia in materias)
                {
                    nomes.Add(materia.Nome);
                    aux++;
                }

                tboxMateria.ItemsSource = nomes;
            }

        }

        private void btSalvar_Click_1(object sender, EventArgs e)
        {
            if (tboxMateria.Text.Equals(""))
            {
                MessageBox.Show("Digite o nome da disciplina");
            }
            else if (tboxCreditos.Text.Trim().Equals("") || tboxCreditos.Text.Equals(".") || tboxCreditos.Text.Equals("0") || tboxCreditos.Text.Equals(",") || tboxCreditos.Text.Equals("-"))
            {
                MessageBox.Show("Digite o número de créditos da disciplina");
                tboxCreditos.Text = "";
            }
            else if ((App.Current as App).edita == false && nomes.Contains(tboxMateria.Text) == false)
            {
                MessageBox.Show("Escolha uma disciplina existente");
                tboxMateria.Text = "";
            }
            else
            {
                int creditos = int.Parse(tboxCreditos.Text);
                string conceito = tboxConceito.Text.ToUpper();
                if (conceito == "A" || conceito == "B" || conceito == "C" || conceito == "D" || conceito == "E" || conceito == "F" || conceito == "O" || conceito == "")
                {
                    if ((App.Current as App).edita == false)
                    {
                        App.db.Historicos.InsertOnSubmit(new banco_de_dados_local.Model.Historico
                        {
                            Materia = tboxMateria.Text,
                            Tipo = listTipo.SelectedIndex,
                            Quadrimestre = (App.Current as App).quadrimestre,
                            Credito = creditos,
                            Conceito = conceito,
                            Professor = tboxProf.Text,
                            Site = tboxSite.Text
                        });


                        if ((App.Current as App).quadrimestre == (App.Current as App).quadriAtual)
                        {
                            if (App.ViewModel.MateriasNoQuad != null)
                            {
                                App.ViewModel.MateriasNoQuad.Add(new banco_de_dados_local.Model.Historico
                                {
                                    Materia = tboxMateria.Text,
                                    Tipo = listTipo.SelectedIndex,
                                    Quadrimestre = (App.Current as App).quadrimestre,
                                    Credito = creditos,
                                    Conceito = conceito,
                                    Professor = tboxProf.Text,
                                    Site = tboxSite.Text
                                });
                            }
                            else
                            {
                                App.ViewModel.MateriasNoQuad = new System.Collections.ObjectModel.ObservableCollection<banco_de_dados_local.Model.Historico>();
                                App.ViewModel.MateriasNoQuad.Add(new banco_de_dados_local.Model.Historico
                                {
                                    Materia = tboxMateria.Text,
                                    Tipo = listTipo.SelectedIndex,
                                    Quadrimestre = (App.Current as App).quadrimestre,
                                    Credito = creditos,
                                    Conceito = conceito,
                                    Professor = tboxProf.Text,
                                    Site = tboxSite.Text
                                });
                            }
                        }
                    }
                    else
                    {
                        var query = from banco_de_dados_local.Model.Historico hist in App.db.Historicos where hist.HistId == (App.Current as App).Hist.HistId select hist;

                        foreach (banco_de_dados_local.Model.Historico hist in query)
                        {
                            hist.Materia = tboxMateria.Text;
                            hist.Tipo = listTipo.SelectedIndex;
                            hist.Quadrimestre = (App.Current as App).quadrimestre;
                            hist.Credito = creditos;
                            hist.Conceito = conceito;
                            hist.Professor = tboxProf.Text;
                            hist.Site = tboxSite.Text;
                            (App.Current as App).edita = false;
                        }
                    }

                    if (tboxProf.Text.Trim().Equals("") == false && App.db.Professores.Any(x => x.Nome == tboxProf.Text) == false)
                    {
                        App.db.Professores.InsertOnSubmit(new banco_de_dados_local.Model.Professor { Nome = tboxProf.Text });
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
                    NavigationService.Navigate(new Uri("/Quadrimestre.xaml", UriKind.Relative));
                }

                else
                {
                    MessageBox.Show("Digite um conceito válido ou deixe em branco para matéria não cursada");
                    tboxConceito.Text = "";
                }
            }

        }


        private void btCancelar_Click_1(object sender, EventArgs e)
        {
            (App.Current as App).edita = false;
            NavigationService.GoBack();
        }



        private void tboxMateria_TextChanged(object sender, RoutedEventArgs e)
        {
            if ((App.Current as App).edita == false)
            {
                if (nomes.Contains(tboxMateria.Text))
                {
                    banco_de_dados_local.Model.Materia mat = materias.Single(m => m.Nome == tboxMateria.Text);
                    if (mat.Teoria != 0)
                    {
                        tboxCreditos.Text = (mat.Teoria + mat.Pratica).ToString();
                    }
                }
                else
                {
                    listTipo.SelectedIndex = 0;
                    tboxCreditos.Text = "";
                }
            }
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            if ((App.Current as App).edita == true)
            {
                NavigationService.RemoveBackEntry();
            }
        }

     

    }
}