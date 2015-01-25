using AppStudio.ViewModels;

using Windows.ApplicationModel.DataTransfer;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace AppStudio.Views
{
    public sealed partial class YouTubeDetail : Page
    {
        private DataTransferManager _dataTransferManager;

        private NavigationHelper _navigationHelper;

        private DisplayOrientations _currentOrientations;

        public YouTubeDetail()
        {
            this.InitializeComponent();
            _navigationHelper = new NavigationHelper(this);

            ytViewer.NavigationHelper = _navigationHelper;

            YouTubeModel = new YouTubeViewModel();

            DataContext = this;

            ApplicationView.GetForCurrentView().
                SetDesiredBoundsMode(ApplicationViewBoundsMode.UseVisible);
        }

        public YouTubeViewModel YouTubeModel { get; private set; }

        public NavigationHelper NavigationHelper
        {
            get { return _navigationHelper; }
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            _dataTransferManager = DataTransferManager.GetForCurrentView();
            _dataTransferManager.DataRequested += OnDataRequested;

            _navigationHelper.OnNavigatedTo(e);

            if (YouTubeModel != null)
            {
                await YouTubeModel.LoadItemsAsync();
                YouTubeModel.SelectItem(e.Parameter);

                YouTubeModel.ViewType = ViewTypes.Detail;
            }

            ytViewer.OnNavigatedTo();

            // Allow this page to rotate
            _currentOrientations = DisplayInformation.AutoRotationPreferences;
            DisplayInformation.AutoRotationPreferences = DisplayOrientations.Portrait
                                                        | DisplayOrientations.Landscape
                                                        | DisplayOrientations.LandscapeFlipped
                                                        | DisplayOrientations.PortraitFlipped;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            _navigationHelper.OnNavigatedFrom(e);

            _navigationHelper.OnNavigatedFrom(e);
            
            ytViewer.EmbedUrl = null;

            ytViewer.OnNavigatedFrom();

            // Restore previous rotation preferences
            DisplayInformation.AutoRotationPreferences = _currentOrientations;
        }

        private void OnDataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            if (YouTubeModel != null)
            {
                YouTubeModel.GetShareContent(args.Request);
            }
        }
    }
}
