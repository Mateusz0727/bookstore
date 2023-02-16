using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshop.Migration.Scripts
{
  
        [Migration(24122022)]
        public class _002_AddUserTable : FluentMigrator.Migration
        {


            public override void Up()
            {
               

                    Create.Table("User")
                    .WithId()
                    .WithPublicId()
                    .WithAuditable()
                    .WithColumn("UserName").AsString(250).Unique("IX_UserName")
                    .WithColumn("Email").AsString(250).Unique("IX_Email")
                    .WithColumn("EmailConfirmed").AsBoolean()
                    .WithColumn("PasswordHash").AsString(250).Nullable()
                    .WithColumn("PasswordChangedDateUtc").AsDateTime2().Nullable()
                    .WithColumn("ForcePasswordChange").AsBoolean()
                    .WithColumn("GivenName").AsString(250).Nullable()
                    .WithColumn("Surname").AsString(250).Nullable();

                
            }
            public override void Down()
            {
                if (Schema.Table("User").Exists())
                {
                    Delete.Table("User");
                }
            }
        }
    
}
