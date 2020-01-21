using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Microsoft.EntityFrameworkCore.Cosmos
{
    /// <summary>
    /// Extension methods for <see cref="DbContextOptionsBuilder" />.
    /// </summary>
    public static class DbContextOptionsExtensions
    {
        /// <summary>
        /// Registers the <see cref="CosmosDbOptionsExtension" /> custom extension to the db context.
        /// </summary>
        /// <param name="options">The options to register to.</param>
        /// <returns>The same context options, with extensions added.</returns>
        public static DbContextOptionsBuilder AddCosmosExtensions(this DbContextOptionsBuilder options)
        {
            CosmosDbOptionsExtension extension = options.Options.FindExtension<CosmosDbOptionsExtension>()
                ?? new CosmosDbOptionsExtension();

            var infrastructure = (IDbContextOptionsBuilderInfrastructure)options;
            infrastructure.AddOrUpdateExtension(extension);

            return options;
        }
    }
}
