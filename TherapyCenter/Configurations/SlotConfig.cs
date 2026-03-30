using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using TherapyCenter.Entities;

namespace TherapyCenter.Configurations
{
    public class SlotConfig: IEntityTypeConfiguration<Slot>
    {
        public void Configure(EntityTypeBuilder<Slot> builder)
        {
            builder.HasKey(s => s.SlotId);

            builder.HasOne(s => s.Doctor)
                .WithMany(d => d.Slots)
                .HasForeignKey(s => s.DoctorId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
