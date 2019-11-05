using DotNetCoreArchitecture.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotNetCoreArchitecture.Database
{
    public sealed class {{Name}}EntityConfiguration : IEntityTypeConfiguration<{{Name}}Entity>
    {
        public void Configure(EntityTypeBuilder<{{Name}}Entity> builder)
        {
            builder.ToTable("{{Name}}s", "{{Name}}");

            builder.HasKey(x => x.Id);
			{{#Fields}}
			builder.Property(x => x.{{Name}}).IsRequired();
			{{/Fields}}
        }
    }
}
