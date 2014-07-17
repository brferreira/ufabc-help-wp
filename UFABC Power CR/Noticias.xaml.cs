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
using System.Xml;
using System.IO;
using System.ServiceModel.Syndication;
using Microsoft.Phone.Tasks;
using Newtonsoft.Json;
using Microsoft.Phone.Shell;
using System.Threading.Tasks;


namespace UFABC_Power_CR
{

    public class Noticia
    {
        public int id, id_facebook, approved;
        public string title, body, image, timestamp, fb_first_name;
    }

    public class news
    {
        public string titulo { get; set; }
        public string texto { get; set; }
        public string imagem { get; set; }
        public string horario { get; set; }
        public string autor { get; set; }
    }

    public partial class Noticias : PhoneApplicationPage
    {
        ProgressIndicator prog;
        public Noticias()
        {
            InitializeComponent();
        }

        private async void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (Microsoft.Phone.Net.NetworkInformation.NetworkInterface.NetworkInterfaceType != Microsoft.Phone.Net.NetworkInformation.NetworkInterfaceType.None)
            {
                lerFeed();
                await baixar_noticias();
            }
            else
            {
                MessageBox.Show("Não foi possível fazer baixar as notícias pois não há conexão à internet", "Erro!", MessageBoxButton.OK);
            }
        }



        private void email_Click(object sender, RoutedEventArgs e)
        {
            var selectedListBoxItem = feedListBox.ItemContainerGenerator.ContainerFromItem(((MenuItem)sender).DataContext) as ListBoxItem;
            EmailComposeTask email = new EmailComposeTask();
            SyndicationItem sItem = (SyndicationItem)selectedListBoxItem.Content;
            int index = sItem.Links.FirstOrDefault().Uri.ToString().IndexOf("&id");
            email.Body = sItem.Summary.Text.Remove(0, 31) + "\n\n" + "http://www.ufabc.edu.br/index.php?option=com_content" + sItem.Links.FirstOrDefault().Uri.ToString().Substring(index, 8) + "\n\nNotícia enviada pelo app UFABC Help para Windows Phone";
            email.Subject = sItem.Title.Text;
            email.Show();
        }

        private void sms_Click(object sender, RoutedEventArgs e)
        {
            var selectedListBoxItem = feedListBox.ItemContainerGenerator.ContainerFromItem(((MenuItem)sender).DataContext) as ListBoxItem;
            SmsComposeTask sms = new SmsComposeTask();
            SyndicationItem sItem = (SyndicationItem)selectedListBoxItem.Content;
            int index = sItem.Links.FirstOrDefault().Uri.ToString().IndexOf("&id");
            sms.Body = sItem.Title.Text + "\n\n" + "http://www.ufabc.edu.br/index.php?option=com_content" + sItem.Links.FirstOrDefault().Uri.ToString().Substring(index, 8) + "\n\nNotícia enviada pelo app UFABC Help para Windows Phone";
            sms.Show();
        }

        private async void btAtualiza_Click(object sender, EventArgs e)
        {
            if (Microsoft.Phone.Net.NetworkInformation.NetworkInterface.NetworkInterfaceType != Microsoft.Phone.Net.NetworkInformation.NetworkInterfaceType.None)
            {
                switch (pivotNews.SelectedIndex)
                {

                    case 0:
                        lerFeed();
                        break;
                    case 1:
                        await baixar_noticias();
                        break;
                }
            }
            else
            {
                MessageBox.Show("Não foi possível fazer baixar as notícias pois não há conexão à internet", "Erro!", MessageBoxButton.OK);
            }
            

        }



        #region noticiasUFABC
        void lerFeed()
        {


            WebClient webClient = new WebClient();

            webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(webClient_DownloadStringCompleted);

            //baixa o feed RSS do site da UFABC
            prog = new ProgressIndicator();
            prog.IsVisible = true;
            prog.Text = "baixando notícias da UFABC";
            prog.IsIndeterminate = true;
            SystemTray.SetProgressIndicator(this, prog);
            webClient.DownloadStringAsync(new System.Uri("http://www.ufabc.edu.br/index.php?option=com_content&view=category&id=731&Itemid=183&format=feed&type=rss"));
        }

