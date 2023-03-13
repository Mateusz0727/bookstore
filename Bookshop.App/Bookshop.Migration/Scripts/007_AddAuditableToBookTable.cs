using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshop.Migration.Scripts
{
    [Migration(130320221728)]
    public class _007_AddAuditableToBookTable : FluentMigrator.Migration
    {
        public override void Up()
        {

            Alter.Table("Book").AddColumn("DateCreatedUtc").AsDateTime2().WithDefaultValue(DateTime.UtcNow)
                .AddColumn("CreatedBy").AsLong().WithDefaultValue(1)
                .AddColumn("DateModifiedUtc").AsDateTime2().Nullable()
                .AddColumn("ModifiedBy").AsLong().Nullable();


        }
        public override void Down()
        {
            if (Schema.Table("Book").Column("DateCreatedUtc").Exists())
            {
                Delete.Column("DateCreatedUtc").FromTable("Book");
            }
            if (Schema.Table("Book").Column("CreatedBy").Exists())
            {
                Delete.Column("CreatedBy").FromTable("Book");
            }
            if (Schema.Table("Book").Column("DateModifiedUtc").Exists())
            {
                Delete.Column("DateModifiedUtc").FromTable("Book");
            }
            if (Schema.Table("Book").Column("ModifiedBy").Exists())
            {
                Delete.Column("ModifiedBy").FromTable("Book");
            }
        }
    }
}
