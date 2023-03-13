

using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshop.Migration.Scripts
{
    [Migration(130320221754)]
    public class _008_AddBookLanguageTable : FluentMigrator.Migration
    {
        public override void Up()
        {

            Create.Table("BookLanguage")
                .WithId()
            .WithPublicId()
            .WithColumn("LanguageCode").AsString()
            .WithColumn("LanguageName").AsString();

            Insert.IntoTable("BookLanguage").Row(new
            {
                PublicId = Guid.NewGuid(),
                LanguageCode = "pl-PL",
                LanguageName = "Polish Poland"
            });

            Insert.IntoTable("BookLanguage").Row(new
            {
                PublicId = Guid.NewGuid(),
                LanguageCode = "en-GB",
                LanguageName = "ENglish United Kingdom"
            });
            Alter.Table("Book").AddColumn("LangugaeId").AsInt64().WithDefaultValue(1).ForeignKey("FK_book_bookLanguage", "BookLanguage", "Id");
           
        }
        public override void Down()
        {
            if (Schema.Table("BookLanguage").Exists())
            {
                Delete.Table("BookLanguage");
            }
        }
    }
}
