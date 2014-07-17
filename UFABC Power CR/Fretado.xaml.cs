using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using banco_de_dados_local.Model;


namespace UFABC_Power_CR
{

    public class Viagem
    {
        public string linha, origem, destino, horario_saida, horario_chegada;
    }

    public class ViagemFormat
    {
        public string linha{get;set;}
        public string origemDestino{get;set;}
    }

    public partial class Fretado : PhoneApplicationPage
    {
        public ObservableCollection<string> pontos;
        List<int[]> favoritos;
        ProgressIndicator prog;
        int favAtual;
        public Fretado()
        {
            InitializeComponent();
            favAtual = 0;
            pontos = new ObservableCollection<string>();
            pontos.Add("Alfa");
            pontos.Add("Atl\u00e2ntica");
            pontos.Add("Catequese");
            pontos.Add("Pra\u00e7a Expedicional");
            pontos.Add("Rua Aboli\u00e7\u00e3o");
            pontos.Add("Terminal Leste");
            pontos.Add("Terminal SBC");
            lpOrigem.ItemsSource = new ReadOnlyCollection<string>(pontos);
            lpDestino.ItemsSource = new ReadOnlyCollection<string>(pontos);
            if (App.login == true)
            {
                favoritos = new List<int[]>();
                if (App.db.rotas.Count() > 0)
                {
                    foreach (banco_de_dados_local.Model.Rotas favs in App.db.rotas.ToList())
                    {
                        favoritos.Add(new int[] { favs.Origem, favs.Destino });
                    }
                    lpOrigem.SelectedIndex = favoritos.ElementAt(0)[0];
                    lpDestino.SelectedIndex = favoritos.ElementAt(0)[1];
                }
            }
            else
            {
                ApplicationBarIconButton addFav = (ApplicationBarIconButton)ApplicationBar.Buttons[2];
                ApplicationBarIconButton Favoritos = (ApplicationBarIconButton)ApplicationBar.Buttons[3];
                ApplicationBarMenuItem apaga = (ApplicationBarMenuItem)ApplicationBar.MenuItems[0];
                addFav.IsEnabled = false;
                Favoritos.IsEnabled = false;
                apaga.IsEnabled = false;
            }

        }

        private  void  baixar_info(string origem, string destino)
        {
            try
            {
                WebClient wc = new WebClient();
                wc.DownloadStringAsync(
                   new Uri("http://www.ufabchelp.me/server/index.php/bus?destino=" + destino + "&origem=" + origem));
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
                    List<Viagem> viagens = JsonConvert.DeserializeObject<List<Viagem>>(e.Result);
                    List<ViagemFormat>  viagensF = new List<ViagemFormat>();
                    foreach (Viagem viagem in viagens)
                    {
                        string[] horaSaida = viagem.horario_saida.Split(':');
                        string[] horaChegada = viagem.horario_chegada.Split(':');
                        viagensF.Add(new ViagemFormat { linha = "Linha " + viagem.linha.Split('a')[1], origemDestino = viagem.origem + " (" + horaSaida[0] + ":" + horaSaida[1] + ") " + " → " + viagem.destino + " (" + horaChegada[0] + ":" + horaChegada[1] + ") " });
                    }
                   
                    lbHorarios.DataContext = viagensF;

                }
                prog.IsVisible = false;
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }

        private void btPesquisa_Click(object sender, EventArgs e)
        {
            if (lpDestino.SelectedItem as string != lpOrigem.SelectedItem as string)
            {
                if (Microsoft.Phone.Net.NetworkInformation.NetworkInterface.NetworkInterfaceType != Microsoft.Phone.Net.NetworkInformation.NetworkInterfaceType.None)
                {
                     baixar_info(lpOrigem.SelectedItem as string, lpDestino.SelectedItem as string);
                }
                else
                {
                    MessageBox.Show("Não foi possível baixar os horários pois não há conexão à internet.", "Erro!", MessageBoxButton.OK);
                }
            }
            else
            {
                MessageBox.Show("Selecione um destino diferente da origem");
            }
        }

        private void btInverte_Click(object sender, EventArgs e)
        {
            int origem = lpOrigem.SelectedIndex;
            lpOrigem.SelectedIndex = lpDestino.SelectedIndex;
            lpDestino.SelectedIndex = origem;
        }

        private void btAddFav_Click(object sender, EventArgs e)
        {
            if (lpOrigem.SelectedIndex != lpDestino.SelectedIndex)
            {
                foreach (banco_de_dados_local.Model.Rotas favs in App.db.rotas.ToList())
                {
                    if (favs.Origem == lpOrigem.SelectedIndex && favs.Destino == lpDestino.SelectedIndex)
                    {
                        MessageBox.Show("Esta rota já foi adicionada aos favoritos!");
                        return;
                    }
                }
                App.db.rotas.InsertOnSubmit(new Rotas { Origem = lpOrigem.SelectedIndex, Destino = lpDestino.SelectedIndex});
                App.db.SubmitChanges();
                favoritos.Add(new int[] { lpOrigem.SelectedIndex, lpDestino.SelectedIndex });
                MessageBox.Show("Rota gravada com sucesso!");
            }
            else
            {
                MessageBox.Show("Selecione um destino diferente da origem");
            }
        }

        private void btFavoritos_Click(object sender, EventArgs e)
        {
            if (favoritos.Count > 0)
            {
                if (favAtual < favoritos.Count - 1)
                {
                    favAtual++;
                }
                else
                {
                    favAtual = 0;
                }
                lpOrigem.SelectedIndex = favoritos.ElementAt(favAtual)[0];
                lpDestino.SelectedIndex = favoritos.ElementAt(favAtual)[1];
               
            }
            else
            {
                MessageBox.Show("Não há rotas adicionadas como favoritas!");
            }
        }

        private void itemApaga_Click(object sender, EventArgs e)
        {
            MessageBoxResult resp = MessageBox.Show("Deseja apagar as rotas favoritas?", "Mensagem", MessageBoxButton.OKCancel);
            if (resp == MessageBoxResult.OK)
            {
                foreach (banco_de_dados_local.Model.Rotas favs in App.db.rotas.ToList())
                {
                    App.db.rotas.DeleteOnSubmit(favs);
                }
                App.db.SubmitChanges();
                favoritos.Clear();
            }
        }

    }
}