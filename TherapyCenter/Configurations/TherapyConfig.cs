using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TherapyCenter.Entities;

namespace TherapyCenter.Configurations
{
    public class TherapyConfig: IEntityTypeConfiguration<Therapy>
    {
        public void Configure(EntityTypeBuilder<Therapy> builder)
        {
            builder.HasKey(t => t.TherapyId);
            builder.Property(t => t.Name)
                .IsRequired();

            builder.Property(t => t.Cost)
                .HasColumnType("decima(10,2)");
        }
    }
}
