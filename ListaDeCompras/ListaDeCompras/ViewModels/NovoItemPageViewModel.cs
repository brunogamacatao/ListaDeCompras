using ListaDeCompras.Events;
using ListaDeCompras.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ListaDeCompras.ViewModels
{
    public class NovoItemPageViewModel : BindableBase
    {
        private IEventAggregator _eventAggregator;
        private IListaDeComprasService _listaDeComprasService;

        public NovoItemPageViewModel(   IEventAggregator eventAggregator, 
                                        IListaDeComprasService listaDeComprasService)
        {
            _eventAggregator = eventAggregator;
            _listaDeComprasService = listaDeComprasService;
        }

        private string _nome;
        public string Nome
        {
            get { return _nome; }
            set { SetProperty(ref _nome, value); OnSalvarCommand.RaiseCanExecuteChanged(); }
        }

        private string _quantidade;
        public string Quantidade
        {
            get { return _quantidade; }
            set { SetProperty(ref _quantidade, value); OnSalvarCommand.RaiseCanExecuteChanged(); }
        }

        private bool _isLoading = false;
        public bool IsLoading
        {
            get { return _isLoading; }
            set { SetProperty(ref _isLoading, value); }
        }

        private DelegateCommand _salvarCommand;
        public DelegateCommand OnSalvarCommand =>
            _salvarCommand ?? (_salvarCommand = new DelegateCommand(OnSalvar, CanOnSalvar));

        private bool CanOnSalvar()
        {
            return !string.IsNullOrWhiteSpace(Nome) && !string.IsNullOrWhiteSpace(Quantidade);
        }

        private async void OnSalvar()
        {
            IsLoading = true;
            await _listaDeComprasService.Cadastrar(new Model.Item() {
                Nome = Nome,
                Quantidade = int.Parse(Quantidade)
            });
            IsLoading = false;

            // Limpa o formulário
            Nome = "";
            Quantidade = "";

            // Notifica os interessados que a lista de compras mudou
            _eventAggregator.GetEvent<ListaDeComprasMudouEvent>().Publish(true);
        }
    }
}
