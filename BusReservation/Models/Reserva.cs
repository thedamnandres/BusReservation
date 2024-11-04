using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BusReservation.Models
{
    public enum MetodoPago
    {
        [Display(Name = "Tarjeta Crédito")]
        TarjetaCredito,
        [Display(Name = "Tarjeta Metro")]
        TarjetaMetro,
        [Display(Name = "Efectivo")]
        Efectivo
    }
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

        [Required(ErrorMessage = "La fecha es obligatorio")]
        [DataType(DataType.Date)]
        public DateTime FechaReserva { get; set; }

        [Required(ErrorMessage = "El asiento es obligatorio")]
        public string Asiento { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio")]
        public bool EstadoReserva { get; set; }

        [Required]
        
        public MetodoPago MetodoPago { get; set; }
        
        [Range(1, 8, ErrorMessage = "El precio no excede $8")]
        public float Precio { get; set; }
        
        public Boleto? Boleto { get; set; }
        
    }
}
