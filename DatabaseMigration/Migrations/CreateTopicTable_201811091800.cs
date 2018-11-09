using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;
namespace DatabaseMigration.Migrations
{
    [Migration(201811091800)]
    public class CreateTopicTable_201811091800 : Migration
    {
       
        public override void Up()
        {
            Create.Table("Topic")
               .WithColumn("Id").AsInt64().PrimaryKey().Identity()
               .WithColumn("TopicTitle").AsString(50);
        }

        public override void Down()
        {
            Delete.Table("Topic");
        }
    }
}
