using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarfrigCompras.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Pecuarista")]
    public class PecuaristaController : Controller
    {
        [HttpGet]
        [Route("ListaPecuaristas")]
        public List<Models.Pecuarista> RetornaCompras()
        {
            var pecuarista = new Models.Pecuarista();
            var pecuaristaList = pecuarista.ListaPecuaristas();

            return pecuaristaList;
        }
    }
}