using System.ComponentModel.DataAnnotations;

namespace BusReservation.Models
{
    public class Cliente
    {
        [Key]
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio")]
        [StringLength(50)]
        public string Apellido { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "El email es obligatorio")]
        public string Email { get; set; }

        [Phone]
        public string Telefono { get; set; }

        public ICollection<Reserva>? Reservas { get; set; } = new List<Reserva>();


    }
}
