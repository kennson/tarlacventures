using System;
using System.Windows;
using System.Windows.Input;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using AppStudio.Services;
using AppStudio.Data;

namespace AppStudio.ViewModels
{
    public class YouTubeViewModel : ViewModelBase<YouTubeSchema>
    {
        private RelayCommandEx<YouTubeSchema> itemClickCommand;
        public RelayCommandEx<YouTubeSchema> ItemClickCommand
        {
            get
            {
                if (itemClickCommand == null)
                {
                    itemClickCommand = new RelayCommandEx<YouTubeSchema>(
                        (item) =>
                        {
                            NavigationServices.NavigateToPage("YouTubeDetail", item);
                        });
                }

                return itemClickCommand;
            }
        }

        override protected DataSourceBase<YouTubeSchema> CreateDataSource()
        {
            return new YouTubeDataSource(); // YouTubeDataSource
        }


        override public Visibility GoToSourceVisibility
        {
            get { return ViewType == ViewTypes.Detail ? Visibility.Visible : Visibility.Collapsed; }
        }

        override protected void GoToSource()
        {
            base.GoToSource("{ExternalUrl}");
        }

        override public Visibility RefreshVisibility
        {
            get { return ViewType == ViewTypes.List ? Visibility.Visible : Visibility.Collapsed; }
        }

        public RelayCommandEx<Slider> IncreaseSlider
        {
            get
            {
                return new RelayCommandEx<Slider>(s => s.Value++);
            }
        }

        public RelayCommandEx<Slider> DecreaseSlider
        {
            get
            {
                return new RelayCommandEx<Slider>(s => s.Value--);
            }
        }

        override public void NavigateToSectionList()
        {
            NavigationServices.NavigateToPage("YouTubeList");
        }

        override protected void NavigateToSelectedItem()
        {
            NavigationServices.NavigateToPage("YouTubeDetail");
        }

        public override void GetShareContent(Windows.ApplicationModel.DataTransfer.DataRequest dataRequest)
        {
            var currentItem = GetCurrentItem();
            if (currentItem != null)
            {
                // Share SelectedItem Title
                dataRequest.Data.Properties.Title = currentItem.DefaultTitle ?? App.APP_NAME;

                // Share SelectedItem Summary
                if (!String.IsNullOrEmpty(currentItem.ExternalUrl))
                {
                    dataRequest.Data.SetWebLink(new Uri(currentItem.ExternalUrl));
                }
            }
        }
    }
}
