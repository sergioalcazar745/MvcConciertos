using CallApi.Helpers;
using MvcConciertos.Helpers;
using MvcConciertos.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http.Headers;
using System.Reflection.PortableExecutable;
using System.Text;

namespace MvcConciertos.Services
{
    public class ServiceConciertos
    {
        private HelperCallApi callApi;
        private Secret secret;
        private MediaTypeWithQualityHeaderValue media;
        public ServiceConciertos(HelperCallApi callApi, Secret secret)
        {
            this.callApi = callApi;
            this.callApi.Uri = secret.api;
            this.callApi.Header = new MediaTypeWithQualityHeaderValue("application/json");
            this.secret = secret;
            this.media = new MediaTypeWithQualityHeaderValue("application/json");
        }

        public async Task<T> GetApiAsync<T>(string request)
        {
            using HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(secret.api + request);
            string ho = secret.api;
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(this.media);
            HttpResponseMessage httpResponseMessage = await client.GetAsync("");
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return await httpResponseMessage.Content.ReadAsAsync<T>();
            }

            return default(T);
        }
        public async Task<bool> PostApiAsync(string request, object objeto)
        {
            using HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(secret.api + "/api/Conciertos/InsertEvento");
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(this.media);
            StringContent content = new StringContent(JsonConvert.SerializeObject(objeto), Encoding.UTF8, "application/json");
            return (await client.PostAsync(request, content)).IsSuccessStatusCode;
        }

        public async Task<List<Evento>> Eventos()
        {
            List<Evento> eventos = await this.GetApiAsync<List<Evento>>("/api/conciertos/eventos");
            return eventos;
        }

        public async Task<List<Evento>> EventosCategorias(int id)
        {
            return await this.GetApiAsync<List<Evento>>("/api/conciertos/eventoscategoria/" + id);
        }

        public async Task<List<CategoriaEvento>> CategoriasEventos()
        {
            List<CategoriaEvento> categorias = await this.GetApiAsync<List<CategoriaEvento>>("/api/Conciertos/CategoriasEventos");
            return categorias;
        }

        public async Task InsertEvento(Evento evento)
        {
            bool l = await this.PostApiAsync("/api/conciertos/insertevento", evento);
        }
    }
}
