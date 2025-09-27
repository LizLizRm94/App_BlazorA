﻿using AppBlazor.Entities;

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
        private TipoLibroService tipoLibroService;
        public LibroService(TipoLibroService _tipoLibroService)
        {
            tipoLibroService = _tipoLibroService;
            lista = new List<LibroListCLS>();
            lista.Add(new LibroListCLS { idlibro = 1, titulo = "Caperucita Roja", nombretipolibro = "Cuento" });
            lista.Add(new LibroListCLS { idlibro = 2, titulo = "Don Quijote de la Mancha", nombretipolibro = "Novela" });
        }

        public List<LibroListCLS> listarLibros()
        {
            return lista;
        }

        public List<LibroListCLS> filtrarLibros(string nombretitulo)
        {
             List<LibroListCLS> l = listarLibros();
            if (nombretitulo == "")
            {
                return l;
            }
            else
            {
                List<LibroListCLS> listarfiltrada = l.Where(p => p.titulo.ToUpper().Contains(nombretitulo.ToUpper())).ToList();
                return listarfiltrada;
            }
        }
        public void eliminarLibro(int idlibro)
        {
            var listaQueda = lista.Where(p => p.idlibro != idlibro).ToList();
            lista = listaQueda;
        }

        public LibroFormCLS recuperaLibroPorId(int idlibro)
        {
            var obj = lista.Where(p => p.idlibro == idlibro).FirstOrDefault();
            if (obj != null)
            {
                return new LibroFormCLS
                {
                    idLibro = obj.idlibro,
                    titulo = obj.titulo,
                    resumen = "Resumen",
                    idtipolibro = tipoLibroService.obtenerIdTipoLibro(obj.nombretipolibro),
                    image = obj.imagen, nombrearchivo = obj.nombrearchivo

                    
                };
            }
            else
            {
                return new LibroFormCLS();
            }
        }
        public string recuperaArchivoPorId(int idlibro)
        {
            var obj = lista.Where(p => p.idlibro == idlibro).FirstOrDefault();
            if (obj != null && obj.archivo != null)
            {
                return Convert.ToBase64String(obj.archivo);
            }
            else
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
