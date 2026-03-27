using Microsoft.EntityFrameworkCore;
using TherapyCenter.Entities;

namespace TherapyCenter.Data
{
    public class AppDbContext: DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(){


        }

        DbSet<User> Users { get; set; }
        DbSet<Patient> Patients { get; set; }
        DbSet<Therapy> Therapies { get; set; }
        DbSet<Doctor> Doctors { get; set; }
        DbSet<Appointment> Appointments { get; set; }
        DbSet<DoctorFinding> DoctorFindings { get; set; }

        DbSet<Payment> Payments { get; set; }
        DbSet<Slot> Slots { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        }
    }
}
