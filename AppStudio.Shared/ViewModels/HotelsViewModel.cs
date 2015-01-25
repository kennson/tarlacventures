using System;
using System.Windows;
using System.Windows.Input;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using AppStudio.Services;
using AppStudio.Data;

namespace AppStudio.ViewModels
{
    public class HotelsViewModel : ViewModelBase<HotelsSchema>
    {
        private RelayCommandEx<HotelsSchema> itemClickCommand;
        public RelayCommandEx<HotelsSchema> ItemClickCommand
        {
            get
            {
                if (itemClickCommand == null)
                {
                    itemClickCommand = new RelayCommandEx<HotelsSchema>(
                        (item) =>
                        {
                            NavigationServices.NavigateToPage("HotelsDetail", item);
                        });
                }

                return itemClickCommand;
            }
        }

        override protected DataSourceBase<HotelsSchema> CreateDataSource()
        {
            return new HotelsDataSource(); // CollectionDataSource
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
            NavigationServices.NavigateToPage("HotelsList");
        }

        override protected void NavigateToSelectedItem()
        {
            NavigationServices.NavigateToPage("HotelsDetail");
        }
    }
}
