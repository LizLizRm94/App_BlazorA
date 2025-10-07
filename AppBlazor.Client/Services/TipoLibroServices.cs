using AppBlazor.Entities;
using System.Runtime.CompilerServices;

namespace AppBlazor.Client.Services
{
    public class TipoLibroServices
    {
        private List<TipoLibroCLS> lista;
        public TipoLibroServices()
        {
            lista = new List<TipoLibroCLS>();
            lista.Add(new TipoLibroCLS() { idtipolibro = 1, nombretipolibro = "Cuento" });
            lista.Add(new TipoLibroCLS() { idtipolibro = 2, nombretipolibro = "Novela" });
        }
        public List<TipoLibroCLS> listartipolibros()
        {
            return lista;
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