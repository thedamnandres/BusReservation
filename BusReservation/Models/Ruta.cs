using System.ComponentModel.DataAnnotations;

namespace BusReservation.Models
{
    public class Ruta
    {
        [Key]
        public int RutaId { get; set; }

        [Required]
        [StringLength(100)]
        public string Origen { get; set; }

        [Required]
        [StringLength(100)]
        public string Destino { get; set; }

        public TimeSpan Duracion { get; set; }

        public ICollection<Horario>? Horarios { get; set; } = new List<Horario>();
    }
}
