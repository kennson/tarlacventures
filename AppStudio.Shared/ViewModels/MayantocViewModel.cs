using System;
using System.Windows;
using System.Windows.Input;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using AppStudio.Services;
using AppStudio.Data;

namespace AppStudio.ViewModels
{
    public class MayantocViewModel : ViewModelBase<MayantocSchema>
    {
        private RelayCommandEx<MayantocSchema> itemClickCommand;
        public RelayCommandEx<MayantocSchema> ItemClickCommand
        {
            get
            {
                if (itemClickCommand == null)
                {
                    itemClickCommand = new RelayCommandEx<MayantocSchema>(
                        (item) =>
                        {
                            NavigationServices.NavigateToPage("MayantocDetail", item);
                        });
                }

                return itemClickCommand;
            }
        }

        override protected DataSourceBase<MayantocSchema> CreateDataSource()
        {
            return new MayantocDataSource(); // CollectionDataSource
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
            NavigationServices.NavigateToPage("MayantocList");
        }

        override protected void NavigateToSelectedItem()
        {
            NavigationServices.NavigateToPage("MayantocDetail");
        }
    }
}
