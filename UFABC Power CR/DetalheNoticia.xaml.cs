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
    public partial class DetalheNoticia : PhoneApplicationPage
    {
        public DetalheNoticia()
        {
            InitializeComponent();
        }

        private Model.News selected;

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(NavigationContext.QueryString["id"]);
            var range = (from news in MainPage.webServiceDataContext.News where news.Id == id select news);
            if (range.Count() != 1)
            {
                MessageBox.Show("Notícia inválida!");
                NavigationService.GoBack();
                return;
            }
            selected = range.Single();

            newsTitle.Text = selected.Title;
            newsContent.Text = selected.Body;
            string dateStr;
            double yearDiff = Math.Floor((DateTime.Now - selected.Timestamp).TotalDays / 365.0);
            double monDiff = Math.Floor((DateTime.Now - selected.Timestamp).TotalDays / 30.0);
            double dayDiff = Math.Floor((DateTime.Now - selected.Timestamp).TotalDays);
            if (yearDiff > 0)
            {
                dateStr = (yearDiff == 1 ? "um" : yearDiff.ToString()) + " ano" + (yearDiff != 1 ? "s" : "") + " atrás";
            }
            else if (monDiff > 0)
            {
                dateStr = (monDiff != 1 ? monDiff.ToString() + " meses" : "um mês") + " atrás";
            }
            else if (dayDiff == 7)
            {
                dateStr = "uma semana atrás";
            }
            else
            {
                dateStr = (dayDiff == 1 ? "ontem" : (dayDiff == 0 ? "hoje" : "há " + dayDiff.ToString() + " dias")) + ", ";
                if (selected.Timestamp.Minute <= 3)
                {
                    if (selected.Timestamp.Hour == 0) dateStr = dateStr + "à meia-noite";
                    else if (selected.Timestamp.Hour == 12) dateStr = dateStr + "ao meio-dia";
                    else dateStr = dateStr + selected.Timestamp.ToShortTimeString();
                }
                else dateStr = dateStr + selected.Timestamp.ToShortTimeString();
            }
            newsAuthorDate.Text = selected.FacebookFirstNameFormatted + ", " + dateStr;
        }
    }
}