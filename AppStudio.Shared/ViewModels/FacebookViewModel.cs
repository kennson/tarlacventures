using System;
using System.Windows;
using System.Windows.Input;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using AppStudio.Services;
using AppStudio.Data;

namespace AppStudio.ViewModels
{
    public class FacebookViewModel : ViewModelBase<FacebookSchema>
    {
        private RelayCommandEx<FacebookSchema> itemClickCommand;
        public RelayCommandEx<FacebookSchema> ItemClickCommand
        {
            get
            {
                if (itemClickCommand == null)
                {
                    itemClickCommand = new RelayCommandEx<FacebookSchema>(
                        (item) =>
                        {
                            NavigationServices.NavigateToPage("", item);
                        });
                }

                return itemClickCommand;
            }
        }

        override protected DataSourceBase<FacebookSchema> CreateDataSource()
        {
            return new FacebookDataSource(); // FacebookDataSource
        }


        override public Visibility GoToSourceVisibility
        {
            get { return ViewType == ViewTypes.Detail ? Visibility.Visible : Visibility.Collapsed; }
        }

        override protected void GoToSource()
        {
            base.GoToSource("{FeedUrl}");
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
    }
}
