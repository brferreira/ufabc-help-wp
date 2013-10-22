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


namespace UFABC_Power_CR
{

    public class fb
    {
        public int uid;
    }

    public partial class Grade : PhoneApplicationPage
    {
        int uid;
        //List<object> cbs = new List<object>();
        List<CheckBox> bts = new List<CheckBox>();
        ProgressIndicator prog;

        public Grade()
        {
            InitializeComponent();
            this.DataContext = App.ViewModel;
            switch (System.DateTime.Now.DayOfWeek.ToString())
            {
                case ("Monday"):
                    abas.SelectedItem = abaSeg;
                    break;
                case("Tuesday"):
                    abas.SelectedItem = abaTer;
                    break;
                case("Wednesday"):
                    abas.SelectedItem = abaQuar;
                    break;
                case("Thursday"):
                    abas.SelectedItem = abaQui;
                    break;
                case("Friday"):
                    abas.SelectedItem = abaSex;
                    break;
                case ("Saturday"):
                    abas.SelectedItem = abaSab;
                    break;
                case("Sunday"):
                    abas.SelectedItem = abaDom;
                    break;
                   
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

            foreach (CheckBox cb in bts)
            {
                banco_de_dados_local.Model.Grade horario = cb.DataContext as banco_de_dados_local.Model.Grade;
                if (App.db.Grades.Any(z => z.Nome == horario.Nome && z.HoraInicio == horario.HoraInicio))
                {
                    App.db.Grades.DeleteOnSubmit(App.db.Grades.Single(x => x.Nome == horario.Nome));

                    if (horario.Dias.Contains(DateTime.Today.DayOfWeek.ToString()) && App.ViewModel.HorariosHoje.Any(x => x.Nome == horario.Nome))
                    {
                        App.ViewModel.HorariosHoje.Remove(App.ViewModel.HorariosHoje.Single(x => x.Nome == horario.Nome));
                    }
                    if (horario.Dias.Contains("Monday") && App.ViewModel.HorariosSeg.Any(x => x.Nome == horario.Nome))
                    {
                        App.ViewModel.HorariosSeg.Remove(App.ViewModel.HorariosSeg.Single(x=> x.Nome == horario.Nome));
                    }
                    if (horario.Dias.Contains("Tuesday") && App.ViewModel.HorariosTer.Any(x => x.Nome == horario.Nome))
                    {
                        App.ViewModel.HorariosTer.Remove(App.ViewModel.HorariosTer.Single(x => x.Nome == horario.Nome));
                    }
                    if (horario.Dias.Contains("Wednesday") && App.ViewModel.HorariosQua.Any(x => x.Nome == horario.Nome))
                    {
                        App.ViewModel.HorariosQua.Remove(App.ViewModel.HorariosQua.Single(x => x.Nome == horario.Nome));
                    }
                    if (horario.Dias.Contains("Thursday") && App.ViewModel.HorariosQui.Any(x => x.Nome == horario.Nome))
                    {
                        App.ViewModel.HorariosQui.Remove(App.ViewModel.HorariosQui.Single(x => x.Nome == horario.Nome));
                    }
                    if (horario.Dias.Contains("Friday") && App.ViewModel.HorariosSex.Any(x => x.Nome == horario.Nome))
                    {
                        App.ViewModel.HorariosSex.Remove(App.ViewModel.HorariosSex.Single(x => x.Nome == horario.Nome));
                    }
                    if (horario.Dias.Contains("Saturday") && App.ViewModel.HorariosSab.Any(x => x.Nome == horario.Nome))
                    {
                        App.ViewModel.HorariosSab.Remove(App.ViewModel.HorariosSab.Single(x => x.Nome == horario.Nome));
                    }
                    if (horario.Dias.Contains("Sunday") && App.ViewModel.HorariosDom.Any(x => x.Nome == horario.Nome))
                    {
                        App.ViewModel.HorariosDom.Remove(App.ViewModel.HorariosDom.Single(x=> x.Nome == horario.Nome));
                    }
                }

                else
                {
                }
            }
            bts.Clear();
            App.db.SubmitChanges();




            /*System.Windows.Controls.CheckBox[] bt = new CheckBox[cbs.Count];
            for (int aux = cbs.Count() - 1; aux >= 0; aux--)
            {
                bt[aux] = cbs.ElementAt<object>(aux) as CheckBox;
                if (bt[aux] != null)
                {
                    banco_de_dados_local.Model.Grade horario = bt[aux].DataContext as banco_de_dados_local.Model.Grade;
                    try
                    {
                        App.db.Grades.DeleteOnSubmit(App.db.Grades.Single(x => x == horario));
                        if (horario.Dias.Contains(DateTime.Today.DayOfWeek.ToString()))
                        {
                            App.ViewModel.HorariosHoje.Remove(horario);
                        }
                        if (horario.Dias.Contains("Monday"))
                        {
                            App.ViewModel.HorariosSeg.Remove(horario);
                        }
                        if (horario.Dias.Contains("Tuesday"))
                        {
                            App.ViewModel.HorariosTer.Remove(horario);
                        }
                        if (horario.Dias.Contains("Wednesday"))
                        {
                            App.ViewModel.HorariosQua.Remove(horario);
                        }
                        if (horario.Dias.Contains("Thursday"))
                        {
                            App.ViewModel.HorariosQui.Remove(horario);
                        }
                        if (horario.Dias.Contains("Friday"))
                        {
                            App.ViewModel.HorariosSex.Remove(horario);
                        }
                        if (horario.Dias.Contains("Saturday"))
                        {
                            App.ViewModel.HorariosSab.Remove(horario);
                        }
                        if (horario.Dias.Contains("Sunday"))
                        {
                            App.ViewModel.HorariosDom.Remove(horario);
                        }
                        cbs.RemoveAt(aux);
                    }
                    catch(Exception es)
                    {
                        for (int i = cbs.Count() - 1; i >= 0; i--)
                        {
                            bt[i].IsChecked = false;
                        }
                        cbs.Clear();
                        MessageBox.Show(es.Message);
                    }
                }
            }
            App.db.SubmitChanges();
            this.Focus();
             */

        }

        private void PhoneApplicationPage_Loaded_1(object sender, RoutedEventArgs e)
        {
            if (NavigationService.BackStack.First().Source.ToString().Equals("/NovoHorario.xaml"))
            {
                NavigationService.RemoveBackEntry();
            }
        }

        private void btDownload_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(App.token);
            baixar_info();
        }

        private void baixar_info()
        {
            try
            {
                WebClient wc = new WebClient();
                wc.DownloadStringAsync(
                   new Uri("http://www.ufabchelp.me/painel/servicos/fb.php?user_id=" + App.id));
                wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(wc_DownloadStringCompleted);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        void wc_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
           // try
            //{
                if (e.Result != null)
                {
                    MessageBox.Show(e.Result);
                }
            //}
            //catch (Exception erro)
            //{
              //  MessageBox.Show(erro.Message);
            //}
        }


    }
}