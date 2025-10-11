using AppBlazor.Entities;
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
        public LibroService(TipoLibroServices _tipoLibroService, HttpClient _http)
        {
            http = _http;
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
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                return "Error: " + await response.Content.ReadAsStringAsync();
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

        public void guardarLibro(LibroFormCLS oLibroFormCLS)
        {
            if (oLibroFormCLS.idLibro == 0)
            {
                int idlibro = lista.Select(p => p.idlibro).Max() + 1;
                lista.Add(new LibroListCLS
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
    }
}
