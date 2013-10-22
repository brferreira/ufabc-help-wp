using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Newtonsoft.Json;

namespace UFABC_Power_CR
{

    public class disciplina
    {
        public string recomendacao, ementa, nr_avaliacoes, nr_dedicacao, nr_trabalhos, nr_requisitos;
        public int T, P, I;
        //public double;
    }

    public partial class Disciplina : PhoneApplicationPage
    {
        int id;
        ProgressIndicator prog;

        public Disciplina()
        {
            InitializeComponent();
            banco_de_dados_local.Model.Materia materia = App.db.Materias.Single(x => x.Nome == App.disciplina);
            id = materia.HelpId;
            tbNome.Text = App.disciplina.ToUpper();
            if (materia.Teoria != 0)
            {
                tbTPI.Text = "(" + materia.Teoria + "-" + materia.Pratica + "-" + materia.Individual + ")";
            }
        }

        private void baixar_info()
        {
            try
            {
                WebClient wc = new WebClient();
                wc.DownloadStringAsync(
                   new Uri("http://www.ufabchelp.me/painel/servicos/disciplina.php?id=" + id.ToString()));
                SystemTray.SetIsVisible(this, true);
                SystemTray.SetOpacity(this, 0);
                prog = new ProgressIndicator();
                prog.IsVisible = true;
                prog.Text = "baixando informações";
                prog.IsIndeterminate = true;
                SystemTray.SetProgressIndicator(this, prog);
                wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(wc_DownloadStringCompleted);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }


        void wc_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                if (e.Result != null)
                {
                    disciplina disc = JsonConvert.DeserializeObject<disciplina>(e.Result);
                    tbTPI.Text = "(" + disc.T + "-" + disc.P + "-" + disc.I + ")";
                    tbRecomendacao.Text = disc.recomendacao;
                    tbEmenta.Text = disc.ementa;
                    if (disc.nr_avaliacoes != null)
                    {
                        tbAvaliacoes.Text = disc.nr_avaliacoes + " avaliações";
                        rtDificuldade.Value = Double.Parse(disc.nr_dedicacao.Replace(".", ","));
                        int dific = (int) Math.Round(rtDificuldade.Value,0);
                        switch (dific)
                        {
                            case 0:
                                tbDificuldade.Text = "Nenhuma";
                                break;
                            case 1:
                                tbDificuldade.Text = "Nenhuma";
                                break;
                            case 2:
                                tbDificuldade.Text = "Dificuldade easy";
                                break;
                            case 3:
                                tbDificuldade.Text = "Dificuldade normal";
                                break;
                            case 4:
                                tbDificuldade.Text = "Dificuldade hard";
                                break;
                            case 5:
                                tbDificuldade.Text = "Dificuldade extreme";
                                break;
                        }
                        rtRequisitos.Value = Double.Parse(disc.nr_requisitos.Replace(".", ","));
                        int requisitos = (int) Math.Round(rtRequisitos.Value,0);
                        switch (requisitos)
                        {
                            case 0:
                                tbRequisitos.Text = "Que nada!";
                                break;
                            case 1:
                                tbRequisitos.Text = "Que nada!";
                                break;
                            case 2:
                                tbRequisitos.Text = "Não muito";
                                break;
                            case 3:
                                tbRequisitos.Text = "Mais ou menos";
                                break;
                            case 4:
                                tbRequisitos.Text = "Sim, mas não totalmente";
                                break;
                            case 5:
                                tbRequisitos.Text = "Totalmente";
                                break;
                        }
                        rtTrabalhos.Value = Double.Parse(disc.nr_trabalhos.Replace(".", ","));
                        int trabalhos = (int) Math.Round(rtTrabalhos.Value,0);
                        switch (trabalhos)
                        {
                            case 0:
                                tbTrabalhos.Text = "Nenhum";
                                break;
                            case 1:
                                tbTrabalhos.Text = "Nenhum";
                                break;
                            case 2:
                                tbTrabalhos.Text = "Poucas coisas";
                                break;
                            case 3:
                                tbTrabalhos.Text = "Quantidade razoável";
                                break;
                            case 4:
                                tbTrabalhos.Text = "Muita coisa para fazer";
                                break;
                            case 5:
                                tbTrabalhos.Text = "Praticamente namorei com a disciplina";
                                break;
                        }
                    }
                    var query = from banco_de_dados_local.Model.Materia discx in App.db.Materias where discx.HelpId == id select discx;
                    foreach (banco_de_dados_local.Model.Materia discX in query)
                    {
                        discX.Ementa = disc.ementa;
                        discX.Teoria = disc.T;
                        discX.Pratica = disc.P;
                        discX.Individual = disc.I;
                        discX.Recomendacao = disc.recomendacao;
                    }
                    App.db.SubmitChanges();
                }
                prog.IsVisible = false;
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
                prog.IsVisible = false;
            }
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            baixar_info();
        }

    }

}