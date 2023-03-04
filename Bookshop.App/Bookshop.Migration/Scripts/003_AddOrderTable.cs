﻿using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshop.Migration.Scripts
{
    [Migration(03032023)]
    public class _003_AddOrderTable : FluentMigrator.Migration
    {
        public override void Up()
        {
            Create.Table("Order")
            .WithId()
            .WithPublicId()
            .WithAuditable()
            .WithColumn("User_Id").AsLong().ForeignKey("FK_order_user", "User", "Id")
            .WithColumn("Status").AsString()
            .WithColumn("Amount").AsFloat();

            Create.Table("OrderPositions")
                .WithId()
            .WithPublicId()
            .WithColumn("Book_Id").AsLong().ForeignKey("FK_orderPositions_book", "Book", "Id")
            .WithColumn("Price");


        }
        public override void Down()
        {
            if (Schema.Table("Order").Exists())
            {
                Delete.Table("Order");
            }
            if (Schema.Table("OrderPositions").Exists())
            {
                Delete.Table("OrderPositions");
            }
        }
    }
   
}
