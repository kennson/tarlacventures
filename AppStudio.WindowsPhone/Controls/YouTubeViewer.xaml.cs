using System.ComponentModel;
using AppStudio.Data;
using Windows.Graphics.Display;
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
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnNavigatedTo()
        {
            // Handle orientation changes
            DisplayInformation.GetForCurrentView().OrientationChanged += this.OnOrientationChanged;
            this.TransitionStoryboardState();
        }

        public void OnNavigatedFrom()
        {
            // Handle orientation changes
            DisplayInformation.GetForCurrentView().OrientationChanged -= this.OnOrientationChanged;
        }

        private void OnOrientationChanged(DisplayInformation sender, object args)
        {
            this.TransitionStoryboardState();
        }

        private void TransitionStoryboardState()
        {
            string displayOrientation;

            switch (DisplayInformation.GetForCurrentView().CurrentOrientation)
            {
                case DisplayOrientations.Landscape:
                case DisplayOrientations.LandscapeFlipped:
                    displayOrientation = "Landscape";
                    break;
                case DisplayOrientations.Portrait:
                case DisplayOrientations.PortraitFlipped:
                default:
                    displayOrientation = "Portrait";
                    break;
            }

            VisualStateManager.GoToState(this, displayOrientation, false);
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
