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


namespace UFABC_Power_CR
{
    public partial class FacebookLogin : PhoneApplicationPage
    {


        private const string AppId = "386087481457415";
        private const string ExtendedPermissions = "user_about_me,read_stream,publish_stream";
        private readonly FacebookClient _fb = new FacebookClient();


        public FacebookLogin()
        {
            InitializeComponent();
        }

        private void webBrowser1_Loaded(object sender, RoutedEventArgs e)
        {
            var loginUrl = GetFacebookLoginUrl(AppId, ExtendedPermissions);
            webBrowser1.Navigate(loginUrl);
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

               /* using (var store = System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication())
                {
                            using (var isoFileStream = new System.IO.IsolatedStorage.IsolatedStorageFileStream("helplogin",System.IO.FileMode.OpenOrCreate,System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication()))
                            {
                                // Write the data from the textbox.
                                using (var isoFileWriter = new System.IO.StreamWriter(isoFileStream))
                                {
                                    isoFileWriter.WriteLine();
                                }
                            }
                        
                        else
                        {
                            //MessageBox.Show("Você não possui cadastro. Faça o cadastro para continuar");
                            App.fblogin = true;
                            Dispatcher.BeginInvoke(() => NavigationService.RemoveBackEntry());
                            Dispatcher.BeginInvoke(() => NavigationService.Navigate(new Uri("/Inicio.xaml", UriKind.Relative)));
                        }
                    }
                }*/
            };

            fb.GetAsync("me?fields=id");
            NavigationService.RemoveBackEntry();
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));

        }

    }
}