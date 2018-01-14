using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarfrigCompras
{
    public class CompraGado
    {
        #region Properties
        public int Id { get; set; }
        public DateTime DataEntrega { get; set; }
        public int PecuaristaID { get; set; } 
        #endregion

    }

    public class CompraGadoItens
    {
        #region Properties
        public int AnimalID { get; set; }
        public string AnimalNome { get; set; }
        public int Quantidade { get; set; }
        public double Valor { get; set; }
        public double Total { get; set; }
        #endregion
    }

    public class CompraGadoItensReportAdapter
    {
        #region Properties
        public string Id { get; set; }
        public string Quantidade { get; set; }
        public string CompraGadoId { get; set; }
        public string AnimalId { get; set; }
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

    public class CompraGadoItensData
    {
        #region Properties
        public int Quantidade { get; set; }
        public int CompraGadoId { get; set; }
        public int AnimalID { get; set; }
        #endregion
    }

    public class CompraGadoGrid
    {
        #region Properties
        public int Id { get; set; }
        public DateTime DataEntrega { get; set; }
        public string Pecuarista { get; set; }
        public double ValorTotal { get; set; }
        #endregion
    }

    public class CompraGadoFilter
    {
        #region Properties
        public int Id { get; set; }
        public int PecuaristaID { get; set; }
        public string BeginDate { get; set; }
        public string EndDate { get; set; }
        #endregion
    }
}
