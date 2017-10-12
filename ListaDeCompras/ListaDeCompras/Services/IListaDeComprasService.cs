using ListaDeCompras.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListaDeCompras.Services
{
    public interface IListaDeComprasService
    {
        Task<List<Item>> Listar();
        Task<Item> Cadastrar(Item novoItem);
        Task<Item> Alterar(Item item);
        Task<Item> Remover(string itemId);
    }
}
