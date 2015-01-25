using System;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Net.NetworkInformation;

using Windows.UI.Xaml;

using AppStudio.Services;
using AppStudio.Data;

namespace AppStudio.ViewModels
{
    public class MainViewModel : BindableBase
    {
       private EcoTourismViewModel _ecoTourismModel;
       private HotelsViewModel _hotelsModel;
       private SocialNetworkViewModel _socialNetworkModel;
       private EmergencySecurityViewModel _emergencySecurityModel;
        private PrivacyViewModel _privacyModel;

        private ViewModelBase _selectedItem = null;

        public MainViewModel()
        {
            _selectedItem = EcoTourismModel;
            _privacyModel = new PrivacyViewModel();

        }
 
        public EcoTourismViewModel EcoTourismModel
        {
            get { return _ecoTourismModel ?? (_ecoTourismModel = new EcoTourismViewModel()); }
        }
 
        public HotelsViewModel HotelsModel
        {
            get { return _hotelsModel ?? (_hotelsModel = new HotelsViewModel()); }
        }
 
        public SocialNetworkViewModel SocialNetworkModel
        {
            get { return _socialNetworkModel ?? (_socialNetworkModel = new SocialNetworkViewModel()); }
        }
 
        public EmergencySecurityViewModel EmergencySecurityModel
        {
            get { return _emergencySecurityModel ?? (_emergencySecurityModel = new EmergencySecurityViewModel()); }
        }

        public void SetViewType(ViewTypes viewType)
        {
            EcoTourismModel.ViewType = viewType;
            HotelsModel.ViewType = viewType;
            SocialNetworkModel.ViewType = viewType;
            EmergencySecurityModel.ViewType = viewType;
        }

        public ViewModelBase SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                SetProperty(ref _selectedItem, value);
                UpdateAppBar();
            }
        }

        public Visibility AppBarVisibility
        {
            get
            {
                return SelectedItem == null ? AboutVisibility : SelectedItem.AppBarVisibility;
            }
        }

        public Visibility AboutVisibility
        {

      get { return Visibility.Collapsed; }
        }

        public void UpdateAppBar()
        {
            OnPropertyChanged("AppBarVisibility");
            OnPropertyChanged("AboutVisibility");
        }

        /// <summary>
        /// Load ViewModel items asynchronous
        /// </summary>
        public async Task LoadDataAsync(bool forceRefresh = false)
        {
            var loadTasks = new Task[]
            { 
                EcoTourismModel.LoadItemsAsync(forceRefresh),
                HotelsModel.LoadItemsAsync(forceRefresh),
                SocialNetworkModel.LoadItemsAsync(forceRefresh),
                EmergencySecurityModel.LoadItemsAsync(forceRefresh),
            };
            await Task.WhenAll(loadTasks);
        }

        //
        //  ViewModel command implementation
        //
        public ICommand RefreshCommand
        {
            get
            {
                return new DelegateCommand(async () =>
                {
                    await LoadDataAsync(true);
                });
            }
        }

        public ICommand AboutCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    NavigationServices.NavigateToPage("AboutThisAppPage");
                });
            }
        }

        public ICommand PrivacyCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    NavigationServices.NavigateTo(_privacyModel.Url);
                });
            }
        }
    }
}
