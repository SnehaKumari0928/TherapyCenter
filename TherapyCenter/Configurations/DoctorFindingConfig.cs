using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TherapyCenter.Entities;

namespace TherapyCenter.Configurations
{
    public class DoctorFindingConfig : IEntityTypeConfiguration<DoctorFinding>
    {
        public void Configure(EntityTypeBuilder<DoctorFinding> builder)
        {
            builder.HasKey(df => df.FindingId);

            builder.HasOne(df => df.Appointment)
                .WithOne(a => a.DoctorFinding)
                .HasForeignKey<DoctorFinding>(df => df.AppointmentId);

           

        }
    }
}
