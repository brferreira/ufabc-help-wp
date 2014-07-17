using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using banco_de_dados_local.Model;
using Newtonsoft.Json;
using Microsoft.Phone.Shell;
using System.IO;


namespace UFABC_Power_CR
{

    public class gradeHelp
    {
        public string disciplina, turma, codigo, prof_teoria, prof_pratica, hr1_inicio, hr1_fim, hr1_local, hr2_inicio, hr2_fim, hr2_local, hr3_inicio, hr3_fim, hr3_local, lab1_inicio, lab1_fim, lab1_local, lab2_inicio, lab2_fim, lab2_local, obs;
        public int? hr1_dia, hr2_dia, hr3_dia, lab1_dia, lab2_dia;
    }

    public partial class Grade : PhoneApplicationPage
    {
        static int i;
        List<int> ids;
        public List<banco_de_dados_local.Model.Grade> HorariosSeg, HorariosTer, HorariosQua, HorariosQui, HorariosSex, HorariosSab, HorariosDom;
        //List<object> cbs = new List<object>();
        List<CheckBox> bts = new List<CheckBox>();
        ProgressIndicator prog;

        public Grade()
        {
            InitializeComponent();
            ids = new List<int>();
            i = 1;
          
            if (App.fblogin == false || App.aluno.Tipo_Usuario != 0) //|| App.tipo_usuario != 1)
            {
                ApplicationBarIconButton baixar = (ApplicationBarIconButton)ApplicationBar.Buttons[1];
                baixar.IsEnabled = false;
            }
        }

        private void btNovoHorario_Click_1(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/NovoHorario.xaml", UriKind.Relative));
        }

        private void CheckBox_Checked_1(object sender, RoutedEventArgs e)
        {
            bts.Add(sender as CheckBox);
        }

        private void CheckBox_Unchecked_1(object sender, RoutedEventArgs e)
        {
            bts.Remove(sender as CheckBox);
        }

        private void btDelTarefa_Click_1(object sender, EventArgs e)
        {
            bool carrega = false;
            foreach (CheckBox cb in bts)
            {
                banco_de_dados_local.Model.Grade horario = cb.DataContext as banco_de_dados_local.Model.Grade;
                if (horario.Aula == false)
                {
                    if (App.db.Grades.Any(z => z.Nome == horario.Nome && z.HoraInicio == horario.HoraInicio))
                    {
                        App.db.Grades.DeleteOnSubmit(App.db.Grades.Single(x => x.Nome == horario.Nome));

                        if (horario.Dias.Contains(DateTime.Today.DayOfWeek.ToString()) && App.ViewModel.HorariosHoje.Any(x => x.Nome == horario.Nome))
                        {
                            App.ViewModel.HorariosHoje.Remove(App.ViewModel.HorariosHoje.Single(x => x.Nome == horario.Nome));
                        }

                        if (horario.Dias.Contains("Monday") && HorariosSeg.Any(x => x.Nome == horario.Nome))
                        {
                            HorariosSeg.Remove(HorariosSeg.Single(x => x.Nome == horario.Nome));
                        }
                        if (horario.Dias.Contains("Tuesday") && HorariosTer.Any(x => x.Nome == horario.Nome))
                        {
                            HorariosTer.Remove(HorariosTer.Single(x => x.Nome == horario.Nome));
                        }
                        if (horario.Dias.Contains("Wednesday") && HorariosQua.Any(x => x.Nome == horario.Nome))
                        {
                            HorariosQua.Remove(HorariosQua.Single(x => x.Nome == horario.Nome));
                        }
                        if (horario.Dias.Contains("Thursday") && HorariosQui.Any(x => x.Nome == horario.Nome))
                        {
                            HorariosQui.Remove(HorariosQui.Single(x => x.Nome == horario.Nome));
                        }
                        if (horario.Dias.Contains("Friday") && HorariosSex.Any(x => x.Nome == horario.Nome))
                        {
                            HorariosSex.Remove(HorariosSex.Single(x => x.Nome == horario.Nome));
                        }
                        if (horario.Dias.Contains("Saturday") && HorariosSab.Any(x => x.Nome == horario.Nome))
                        {
                            HorariosSab.Remove(HorariosSab.Single(x => x.Nome == horario.Nome));
                        }
                        if (horario.Dias.Contains("Sunday") && HorariosDom.Any(x => x.Nome == horario.Nome))
                        {
                            HorariosDom.Remove(HorariosDom.Single(x => x.Nome == horario.Nome));
                        }

                    }

                    else
                    {
                    }
                }
                else
                {
                    if (App.db.GradesHelp.Any(z => z.Disciplina == horario.Nome.Split('(')[0].Trim() && (z.Hr1_Inicio == horario.HoraInicio || z.Hr2_Inicio == horario.HoraInicio || z.Hr3_Inicio == horario.HoraInicio || z.Lab1_Inicio == horario.HoraInicio || z.Lab2_Inicio == horario.HoraInicio)))
                    {
                        App.db.GradesHelp.DeleteOnSubmit(App.db.GradesHelp.First(x => x.Disciplina == horario.Nome.Split('(')[0].Trim()));
                        carrega = true;
                    }
                }
            }
            bts.Clear();
            App.db.SubmitChanges();

            if (carrega == true)
            {
                loadGrade();
                App.atualizaAtiv = true;
            }

        }

