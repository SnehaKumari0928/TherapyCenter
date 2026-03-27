using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TherapyCenter.Entities;

namespace TherapyCenter.Configurations
{
    public class PatientConfig : IEntityTypeConfiguration<Patient>
    {

        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.HasKey(p => p.PatientId);

            builder.Property(p => p.FirstName)
                .IsRequired();

            builder.Property(p => p.LastName)
                .IsRequired();

            builder.HasOne(p => p.Guardian)
                .WithMany(u => u.Guardians)
                .HasForeignKey(p => p.GuardianId)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
