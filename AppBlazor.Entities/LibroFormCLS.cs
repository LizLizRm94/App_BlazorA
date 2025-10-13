using System.ComponentModel.DataAnnotations;

namespace AppBlazor.Entities
{
    public class LibroFormCLS
    {
        [Required(ErrorMessage = "EL id es requerido")]
        [Range(0, int.MaxValue, ErrorMessage = "El valor debe ser positivo")]
        public int idLibro { get; set; }
        [Required(ErrorMessage = "EL titulo es requerido")]
        [MaxLength(100, ErrorMessage = "La longitud maxima es de 100 caracteres")]
        public string titulo { get; set; } = null!;
        [Required(ErrorMessage = "EL resumen es requerido")]
        [MinLength(5, ErrorMessage = "La longitud minima es de 5 caracteres")]
        public string resumen { get; set; } = null!;
        [Range(1, int.MaxValue, ErrorMessage = "Debe selecionar un tipo de libro")]
        public int idtipolibro { get; set; }
        public byte[]? image { get; set; }
        public byte[]? archivo { get; set; }
        public string nombrearchivo { get; set; } = null!;
        [Range(1, int.MaxValue, ErrorMessage ="Debe ingresar un numero de paginas mayor a 0 y menor o igual a 5000")]
        public int idautor { get; set; }
        public int numeropaginas {  get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Debe ingresar un stock mayor a 0")]
        public int stock {  get; set; }
    }
}
