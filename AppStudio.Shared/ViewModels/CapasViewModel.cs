using System;
using System.Windows;
using System.Windows.Input;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using AppStudio.Services;
using AppStudio.Data;

namespace AppStudio.ViewModels
{
    public class CapasViewModel : ViewModelBase<CapasSchema>
    {
        private RelayCommandEx<CapasSchema> itemClickCommand;
        public RelayCommandEx<CapasSchema> ItemClickCommand
        {
            get
            {
                if (itemClickCommand == null)
                {
                    itemClickCommand = new RelayCommandEx<CapasSchema>(
                        (item) =>
                        {
                            NavigationServices.NavigateToPage("CapasDetail", item);
                        });
                }

                return itemClickCommand;
            }
        }

        override protected DataSourceBase<CapasSchema> CreateDataSource()
        {
            return new CapasDataSource(); // CollectionDataSource
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
            NavigationServices.NavigateToPage("CapasList");
        }

        override protected void NavigateToSelectedItem()
        {
            NavigationServices.NavigateToPage("CapasDetail");
        }
    }
}
