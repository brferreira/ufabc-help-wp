﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using banco_de_dados_local.Model;
using banco_de_dados_local.ViewModel;

namespace UFABC_Power_CR
{
    public partial class Login : PhoneApplicationPage
    {
        public Login()
        {
            InitializeComponent();
            UsuariosDataContext dbUsers = new UsuariosDataContext("Data Source='isostore:/usuarios.sdf';Password='w3h3hfs82'");
            if (!dbUsers.DatabaseExists())
            {
                dbUsers.CreateDatabase();
            }
            else
            {
                Queue<string> Usuarios = new Queue<string>();
                var users = from Usuario user in dbUsers.Usuarios select user;
                foreach (Usuario user in users)
                {
                    Usuarios.Enqueue(user.Nome);
                }
                tbNome.ItemsSource = Usuarios;
                if (dbUsers.Login.Count() > 0)
                {
                    tbNome.Text = dbUsers.Login.First().Nome;
                    byte[] cripto = dbUsers.Usuarios.Single(x => x.Nome == tbNome.Text).Senha;
                    byte[] descripto = System.Security.Cryptography.ProtectedData.Unprotect(cripto, null);
                    pbSenha.Password = System.Text.Encoding.UTF8.GetString(descripto, 0, descripto.Length);
                }
               
            }
        
        }

        private void btLogin_Click(object sender, RoutedEventArgs e)
        {
            App.db = new ToDoDataContext("Data Source='isostore:/" + tbNome.Text + ".sdf';Password='" + pbSenha.Password + "'");

            if (App.db.DatabaseExists().Equals(true))
            {

                try
                {
                    App.aluno = App.db.Aluno.First();
                    App.usuario = tbNome.Text;
                    (App.Current as App).quadriAtual = App.aluno.QuadriAtual;
                    // Create the ViewModel object.

                    

                    App.ViewModel = new banco_de_dados_local.ViewModel.ToDoViewModel();
                    // Query the local database and load observable collections.
                    App.ViewModel.LoadCollectionsFromDatabase();

                    
                    NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
                }
                catch 
                {
                    MessageBox.Show("A senha está incorreta");
                    pbSenha.Password = "";
                }
            }
            else
            {
                MessageBox.Show("O nome de usuário está incorreto");
                tbNome.Text = "";
                pbSenha.Password = "";
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Inicio.xaml", UriKind.Relative));
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack == true)
            {
                NavigationService.RemoveBackEntry();
            }
        }

        

        

    }
}
