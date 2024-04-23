using Microsoft.EntityFrameworkCore;

namespace poc.vertical.slices.net8.Extensions;

public static class ModelBuilderExtensions
{
    /// <summary>
    /// Remove a delação em cascata de chaves estrangeiras (FK).
    /// </summary>
    /// <param name="modelBuilder">The model builder.</param>
    public static ModelBuilder RemoveCascadeDeleteConvention(this ModelBuilder modelBuilder)
    {
        var foreignKeys = modelBuilder.Model
            .GetEntityTypes()
            .SelectMany(entity => entity.GetForeignKeys())
            .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

        foreach (var fk in foreignKeys)
            fk.DeleteBehavior = DeleteBehavior.Restrict;

        return modelBuilder;
    }
}
