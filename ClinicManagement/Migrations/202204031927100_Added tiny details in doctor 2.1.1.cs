namespace ClinicManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedtinydetailsindoctor211 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Doctors", "AvailableDays", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Doctors", "AvailableDays");
        }
    }
}
