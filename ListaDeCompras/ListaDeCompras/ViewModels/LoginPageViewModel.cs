using ListaDeCompras.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace ListaDeCompras.ViewModels
{
    public class LoginPageViewModel : BindableBase
    {
        private INavigationService _navigationService;
        private IPageDialogService _pageDialogService;
        private IAutenticacaoService _autenticacao;

        private string _userName;
        private string _password;
        private bool _allFieldsAreValid;
        private bool _isLoading = false;

        public LoginPageViewModel(INavigationService navigationService,
                                  IPageDialogService pageDialogService,
                                  IAutenticacaoService autenticacao)
        {
            _navigationService = navigationService;
            _pageDialogService = pageDialogService;
            _autenticacao = autenticacao;

            OnLoginCommand = new DelegateCommand(DoLogin).ObservesCanExecute(() => AllFieldsAreValid);
        }

        public string UserName
        {
            get { return _userName; }
            set
            {
                SetProperty(ref _userName, value);
                AllFieldsAreValid = (!string.IsNullOrWhiteSpace(_userName) && !string.IsNullOrWhiteSpace(_password));
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                SetProperty(ref _password, value);
                AllFieldsAreValid = (!string.IsNullOrWhiteSpace(_userName) && !string.IsNullOrWhiteSpace(_password));
            }
        }

        public bool IsLoading
        {
            get { return _isLoading; }
            set { SetProperty(ref _isLoading, value); }
        }

        public ICommand OnLoginCommand { get; set; }

        public bool AllFieldsAreValid
        {
            get { return _allFieldsAreValid; }
            set { SetProperty(ref _allFieldsAreValid, value); }
        }

        public async void DoLogin()
        {
            IsLoading = true;

            if (await _autenticacao.Autentica(UserName, Password))
            {
                await _navigationService.NavigateAsync(new Uri("/ListaDeComprasTabs", UriKind.Absolute));
            }
            else
            {
                await _pageDialogService.DisplayAlertAsync("Credenciais Inválidas", "Não foi possível entrar com o email/senha informados", "Ok");
            }

            IsLoading = false;
        }
    }
}
