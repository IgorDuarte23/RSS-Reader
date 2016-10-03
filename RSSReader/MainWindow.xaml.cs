using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.ServiceModel.Syndication;
using System.Diagnostics;

namespace RSSReader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Variables
        private readonly rssreader _model;
        private string _path;
        #endregion
        #region Properties
        private rssreader Model
        {
            get { return _model; }
        }
        #endregion
        public MainWindow()
        {
            _model = new rssreader();
            WindowState = WindowState.Maximized;
            Model.ReadUrls();
            InitializeComponent();
            DataContext = _model;

        }

        private void add_url_Click(object sender, RoutedEventArgs e)
        {
            Model.AddUrlOnArray();
        }
        private void del_url_Click(object sender, RoutedEventArgs e)
        {
            Model.DeleteUrlFromList();
        }

        private void Row_DoubleClick(object  sender, MouseButtonEventArgs e)
        {
            Model.ChangeDescription(feel,peel);
        }
        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new System.Diagnostics.ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }
    }
}
