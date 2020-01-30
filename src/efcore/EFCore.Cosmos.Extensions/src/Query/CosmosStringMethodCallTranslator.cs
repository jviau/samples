using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Cosmos.Query.Internal;
using Microsoft.EntityFrameworkCore.Storage;

namespace Microsoft.EntityFrameworkCore.Cosmos
{
#pragma warning disable EF1001 // Internal EF Core API usage.

    /// <summary>
    /// Translator for <see cref="string" /> methods.
    /// </summary>
    public class CosmosStringMethodCallTranslator : IMethodCallTranslator
    {
        private static readonly MethodInfo s_toLowerMethodInfo
            = typeof(string).GetRuntimeMethod(nameof(string.ToLower), null);

        private static readonly MethodInfo s_toUpperMethodInfo
            = typeof(string).GetRuntimeMethod(nameof(string.ToUpper), null);

        private static readonly MethodInfo s_trimMethodInfo
            = typeof(string).GetRuntimeMethod(nameof(string.Trim), null);

        private static readonly MethodInfo s_trimStartMethodInfo
            = typeof(string).GetRuntimeMethod(nameof(string.TrimStart), null);

        private static readonly MethodInfo s_trimEndMethodInfo
            = typeof(string).GetRuntimeMethod(nameof(string.TrimEnd), null);

        private static readonly MethodInfo s_containsMethodInfo
            = typeof(string).GetRuntimeMethod(nameof(string.Contains), new[] { typeof(string) });

        private static readonly MethodInfo s_startsWithMethodInfo
            = typeof(string).GetRuntimeMethod(nameof(string.StartsWith), new[] { typeof(string) });

        private static readonly MethodInfo s_endsWithMethodInfo
            = typeof(string).GetRuntimeMethod(nameof(string.EndsWith), new[] { typeof(string) });

        private static readonly MethodInfo s_substringMethodInfo
            = typeof(string).GetRuntimeMethod(nameof(string.Substring), new[] { typeof(int), typeof(int) });

        private static readonly MethodInfo s_indexOf1MethodInfo
            = typeof(string).GetRuntimeMethod(nameof(string.IndexOf), new[] { typeof(char) });

        private static readonly MethodInfo s_indexOf2MethodInfo
            = typeof(string).GetRuntimeMethod(nameof(string.IndexOf), new[] { typeof(char), typeof(int) });

        private readonly ISqlExpressionFactory _sqlExpressionFactory;
        private readonly ITypeMappingSource _typeMappingSource;

        /// <summary>
        /// Initializes a new instance of <see cref="CosmosStringMethodCallTranslator" />.
        /// </summary>
        /// <param name="sqlExpressionFactory">The sql expression factory.</param>
        /// <param name="typeMappingSource">The type mapping source.</param>
        public CosmosStringMethodCallTranslator(
            ISqlExpressionFactory sqlExpressionFactory, ITypeMappingSource typeMappingSource)
        {
            _sqlExpressionFactory = sqlExpressionFactory ?? throw new ArgumentNullException(nameof(sqlExpressionFactory));
            _typeMappingSource = typeMappingSource ?? throw new ArgumentNullException(nameof(typeMappingSource));
        }

        /// <inheritdoc />
        public SqlExpression Translate(
            SqlExpression instance, MethodInfo method, IReadOnlyList<SqlExpression> arguments)
        {
            if (s_toLowerMethodInfo.Equals(method))
            {
                return TranslateSystemFunction("LOWER", typeof(string), instance);
            }

            if (s_toUpperMethodInfo.Equals(method))
            {
                return TranslateSystemFunction("UPPER", typeof(string), instance);
            }

            if (s_trimMethodInfo.Equals(method))
            {
                return TranslateSystemFunction("TRIM", typeof(string), instance);
            }

            if (s_trimStartMethodInfo.Equals(method))
            {
                return TranslateSystemFunction("LTRIM", typeof(string), instance);
            }

            if (s_trimEndMethodInfo.Equals(method))
            {
                return TranslateSystemFunction("RTRIM", typeof(string), instance);
            }

            if (s_containsMethodInfo.Equals(method))
            {
                return TranslateSystemFunction("CONTAINS", typeof(bool), instance, arguments[0]);
            }

            if (s_startsWithMethodInfo.Equals(method))
            {
                return TranslateSystemFunction("STARTSWITH", typeof(bool), instance, arguments[0]);
            }

            if (s_endsWithMethodInfo.Equals(method))
            {
                return TranslateSystemFunction("ENDSWITH", typeof(bool), instance, arguments[0]);
            }

            if (s_substringMethodInfo.Equals(method))
            {
                return TranslateSystemFunction("SUBSTRING", typeof(string), instance, arguments[0], arguments[1]);
            }

            if (s_indexOf1MethodInfo.Equals(method))
            {
                return TranslateSystemFunction("INDEX_OF", typeof(int), instance, arguments[0]);
            }

            if (s_indexOf2MethodInfo.Equals(method))
            {
                return TranslateSystemFunction("INDEX_OF", typeof(int), instance, arguments[0], arguments[1]);
            }

            return null;
        }

        private SqlExpression TranslateSystemFunction(
            string function, Type returnType, params SqlExpression[] arguments)
        {
            return _sqlExpressionFactory.Function(
                function, arguments, returnType, _typeMappingSource.FindMapping(returnType));
        }
    }

#pragma warning restore EF1001 // Internal EF Core API usage.
}
