using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarfrigCompras.WebApi.Models
{
    public class CompraGadoItem
    {
        #region Properties
        public int Id { get; set; }
        public string Quantidade { get; set; }
        public int CompraGadoId { get; set; }
        public int AnimalId { get; set; } 
        #endregion
    }

    public class CompraGadoItensReport
    {
        #region Properties
        public string Animal { get; set; }
        public string Quantidade { get; set; }
        public string Preco { get; set; }
        public string Total { get; set; }
        #endregion
    }
}
