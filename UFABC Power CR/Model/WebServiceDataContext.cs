using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Linq;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace UFABC_Power_CR.Model
{
    public class WebServiceDataContext
    {
        public WebServiceDataContext(Uri baseUri)
        {
            BaseUri = baseUri;
            News = new ObservableCollection<News>();
        }

        private async Task<string> GetUrlAsync(string url)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = BaseUri;
            var response = await client.GetAsync(url);
            return await response.Content.ReadAsStringAsync();
        }

        private async Task<bool> LoadNews()
        {
            News.Clear();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.BaseAddress = BaseUri;
            var response = await client.GetStringAsync(Model.News.GetPath());
            var newsList = JsonConvert.DeserializeObject<List<News>>(response);
            foreach (var news in newsList)
            {
                News.Add(news);
            }

            return true;
        }

        public async Task<bool> LoadData()
        {
            try
            {
                await LoadNews();

                return true;
            }
            catch { return false; }
        }

        public Uri BaseUri { get; private set; }

        public ObservableCollection<News> News;
    }

    public class WebServiceBase
    {
        static public string GetPath()
        {
            return "";
        }
    }

    public class News : WebServiceBase, INotifyPropertyChanging, INotifyPropertyChanged
    {
        new static public string GetPath()
        {
            return "/server/index.php/news";
        }

        private int _Id;
        [JsonProperty(PropertyName = "id")]
        public int Id
        {
            get
            {
                return _Id;
            }
            set
            {
                NotifyPropertyChanging("Id");
                _Id = value;
                NotifyPropertyChanged("Id");
            }
        }

        private int _IdFacebook;
        [JsonProperty(PropertyName = "id_facebook")]
        public int IdFacebook
        {
            get
            {
                return _IdFacebook;
            }
            set
            {
                NotifyPropertyChanging("IdFacebook");
                _IdFacebook = value;
                NotifyPropertyChanged("IdFacebook");
            }
        }

        private string _Title;
        [JsonProperty(PropertyName = "title")]
        public string Title
        {
            get
            {
                return _Title;
            }
            set
            {
                NotifyPropertyChanging("Title");
                _Title = value;
                NotifyPropertyChanged("Title");
            }
        }

        private string _Body;
        [JsonProperty(PropertyName = "body")]
        public string Body
        {
            get
            {
                return _Body;
            }
            set
            {
                NotifyPropertyChanging("Body");
                _Body = value;
                NotifyPropertyChanged("Body");
            }
        }

        private string _Image;
        [JsonProperty(PropertyName = "image")]
        public string Image
        {
            get
            {
                return _Image;
            }
            set
            {
                NotifyPropertyChanging("Image");
                _Image = value;
                NotifyPropertyChanged("Image");
            }
        }

        private int _Approved;
        [JsonProperty(PropertyName = "approved")]
        public int Approved
        {
            get
            {
                return _Approved;
            }
            set
            {
                NotifyPropertyChanging("Approved");
                _Approved = value;
                NotifyPropertyChanged("Approved");
            }
        }

        private DateTime _Timestamp;
        [JsonProperty(PropertyName = "timestamp")]
        public DateTime Timestamp
        {
            get
            {
                return _Timestamp;
            }
            set
            {
                NotifyPropertyChanging("Timestamp");
                _Timestamp = value;
                NotifyPropertyChanged("Timestamp");
            }
        }

        private string _FacebookFirstName;
        [JsonProperty(PropertyName = "fb_first_name")]
        public string FacebookFirstName
        {
            get
            {
                return _FacebookFirstName;
            }
            set
            {
                NotifyPropertyChanging("FacebookFirstName");
                _FacebookFirstName = value;
                NotifyPropertyChanged("FacebookFirstName");
            }
        }

        public string FacebookFirstNameFormatted { get { return "por " + _FacebookFirstName; } }

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region INotifyPropertyChanging Members
        public event PropertyChangingEventHandler PropertyChanging;

        private void NotifyPropertyChanging(string propertyName)
        {
            if (PropertyChanging != null)
            {
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }
        #endregion
    }
}
