using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BusReservation.Models
{
    public class Horario
    {
        [Key]
        public int HorarioId { get; set; }

        [ForeignKey("Ruta")]
        public int RutaId { get; set; }
        public Ruta? Ruta { get; set; }

        public DateTime FechaHoraSalida { get; set; }
        public int AsientosDisponibles { get; set; }
        public int AsientosOcupados { get; set; }
    }
}