        private void PhoneApplicationPage_Loaded_1(object sender, RoutedEventArgs e)
        {
            if (NavigationService.BackStack.First().Source.ToString().Equals("/NovoHorario.xaml"))
            {
                NavigationService.RemoveBackEntry();
            }

            switch (System.DateTime.Now.DayOfWeek.ToString())
            {
                case ("Monday"):
                    abas.SelectedItem = abaSeg;
                    break;
                case ("Tuesday"):
                    abas.SelectedItem = abaTer;
                    break;
                case ("Wednesday"):
                    abas.SelectedItem = abaQuar;
                    break;
                case ("Thursday"):
                    abas.SelectedItem = abaQui;
                    break;
                case ("Friday"):
                    abas.SelectedItem = abaSex;
                    break;
                case ("Saturday"):
                    abas.SelectedItem = abaSab;
                    break;
                case ("Sunday"):
                    abas.SelectedItem = abaDom;
                    break;
            }
            loadGrade();


        }

        private void btDownload_Click(object sender, EventArgs e)
        {
            baixar_info();
        }

        private void baixar_info()
        {
            try
            {
                WebClient wc = new WebClient();
                wc.DownloadStringAsync(
                   new Uri("http://ufabchelp.me/server/index.php/grade?id=" + App.uid));
                SystemTray.SetIsVisible(this, true);
                SystemTray.SetOpacity(this, 0);
                prog = new ProgressIndicator();
                prog.IsVisible = true;
                prog.Text = "baixando grade";
                prog.IsIndeterminate = true;
                SystemTray.SetProgressIndicator(this, prog);
                wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(wc_DownloadStringCompleted);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }


        private void ReadWebRequestCallback(IAsyncResult callbackResult)
        {
            HttpWebRequest myRequest = (HttpWebRequest)callbackResult.AsyncState;
            HttpWebResponse myResponse = (HttpWebResponse)myRequest.EndGetResponse(callbackResult);
            using (StreamReader httpwebStreamReader = new StreamReader(myResponse.GetResponseStream()))
            {
                string results = httpwebStreamReader.ReadToEnd();
                //TextBlockResults.Text = results; //-- on another thread!
                Dispatcher.BeginInvoke(() => MessageBox.Show(results));
            }
            myResponse.Close();
        }

        private void loadGrade()
        {
            HorariosSeg = new List<banco_de_dados_local.Model.Grade>();
            HorariosTer = new List<banco_de_dados_local.Model.Grade>();
            HorariosQua = new List<banco_de_dados_local.Model.Grade>();
            HorariosQui = new List<banco_de_dados_local.Model.Grade>();
            HorariosSex = new List<banco_de_dados_local.Model.Grade>();
            HorariosSab = new List<banco_de_dados_local.Model.Grade>();
            HorariosDom = new List<banco_de_dados_local.Model.Grade>();

            foreach (banco_de_dados_local.Model.Grade horario in App.db.Grades)
            {
                if(horario.Dias.Contains("Monday"))
                {
                    HorariosSeg.Add(horario);
                }
                if(horario.Dias.Contains("Tuesday"))
                {
                    HorariosTer.Add(horario);
                }
                if(horario.Dias.Contains("Wednesday"))
                {
                    HorariosQua.Add(horario);
                }
                if(horario.Dias.Contains("Thursday"))
                {
                    HorariosQui.Add(horario);
                }
                if(horario.Dias.Contains("Friday"))
                {
                    HorariosSex.Add(horario);
                }
                if(horario.Dias.Contains("Saturday"))
                {
                    HorariosSab.Add(horario);
                }
                if(horario.Dias.Contains("Sunday"))
                {
                    HorariosDom.Add(horario);
                }
            }

            foreach (GradeHelp grade in UFABC_Power_CR.App.db.GradesHelp)
            {
                switch (grade.Hr1_Dia)
                {
                    case 2:
                        HorariosSeg.Add(new banco_de_dados_local.Model.Grade { Nome = grade.Disciplina + " (" + grade.Codigo + ")", Local = grade.Hr1_Local, HoraInicio = grade.Hr1_Inicio, HoraFim = grade.Hr1_Fim, Aula = true });
                        break;
                    case 3:
                        HorariosTer.Add(new banco_de_dados_local.Model.Grade { Nome = grade.Disciplina + " (" + grade.Codigo + ")", Local = grade.Hr1_Local, HoraInicio = grade.Hr1_Inicio, HoraFim = grade.Hr1_Fim, Aula = true });
                        break;
                    case 4:
                        HorariosQua.Add(new banco_de_dados_local.Model.Grade { Nome = grade.Disciplina + " (" + grade.Codigo + ")", Local = grade.Hr1_Local, HoraInicio = grade.Hr1_Inicio, HoraFim = grade.Hr1_Fim, Aula = true });
                        break;
                    case 5:
                        HorariosQui.Add(new banco_de_dados_local.Model.Grade { Nome = grade.Disciplina + " (" + grade.Codigo + ")", Local = grade.Hr1_Local, HoraInicio = grade.Hr1_Inicio, HoraFim = grade.Hr1_Fim, Aula = true });
                        break;
                    case 6:
                        HorariosSex.Add(new banco_de_dados_local.Model.Grade { Nome = grade.Disciplina + " (" + grade.Codigo + ")", Local = grade.Hr1_Local, HoraInicio = grade.Hr1_Inicio, HoraFim = grade.Hr1_Fim, Aula = true });
                        break;
                }
                switch (grade.Hr2_Dia)
                {
                    case 2:
                        HorariosSeg.Add(new banco_de_dados_local.Model.Grade { Nome = grade.Disciplina + " (" + grade.Codigo + ")", Local = grade.Hr2_Local, HoraInicio = grade.Hr2_Inicio, HoraFim = grade.Hr2_Fim, Aula = true });
                        break;
                    case 3:
                        HorariosTer.Add(new banco_de_dados_local.Model.Grade { Nome = grade.Disciplina + " (" + grade.Codigo + ")", Local = grade.Hr2_Local, HoraInicio = grade.Hr2_Inicio, HoraFim = grade.Hr2_Fim, Aula = true });
                        break;
                    case 4:
                        HorariosQua.Add(new banco_de_dados_local.Model.Grade { Nome = grade.Disciplina + " (" + grade.Codigo + ")", Local = grade.Hr2_Local, HoraInicio = grade.Hr2_Inicio, HoraFim = grade.Hr2_Fim, Aula = true });
                        break;
                    case 5:
                        HorariosQui.Add(new banco_de_dados_local.Model.Grade { Nome = grade.Disciplina + " (" + grade.Codigo + ")", Local = grade.Hr2_Local, HoraInicio = grade.Hr2_Inicio, HoraFim = grade.Hr2_Fim, Aula = true });
                        break;
                    case 6:
                        HorariosSex.Add(new banco_de_dados_local.Model.Grade { Nome = grade.Disciplina + " (" + grade.Codigo + ")", Local = grade.Hr2_Local, HoraInicio = grade.Hr2_Inicio, HoraFim = grade.Hr2_Fim, Aula = true });
                        break;
                }
                switch (grade.Hr3_Dia)
                {
                    case 2:
                        HorariosSeg.Add(new banco_de_dados_local.Model.Grade { Nome = grade.Disciplina + " (" + grade.Codigo + ")", Local = grade.Hr3_Local, HoraInicio = grade.Hr3_Inicio, HoraFim = grade.Hr3_Fim, Aula = true });
                        break;
                    case 3:
                        HorariosTer.Add(new banco_de_dados_local.Model.Grade { Nome = grade.Disciplina + " (" + grade.Codigo + ")", Local = grade.Hr3_Local, HoraInicio = grade.Hr3_Inicio, HoraFim = grade.Hr3_Fim, Aula = true });
                        break;
                    case 4:
                        HorariosQua.Add(new banco_de_dados_local.Model.Grade { Nome = grade.Disciplina + " (" + grade.Codigo + ")", Local = grade.Hr3_Local, HoraInicio = grade.Hr3_Inicio, HoraFim = grade.Hr3_Fim, Aula = true });
                        break;
                    case 5:
                        HorariosQui.Add(new banco_de_dados_local.Model.Grade { Nome = grade.Disciplina + " (" + grade.Codigo + ")", Local = grade.Hr3_Local, HoraInicio = grade.Hr3_Inicio, HoraFim = grade.Hr3_Fim, Aula = true });
                        break;
                    case 6:
                        HorariosSex.Add(new banco_de_dados_local.Model.Grade { Nome = grade.Disciplina + " (" + grade.Codigo + ")", Local = grade.Hr3_Local, HoraInicio = grade.Hr3_Inicio, HoraFim = grade.Hr3_Fim, Aula = true });
                        break;
                }
                switch (grade.Lab1_Dia)
                {
                    case 2:
                        HorariosSeg.Add(new banco_de_dados_local.Model.Grade { Nome = grade.Disciplina + " (" + grade.Codigo + ")", Local = grade.Lab1_Local, HoraInicio = grade.Lab1_Inicio, HoraFim = grade.Lab1_Fim, Aula = true });
                        break;
                    case 3:
                        HorariosTer.Add(new banco_de_dados_local.Model.Grade { Nome = grade.Disciplina + " (" + grade.Codigo + ")", Local = grade.Lab1_Local, HoraInicio = grade.Lab1_Inicio, HoraFim = grade.Lab1_Fim, Aula = true });
                        break;
                    case 4:
                        HorariosQua.Add(new banco_de_dados_local.Model.Grade { Nome = grade.Disciplina + " (" + grade.Codigo + ")", Local = grade.Lab1_Local, HoraInicio = grade.Lab1_Inicio, HoraFim = grade.Lab1_Fim, Aula = true });
                        break;
                    case 5:
                        HorariosQui.Add(new banco_de_dados_local.Model.Grade { Nome = grade.Disciplina + " (" + grade.Codigo + ")", Local = grade.Lab1_Local, HoraInicio = grade.Lab1_Inicio, HoraFim = grade.Lab1_Fim, Aula = true });
                        break;
                    case 6:
                        HorariosSex.Add(new banco_de_dados_local.Model.Grade { Nome = grade.Disciplina + " (" + grade.Codigo + ")", Local = grade.Lab1_Local, HoraInicio = grade.Lab1_Inicio, HoraFim = grade.Lab1_Fim, Aula = true });
                        break;
                }
                switch (grade.Lab2_Dia)
                {
                    case 2:
                        HorariosSeg.Add(new banco_de_dados_local.Model.Grade { Nome = grade.Disciplina + " (" + grade.Codigo + ")", Local = grade.Lab2_Local, HoraInicio = grade.Lab2_Inicio, HoraFim = grade.Lab2_Fim, Aula = true });
                        break;
                    case 3:
                        HorariosTer.Add(new banco_de_dados_local.Model.Grade { Nome = grade.Disciplina + " (" + grade.Codigo + ")", Local = grade.Lab2_Local, HoraInicio = grade.Lab2_Inicio, HoraFim = grade.Lab2_Fim, Aula = true });
                        break;
                    case 4:
                        HorariosQua.Add(new banco_de_dados_local.Model.Grade { Nome = grade.Disciplina + " (" + grade.Codigo + ")", Local = grade.Lab2_Local, HoraInicio = grade.Lab2_Inicio, HoraFim = grade.Lab2_Fim, Aula = true });
                        break;
                    case 5:
                        HorariosQui.Add(new banco_de_dados_local.Model.Grade { Nome = grade.Disciplina + " (" + grade.Codigo + ")", Local = grade.Lab2_Local, HoraInicio = grade.Lab2_Inicio, HoraFim = grade.Lab2_Fim, Aula = true });
                        break;
                    case 6:
                        HorariosSex.Add(new banco_de_dados_local.Model.Grade { Nome = grade.Disciplina + " (" + grade.Codigo + ")", Local = grade.Lab2_Local, HoraInicio = grade.Lab2_Inicio, HoraFim = grade.Lab2_Fim, Aula = true });
                        break;
                }

            }

            listSeg.ItemsSource = HorariosSeg.OrderBy(x => System.DateTime.Parse(x.HoraInicio));

            listTer.ItemsSource = HorariosTer.OrderBy(x => System.DateTime.Parse(x.HoraInicio));

            listQua.ItemsSource = HorariosQua.OrderBy(x => System.DateTime.Parse(x.HoraInicio));

            listQui.ItemsSource = HorariosQui.OrderBy(x => System.DateTime.Parse(x.HoraInicio));

            listSex.ItemsSource = HorariosSex.OrderBy(x => System.DateTime.Parse(x.HoraInicio));

            listSab.ItemsSource = HorariosSab.OrderBy(x => System.DateTime.Parse(x.HoraInicio));

            listDom.ItemsSource = HorariosDom.OrderBy(x => System.DateTime.Parse(x.HoraInicio));
        }

        void wc_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                if (e.Result != null)
                {
                    prog.IsVisible = false;
                    List<gradeHelp> grades = JsonConvert.DeserializeObject<List<gradeHelp>>(e.Result);
                    App.db.GradesHelp.DeleteAllOnSubmit(App.db.GradesHelp.ToList());
                    App.db.SubmitChanges();
                    foreach (gradeHelp grade in grades)
                    {
                        App.db.GradesHelp.InsertOnSubmit(new GradeHelp
                        {
                            Codigo = grade.codigo,
                            Disciplina = grade.disciplina,
                            Hr1_Dia = grade.hr1_dia.HasValue ? grade.hr1_dia.Value : 0,
                            Hr1_Fim = grade.hr1_fim,
                            Hr1_Inicio = grade.hr1_inicio,
                            Hr1_Local = grade.hr1_local,
                            Hr2_Dia = grade.hr2_dia.HasValue ? grade.hr2_dia.Value : 0,
                            Hr2_Fim = grade.hr2_fim,
                            Hr2_Inicio = grade.hr2_inicio,
                            Hr2_Local = grade.hr2_local,
                            Hr3_Dia = grade.hr3_dia.HasValue ? grade.hr3_dia.Value : 0,
                            Hr3_Fim = grade.hr3_fim,
                            Hr3_Inicio = grade.hr3_inicio,
                            Hr3_Local = grade.hr3_local,
                            Lab1_Dia = grade.lab1_dia.HasValue ? grade.lab1_dia.Value : 0,
                            Lab1_Fim = grade.lab1_fim,
                            Lab1_Inicio = grade.lab1_inicio,
                            Lab1_Local = grade.lab1_local,
                            Lab2_Dia = grade.lab2_dia.HasValue ? grade.lab2_dia.Value : 0,
                            Lab2_Fim = grade.lab2_fim,
                            Lab2_Inicio = grade.lab2_inicio,
                            Lab2_Local = grade.lab2_local,
                            Obs = grade.obs,
                            Prof_Pratica = grade.prof_pratica,
                            Prof_Teoria = grade.prof_teoria,
                            Turma = grade.turma
                        });
                    }
                    App.db.SubmitChanges();
                    loadGrade();
                    App.atualizaAtiv = true;
                }
                else
                {
                    MessageBox.Show("Não foi possível baixar a grade, tente novamente");
                    prog.IsVisible = false;
                }
            }
            catch
            {
            }
            i++;
        }


    }
}