using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ListaDeCompras.Model;
using ListaDeCompras.Services;
using Firebase.Xamarin.Database;

namespace ListaDeCompras.Droid.Services
{
    class ListaDeComprasService : IListaDeComprasService
    {
        private const string DATABASE_URL = "https://prismauth.firebaseio.com";

        private FirebaseClient _firebase;
        private IAutenticacaoService _autenticacaoService;

        public ListaDeComprasService(IAutenticacaoService autenticacaoService)
        {
            _autenticacaoService = autenticacaoService;
        }

        public async Task<List<Item>> Listar()
        {
            var itens = new List<Item>();
            var retorno = await Firebase.Child("itens").OnceAsync<Item>();

            foreach (var item in retorno)
            {
                item.Object.ItemId = item.Key;
                itens.Add(item.Object);
            }

            return itens;
        }

        public async Task<Item> Cadastrar(Item novoItem)
        {
            var registro = await Firebase.Child("itens").PostAsync<Item>(novoItem);
            novoItem.ItemId = registro.Key;
            return novoItem;
        }

        public async Task<Item> Alterar(Item item)
        {
            await Firebase.Child($"itens/{item.ItemId}").PutAsync<Item>(item);
            return item;
        }

        public async Task<Item> Remover(string itemId)
        {
            var registro = await Firebase.Child($"itens/{itemId}").OnceSingleAsync<Item>();
            await Firebase.Child($"itens/{itemId}").DeleteAsync();
            return registro;
        }

        private FirebaseClient Firebase
        {
            get
            {
                if (_firebase == null)
                {
                    _firebase = new FirebaseClient(DATABASE_URL, () => Task.FromResult(_autenticacaoService.Token));
                }

                return _firebase;
            }
        }
    }
}