namespace ClinicManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SlightChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Patients", "username", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Patients", "username");
        }
    }
}
