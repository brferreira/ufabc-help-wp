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
    public partial class addProf : PhoneApplicationPage
    {
        public addProf()
        {
            InitializeComponent();
                var query = from banco_de_dados_local.Model.Professor prof in App.db.Professores where prof.Nome == App.professor select prof;
                foreach (banco_de_dados_local.Model.Professor prof in query)
                {
                    if (prof.Nome != null)
                    {
                        tbNome.Text = prof.Nome;
                    }
                    if (prof.Centro != null)
                    {
                        switch (prof.Centro)
                        {
                            case "CECS":
                                lpCentro.SelectedIndex = 0;
                                break;
                            case "CMCC":
                                lpCentro.SelectedIndex = 1;
                                break;
                            case "CCNH":
                                lpCentro.SelectedIndex = 2;
                                break;
                        }
                    }
                    if (prof.Local != null)
                    {
                        switch (prof.Local)
                        {
                            case "Bloco A":
                                lpLocal.SelectedIndex = 0;
                                break;
                            case "Bloco B":
                                lpLocal.SelectedIndex = 1;
                                break;
                            case "Bloco Sigma":
                                lpLocal.SelectedIndex = 2;
                                break;
                            case "Bloco Delta":
                                lpLocal.SelectedIndex = 3;
                                break;
                        }
                    }
                    if (prof.Email != null)
                    {
                        tbEmail.Text = prof.Email;
                    }
                    if (prof.Sala != null)
                    {
                        tbSala.Text = prof.Sala;
                    }
                    if (prof.Telefone != null)
                    {
                        tbTel.Text = prof.Telefone;
                    }
                }
        }

       
        private void btSalvar_Click(object sender, EventArgs e)
        {
                if (tbNome.Text.Equals("") == false)
                {
                    if (tbEmail.Text.Equals("") == false && tbEmail.Text.Contains('@') == false)
                    {
                        MessageBox.Show("Digite um e-mail válido, ou deixe o campo em branco");
                    }
                    else
                    {
                        string centro, local;
                        switch (lpCentro.SelectedIndex)
                        {
                            case 0:
                                centro = "CECS";
                                break;
                            case 1:
                                centro = "CMCC";
                                break;
                            case 2:
                                centro = "CCNH";
                                break;
                            default:
                                centro = "";
                                break;
                        }
                        switch (lpLocal.SelectedIndex)
                        {
                            case 0:
                                local = "Bloco A";
                                break;
                            case 1:
                                local = "Bloco B";
                                break;
                            case 2:
                                local = "Bloco Sigma";
                                break;
                            case 3:
                                local = "Bloco Delta";
                                break;
                            default:
                                local = "";
                                break;
                        }
                        var query = from banco_de_dados_local.Model.Professor prof in App.db.Professores where prof.Nome == App.professor select prof;
                        foreach (banco_de_dados_local.Model.Professor prof in query)
                        {
                            prof.Nome = tbNome.Text;
                            prof.Sala = tbSala.Text;
                            prof.Telefone = tbTel.Text;
                            prof.Email = tbEmail.Text;
                            prof.Local = local;
                            prof.Centro = centro;
                        }
                        App.db.SubmitChanges();
                        NavigationService.RemoveBackEntry();
                        NavigationService.RemoveBackEntry();
                        NavigationService.Navigate(new Uri("/Professores.xaml", UriKind.Relative));
                    }
                }
                else
                {
                    MessageBox.Show("Digite o nome do(a) professor(a)");
                }
        }

        private void btCancelar_Click(object sender, EventArgs e)
        {

            NavigationService.GoBack();
        }
    }
}