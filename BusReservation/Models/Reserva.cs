using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BusReservation.Models
{
    public class Reserva
    {

        [Key]
        public int ReservaId { get; set; }

        [ForeignKey("Cliente")]
        public int ClienteId { get; set; }
        public Cliente? Cliente { get; set; }

        [ForeignKey("Ruta")]
        public int RutaId { get; set; }
        public Ruta? Ruta { get; set; }

        public DateTime FechaReserva { get; set; }

        [Required]
        [StringLength(5)]
        public string Asiento { get; set; }

        [Required]
        public bool EstadoReserva { get; set; }

        public string MetodoPago { get; set; }
        public float Precio { get; set; }
    }
}
