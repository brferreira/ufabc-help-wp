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
using Newtonsoft.Json;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace UFABC_Power_CR
{

    public class Prof
    {
        public string nome, email, centro, local, sala, telefone, nr_avaliacoes;
        public float exigencia, avaliacao, didatica, dominio, organizacao, disponibilidade, relacionamento;
        public double cr_alunos, cr_professor;
    }
    public class Comentario
    {
        public string comentario, nm_disciplina, dc_quad, nm_conceito, is_trancado;
    }
    public class comment
    {
        public string Texto {get;set;}
        public string Materia {get; set;}
        public string Quadrimestre {get; set;}
        public string Conceito { get; set; }
        public string Pasta { get; set; }
        public string Calendario { get; set; }
        public string Lapis { get; set; }

    }

    public partial class Professor : PhoneApplicationPage
    {
        int id;
        ProgressIndicator prog;
        public Professor()
        {
            InitializeComponent();
            banco_de_dados_local.Model.Professor prof = App.db.Professores.Single(x => x.Nome == App.professor);
            tbNome.Text = prof.Nome.ToUpper();
            id = prof.helpId;
            if (prof.Sala != null)
            {
                tbCentro.Text = prof.Centro + ", Sala: " + prof.Sala + " (" + prof.Local + ")";
            }

            if (prof.Email != null)
            {
                if (prof.Email.Equals("") == false)
                {
                    tbEmail.Text = prof.Email;
                    ApplicationBarIconButton a = (ApplicationBarIconButton)ApplicationBar.Buttons[0];
                    a.IsEnabled = true;
                }
            }
            if (prof.Telefone != null)
            {
                if (prof.Telefone.Equals("") == false)
                {
                    tbTel.Text = prof.Telefone;
                    ApplicationBarIconButton a = (ApplicationBarIconButton)ApplicationBar.Buttons[1];
                    a.IsEnabled = true;
                }
            }
        }

        private void tbTel_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            telefonar();
        }

        private void btEmail_Click(object sender, EventArgs e)
        {
            email();
        }

        private void btTel_Click(object sender, EventArgs e)
        {
            telefonar();
        }

        private void email()
        {
            Microsoft.Phone.Tasks.EmailComposeTask email = new Microsoft.Phone.Tasks.EmailComposeTask();
            email.To = tbEmail.Text;
            email.Show();
        }

        private void telefonar()
        {
            Microsoft.Phone.Tasks.PhoneCallTask ligacao = new Microsoft.Phone.Tasks.PhoneCallTask();
            ligacao.PhoneNumber = tbTel.Text;
            ligacao.DisplayName = tbNome.Text;
            ligacao.Show();
        }



        void wc_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                if (e.Result != null)
                {
                    Prof prof = JsonConvert.DeserializeObject<Prof>(e.Result);
                    tbEmail.Text = prof.email;
                    tbTel.Text = prof.telefone;
                    tbCentro.Text = prof.centro + ", Sala: " + prof.sala + " (" + prof.local + ")";
                    tbCR_Alunos.Text = Math.Round(prof.cr_alunos, 3).ToString();
                    tbCR_Prof.Text = Math.Round(prof.cr_professor, 3).ToString();
                    tbN_Avaliacoes.Text = prof.nr_avaliacoes;
                    rtExigencia.Value = prof.exigencia;
                    rtAvaliacao.Value = prof.avaliacao;
                    rtDidatica.Value = prof.didatica;
                    rtAprofundamento.Value = prof.dominio;
                    rtOrganizacao.Value = prof.organizacao;
                    rtDisponibilidade.Value = prof.disponibilidade;
                    rtRelacionamento.Value = prof.relacionamento;
                    var query = from banco_de_dados_local.Model.Professor profX in App.db.Professores where profX.helpId == id select profX;
                    foreach (banco_de_dados_local.Model.Professor profX in query)
                    {
                        profX.Sala = prof.sala;
                        profX.Telefone = prof.telefone;
                        profX.Email = prof.email;
                        profX.Local = prof.local;
                        profX.Centro = prof.centro;
                    }
                    App.db.SubmitChanges();
                    ApplicationBarIconButton btEmail = (ApplicationBarIconButton)ApplicationBar.Buttons[0];
                    btEmail.IsEnabled = true;
                    ApplicationBarIconButton btTel = (ApplicationBarIconButton)ApplicationBar.Buttons[1];
                    btTel.IsEnabled = true;
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }

        void wc_DownloadStringCompleted2(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                if (e.Result != null)
                {
                    List<Comentario> comentarios = JsonConvert.DeserializeObject<List<Comentario>>(e.Result);
                    List<comment> comments = new List<comment>();
                    Visibility darkBackgroundVisibility = (Visibility)Application.Current.Resources["PhoneDarkThemeVisibility"];
                    string pasta, lapis, calendario;
                    if (darkBackgroundVisibility == Visibility.Visible)
                    {
                        pasta = "/Imagens/folder_dark.png";
                        lapis = "/Imagens/edit.png";
                        calendario = "/Imagens/feature.calendar_dark.png";
                    }
                    else
                    {
                        pasta = "/Imagens/folder.png";
                        lapis = "/Imagens/edit_light.png";
                        calendario = "/Imagens/feature.calendar.png";
                    }
                    foreach (Comentario comentario in comentarios)
                    {
                        string conceito;
                        if(comentario.is_trancado == "0")
                        {
                            conceito ="Conceito " + comentario.nm_conceito;
                        }
                        else
                        {
                            conceito = "Trancamento";
                        }
                        comments.Add(new comment{Texto = comentario.comentario, Materia = comentario.nm_disciplina, Conceito = conceito, Quadrimestre = comentario.dc_quad, Calendario = calendario, Lapis = lapis, Pasta = pasta });                       
                    }
                    lbComentarios.DataContext = comments;

                }
                prog.IsVisible = false;
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }

        private void baixar_info()
        {
            try
            {
                WebClient wc = new WebClient();
                wc.DownloadStringAsync(
                   new Uri("http://www.ufabchelp.me/painel/servicos/professor.php?id=" + id.ToString()));
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

        private void baixar_comentarios()
        {
            try
            {
                WebClient wc = new WebClient();
                wc.DownloadStringAsync(
                   new Uri("http://www.ufabchelp.me/painel/servicos/professor-comentario.php?id=" + id.ToString()));
                wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(wc_DownloadStringCompleted2);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (App.fblogin == true)
            {
                if (id != 0)
                {
                    if (Microsoft.Phone.Net.NetworkInformation.NetworkInterface.NetworkInterfaceType != Microsoft.Phone.Net.NetworkInformation.NetworkInterfaceType.None)
                    {
                        baixar_info();
                        baixar_comentarios();
                    }
                }
                else
                {
                    MessageBox.Show("Não é possível obter informações sobre o professor, pois ele não está cadastrado no banco de dados do UFABC Help!");
                    ApplicationBarMenuItem editar = (ApplicationBarMenuItem)ApplicationBar.MenuItems[0];
                    ApplicationBarMenuItem excluir = (ApplicationBarMenuItem)ApplicationBar.MenuItems[1];
                    editar.IsEnabled = true;
                    excluir.IsEnabled = true;
                }
            }
            else
            {
                MessageBox.Show("Você não está logado ao sistema do \"UFABC Help!\", faça login no facebook no menu principal para acessar este conteúdo");
            }
        }

        private void btEdita_Click(object sender, EventArgs e)
        {
            (App.Current as App).edita = true;
            App.professor = tbNome.Text;
            NavigationService.Navigate(new Uri("/addProf.xaml", UriKind.Relative));
        }

        private void btExclui_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja mesmo apagar o(a) professor(a)?", "Aviso", MessageBoxButton.OKCancel).Equals(MessageBoxResult.OK))
            {
                App.db.Professores.DeleteOnSubmit(App.db.Professores.Single(x => x.Nome == App.professor));
                App.db.SubmitChanges();
                NavigationService.RemoveBackEntry();
                NavigationService.Navigate(new Uri("/Professores.xaml", UriKind.Relative));
            }
        }



    }
}