using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Facebook;
using banco_de_dados_local.Model;
using RestSharp.Deserializers;


namespace UFABC_Power_CR
{
    public partial class FacebookLogin : PhoneApplicationPage
    {
        private const string AppId = "386087481457415";
        private const string ExtendedPermissions = "user_about_me,read_stream,publish_stream,offline_access";
        private readonly FacebookClient _fb = new FacebookClient();


        public FacebookLogin()
        {
            InitializeComponent();
        }

        private void webBrowser1_Loaded(object sender, RoutedEventArgs e)
        {
            var loginUrl = GetFacebookLoginUrl(AppId, ExtendedPermissions);
            //webBrowser1.Navigate(loginUrl);
        }

        private Uri GetFacebookLoginUrl(string appId, string extendedPermissions)
        {
            var parameters = new Dictionary<string, object>();
            parameters["client_id"] = appId;
            parameters["redirect_uri"] = "https://www.facebook.com/connect/login_success.html";
            parameters["response_type"] = "token";
            parameters["display"] = "touch";

            // add the 'scope' only if we have extendedPermissions.
            if (!string.IsNullOrEmpty(extendedPermissions))
            {
                // A comma-delimited list of permissions
                parameters["scope"] = extendedPermissions;
            }

            return _fb.GetLoginUrl(parameters);
        }

        private void webBrowser1_Navigated(object sender, NavigationEventArgs e)
        {
            FacebookOAuthResult oauthResult;
            if (!_fb.TryParseOAuthCallbackUrl(e.Uri, out oauthResult))
            {
                return;
            }

            if (oauthResult.IsSuccess)
            {
                var accessToken = oauthResult.AccessToken;
                LoginSucceded(accessToken);
            }
            else
            {
                // user cancelled
                MessageBox.Show(oauthResult.ErrorDescription);
            }
        }

        private void LoginSucceded(string accessToken)
        {
            var fb = new FacebookClient(accessToken);

            fb.GetCompleted += (o, e) =>
            {
                if (e.Error != null)
                {
                    Dispatcher.BeginInvoke(() => MessageBox.Show(e.Error.Message));
                    return;
                }

                var result = (IDictionary<string, object>)e.GetResultData();
                var id = (string)result["id"];

                //var url = string.Format("/Pages/FacebookInfoPage.xaml?access_token={0}&id={1}", accessToken, id);

                App.id = id;
                App.token = accessToken;
                App.fblogin = true;

                var client = new RestSharp.RestClient("http://ufabchelp.me/server/index.php/usuario");
                var request = new RestSharp.RestRequest(RestSharp.Method.GET);
                request.AddParameter("token", accessToken);
                client.ExecuteAsync<Model.User>(request, (response, data) =>
                {
                    App.uid = response.Data.IdAluno;
                    App.tipo_usuario = response.Data.TipoUsuario;

                    // Verifica se ja está logado localmente
                    if (App.login)
                    {
                        App.db.Aluno.First().Uid = App.uid;
                        App.db.Aluno.First().Tipo_Usuario = App.tipo_usuario;
                        App.db.SubmitChanges();
                        Dispatcher.BeginInvoke(() =>
                        {
                            NavigationService.RemoveBackEntry();
                            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
                        });
                    }
                    else
                    {
                        bool existeUsuario = false;
                        UsuariosDataContext dbUsers = new UsuariosDataContext("Data Source='isostore:/usuarios.sdf';Password='w3h3hfs82'");
                        foreach (Usuario usuario in dbUsers.Usuarios)
                        {
                            if (usuario.FbId == App.id)//tem conta
                            {
                                byte[] cripto = usuario.Senha;
                                byte[] descripto = System.Security.Cryptography.ProtectedData.Unprotect(cripto, null);
                                App.db = new ToDoDataContext("Data Source='isostore:/" + usuario.Nome + ".sdf';Password='" + System.Text.Encoding.UTF8.GetString(descripto, 0, descripto.Length) + "'");
                                existeUsuario = true;
                                break;
                            }
                        }
                        if (existeUsuario == true)//tem conta
                        {
                            App.aluno = App.db.Aluno.First();
                            (App.Current as App).quadriAtual = App.aluno.QuadriAtual;
                            // Create the ViewModel object.



                            App.ViewModel = new banco_de_dados_local.ViewModel.ToDoViewModel();
                            // Query the local database and load observable collections.
                            App.ViewModel.LoadCollectionsFromDatabase();

                            App.login = true;
                            Dispatcher.BeginInvoke(() =>
                            {
                                NavigationService.RemoveBackEntry();
                                NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
                            });
                        }
                        else
                        {
                            Dispatcher.BeginInvoke(() =>
                            {
                                NavigationService.RemoveBackEntry();
                                NavigationService.Navigate(new Uri("/Inicio.xaml", UriKind.Relative));
                            });
                        }
                    }
                });
            };

            fb.GetAsync("me?fields=id");
        }

        private async void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            Facebook.Client.FacebookSessionClient fb = new Facebook.Client.FacebookSessionClient(AppId);
            var result = await fb.LoginAsync(ExtendedPermissions);
            if (result.AccessToken != null && result.AccessToken.Length > 0)
            {
                LoginSucceded(result.AccessToken);
            }
        }
    }
}