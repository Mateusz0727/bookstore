using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshop.Migration.Scripts
{
  
    [Migration(130320221606)]
    public class _006_AddDiscountToBookTable : FluentMigrator.Migration
    {
        public override void Up()
        {

            Alter.Table("Book")
                .AddColumn("IsDiscount").AsBoolean().WithDefaultValue(false)
                .AddColumn("Discount").AsInt32().Nullable();


        }
        public override void Down()
        {
            if (Schema.Table("Book").Column("IsDiscount").Exists())
            {
                Delete.Column("Discount").FromTable("Book");
            }
            if (Schema.Table("Book").Column("IsDiscount").Exists())
            {
                Delete.Column("Discount").FromTable("Book");
            }
        }

    }
}
