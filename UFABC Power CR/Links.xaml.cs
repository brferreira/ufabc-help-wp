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
    public partial class Links : PhoneApplicationPage
    {
        public Links()
        {
            InitializeComponent();
        }

        private void ufabc_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            WebBrowserTask wbt = new WebBrowserTask();
            wbt.Uri = new Uri("http://www.ufabc.edu.br/", UriKind.Absolute);
            wbt.Show();
        }

        private void imgPrograd_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            WebBrowserTask wbt = new WebBrowserTask();
            wbt.Uri = new Uri("http://prograd.ufabc.edu.br/", UriKind.Absolute);
            wbt.Show();
        }

        private void imgBiblio_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            WebBrowserTask wbt = new WebBrowserTask();
            wbt.Uri = new Uri("http://biblioteca.ufabc.edu.br/", UriKind.Absolute);
            wbt.Show();
        }

        private void imgRU_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            WebBrowserTask wbt = new WebBrowserTask();
            wbt.Uri = new Uri("http://proap.ufabc.edu.br/images/PDF/Cardapio.pdf", UriKind.Absolute);
            wbt.Show();
        }

        private void ingProap_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            WebBrowserTask wbt = new WebBrowserTask();
            wbt.Uri = new Uri("http://proap.ufabc.edu.br/", UriKind.Absolute);
            wbt.Show();
        }

        private void imgPropg_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            WebBrowserTask wbt = new WebBrowserTask();
            wbt.Uri = new Uri("http://propg.ufabc.edu.br/", UriKind.Absolute);
            wbt.Show();
        }

        private void imgPropes_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            WebBrowserTask wbt = new WebBrowserTask();
            wbt.Uri = new Uri("http://propes.ufabc.edu.br/", UriKind.Absolute);
            wbt.Show();
        }

        private void imgProex_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            WebBrowserTask wbt = new WebBrowserTask();
            wbt.Uri = new Uri("http://proex.ufabc.edu.br/", UriKind.Absolute);
            wbt.Show();
        }

        private void imgRI_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            WebBrowserTask wbt = new WebBrowserTask();
            wbt.Uri = new Uri("http://ri.ufabc.edu.br/", UriKind.Absolute);
            wbt.Show();
        }

        private void imgNti_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            WebBrowserTask wbt = new WebBrowserTask();
            wbt.Uri = new Uri("http://nti.ufabc.edu.br/", UriKind.Absolute);
            wbt.Show();
        }

        private void imgPu_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            WebBrowserTask wbt = new WebBrowserTask();
            wbt.Uri = new Uri("http://pu.ufabc.edu.br/", UriKind.Absolute);
            wbt.Show();
        }

        private void imgUab_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            WebBrowserTask wbt = new WebBrowserTask();
            wbt.Uri = new Uri("http://uab.ufabc.edu.br/", UriKind.Absolute);
            wbt.Show();
        }

        private void imgCecs_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            WebBrowserTask wbt = new WebBrowserTask();
            wbt.Uri = new Uri("http://cecs.ufabc.edu.br/", UriKind.Absolute);
            wbt.Show();
        }

        private void imgCMCC_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            WebBrowserTask wbt = new WebBrowserTask();
            wbt.Uri = new Uri("http://cmcc.ufabc.edu.br/", UriKind.Absolute);
            wbt.Show();
        }

        private void imgCCNH_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            WebBrowserTask wbt = new WebBrowserTask();
            wbt.Uri = new Uri("http://ccnh.ufabc.edu.br/", UriKind.Absolute);
            wbt.Show();
        }

        private void imgTidia_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            WebBrowserTask wbt = new WebBrowserTask();
            wbt.Uri = new Uri("http://tidia-ae.ufabc.edu.br/portal", UriKind.Absolute);
            wbt.Show();
        }

        private void dce_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            WebBrowserTask wbt = new WebBrowserTask();
            wbt.Uri = new Uri("http://www.dceufabc.com.br/", UriKind.Absolute);
            wbt.Show();
        }

        private void ImgDA_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            WebBrowserTask wbt = new WebBrowserTask();
            wbt.Uri = new Uri("https://www.facebook.com/DA.Sigma.UFABC", UriKind.Absolute);
            wbt.Show();
        }

        private void imgAxis_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            WebBrowserTask wbt = new WebBrowserTask();
            wbt.Uri = new Uri("http://axis.ufabc.net/", UriKind.Absolute);
            wbt.Show();
        }

        private void imgAR_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            WebBrowserTask wbt = new WebBrowserTask();
            wbt.Uri = new Uri("https://www.facebook.com/pages/Associa%C3%A7%C3%A3o-das-Rep%C3%BAblicas/244574892269620", UriKind.Absolute);
            wbt.Show();
        }

        private void imgIEEE_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            WebBrowserTask wbt = new WebBrowserTask();
            wbt.Uri = new Uri("https://www.facebook.com/ieee.ufabc", UriKind.Absolute);
            wbt.Show();
        }

        private void imgJr_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            WebBrowserTask wbt = new WebBrowserTask();
            wbt.Uri = new Uri("https://www.facebook.com/ufabcjr", UriKind.Absolute);
            wbt.Show();
        }

        private void imgGPDA_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            WebBrowserTask wbt = new WebBrowserTask();
            wbt.Uri = new Uri("http://gpdaufabc.com.br/", UriKind.Absolute);
            wbt.Show();
        }

        private void imgBaja_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            WebBrowserTask wbt = new WebBrowserTask();
            wbt.Uri = new Uri("http://bajaufabc.com.br/", UriKind.Absolute);
            wbt.Show();
        }

        private void imgAnime_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            WebBrowserTask wbt = new WebBrowserTask();
            wbt.Uri = new Uri("http://animeufabc.wordpress.com/", UriKind.Absolute);
            wbt.Show();
        }

        private void imgCheers_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            WebBrowserTask wbt = new WebBrowserTask();
            wbt.Uri = new Uri("https://www.facebook.com/cheerUFABC", UriKind.Absolute);
            wbt.Show();
        }

        private void imgInfanteria_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            WebBrowserTask wbt = new WebBrowserTask();
            wbt.Uri = new Uri("https://www.facebook.com/pages/Infanteria/134997089896817", UriKind.Absolute);
            wbt.Show();
        }

        private void imgGeb_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            WebBrowserTask wbt = new WebBrowserTask();
            wbt.Uri = new Uri("http://gebufabc.webs.com/", UriKind.Absolute);
            wbt.Show();
        }

        private void imgLiga_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            WebBrowserTask wbt = new WebBrowserTask();
            wbt.Uri = new Uri("https://www.facebook.com/pages/Liga-das-Lutas-UFABC/132835136818895?fref=ts", UriKind.Absolute);
            wbt.Show();
        }

        private void imgDiv_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            WebBrowserTask wbt = new WebBrowserTask();
            wbt.Uri = new Uri("https://www.facebook.com/UFABCDiversidade?fref=ts", UriKind.Absolute);
            wbt.Show();
        }

        private void imgAlcool_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            WebBrowserTask wbt = new WebBrowserTask();
            wbt.Uri = new Uri("https://www.facebook.com/tamandualcool.ufabc.7?fref=ts", UriKind.Absolute);
            wbt.Show();
        }

        private void imgCaap_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            WebBrowserTask wbt = new WebBrowserTask();
            wbt.Uri = new Uri("https://www.facebook.com/caap.ufabc", UriKind.Absolute);
            wbt.Show();
        }

        private void imgLue_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            WebBrowserTask wbt = new WebBrowserTask();
            wbt.Uri = new Uri("http://lueufabc.com.br/index.html", UriKind.Absolute);
            wbt.Show();
        }
    }
}