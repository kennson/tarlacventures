using System;
using System.Windows;
using System.Windows.Input;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using AppStudio.Services;
using AppStudio.Data;

namespace AppStudio.ViewModels
{
    public class EmergencySecurityViewModel : ViewModelBase<EmergencySecuritySchema>
    {
        private RelayCommandEx<EmergencySecuritySchema> itemClickCommand;
        public RelayCommandEx<EmergencySecuritySchema> ItemClickCommand
        {
            get
            {
                if (itemClickCommand == null)
                {
                    itemClickCommand = new RelayCommandEx<EmergencySecuritySchema>(
                        (item) =>
                        {
                            NavigationServices.NavigateToPage("EmergencySecurityDetail", item);
                        });
                }

                return itemClickCommand;
            }
        }

        override protected DataSourceBase<EmergencySecuritySchema> CreateDataSource()
        {
            return new EmergencySecurityDataSource(); // CollectionDataSource
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
            NavigationServices.NavigateToPage("EmergencySecurityList");
        }

        override protected void NavigateToSelectedItem()
        {
            NavigationServices.NavigateToPage("EmergencySecurityDetail");
        }
    }
}
