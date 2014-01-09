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

using Microsoft.Phone.Tasks;
using Microsoft.Phone.Shell;



namespace UFABC_Power_CR
{
    public partial class MainPage : PhoneApplicationPage
    {
        public MainPage()
        {

            InitializeComponent();

            //MVC - relaciona o DataContext da classe com o objeto viewModel da classe ViewModel 
            this.DataContext = App.ViewModel;
        }

        private void TextBlock_Tap_1(object sender, System.Windows.Input.GestureEventArgs e)
        {
            //NavigationService.Navigate -> método para navegar de uma página para outra
            NavigationService.Navigate(new Uri("/Quadrimestres.xaml", UriKind.Relative));
        }

        #region noticias
        // Objeto usado para comunicar com o webservice (somente para noticias no momento)
        public static Model.WebServiceDataContext webServiceDataContext = new Model.WebServiceDataContext(new Uri("http://ufabchelp.me"));

        /*void lerFeed()
        {


            WebClient webClient = new WebClient();



            webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(webClient_DownloadStringCompleted);

            //baixa o feed RSS do site da UFABC
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

        // evento em que a seleção do ListBox mudou
        
        */
        #endregion


        //método chamado quando o usuário aperta um item da ListBox da agenda (atividades da semana)
        private void tbItem_Tap_1(object sender, System.Windows.Input.GestureEventArgs e)
        {
            //cria um TextBlock que vai pegar as propriedades do item da ListBox
            TextBlock tb = new TextBlock();
            tb = sender as TextBlock;
            //atribue o DataContext do Textblock a variavel de ref (da classe ToDoItem) item na pagina App (funciona como uma var global)
            (App.Current as App).item = tb.DataContext as ToDoItem;
            NavigationService.Navigate(new Uri("/ativDetalhes.xaml", UriKind.Relative));
        }

        //método chamado quando a página carrega
        private void PhoneApplicationPage_Loaded_1(object sender, RoutedEventArgs e)
        {
           
            //FAÇADE -> imprime o CR e CA nos textblocks da pag inicial. O cálculo dos coeficientes é feito na classe CR
            banco_de_dados_local.ViewModel.ToDoViewModel a = new banco_de_dados_local.ViewModel.ToDoViewModel();
            tbCR.Text = "CR: " + App.aluno.CR;
            tbCA.Text = "CA: " + App.aluno.CA;

            //FAÇADE -> imprime o CP e análise dos coeficientes. O código está nas classes CP e Analise
            tbCP.Text = "CP: " + App.aluno.CP;
           
            //Se o aplicativo pode retornar, apaga a ultima pagina visitada do registro. Isso garante que ao clicar no botão voltar
            // o usuário saia do programa
            if (NavigationService.CanGoBack == true)
            {
                NavigationService.RemoveBackEntry();
            }
            if (App.fblogin == true)
            {
            }

        }

        private void panoramaX_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplicationBarIconButton botao = (ApplicationBarIconButton)ApplicationBar.Buttons[0];
            ApplicationBarMenuItem item = (ApplicationBarMenuItem)ApplicationBar.MenuItems[0];
            switch (panoramaX.SelectedIndex)
            {
                case 0:
                    botao.IconUri = new Uri("/Imagens/next.png", UriKind.Relative);
                    botao.Text = "agenda completa";
                    item.Text = "ver agenda completa";
                    botao.IsEnabled = true;
                    item.IsEnabled = true;
                    break;
                case 1:
                    botao.IconUri = new Uri("/Imagens/next.png", UriKind.Relative);
                    botao.Text = "grade completa";
                    item.Text = "ver grade completa";
                    botao.IsEnabled = true;
                    item.IsEnabled = true;
                    break;
                case 2:
                    botao.IconUri = new Uri("/Imagens/next.png", UriKind.Relative);
                    botao.Text = "histórico";
                    item.Text = "ver histórico";
                    botao.IsEnabled = true;
                    item.IsEnabled = true;
                    break;
                case 3:
                    botao.IconUri = new Uri("/Imagens/refresh.png", UriKind.Relative);
                    botao.Text = "atualizar";
                    item.Text = "atualizar notícias";
                    botao.IsEnabled = true;
                    item.IsEnabled = true;
                    break;
                case 4:
                    botao.IsEnabled = false;
                    item.IsEnabled = false;

                    break;
            }
        }

