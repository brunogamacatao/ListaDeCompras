using ListaDeCompras.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;

namespace ListaDeCompras.ViewModels
{
	public class RegistroPageViewModel : BindableBase
	{
        private INavigationService _navigationService;
        private IPageDialogService _pageDialogService;
        private IAutenticacaoService _autenticacao;

        public RegistroPageViewModel(INavigationService navigationService,
                                     IPageDialogService pageDialogService,
                                     IAutenticacaoService autenticacao)
        {
            _navigationService = navigationService;
            _pageDialogService = pageDialogService;
            _autenticacao = autenticacao;
        }

        private string _username;
        public string UserName
        {
            get { return _username; }
            set { SetProperty(ref _username, value); OnRegistrarCommand.RaiseCanExecuteChanged(); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); OnRegistrarCommand.RaiseCanExecuteChanged(); }
        }

        private bool _isloading = false;
        public bool IsLoading
        {
            get { return _isloading; }
            set { SetProperty(ref _isloading, value); }
        }

        private DelegateCommand registrarCommand;
        public DelegateCommand OnRegistrarCommand =>
            registrarCommand ?? (registrarCommand = new DelegateCommand(OnRegistrar, CanOnRegistrar));

        private async void OnRegistrar()
        {
            IsLoading = true;

            if (await _autenticacao.Registrar(UserName, Password))
            {
                IsLoading = false;
                await _pageDialogService.DisplayAlertAsync("Sucesso", "Usuário registrado com sucesso", "Ok");
                await _navigationService.NavigateAsync(new Uri("/LoginPage", UriKind.Absolute));
            }
            else
            {
                IsLoading = false;
                await _pageDialogService.DisplayAlertAsync("Erro", "Não foi possível registrar-se", "Ok");
            }
        }

        private bool CanOnRegistrar()
        {
            return !string.IsNullOrWhiteSpace(UserName) && !string.IsNullOrWhiteSpace(Password);
        }
    }
}
