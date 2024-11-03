using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BusReservation.Models;

namespace BusReservation.Data
{
    public class BusReservationContext : DbContext
    {
        public BusReservationContext (DbContextOptions<BusReservationContext> options)
            : base(options)
        {
        }

        public DbSet<BusReservation.Models.Cliente> Cliente { get; set; } = default!;
        public DbSet<BusReservation.Models.Reserva> Reserva { get; set; } = default!;
        public DbSet<BusReservation.Models.Boleto> Boleto { get; set; } = default!;
        public DbSet<BusReservation.Models.Ruta> Ruta { get; set; } = default!;
    }
}
