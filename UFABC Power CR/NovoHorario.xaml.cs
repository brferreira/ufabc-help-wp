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
    public partial class NovoHorario : PhoneApplicationPage
    {
        DateTime horaInicio=DateTime.Now, horaFim=DateTime.Now;
        public NovoHorario()
        {
            InitializeComponent();

            this.DataContext = App.ViewModel;
           
            
        }


        private void listTipo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (listTipo.SelectedIndex == 0)
                {
                    tbAulas.Visibility = System.Windows.Visibility.Visible;
                    listAulas.Visibility = System.Windows.Visibility.Visible;
                    tbExtras.Visibility = System.Windows.Visibility.Collapsed;
                    tboxExtras.Visibility = System.Windows.Visibility.Collapsed;
                }
                else
                {
                    tbAulas.Visibility = System.Windows.Visibility.Collapsed;
                    listAulas.Visibility = System.Windows.Visibility.Collapsed;
                    tbExtras.Visibility = System.Windows.Visibility.Visible;
                    tboxExtras.Visibility = System.Windows.Visibility.Visible;
                }
            }
            catch
            {
            }
        }

        private void btSalvar_Click_1(object sender, EventArgs e)
        {
            string ativ="", dias="";
            bool aula=true;
            if (tbAulas.Visibility == System.Windows.Visibility.Visible)
            {
                if (listAulas.Items.Count != 0)
                {
                    banco_de_dados_local.Model.Historico hist = listAulas.SelectedItem as banco_de_dados_local.Model.Historico;
                    ativ = hist.Materia;
                    aula = true;
                }
                else
                {
                    MessageBox.Show("Não há matérias salvas no quadrimestre para se adicionar à grade");
                }
            }
            else
            {
                    ativ = tboxExtras.Text;
                    aula = false;
            }

            if (ativ != "")
            {
                if (dayPicker.SelectedItems != null && ativ != "")
                {
                    foreach (object a in dayPicker.SelectedItems)
                    {
                        switch (a.ToString())
                        {
                            case "segunda-feira":
                                dias = "Monday";
                                break;
                            case "terça-feira":
                                dias += ",Tuesday";
                                break;
                            case "quarta-feira":
                                dias += ",Wednesday";
                                break;
                            case "quinta-feira":
                                dias += ",Thursday";
                                break;
                            case "sexta-feira":
                                dias += ",Friday";
                                break;
                            case "sábado":
                                dias += ",Saturday";
                                break;
                            case "domingo":
                                dias += ",Sunday";
                                break;
                        }

                    }

                    banco_de_dados_local.Model.Grade horario = new banco_de_dados_local.Model.Grade { Nome = ativ, Dias = dias, HoraInicio = horaInicio.ToString("HH:mm"), HoraFim = horaFim.ToString("HH:mm"), Aula = aula };
                    App.db.Grades.InsertOnSubmit(horario);
                    if (dias.Contains("Monday"))
                    {
                        App.ViewModel.HorariosSeg = novaColecao(horario, App.ViewModel.HorariosSeg);
                    }
                    if (dias.Contains("Tuesday"))
                    {
                        App.ViewModel.HorariosTer = novaColecao(horario, App.ViewModel.HorariosTer);
                    }
                    if (dias.Contains("Wednesday"))
                    {
                        App.ViewModel.HorariosQua = novaColecao(horario, App.ViewModel.HorariosQua);
                    }
                    if (dias.Contains("Thursday"))
                    {
                        App.ViewModel.HorariosQui = novaColecao(horario, App.ViewModel.HorariosQui);
                    }
                    if (dias.Contains("Friday"))
                    {
                        App.ViewModel.HorariosSex = novaColecao(horario, App.ViewModel.HorariosSex);
                    }
                    if (dias.Contains("Saturday"))
                    {
                        App.ViewModel.HorariosSab = novaColecao(horario, App.ViewModel.HorariosSab);
                    }
                    if (dias.Contains("Sunday"))
                    {
                        App.ViewModel.HorariosDom = novaColecao(horario, App.ViewModel.HorariosDom);
                    }
                    if (dias.Contains(DateTime.Today.DayOfWeek.ToString()))
                    {
                        App.ViewModel.HorariosHoje = novaColecao(horario, App.ViewModel.HorariosHoje);
                        
                    }
                    App.db.SubmitChanges();
                    NavigationService.RemoveBackEntry();
                    NavigationService.Navigate(new Uri("/Grade.xaml", UriKind.Relative));
                }
                else
                {
                    MessageBox.Show("Escolha o(s) dia(s) da atividade");
                }
            }
            else
            {
                MessageBox.Show("Digite o nome da atividade");
            }

        }

        private void tpHoraInicio_ValueChanged(object sender, DateTimeValueChangedEventArgs e)
        {
            horaInicio = (DateTime)e.NewDateTime;
        }

        private void tpHoraFim_ValueChanged(object sender, DateTimeValueChangedEventArgs e)
        {
            horaFim = (DateTime)e.NewDateTime;
        }

        private void PhoneApplicationPage_Loaded_1(object sender, RoutedEventArgs e)
        {
            
        }

        private void appBarCancelButton_Click(object sender, EventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }

        private System.Collections.ObjectModel.ObservableCollection<banco_de_dados_local.Model.Grade> novaColecao(banco_de_dados_local.Model.Grade horario, System.Collections.ObjectModel.ObservableCollection<banco_de_dados_local.Model.Grade> colecao)
        {
            List<banco_de_dados_local.Model.Grade> horarios = new List<banco_de_dados_local.Model.Grade>();
            horarios.AddRange(colecao);
            horarios.Add(horario);
            colecao.Clear();
            colecao = new System.Collections.ObjectModel.ObservableCollection<banco_de_dados_local.Model.Grade>(horarios.OrderBy(x => System.DateTime.Parse(x.HoraInicio)));
            return colecao;
        }

        
    }
}