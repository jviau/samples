using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Cosmos.Query.Internal;
using Microsoft.EntityFrameworkCore.Storage;

namespace Microsoft.EntityFrameworkCore.Cosmos
{
    /// <summary>
    /// A plugin for supplying extra <see cref="IMethodCallTranslator" /> to efcore.
    /// </summary>
    public class CosmosMethodCallTranslatorPlugin : IMethodCallTranslatorPlugin
    {
        /// <summary>
        /// Initializes a new instance of <see cref="CosmosMethodCallTranslatorPlugin" />
        /// </summary>
        /// <param name="sqlExpressionFactory">The sql expression factory.</param>
        /// <param name="typeMappingSource">The type mapping source.</param>
        public CosmosMethodCallTranslatorPlugin(
            ISqlExpressionFactory sqlExpressionFactory, ITypeMappingSource typeMappingSource)
        {
            Translators = new[] { new CosmosStringMethodCallTranslator(sqlExpressionFactory, typeMappingSource) };
        }

        /// <inheritdoc />
        public IEnumerable<IMethodCallTranslator> Translators { get; }
    }
}
