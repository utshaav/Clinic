namespace ClinicManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedtinydetailsindoctor214 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Doctors", "TimeFrom", c => c.String());
            AlterColumn("dbo.Doctors", "TimeTo", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Doctors", "TimeTo", c => c.DateTime());
            AlterColumn("dbo.Doctors", "TimeFrom", c => c.DateTime());
        }
    }
}
