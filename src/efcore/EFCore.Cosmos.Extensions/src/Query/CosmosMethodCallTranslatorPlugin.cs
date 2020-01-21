using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Cosmos.Query.Internal;

namespace Microsoft.EntityFrameworkCore.Cosmos
{
    /// <summary>
    /// A plugin for supplying extra <see cref="IMethodCallTranslator" /> to efcore.
    /// </summary>
    public class CosmosMethodCallTranslatorPlugin : IMethodCallTranslatorPlugin
    {
        private readonly ISqlExpressionFactory _sqlExpressionFactory;

        /// <summary>
        /// Initializes a new instance of <see cref="CosmosMethodCallTranslatorPlugin" />
        /// </summary>
        /// <param name="sqlExpressionFactory">The sql expression factory.</param>
        public CosmosMethodCallTranslatorPlugin(ISqlExpressionFactory sqlExpressionFactory)
        {
            _sqlExpressionFactory = sqlExpressionFactory ?? throw new ArgumentNullException(nameof(sqlExpressionFactory));
            Translators = new[] { new CosmosStringMethodCallTranslator(_sqlExpressionFactory) };
        }

        /// <inheritdoc />
        public IEnumerable<IMethodCallTranslator> Translators { get; }
    }
}
