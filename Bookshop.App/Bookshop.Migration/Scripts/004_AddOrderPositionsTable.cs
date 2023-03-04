using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshop.Migration.Scripts
{
    [Migration(040320230022)]
    public class _004_AddOrderPositionsTable : FluentMigrator.Migration
    {
        public override void Up()
        {

            Create.Table("OrderPositions")
                .WithId()
            .WithPublicId()
            .WithColumn("Order_Id").AsLong().ForeignKey("FK_orderPositions_order","Order","Id")
            .WithColumn("Book_Id").AsLong().ForeignKey("FK_orderPositions_book", "Book", "Id")
            .WithColumn("Price").AsFloat();


        }
        public override void Down()
        {
            if (Schema.Table("OrderPositions").Exists())
            {
                Delete.Table("OrderPositions");
            }
        }
    }
   
}
