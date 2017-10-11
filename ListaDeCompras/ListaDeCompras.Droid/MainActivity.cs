using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Prism.Unity;
using Microsoft.Practices.Unity;
using ListaDeCompras.Droid.Services;
using ListaDeCompras.Services;

namespace ListaDeCompras.Droid
{
    [Activity(Label = "ListaDeCompras", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.tabs;
            ToolbarResource = Resource.Layout.toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App(new AndroidInitializer()));
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        static AutenticacaoService autenticacaoService = new AutenticacaoService();

        public void RegisterTypes(IUnityContainer container)
        {
            container.RegisterInstance<IAutenticacaoService>(autenticacaoService, new ExternallyControlledLifetimeManager());
        }
    }
}

