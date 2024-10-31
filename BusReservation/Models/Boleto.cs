using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BusReservation.Models
{
    public class Boleto
    {
        [Key]
        public int BoletoId { get; set; }

        [ForeignKey("Reserva")]
        public int ReservaId { get; set; }
        public Reserva? Reserva { get; set; }

        [Required]
        public string Detalles { get; set; }

        public string CodigoQR { get; set; }
        public DateTime FechaEmision { get; set; }
    }
}
