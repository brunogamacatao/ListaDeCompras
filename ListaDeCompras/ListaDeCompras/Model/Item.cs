using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListaDeCompras.Model
{
    public class Item
    {
        public string ItemId { get; set; }
        public string Nome { get; set; }
        public int Quantidade { get; set; } = 1;
        public bool Comprado { get; set; } = false;

        public override string ToString()
        {
            return $"{Nome} - {Quantidade}";
        }
    }
}
