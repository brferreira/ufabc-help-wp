using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace UFABC_Power_CR
{
    public partial class VinculaFb : PhoneApplicationPage
    {
        public VinculaFb()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml"));
        }

        private void btLogin_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/FacebookLogin.xaml"));
        }
    }
}