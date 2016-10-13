using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using System.ComponentModel;
using System.Xml;
using System.ServiceModel.Syndication;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Controls;

namespace RSSReader_1
{
    class rssreader
    {
        #region variaveis
        string mydocpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private ObservableCollection<url> urls = new ObservableCollection<url>();
        private ObservableCollection<string> url = new ObservableCollection<string>();
        private string _link;
        private string _selectedUrl;
        private string _urlToAdd;
        private url _selectedCustomer;
        private string _descriptionMessage;
        #endregion
        #region propriedades


        public url SelectedCustomer
        {
            get
            {
                return _selectedCustomer;
            }

            set
            {
                _selectedCustomer = value;
                Link = _selectedCustomer.HyperLink;
                DescriptionMessage = _selectedCustomer.Description;
                OnPropertyChanged("SelectedCustomer");
            }
        }

        public ObservableCollection<url> Urls
        {
            get
            {
                return urls;
            }
            set
            {
                urls = value;
                OnPropertyChanged("Urls");
            }
        }
        public ObservableCollection<string> Urlsss
        {
            get
            {
                return url;
            }
            set
            {
                url = value;
                OnPropertyChanged("Urlsss");
            }
        }

        public string SelectedUrls
        {
            get
            {
                return _selectedUrl;
            }
            set
            {
                _selectedUrl = value;
                RssReaderFromUrl(_selectedUrl);
                OnPropertyChanged("SelectedUrls");
            }
        }

        public string UrlToAdd
        {
            get
            { 
                return _urlToAdd;
            }
            set
            { 
               _urlToAdd = value;
               OnPropertyChanged("UrlToAdd");
            }
        }
        public string DescriptionMessage
        {
            get
            {
                return _descriptionMessage;
            }
            set
            {
                _descriptionMessage = value;
                OnPropertyChanged("DescriptionMessage");
            }

        }
        public string Link
        {
            get
            {
                return _link;
            }
            set
            {
                _link = value;
                OnPropertyChanged("Link");
            }
        }

        #endregion
        #region construtor
        public rssreader()
        {
            urls.CollectionChanged += urlsChanged;
       //     Link = "http://microsoft.com.br/";
        }
        #endregion
        #region adiciona na lista de urls
        public void AddUrlOnArray()
        {
            if (UrlToAdd != null)
            {
                Urlsss.Add(UrlToAdd);
                WriteUrlsOnTextFile();
              
            }
        }

        #endregion
        #region Remove Url
        public void WriteUrlsOnTextFileRemovingActualFile()
        {
            File.Delete( mydocpath + @"\RssReaderUrl.txt");
            using (StreamWriter outputFile = new StreamWriter(mydocpath + @"\RssReaderUrl.txt", true))
            {
                foreach (var item in Urlsss)
                {
                    outputFile.WriteLine(item);
                }
            }
        }

        public void DeleteUrlFromList()
        {
            if (SelectedUrls != null)
            {
                Urlsss.Remove(SelectedUrls);
                WriteUrlsOnTextFileRemovingActualFile();
            }
        }
        #endregion
        #region Escreve Url no .txt
        public void WriteUrlsOnTextFile()
        {
            using (StreamWriter outputFile = new StreamWriter(mydocpath + @"\RssReaderUrl.txt", true))
            {
                    outputFile.WriteLine(UrlToAdd);
            }
        }
        #endregion    
        #region le as urls e o RSS
        public void ReadUrls()
        {
            if (!System.IO.File.Exists(@"\RssReaderUrl.txt"))
            {
                StreamWriter outputfile = new StreamWriter(mydocpath + @"\RssReaderUrl.txt", true);
                outputfile.Close();
            }
            using (StreamReader inputFile = new StreamReader(mydocpath + @"\RssReaderUrl.txt", true))
            {
                string line;
                while ((line = inputFile.ReadLine()) != null)
                {
                    Urlsss.Add(line);
                }
            }

        }
        public void RssReaderFromUrl(string url)
        {
            
                    XmlReader reader = XmlReader.Create(url);
                    int index = 0;
                        SyndicationFeed feed = SyndicationFeed.Load(reader);
                        reader.Close();
                        ObservableCollection<url> u = new ObservableCollection<url>();
                        foreach (SyndicationItem item in feed.Items)
                        {
                            
                            String titulo = item.Title.Text.ToString();
                            String autor = GetAutor(item.Authors);
                            String data = item.PublishDate.DateTime.ToString();
                            String categoria = GetCategoria(item.Categories);
                            String description = item.Summary.Text;
                            String hyperlink = item.Links[0].Uri.ToString();
                           
                            u.Add(new url(titulo, autor, data, categoria, url, description, index,hyperlink));
                            
                            index++;
                        }
                        Urls.Clear();
                        foreach (var item in u)
                        {
                            Urls.Add(item);
                        }
        }
        
                private string GetAutor(Collection<SyndicationPerson> collection)
                {
 	                var builder = new StringBuilder();
                            for (var i = 0; i < collection.Count; i++)
                            {
                                builder.Append(collection[i].Name);
                                if (i != collection.Count - 1)
                                    builder.Append(", ");
                            }
                            return builder.ToString();
                }

                private string GetCategoria(Collection<SyndicationCategory> collection)
                {
 	                  var builder = new StringBuilder();
                            for (var i = 0; i < collection.Count; i++)
                            {
                                builder.Append(collection[i].Name);
                                if (i != collection.Count - 1)
                                    builder.Append(", ");
                            }
                            return builder.ToString();
                }
   
        #endregion         
        #region PropertyChanged
                public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
        #region Atualiza DescriptionMessage
        private void urlsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("Urls");
        }
       

        internal void ChangeDescription(TextBlock feel,WebBrowser peel)
        {
            var newLink = new Uri(Link);
            feel.Text = DescriptionMessage;
            peel.Source = newLink;
        }
        #endregion
    }
}
