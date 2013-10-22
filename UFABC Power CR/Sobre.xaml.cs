using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;

namespace UFABC_Power_CR
{
    public partial class Sobre : PhoneApplicationPage
    {
        bool logado = false;
        public Sobre()
        {
            InitializeComponent();
        }


        private void TextBlock_Tap_1(object sender, System.Windows.Input.GestureEventArgs e)
        {
            EmailComposeTask email = new EmailComposeTask();
            email.To = "ufabcpowercr@outlook.com";
            email.Show();
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Phone.Tasks.MarketplaceReviewTask a = new MarketplaceReviewTask();
            a.Show();
        }

        

        
    }
}