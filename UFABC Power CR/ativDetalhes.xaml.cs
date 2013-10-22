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
    public partial class ativDetalhes : PhoneApplicationPage
    {
        public ativDetalhes()
        {
            InitializeComponent();
        }


        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {

            if((App.Current as App).edita == true)
            {
                (App.Current as App).edita = false;

                if (NavigationService.BackStack.First().Source.ToString().Equals("/NovaTarefa.xaml"))
                {
                    NavigationService.RemoveBackEntry();
                }
            }
            base.OnNavigatedTo(e);

            tbNome.Text = (App.Current as App).item.ItemName;
            tbData.Text = (App.Current as App).item.ItemData;
            tbHora.Text = (App.Current as App).item.ItemHora;
            tbDetalhes.Text = (App.Current as App).item.ItemDetalhes;
        }


        private void appBarEditButton_Click_1(object sender, EventArgs e)
        {
            (App.Current as App).edita = true;
            NavigationService.Navigate(new Uri("/NovaTarefa.xaml", UriKind.Relative));
        }


        private void appBarDeleteButton_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja mesmo apagar a atividade?", "Apagar", MessageBoxButton.OKCancel).Equals(MessageBoxResult.OK))
            {
                App.ViewModel.DeleteToDoItem((App.Current as App).item);
                NavigationService.RemoveBackEntry();
                NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
            }
        }

    }
}