namespace AcademicosUem.Migrations.AcademicosUemDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Area",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        CursoID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Curso", t => t.CursoID, cascadeDelete: true)
                .Index(t => t.CursoID);
            
            CreateTable(
                "dbo.Curso",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Autor",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(maxLength: 250),
                        Telefone = c.String(maxLength: 50),
                        Email = c.String(maxLength: 50),
                        CursoID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Curso", t => t.CursoID, cascadeDelete: true)
                .Index(t => t.CursoID);
            
            CreateTable(
                "dbo.Temas",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Titulo = c.String(),
                        Descricao = c.String(),
                        AutorID = c.Int(nullable: false),
                        AreaID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Area", t => t.AreaID)
                .ForeignKey("dbo.Autor", t => t.AutorID, cascadeDelete: true)
                .Index(t => t.AutorID)
                .Index(t => t.AreaID);
            
            CreateTable(
                "dbo.Trabalho",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Titulo = c.String(),
                        Descricao = c.String(),
                        Data_Publicacao = c.String(),
                        Grau_Academico = c.String(),
                        Estado = c.String(),
                        DirectorioDoc = c.String(),
                        AreaID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Area", t => t.AreaID, cascadeDelete: true)
                .Index(t => t.AreaID);
            
            CreateTable(
                "dbo.TrabalhoAutors",
                c => new
                    {
                        Trabalho_Id = c.Int(nullable: false),
                        Autor_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Trabalho_Id, t.Autor_Id })
                .ForeignKey("dbo.Trabalho", t => t.Trabalho_Id)
                .ForeignKey("dbo.Autor", t => t.Autor_Id)
                .Index(t => t.Trabalho_Id)
                .Index(t => t.Autor_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TrabalhoAutors", "Autor_Id", "dbo.Autor");
            DropForeignKey("dbo.TrabalhoAutors", "Trabalho_Id", "dbo.Trabalho");
            DropForeignKey("dbo.Trabalho", "AreaID", "dbo.Area");
            DropForeignKey("dbo.Temas", "AutorID", "dbo.Autor");
            DropForeignKey("dbo.Temas", "AreaID", "dbo.Area");
            DropForeignKey("dbo.Autor", "CursoID", "dbo.Curso");
            DropForeignKey("dbo.Area", "CursoID", "dbo.Curso");
            DropIndex("dbo.TrabalhoAutors", new[] { "Autor_Id" });
            DropIndex("dbo.TrabalhoAutors", new[] { "Trabalho_Id" });
            DropIndex("dbo.Trabalho", new[] { "AreaID" });
            DropIndex("dbo.Temas", new[] { "AreaID" });
            DropIndex("dbo.Temas", new[] { "AutorID" });
            DropIndex("dbo.Autor", new[] { "CursoID" });
            DropIndex("dbo.Area", new[] { "CursoID" });
            DropTable("dbo.TrabalhoAutors");
            DropTable("dbo.Trabalho");
            DropTable("dbo.Temas");
            DropTable("dbo.Autor");
            DropTable("dbo.Curso");
            DropTable("dbo.Area");
        }
    }
}
