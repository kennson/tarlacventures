using System.ComponentModel;
using AppStudio.Data;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace AppStudio.Controls
{
    public sealed partial class YouTubeViewer : UserControl, INotifyPropertyChanged
    {
        private NavigationHelper _navigationHelper;

        public static readonly DependencyProperty TitleTextProperty =
            DependencyProperty.Register("TitleText", typeof(string), typeof(YouTubeViewer), new PropertyMetadata(null));

        public static readonly DependencyProperty SummaryTextProperty =
            DependencyProperty.Register("SummaryText", typeof(string), typeof(YouTubeViewer), new PropertyMetadata(null));

        public static readonly DependencyProperty EmbedUrlProperty =
            DependencyProperty.Register("EmbedUrl", typeof(string), typeof(YouTubeViewer), new PropertyMetadata(null));

        public string TitleText
        {
            get { return (string)GetValue(TitleTextProperty); }
            set { SetValue(TitleTextProperty, value); }
        }

        public string SummaryText
        {
            get { return (string)GetValue(SummaryTextProperty); }
            set { SetValue(SummaryTextProperty, value); }
        }

        public string EmbedUrl
        {
            get { return (string)GetValue(EmbedUrlProperty); }
            set
            {
                SetValue(EmbedUrlProperty, value);
                OnPropertyChanged("EmbedUrl");
            }
        }

        public NavigationHelper NavigationHelper
        {
            get
            {
                return _navigationHelper;
            }
            set
            {
                _navigationHelper = value;
                OnPropertyChanged("NavigationHelper");
            }
        }

        public bool NetworkAvailable
        {
            get { return InternetConnection.IsInternetAvailable(); }
        }

        public YouTubeViewer()
        {
            this.InitializeComponent();

            DataContext = this;

            SizeChanged += OnSizeChanged;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Width < 500)
            {
                VisualStateManager.GoToState(this, "SnappedView", true);
            }
            else if (e.NewSize.Width < e.NewSize.Height)
            {
                VisualStateManager.GoToState(this, "PortraitView", true);
            }
            else
            {
                VisualStateManager.GoToState(this, "FullscreenView", true);
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
