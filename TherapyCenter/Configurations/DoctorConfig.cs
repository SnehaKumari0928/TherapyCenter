using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TherapyCenter.Entities;

namespace TherapyCenter.Configurations
{
    public class DoctorConfig: IEntityTypeConfiguration<Doctor> 
    {

        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.HasKey(d => d.DoctorId);

            builder.HasOne(d => d.User)
                .WithMany(u => u.Doctors)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