        // Evento que acontece depois do feed ser completamente baixado
        private void webClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {

            //se algum erro ocorreu
            if (e.Error != null)
            {

                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    if ((App.Current as App).msg == false && NavigationService.Source.Equals("/MainPage.xaml"))
                    {
                        MessageBox.Show("Não foi possível se conectar ao site da UFABC. Clique em atualizar para tentar novamente");
                        (App.Current as App).msg = true;
                    }

                });

            }
            else
            {
                try
                {
                    // Guarda o feed na propriedade State caso o aplicativo seja trocado 
                    this.State["feed"] = e.Result;

                    UpdateFeedList(e.Result);
                }
                catch
                {
                }
            }
            prog.IsVisible = false;
        }

        // esse méto.do determina quando o usuário navegou para o aplicativo depois de ter trocado de aplicativo
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            // primeiro checa se o feed está guardado na propriedade State da página
            if (this.State.ContainsKey("feed"))
            {
                //pega o feed denovo somente se a aplicativo havia sido trocado, o que significa que o ListBox estará vazio
                if (feedListBox.Items.Count == 0)
                {
                    UpdateFeedList(State["feed"] as string);
                }
            }
        }

        //esse método configura o feed e o vincula à ListBox
        private void UpdateFeedList(string feedXML)
        {
            //carrega o feed em uma instancia SyndicationFeed
            StringReader stringReader = new StringReader(feedXML);
            XmlReader xmlReader = XmlReader.Create(stringReader);
            SyndicationFeed feed = SyndicationFeed.Load(xmlReader);

            //usando Dispatcher para atualizar a interface de usuário
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                //vincula a lista de SyndicationItems com a ListBox
                feedListBox.ItemsSource = feed.Items;


            });
        }

        private void feedListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox listBox = sender as ListBox;

            if (listBox != null && listBox.SelectedItem != null)
            {
                //itemLista = listBox.SelectedIndex;
                //pega o SyndicationItem do item que foi pressionado
                SyndicationItem sItem = (SyndicationItem)listBox.SelectedItem;

                /*   //configura a página de navegação somente se um link realmente existe no item do feed
                   if (sItem.Links.Count > 0)
                   {
                       //pega o endereço associado ao item do feed
                       Uri uri = sItem.Links.FirstOrDefault().Uri;


                       //cria uma tarefa do browser para navegar para o item do feed
                       WebBrowserTask webBrowserTask = new WebBrowserTask();
                       webBrowserTask.Uri = uri;
                       webBrowserTask.Show();
                   }*/

                (App.Current as App).sItem = sItem;
                NavigationService.Navigate(new Uri("/DetalheNoticia.xaml?id=" + "-1", UriKind.Relative));
            }
        }

        // evento em que a seleção do ListBox mudou

        #endregion

        #region noticiasHelp


        private async Task baixar_noticias()
        {
            /*WebClient wc = new WebClient();
              wc.DownloadStringAsync(
                 new Uri("http://www.ufabchelp.me/server/index.php/news"));*/
            SystemTray.SetIsVisible(this, true);
            SystemTray.SetOpacity(this, 0);
            prog = new ProgressIndicator();
            prog.IsVisible = true;
            prog.Text = "baixando notícias do UFABC Help!";
            prog.IsIndeterminate = true;
            SystemTray.SetProgressIndicator(this, prog);
            //wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(wc_DownloadStringCompleted);
            await MainPage.webServiceDataContext.LoadData();
            lbHelp.DataContext = MainPage.webServiceDataContext;
            lbHelp.ItemsSource = MainPage.webServiceDataContext.News;
            prog.IsVisible = false;
        }

        private void lbHelp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                Model.News selected = (Model.News)e.AddedItems[0];
                NavigationService.Navigate(new Uri("/DetalheNoticia.xaml?id=" + selected.Id.ToString(), UriKind.Relative));
            }
        }

        private void emailHelp_Click(object sender, RoutedEventArgs e)
        {
            MenuItem mnA = (MenuItem)sender;
            Model.News new1 = (Model.News)mnA.DataContext;
            EmailComposeTask email = new EmailComposeTask();
            email.Body = new1.FacebookFirstNameFormatted +  "\n\n" + new1.Body + "\n\nNotícia enviada pelo app UFABC Help para Windows Phone";
            email.Subject = new1.Title;
            email.Show();
        }

        private void smsHelp_Click(object sender, RoutedEventArgs e)
        {
            MenuItem mnA = (MenuItem)sender;
            Model.News new1 = (Model.News)mnA.DataContext;
            var range = (from news in MainPage.webServiceDataContext.News where news.Id == new1.Id select news);
            SmsComposeTask sms = new SmsComposeTask();
            sms.Body = range.Single().Title + "\n\nLeia esta notícia no app UFABC Help para Windows Phone";
            sms.Show();

        }

        

        /*void wc_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                if (e.Result != null)
                {
                    List<Noticia> noticias = JsonConvert.DeserializeObject<List<Noticia>>(e.Result);
                    List<news> lista_news = new List<news>();
                    foreach (Noticia noticia in noticias)
                    {
                        if (noticia.approved == 1)
                        {
                            string dia, mes, ano, hora;
                            dia = noticia.timestamp.Split(' ')[0].Split('-')[2];
                            mes = noticia.timestamp.Split(' ')[0].Split('-')[1];
                            ano = noticia.timestamp.Split(' ')[0].Split('-')[0];
                            hora = noticia.timestamp.Split(' ')[1];
                            lista_news.Add(new news { titulo = noticia.title, texto = noticia.body, autor = noticia.fb_first_name, horario = dia + "/" + mes + "/" + ano + " " + hora, imagem = noticia.image });
                        }
                    }
                    lbHelp.DataContext = lista_news;

                }
                prog.IsVisible = false;
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }*/


        #endregion
    }
}