using AppBlazor.Entities;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace AppBlazor.Client.Services
{
    public class TipoLibroServices
    {
        private readonly HttpClient http;
        private List<TipoLibroCLS> lista;
        public TipoLibroServices(HttpClient _http)
        {
            http = _http;
            lista = new List<TipoLibroCLS>();
            //lista.Add(new TipoLibroCLS() { idtipolibro = 1, nombretipolibro = "Cuento" });
            //lista.Add(new TipoLibroCLS() { idtipolibro = 2, nombretipolibro = "Novela" });
        }
        public async Task<List<TipoLibroCLS>> listartipolibros()
        {
            try 
            {
                var response = await http.GetFromJsonAsync<List<TipoLibroCLS>>("api/TipoLibro");
                if (response == null)
                {
                    return new List<TipoLibroCLS>();
                }
                else
                {
                    return response;
                }
            }
            catch
            {
                return new List<TipoLibroCLS>();
            }
        }
        public int obtenerIdTipoLibro(string nombretipolibro)
        {
            var obj = lista.Where(p => p.nombretipolibro == nombretipolibro).FirstOrDefault();
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return obj.idtipolibro;
            }
        }
        public string obtenerNombreTipoLibro(int idtipolibro)
        {
            var obj = lista.Where(p => p.idtipolibro == idtipolibro).FirstOrDefault();
            if (obj == null)
            {
                return "";
            }
            else
            {
                return obj.nombretipolibro;
            }
        }
    }
}