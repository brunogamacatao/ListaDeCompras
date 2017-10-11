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

using ListaDeCompras.Services;
using Firebase.Xamarin.Auth;

namespace ListaDeCompras.Droid.Services
{
    class AutenticacaoService : IAutenticacaoService
    {
        private const string API_KEY = "AIzaSyAig85XRqXPFq7Kl1vJOnrcqq63zecS5co";

        public async Task<bool> Autentica(string email, string senha)
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(API_KEY));

            try
            {
                var auth = await authProvider.SignInWithEmailAndPasswordAsync(email, senha);

                Token = auth.FirebaseToken;

                return true;
            } catch (Exception)
            {
                return false;
            }
        }

        public string Token { get; private set; }
    }
}