        private void item_Click(object sender, EventArgs e)
        {
            switch (panoramaX.SelectedIndex)
            {
                case 0:
                    NavigationService.Navigate(new Uri("/Agenda.xaml", UriKind.Relative));
                    break;
                case 1:
                    NavigationService.Navigate(new Uri("/Grade.xaml", UriKind.Relative));
                    break;
                case 2:
                    NavigationService.Navigate(new Uri("/Historico.xaml", UriKind.Relative));
                    break;
                case 3:
                    //NavigationService.Navigate(new Uri("/FacebookLogin.xaml", UriKind.Relative));
                    break;
            }
        }

        private void sobre_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Sobre.xaml", UriKind.Relative));
        }

        private void opcoes_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Opcoes.xaml", UriKind.Relative));
        }

        private void avaliar_Click(object sender, EventArgs e)
        {
            Microsoft.Phone.Tasks.MarketplaceReviewTask a = new MarketplaceReviewTask();
            a.Show();
        }

        private void tbHome_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            WebBrowserTask wbt = new WebBrowserTask();
            wbt.Uri = new Uri("http://www.ufabc.edu.br/", UriKind.Absolute);
            wbt.Show();
        }

        private void tbPro_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            WebBrowserTask wbt = new WebBrowserTask();
            wbt.Uri = new Uri("http://prograd.ufabc.edu.br/", UriKind.Absolute);
            wbt.Show();
        }

        private void tbBiblio_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            WebBrowserTask wbt = new WebBrowserTask();
            wbt.Uri = new Uri("http://biblioteca.ufabc.edu.br/", UriKind.Absolute);
            wbt.Show();
        }

        private void tbTransp_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            WebBrowserTask wbt = new WebBrowserTask();
            wbt.Uri = new Uri("http://pu.ufabc.edu.br/transporte", UriKind.Absolute);
            wbt.Show();
        }

        private void tbRU_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            WebBrowserTask wbt = new WebBrowserTask();
            wbt.Uri = new Uri("http://proap.ufabc.edu.br/images/PDF/Cardapio.pdf", UriKind.Absolute);
            wbt.Show();
        }

        private void tbEntid_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/entidades.xaml", UriKind.Relative));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void tbProf_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Professores.xaml", UriKind.Relative));
        }

        private void tbDisciplinas_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Disciplinas.xaml", UriKind.Relative));
        }

        private void tbNoticias_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Noticias.xaml", UriKind.Relative));
        }

        private void fb_login_Click(object sender, EventArgs e)
        {
            if (App.fblogin == false)
            {
                NavigationService.Navigate(new Uri("/FacebookLogin.xaml", UriKind.Relative));
            }
        }

       
      /*  private void email_Click(object sender, RoutedEventArgs e)
        {                  
            var selectedListBoxItem = feedListBox.ItemContainerGenerator.ContainerFromItem(((MenuItem) sender).DataContext) as ListBoxItem;
            EmailComposeTask email = new EmailComposeTask();
            SyndicationItem sItem = (SyndicationItem)selectedListBoxItem.Content;
            email.Body = sItem.Summary.Text.Remove(0, 31) +"\n\n" + sItem.Links.FirstOrDefault().Uri;
            email.Subject = sItem.Title.Text;
            email.Show();
        }

        private void feedListBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            ListBox listBox = sender as ListBox;

            if (listBox != null && listBox.SelectedItem != null)
            {
                //itemLista = listBox.SelectedIndex;
                //pega o SyndicationItem do item que foi pressionado
                SyndicationItem sItem = (SyndicationItem)listBox.SelectedItem;

                //configura a página de navegação somente se um link realmente existe no item do feed
                if (sItem.Links.Count > 0)
                {
                    //pega o endereço associado ao item do feed
                    Uri uri = sItem.Links.FirstOrDefault().Uri;


                    //cria uma tarefa do browser para navegar para o item do feed
                    WebBrowserTask webBrowserTask = new WebBrowserTask();
                    webBrowserTask.Uri = uri;
                    webBrowserTask.Show();
                }
            }
        }

        private void sms_Click(object sender, RoutedEventArgs e)
        {
            var selectedListBoxItem = feedListBox.ItemContainerGenerator.ContainerFromItem(((MenuItem)sender).DataContext) as ListBoxItem;
            SmsComposeTask sms = new SmsComposeTask();
            SyndicationItem sItem = (SyndicationItem)selectedListBoxItem.Content;
            sms.Body = sItem.Title.Text + "\n\n" + sItem.Links.FirstOrDefault().Uri.ToString().Remove(73);
            sms.Show();
        }*/

       

       

       

    }
}
