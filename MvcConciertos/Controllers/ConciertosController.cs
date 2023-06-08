using Microsoft.AspNetCore.Mvc;
using MvcConciertos.Models;
using MvcConciertos.Services;

namespace MvcConciertos.Controllers
{
    public class ConciertosController : Controller
    {
        private ServiceConciertos service;
        private ServiceStorageS3 serviceS3;
        private Secret secret;
    
        public ConciertosController(ServiceConciertos service, ServiceStorageS3 serviceS3, Secret secret)
        {
            this.service = service;
            this.serviceS3 = serviceS3;
            this.secret = secret;
        }

        public async Task<IActionResult> Index()
        {
            List<CategoriaEvento> categorias = await this.service.CategoriasEventos();
            ViewData["CATEGORIAS"] = categorias;
            return View(await this.service.Eventos());
        }

        [HttpPost]
        public async Task<IActionResult> Index(int id)
        {
            ViewData["CATEGORIAS"] = await this.service.CategoriasEventos();
            return View(await this.service.EventosCategorias(id));
        }

        public async Task<IActionResult> Categorias()
        {
            return View(await this.service.CategoriasEventos());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Evento evento, IFormFile file)
        {
            using (Stream stream = file.OpenReadStream())
            {
                await this.serviceS3.UploadFileAsync(file.FileName, stream);
            }
            evento.Imagen = file.FileName;
            await this.service.InsertEvento(evento);
            return RedirectToAction("Index");
        }
    }
}
