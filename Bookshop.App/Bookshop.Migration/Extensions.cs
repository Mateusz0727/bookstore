using FluentMigrator.Builders;
using FluentMigrator.Builders.Create.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator.Infrastructure;

namespace Bookshop.Migration
{
    public static class Extensions
    {
        #region AsLong()
        public static TNext AsLong<TNext>(this IColumnTypeSyntax<TNext> builder) where TNext : IFluentSyntax
        {
            return builder.AsInt64();
        }
        #endregion
        #region WithId()
        public static ICreateTableWithColumnSyntax WithId(this ICreateTableWithColumnSyntax builder)
        {
            return builder
                .WithColumn("Id").AsLong().PrimaryKey().Identity();
        }
        #endregion
        #region WithPublicId()
        public static ICreateTableWithColumnSyntax WithPublicId(this ICreateTableWithColumnSyntax builder)
        {
            return builder
                .WithColumn("PublicId").AsFixedLengthAnsiString(36);
        }
        #endregion
        #region AsText()
        public static TNext AsText<TNext>(this IColumnTypeSyntax<TNext> builder) where TNext : IFluentSyntax
        {
            return builder.AsString(65535);
        }
        #endregion
        #region WithAuditable()
        public static ICreateTableWithColumnSyntax WithAuditable(this ICreateTableWithColumnSyntax builder)
        {
            return builder
                .WithColumn("DateCreatedUtc").AsDateTime2()
                .WithColumn("CreatedBy").AsLong()
                .WithColumn("DateModifiedUtc").AsDateTime2().Nullable()
                .WithColumn("ModifiedBy").AsLong().Nullable();
        }
        #endregion
    }
}
