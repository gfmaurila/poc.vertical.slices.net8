using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using poc.vertical.slices.net8.Domain;
using poc.vertical.slices.net8.Extensions;

namespace poc.vertical.slices.net8.Database.Mappings;

public class ArticleConfiguration : IEntityTypeConfiguration<Article>
{
    public void Configure(EntityTypeBuilder<Article> builder)
    {
        builder.ConfigureBaseEntity();

        builder
            .Property(entity => entity.Title)
            .IsRequired() // NOT NULL
            .IsUnicode(false);

        builder
            .Property(entity => entity.Description)
            .IsRequired() // NOT NULL
            .IsUnicode(false);

        builder
            .Property(entity => entity.CreatedOnUtc)
            .IsRequired() // NOT NULL
            .HasColumnType("DATE");

        builder
            .Property(entity => entity.PublishedOnUtc)
            .HasColumnType("DATE");
    }
}
