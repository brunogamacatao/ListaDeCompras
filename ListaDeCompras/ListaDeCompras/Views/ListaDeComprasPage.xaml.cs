using System;
using ListaDeCompras.ViewModels;
using Prism.Commands;
using Xamarin.Forms;

namespace ListaDeCompras.Views
{
    public partial class ListaDeComprasPage : ContentPage
    {
        public ListaDeComprasPage()
        {
            InitializeComponent();
        }

        private DelegateCommand<string> _removerCommand;
        public DelegateCommand<string> OnRemoverCommand =>
            _removerCommand ?? (_removerCommand = new DelegateCommand<string>(OnRemover));

        private void OnRemover(string ItemId)
        {
            ((ListaDeComprasPageViewModel)this.BindingContext).OnRemover(ItemId);
        }

        private DelegateCommand<string> _mudouEstadoCompradoCommand;
        public DelegateCommand<string> OnMudouEstadoCompradoCommand =>
            _mudouEstadoCompradoCommand ?? (_mudouEstadoCompradoCommand = new DelegateCommand<string>(OnMudouEstadoComprado));

        private void OnMudouEstadoComprado(string ItemId)
        {
            ((ListaDeComprasPageViewModel)this.BindingContext).OnMudouEstadoComprado(ItemId);
        }
    }
}
