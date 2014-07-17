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
    public partial class DetalheNoticia : PhoneApplicationPage
    {
        int id;
        public DetalheNoticia()
        {
            InitializeComponent();
        }

        private Model.News selected;

        private string formatDate(DateTime date)
        {
            string dateStr;
            double yearDiff = Math.Floor((DateTime.Now - date).TotalDays / 365.0);
            double monDiff = Math.Floor((DateTime.Now - date).TotalDays / 30.0);
            double dayDiff = Math.Floor((DateTime.Now - date).TotalDays);
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
                if (date.Minute <= 3)
                {
                    if (date.Hour == 0) dateStr = dateStr + "à meia-noite";
                    else if (date.Hour == 12) dateStr = dateStr + "ao meio-dia";
                    else dateStr = dateStr + date.ToShortTimeString();
                }
                else dateStr = dateStr + date.ToShortTimeString();
            }
            return dateStr;
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            id = int.Parse(NavigationContext.QueryString["id"]);
            if (id != -1)
            {
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

                newsAuthorDate.Text = selected.FacebookFirstNameFormatted + ", " + formatDate(selected.Timestamp);
            }
            else
            {
                newsTitle.Text = (App.Current as App).sItem.Title.Text;
                int index = (App.Current as App).sItem.Links.FirstOrDefault().Uri.ToString().IndexOf("&id");
                newsContent.Text = (App.Current as App).sItem.Summary.Text.Remove(0, 31);
                link.Text = "Leia mais em: " + "http://www.ufabc.edu.br/index.php?option=com_content" + (App.Current as App).sItem.Links.FirstOrDefault().Uri.ToString().Substring(index, 8);
                link.TextDecorations = TextDecorations.Underline;
                newsAuthorDate.Text = formatDate((App.Current as App).sItem.PublishDate.DateTime);


            }
        }

        private void link_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (id == -1)
            {
                WebBrowserTask webBrowserTask = new WebBrowserTask();
                webBrowserTask.Uri = (App.Current as App).sItem.Links.FirstOrDefault().Uri;
                webBrowserTask.Show();

            }
        }

        private void btSMS_Click(object sender, EventArgs e)
        {
            SmsComposeTask sms = new SmsComposeTask();
            if (id == -1)
            {
                sms.Body = newsTitle.Text + "\n\n" + link.Text + "\\nnNotícia enviada pelo app UFABC Help para Windows Phone";
            }
            else
            {
                sms.Body = newsTitle.Text + "\n\nLeia esta notícia no app UFABC Help para Windows Phone";
            }
            sms.Show();
        }

        private void btEmail_Click(object sender, EventArgs e)
        {
            EmailComposeTask email = new EmailComposeTask();
            email.Subject = newsTitle.Text;
            if (id == -1)
            {
                email.Body = newsContent.Text + "\n\n" + link.Text + "\n\nNotícia enviada pelo app UFABC Help para Windows Phone";
            }
            else
            {
                email.Body = newsAuthorDate.Text + "\n\n" + newsContent.Text + "\n\nNotícia enviada pelo app UFABC Help para Windows Phone";
            }
            email.Show();
        }
    }
}