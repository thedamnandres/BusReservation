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
        
        [Required]
        [DataType(DataType.Date)]
        public DateTime FechaSalida { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public TimeSpan Hora { get; set; }
    }
}
