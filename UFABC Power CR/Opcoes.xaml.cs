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
using Facebook;

namespace UFABC_Power_CR
{
    public partial class Opcoes : PhoneApplicationPage
    {
        UsuariosDataContext dbUsers;
        public Opcoes()
        {
            InitializeComponent();
            dbUsers = new UsuariosDataContext("Data Source='isostore:/usuarios.sdf';Password='w3h3hfs82'");
            if (dbUsers.Login.Count() > 0)
            {
                if (dbUsers.Login.First().Nome == App.usuario)
                {
                    cbSenha.IsChecked = true;
                }
            }
            string profilePictureUrl = string.Format("https://graph.facebook.com/{0}/picture?type={1}&access_token={2}", App.id, "square", App.token);
            picProfile.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(profilePictureUrl)); 
        }

        private void cbSenha_Checked(object sender, RoutedEventArgs e)
        {

            if (dbUsers.Login.Count() >= 1)
            {
                if (dbUsers.Login.First().Nome != App.usuario)
                {
                    if (MessageBox.Show("Já existe um usuário com lembrete de senha. Deseja trocar para esse usuário?", "Aviso", MessageBoxButton.OKCancel).Equals(MessageBoxResult.OK))
                    {
                        dbUsers.Login.DeleteOnSubmit(dbUsers.Login.First());
                        dbUsers.SubmitChanges();

                        dbUsers.Login.InsertOnSubmit(new banco_de_dados_local.Model.Login { Nome = App.usuario, Auto = true });
                        dbUsers.SubmitChanges();
                    }
                }
            }
            else
            {
                dbUsers.Login.InsertOnSubmit(new banco_de_dados_local.Model.Login { Nome = App.usuario, Auto = false });
                dbUsers.SubmitChanges();
            }
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var fb = new FacebookClient(App.token);
            if (App.fblogin == true)
            {
                var logoutUrl = fb.GetLogoutUrl(new
                {
                    next = "https://www.facebook.com/connect/login_success.html",
                    access_token = fb.AccessToken
                });
                Dispatcher.BeginInvoke(() => web1.Navigate(logoutUrl));
                web1.Navigate(new Uri("https://www.facebook.com/logout.php?next=[www.facebook.com]&access_token=["+App.token+"]"));

            }
            App.db.Dispose();
            App.usuario = null;
            App.aluno = null;
            App.ViewModel = null;
            NavigationService.RemoveBackEntry();
            App.login = false;
            NavigationService.Navigate(new Uri("/Login.xaml", UriKind.Relative));
        }

        private void cbSenha_Unchecked(object sender, RoutedEventArgs e)
        {
            if (dbUsers.Login.Count() >= 1)
            {
                var login = from banco_de_dados_local.Model.Login log in dbUsers.Login where log.Nome == App.usuario select log;
                if (login.Count() > 0)
                {
                    dbUsers.Login.DeleteOnSubmit(dbUsers.Login.First());
                    dbUsers.SubmitChanges();
                }
            }

        }

        private void btApaga_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Deseja mesmo apagar a conta?", "Apagar?", MessageBoxButton.OKCancel).Equals(MessageBoxResult.OK))
            {
                App.aluno = null;
                App.ViewModel = null;
                App.db.DeleteDatabase();
                dbUsers.Usuarios.DeleteOnSubmit(dbUsers.Usuarios.Single(x => x.Nome == App.usuario));
                if (dbUsers.Login.Count() > 0)
                {
                    if (dbUsers.Login.First().Nome == App.usuario)
                    {
                        dbUsers.Login.DeleteOnSubmit(dbUsers.Login.First());
                    }
                }
                dbUsers.SubmitChanges();
                App.usuario = null;
                NavigationService.RemoveBackEntry();
                NavigationService.Navigate(new Uri("/Login.xaml", UriKind.Relative));
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var fb = new FacebookClient(App.token);
            if (App.fblogin == true)
            {
                var logoutUrl = fb.GetLogoutUrl(new
                {
                    next = "https://www.facebook.com/connect/login_success.html",
                    access_token = fb.AccessToken
                });
                Dispatcher.BeginInvoke(() => web1.Navigate(logoutUrl));
                }
        }









    }
}