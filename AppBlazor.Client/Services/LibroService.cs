using AppBlazor.Entities;
using Microsoft.JSInterop;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace AppBlazor.Client.Services
{
    public class LibroService
    {
        public event Func<string, Task> OnSearch = delegate { return Task.CompletedTask; };
        public async Task notificarBusqueda(string titulolibro)
        {
            if(OnSearch != null)
            {
                await OnSearch.Invoke(titulolibro);
            }
        }
        private List<LibroListCLS> lista;
        private TipoLibroServices tipoLibroService;

        private readonly HttpClient http;
        private readonly IJSRuntime jsRuntime;
        public LibroService(TipoLibroServices _tipoLibroService, HttpClient _http, IJSRuntime _jsRuntime)
        {
            http = _http;
            jsRuntime = _jsRuntime;
            tipoLibroService = _tipoLibroService;
            lista = new List<LibroListCLS>();
            //lista.Add(new LibroListCLS { idlibro = 1, titulo = "Caperucita Roja", nombretipolibro = "Cuento" });
            //lista.Add(new LibroListCLS { idlibro = 2, titulo = "Don Quijote de la Mancha", nombretipolibro = "Novela" });
        }

        public async Task<List<LibroListCLS>> listarLibros()
        {
           try
            {
                var response = await http.GetFromJsonAsync<List<LibroListCLS>>("api/Libro");
                if (response == null)
                {
                    return new List<LibroListCLS>();
                }
                else
                {
                    return response;
                }
            }
            catch
            {
                return new List<LibroListCLS>();
            }
        }


        public async Task<List<LibroListCLS>> filtrarLibros(string nombretitulo)
        {
            List<LibroListCLS> l = await listarLibros();
            if (nombretitulo == "")
            {
                return l;
            }
            else
            {
                List<LibroListCLS> listafiltrada = l.Where(p => p.titulo.ToUpper().Contains(nombretitulo.ToUpper())).ToList();
                return listafiltrada;
            }
        }
        public async Task <string> eliminarLibro(int idlibro)
        {
            var response = await http.DeleteAsync("api/Libro/" + idlibro);
            if(response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                return "Error:" + await response.Content.ReadAsStringAsync();
            }
        }

        public async Task<LibroFormCLS> recuperaLibroPorId(int idlibro)
        {
            try
            {
                var response = await http.GetFromJsonAsync<LibroFormCLS>($"api/Libro/{idlibro}");
                if (response != null)
                    return response;
                else
                    return new LibroFormCLS();
            }
            catch
            {
                return new LibroFormCLS();
            }
        }
        public async Task<string> recuperaArchivoPorId(int idlibro)
        {
            try
            {
                var response = await http.GetFromJsonAsync<byte[]>("api/Libro/recuperarArchivo/" + idlibro);
                if (response == null)
                {
                    return "";
                }
                else
                {
                    return Convert.ToBase64String(response);
                }
            }
            catch
            {
                return "";
            }
        }

        public async Task <string> guardarLibro(LibroFormCLS oLibroFormCLS)
        {
            try
            {
                var response = await http.PostAsJsonAsync("api/Libro", oLibroFormCLS);
                if (response.IsSuccessStatusCode)
                {
                    idlibro = idlibro,
                    titulo = oLibroFormCLS.titulo,
                    nombretipolibro = tipoLibroService.obtenerNombreTipoLibro(oLibroFormCLS.idtipolibro),
                    imagen = oLibroFormCLS.image,

                    archivo = oLibroFormCLS.archivo,
                    nombrearchivo=oLibroFormCLS.nombrearchivo
                });
            }
            else
            {
                var obj = lista.Where(p => p.idlibro == oLibroFormCLS.idLibro).FirstOrDefault();
                if (obj != null)
                {
                    obj.titulo = oLibroFormCLS.titulo;
                    obj.nombretipolibro = tipoLibroService.obtenerNombreTipoLibro(oLibroFormCLS.idtipolibro);
                    obj.imagen = oLibroFormCLS.image;

                    obj.archivo = oLibroFormCLS.archivo;
                    obj.nombrearchivo=oLibroFormCLS.nombrearchivo;
                }
            }
        }
        public async Task descargar(int idlibro, string nombrearchivo)
        {
            string archivo = await recuperaArchivoPorId(idlibro);
            if (archivo != null && archivo.Length > 0)
            {
                await jsRuntime.InvokeVoidAsync("descargarArchivo", archivo, nombrearchivo);
            }
        }
    }
}
