namespace AcademicosUem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Eventoes", "StartDateTime", c => c.String());
            AlterColumn("dbo.Eventoes", "EndDateTime", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Eventoes", "EndDateTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Eventoes", "StartDateTime", c => c.DateTime(nullable: false));
        }
    }
}
