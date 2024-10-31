using System.ComponentModel.DataAnnotations;

namespace BusReservation.Models
{
    public class Cliente
    {
        [Key]
        public int ClienteId { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(50)]
        public string Apellido { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string Telefono { get; set; }

        public ICollection<Reserva>? Reservas { get; set; } = new List<Reserva>();


    }
}
