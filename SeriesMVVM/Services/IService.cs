using SeriesMVVM.Models.EntityFramework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeriesMVVM.Services
{
    public interface IService
    {
        Task<List<Serie>> GetSeriesAsync(string nomControleur);
        Task<Serie?> GetSerieAsync(string nomControleur, int id);
        Task<bool> PutSerieAsync(string nomControleur, int id, Serie serie);
        Task<Serie> PostSerieAsync(string nomControleur, Serie serie);
        Task<bool> DeleteSerieAsync(string nomControleur, int id);
    }
}