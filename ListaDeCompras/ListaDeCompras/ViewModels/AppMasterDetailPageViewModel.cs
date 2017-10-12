using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ListaDeCompras.ViewModels
{
	public class AppMasterDetailPageViewModel : BindableBase
	{
        private INavigationService _navigationService;

        public AppMasterDetailPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        private DelegateCommand logoutCommand;
        public DelegateCommand OnLogoutCommand =>
            logoutCommand ?? (logoutCommand = new DelegateCommand(OnLogout));

        private async void OnLogout()
        {
            await _navigationService.NavigateAsync(new Uri("/LoginPage", UriKind.Absolute));
        }
    }
}
