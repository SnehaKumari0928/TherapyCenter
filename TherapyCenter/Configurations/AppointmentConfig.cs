using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TherapyCenter.Entities;

namespace TherapyCenter.Configurations
{
    public class AppointmentConfig: IEntityTypeConfiguration<Appointment>
    {

        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.HasKey(a => a.Appointmentid);

            builder.HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Doctor)
                .WithMany(p => p.Appointments)
                .HasForeignKey( a => a.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Therapy)
                .WithMany(t => t.Appointments)
                .HasForeignKey(a => a.TherapyId);

            builder.HasOne(a => a.Receptionist)
                .WithMany(u => u.ReceptionistAppointments)
                .HasForeignKey(a => a.ReceptionistId);

            


        }
    }
}
