namespace ClinicManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedtinydetailsindoctor213 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Doctors", "TimeFrom", c => c.DateTime());
            AlterColumn("dbo.Doctors", "TimeTo", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Doctors", "TimeTo", c => c.String());
            AlterColumn("dbo.Doctors", "TimeFrom", c => c.String());
        }
    }
}
