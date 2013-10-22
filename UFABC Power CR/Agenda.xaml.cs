using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
// Directive for the ViewModel.
using banco_de_dados_local.Model;

namespace UFABC_Power_CR
{
    public partial class Page1 : PhoneApplicationPage
    {
        List<object> cbs = new List<object>();

        public Page1()
        {
            InitializeComponent();

            this.DataContext = App.ViewModel;
            
        }

        private void newTaskAppBarButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/NovaTarefa.xaml", UriKind.Relative));
        }



        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            App.ViewModel.SaveChangesToDB();
        }

        private void btDelTarefa_Click_1(object sender, EventArgs e)
        {

           System.Windows.Controls.CheckBox[] bt = new CheckBox[cbs.Count];
           
           for(int aux=cbs.Count()-1; aux>=0; aux--)
           {
             bt[aux] = cbs.ElementAt<object>(aux) as CheckBox;
             if (bt[aux] != null)
                {
                    ToDoItem toDoForDelete = bt[aux].DataContext as ToDoItem;
                    App.ViewModel.DeleteToDoItem(toDoForDelete);
                    cbs.RemoveAt(aux);
                }

           }
           this.Focus();
           
        }

        private void CheckBox_Checked_1(object sender, RoutedEventArgs e)
        {
            cbs.Add(sender);
        }

        private void CheckBox_Unchecked_1(object sender, RoutedEventArgs e)
        {
            cbs.Remove(sender);
        }

        private void TextBlock_Tap_1(object sender, System.Windows.Input.GestureEventArgs e)
        {
            TextBlock tb = new TextBlock();
            tb = sender as TextBlock;
            (App.Current as App).item = tb.DataContext as ToDoItem;
            NavigationService.Navigate(new Uri("/ativDetalhes.xaml",UriKind.Relative));
            
        }

      
      
    }
}