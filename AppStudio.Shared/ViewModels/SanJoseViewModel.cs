using System;
using System.Windows;
using System.Windows.Input;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using AppStudio.Services;
using AppStudio.Data;

namespace AppStudio.ViewModels
{
    public class SanJoseViewModel : ViewModelBase<SanJoseSchema>
    {
        private RelayCommandEx<SanJoseSchema> itemClickCommand;
        public RelayCommandEx<SanJoseSchema> ItemClickCommand
        {
            get
            {
                if (itemClickCommand == null)
                {
                    itemClickCommand = new RelayCommandEx<SanJoseSchema>(
                        (item) =>
                        {
                            NavigationServices.NavigateToPage("SanJoseDetail", item);
                        });
                }

                return itemClickCommand;
            }
        }

        override protected DataSourceBase<SanJoseSchema> CreateDataSource()
        {
            return new SanJoseDataSource(); // CollectionDataSource
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
            NavigationServices.NavigateToPage("SanJoseList");
        }

        override protected void NavigateToSelectedItem()
        {
            NavigationServices.NavigateToPage("SanJoseDetail");
        }
    }
}
