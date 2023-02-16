using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshop.Migration.Scripts
{
    [Migration(030220231254)]
    public class AddBookTable : FluentMigrator.Migration
    {
        public override void Up()
        {

            Create.Table("Book")
                .WithPublicId()
                .WithId()
                .WithColumn("Title").AsString()
                .WithColumn("autor").AsString()
                .WithColumn("Describe").AsText()
                .WithColumn("Price").AsDouble()
                .WithColumn("Publishing_house").AsString();


        }

        public override void Down()
        {
            Delete.Table("Book");

        }
    }
}
