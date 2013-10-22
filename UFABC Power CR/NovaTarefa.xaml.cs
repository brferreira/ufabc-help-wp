using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
// Directive for the data model.
using banco_de_dados_local.Model;

namespace UFABC_Power_CR
{
    public partial class NovaTarefa : PhoneApplicationPage
    {
        DateTime data = DateTime.Now;
        DateTime hora = DateTime.Now;
        public NovaTarefa()
        {
            InitializeComponent();
            this.DataContext = App.ViewModel;
            hora = DateTime.Now.ToLocalTime();
        }

        private void appBarOkButton_Click(object sender, EventArgs e)
        {
            // Confirm there is some text in the text box.
            if (newTaskNameTextBox.Text.Length > 0)
            {
                // Create a new to-do item.
                ToDoItem newToDoItem = new ToDoItem
                {
                    ItemName = newTaskNameTextBox.Text,
                    Category = (ToDoCategory)categoriesListPicker.SelectedItem,
                    ItemData = data.ToShortDateString(),
                    ItemHora = hora.ToString("HH:mm"),
                    ItemDetalhes = tbdDetalhes.Text
                };

                if ((App.Current as App).edita == false)
                {
                    // Add the item to the ViewModel.
                    App.ViewModel.AddToDoItem(newToDoItem);
                }
                else
                {
                    App.ViewModel.EditaItem((App.Current as App).item.ToDoItemId, newToDoItem);
                    
                }
                // Return to the main page.
                if (NavigationService.CanGoBack)
                {
                    NavigationService.GoBack();
                }
            }
        }

        private void appBarCancelButton_Click(object sender, EventArgs e)
        {
            // Return to the main page.
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }

        private void DatePicker1_ValueChanged_1(object sender, DateTimeValueChangedEventArgs e)
        {
            data =(DateTime)e.NewDateTime;
        }

        private void horaPicker_ValueChanged_1(object sender, DateTimeValueChangedEventArgs e)
        {
            hora = (DateTime)e.NewDateTime;
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            if ((App.Current as App).edita == true)
            {
                newTaskNameTextBox.Text = (App.Current as App).item.ItemName;
                if ((App.Current as App).item.Category.Id.Equals(1))
                {
                    categoriesListPicker.SelectedIndex = 0;
                }
                else if((App.Current as App).item.Category.Id.Equals(2))
                {
                    categoriesListPicker.SelectedIndex = 1;
                }
                else if ((App.Current as App).item.Category.Id.Equals(3))
                {
                    categoriesListPicker.SelectedIndex = 2;
                }
                else
                {
                    categoriesListPicker.SelectedIndex = 3;
                }
                categoriesListPicker.IsEnabled = false;
                DatePicker1.Value = DateTime.Parse((App.Current as App).item.ItemData);
                horaPicker.Value = DateTime.Parse((App.Current as App).item.ItemHora);
                tbdDetalhes.Text = (App.Current as App).item.ItemDetalhes;

            }
        }
    }
}