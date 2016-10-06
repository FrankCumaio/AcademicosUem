namespace AcademicosUem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m1 : DbMigration
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
                "dbo.Trabalhos_autor",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TrabalhosID = c.Int(),
                        AutorID = c.Int(),
                        Trabalho_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Autor", t => t.AutorID)
                .ForeignKey("dbo.Trabalho", t => t.Trabalho_Id)
                .Index(t => t.AutorID)
                .Index(t => t.Trabalho_Id);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Trabalhos_autor", "Trabalho_Id", "dbo.Trabalho");
            DropForeignKey("dbo.Trabalho", "AreaID", "dbo.Area");
            DropForeignKey("dbo.Trabalhos_autor", "AutorID", "dbo.Autor");
            DropForeignKey("dbo.Temas", "AutorID", "dbo.Autor");
            DropForeignKey("dbo.Temas", "AreaID", "dbo.Area");
            DropForeignKey("dbo.Autor", "CursoID", "dbo.Curso");
            DropForeignKey("dbo.Area", "CursoID", "dbo.Curso");
            DropIndex("dbo.Trabalho", new[] { "AreaID" });
            DropIndex("dbo.Trabalhos_autor", new[] { "Trabalho_Id" });
            DropIndex("dbo.Trabalhos_autor", new[] { "AutorID" });
            DropIndex("dbo.Temas", new[] { "AreaID" });
            DropIndex("dbo.Temas", new[] { "AutorID" });
            DropIndex("dbo.Autor", new[] { "CursoID" });
            DropIndex("dbo.Area", new[] { "CursoID" });
            DropTable("dbo.Trabalho");
            DropTable("dbo.Trabalhos_autor");
            DropTable("dbo.Temas");
            DropTable("dbo.Autor");
            DropTable("dbo.Curso");
            DropTable("dbo.Area");
        }
    }
}
