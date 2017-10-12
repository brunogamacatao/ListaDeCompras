using ListaDeCompras.Model;
using ListaDeCompras.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListaDeCompras.ViewModels
{
    public class ListaDeComprasPageViewModel : BindableBase
    {
        private IListaDeComprasService _listaDeComprasService;
        private List<Item> _itens;
        private bool _isLoading = false;

        public ListaDeComprasPageViewModel(IListaDeComprasService listaDeComprasService)
        {
            _listaDeComprasService = listaDeComprasService;
            Inicializa();
        }

        public List<Item> Itens
        {
            get { return _itens; }
            set { SetProperty(ref _itens, value); }
        }

        public bool IsLoading
        {
            get { return _isLoading; }
            set { SetProperty(ref _isLoading, value); }
        }

        void Inicializa()
        {
            Task.Run(async () => {
                IsLoading = true;
                Itens = await _listaDeComprasService.Listar();
                IsLoading = false;
            });            
        }
    }
}
