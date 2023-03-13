using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshop.Migration.Scripts
{
    
        [Migration(13032022)]
        public class _005_AddPaypalIdToOrderTable : FluentMigrator.Migration
        {
            public override void Up()
            {

                Alter.Table("Order").AddColumn("PayPalId").AsString().Nullable();


            }
            public override void Down()
            {
                if (Schema.Table("Order").Column("PayPalId").Exists())
                {
                    Delete.Column("PayPalId").FromTable("Order");
                }
            }
        
    }
}
