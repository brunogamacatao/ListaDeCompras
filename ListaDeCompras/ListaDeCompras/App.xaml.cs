using Prism.Unity;
using ListaDeCompras.Views;
using Xamarin.Forms;
using System;

namespace ListaDeCompras
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();

            NavigationService.NavigateAsync(new Uri("/LoginPage", UriKind.Absolute));
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<NavigationPage>();
            Container.RegisterTypeForNavigation<LoginPage>();
            Container.RegisterTypeForNavigation<ListaDeComprasPage>();
            Container.RegisterTypeForNavigation<ListaDeComprasTabs>();
            Container.RegisterTypeForNavigation<NovoItemPage>();
        }
    }
}
