using Microsoft.AspNetCore.Mvc;
using MvcNetCoreZapatillas.Models;
using MvcNetCoreZapatillas.Repositories;

namespace MvcNetCoreZapatillas.Controllers
{
    public class ZapatillasController : Controller
    {
        private RepositoryZapatillas repo;

        public ZapatillasController(RepositoryZapatillas repo)
        {
            this.repo = repo;
        }

        public IActionResult Zapatillas()
        {
            List<Zapatilla> zapatillas = this.repo.GetZapatillas();
            return View(zapatillas);
        }

        public IActionResult DetallesZapatillasPrueba(int idzapatilla)
        {
            Zapatilla zapatilla = this.repo.FindZapatilla(idzapatilla);
            return View(zapatilla);
        }

        public async Task<IActionResult> DetallesImagenZapatillas(int? posicion, int idzapatilla)
        {
            if (posicion == null)
            {
                posicion = 1;
            }
            int numregistros = this.repo.GetNumeroRegistrosVistaImagenZapatillas(idzapatilla);
            int siguiente = posicion.Value + 1;
            if (siguiente > numregistros)
            {
                siguiente = numregistros;
            }
            int anterior = posicion.Value - 1;
            if (anterior < 1)
            {
                anterior = 1;
            }
            List<VistaImagenZapatilla> vistaImagenZapatillas =
                await this.repo.GetVistaImagenZapatillasAsync(posicion.Value, idzapatilla);
            ViewData["ULTIMO"] = numregistros;
            ViewData["SIGUIENTE"] = siguiente;
            ViewData["ANTERIOR"] = anterior;
            return View(vistaImagenZapatillas);
        }
    }
}
