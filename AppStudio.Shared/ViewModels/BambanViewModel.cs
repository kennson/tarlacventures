using System;
using System.Windows;
using System.Windows.Input;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using AppStudio.Services;
using AppStudio.Data;

namespace AppStudio.ViewModels
{
    public class BambanViewModel : ViewModelBase<BambanSchema>
    {
        private RelayCommandEx<BambanSchema> itemClickCommand;
        public RelayCommandEx<BambanSchema> ItemClickCommand
        {
            get
            {
                if (itemClickCommand == null)
                {
                    itemClickCommand = new RelayCommandEx<BambanSchema>(
                        (item) =>
                        {
                            NavigationServices.NavigateToPage("BambanDetail", item);
                        });
                }

                return itemClickCommand;
            }
        }

        override protected DataSourceBase<BambanSchema> CreateDataSource()
        {
            return new BambanDataSource(); // CollectionDataSource
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
            NavigationServices.NavigateToPage("BambanList");
        }

        override protected void NavigateToSelectedItem()
        {
            NavigationServices.NavigateToPage("BambanDetail");
        }
    }
}
