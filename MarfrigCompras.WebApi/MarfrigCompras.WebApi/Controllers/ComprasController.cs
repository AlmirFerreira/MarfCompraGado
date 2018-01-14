using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarfrigCompras.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Compras")]
    public class ComprasController : Controller
    {
        [HttpPost]
        [Route("Compras")]
        public List<Models.CompraGadoGrid> RetornaCompras(Models.CompraGadoFilter filter)
        {
            var compraGado = new Models.CompraGado();
            var compraGadoList = compraGado.ConsultaCompraGado(filter);

            return compraGadoList;
        }

        [HttpPost]
        [Route("ComprasItem")]
        public List<Models.CompraGadoItensReport> RetornaCompras(int compraGadoId)
        {
            var compraGado = new Models.CompraGado();
            var compraGadoItensList = compraGado.ConsultaCompraGadoItens(compraGadoId);

            return compraGadoItensList;
        }

        [HttpPost]
        [Route("InsereCompra")]
        public int InsereCompras(Models.CompraGado compraGado)
        {
            int newCompraId = 0;

            var compraGadoModel = new Models.CompraGado();
            newCompraId = compraGadoModel.Save(compraGado);

            return newCompraId;
        }

        [HttpPost]
        [Route("InsereItens")]
        public void InsereCompraItens(Models.CompraGadoItem compraGadoItem)
        {
            var compraGadoItensModel = new Models.CompraGado();
            compraGadoItensModel.SaveItens(compraGadoItem);
        }

    }
}