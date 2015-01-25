using System;
using System.Windows;
using System.Windows.Input;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using AppStudio.Services;
using AppStudio.Data;

namespace AppStudio.ViewModels
{
    public class InstagramViewModel : ViewModelBase<InstagramSchema>
    {
        private RelayCommandEx<InstagramSchema> itemClickCommand;
        public RelayCommandEx<InstagramSchema> ItemClickCommand
        {
            get
            {
                if (itemClickCommand == null)
                {
                    itemClickCommand = new RelayCommandEx<InstagramSchema>(
                        (item) =>
                        {
                            NavigationServices.NavigateToPage("InstagramDetail", item);
                        });
                }

                return itemClickCommand;
            }
        }

        override protected DataSourceBase<InstagramSchema> CreateDataSource()
        {
            return new InstagramDataSource(); // InstagramDataSource
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
            NavigationServices.NavigateToPage("InstagramList");
        }

        override protected void NavigateToSelectedItem()
        {
            NavigationServices.NavigateToPage("InstagramDetail");
        }
    }
}
