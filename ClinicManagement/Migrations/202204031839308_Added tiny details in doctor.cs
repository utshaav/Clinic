namespace ClinicManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedtinydetailsindoctor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Doctors", "TimeFrom", c => c.String());
            AddColumn("dbo.Doctors", "TimeTo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Doctors", "TimeTo");
            DropColumn("dbo.Doctors", "TimeFrom");
        }
    }
}
