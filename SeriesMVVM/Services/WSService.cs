using SeriesMVVM.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Protection.PlayReady;

namespace SeriesMVVM.Services
{
    public class WSService : IService
    {
        private readonly HttpClient client = new HttpClient();
        public WSService(string uri)
        {
            client.BaseAddress = new Uri(uri);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task<List<Serie>> GetSeriesAsync(string nomControleur)
        {
            try
            {
                return await client.GetFromJsonAsync<List<Serie>>(nomControleur);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Serie?> GetSerieAsync(string nomControleur, int id)
        {
            try
            {
                return await client.GetFromJsonAsync<Serie>($"{nomControleur}/{id}");
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> PostSerieAsync(string nomControleur, Serie serie)
        {
            try
            {
                await client.PostAsJsonAsync<Serie>(nomControleur, serie);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> PutSerieAsync(string nomControleur, int id, Serie serie)
        {
            try
            {
                await client.PutAsJsonAsync<Serie>($"{nomControleur}/{id}", serie);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> DeleteSerieAsync(string nomControleur, int id)
        {
            try
            {
                await client.DeleteAsync($"{nomControleur}/{id}");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
