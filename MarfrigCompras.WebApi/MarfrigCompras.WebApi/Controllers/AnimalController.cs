using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarfrigCompras.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Animal")]
    public class AnimalController : Controller
    {
        [HttpGet]
        [Route("ListaAnimais")]
        public List<Models.Animal> RetornaCompras()
        {
            var animal = new Models.Animal();
            var animalList = animal.ListaAnimais();

            return animalList;
        }
    }
}