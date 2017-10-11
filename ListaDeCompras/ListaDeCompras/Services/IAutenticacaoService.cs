using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListaDeCompras.Services
{
    public interface IAutenticacaoService
    {
        Task<bool> Autentica(string email, string senha);
        string Token { get; }
    }
}
