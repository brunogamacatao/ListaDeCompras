using ListaDeCompras.Events;
using ListaDeCompras.Model;
using ListaDeCompras.Services;
using Prism.Commands;
using Prism.Events;
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
        private IEventAggregator _eventAggregator;
        private IListaDeComprasService _listaDeComprasService;
        private List<Item> _itens;
        private bool _isLoading = false;

        public ListaDeComprasPageViewModel( IEventAggregator eventAggregator, 
                                            IListaDeComprasService listaDeComprasService)
        {
            _eventAggregator = eventAggregator;
            _listaDeComprasService = listaDeComprasService;
            CarregaItens();

            // Sempre que a lista de compras mudar, chama o método ListaDeComprasMudou
            _eventAggregator.GetEvent<ListaDeComprasMudouEvent>().Subscribe(ListaDeComprasMudou);
        }

        public void ListaDeComprasMudou(bool mudou)
        {
            CarregaItens();
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

        void CarregaItens()
        {
            Task.Run(async () => {
                IsLoading = true;
                Itens = await _listaDeComprasService.Listar();
                IsLoading = false;
            });            
        }

        public async void OnRemover(string ItemId)
        {
            IsLoading = true;
            await _listaDeComprasService.Remover(ItemId);
            IsLoading = false;
            CarregaItens();
        }

        public async void OnMudouEstadoComprado(string ItemId)
        {
            IsLoading = true;

            Item itemSelecionado = null;

            foreach (var item in Itens)
            {
                if (item.ItemId.Equals(ItemId))
                {
                    itemSelecionado = item;
                    break;
                }
            }

            if (itemSelecionado != null)
            {
                System.Diagnostics.Debug.WriteLine($"Mudando o estado do item {ItemId} para {itemSelecionado.Comprado}");
                await _listaDeComprasService.Alterar(itemSelecionado);
            }

            IsLoading = false;
        }
    }
}
