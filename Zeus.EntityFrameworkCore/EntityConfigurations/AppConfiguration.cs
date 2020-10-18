using Zeus.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Zeus.EntityFrameworkCore.EntityConfigurations
{
    public class AppConfiguration : IEntityTypeConfiguration<App>
    {
        public void Configure(EntityTypeBuilder<App> builder)
        {
            builder.ToTable("App");
            builder.HasKey(c => c.Id);
        }
    }
}
