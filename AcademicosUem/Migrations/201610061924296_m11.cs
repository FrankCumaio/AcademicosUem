namespace AcademicosUem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m11 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TrabalhoAutors",
                c => new
                    {
                        Trabalho_Id = c.Int(nullable: false),
                        Autor_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Trabalho_Id, t.Autor_Id })
                .ForeignKey("dbo.Trabalho", t => t.Trabalho_Id, cascadeDelete: false)
                .ForeignKey("dbo.Autor", t => t.Autor_Id, cascadeDelete: false)
                .Index(t => t.Trabalho_Id)
                .Index(t => t.Autor_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TrabalhoAutors", "Autor_Id", "dbo.Autor");
            DropForeignKey("dbo.TrabalhoAutors", "Trabalho_Id", "dbo.Trabalho");
            DropIndex("dbo.TrabalhoAutors", new[] { "Autor_Id" });
            DropIndex("dbo.TrabalhoAutors", new[] { "Trabalho_Id" });
            DropTable("dbo.TrabalhoAutors");
        }
    }
}
