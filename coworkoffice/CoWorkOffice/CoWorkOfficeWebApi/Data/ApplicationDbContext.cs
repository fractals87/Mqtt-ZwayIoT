using CoWorkOfficeModel.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoWorkOffice.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Room> Rooms {get;set;}
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<IoTDevice> IoTDevices { get; set; }
        public DbSet<Gateway> Gateways { get; set; }
        public DbSet<Measure> Measures { get; set; }
        public DbSet<ConfortParameter> ConfortParameters { get; set; }
    }
}
