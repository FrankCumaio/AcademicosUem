namespace AcademicosUem.Migrations.AcademicosUemDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class teste : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Curso", "AreaDeConhecimento", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Curso", "AreaDeConhecimento");
        }
    }
}
