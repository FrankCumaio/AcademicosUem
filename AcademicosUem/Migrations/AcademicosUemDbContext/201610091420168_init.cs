namespace AcademicosUem.Migrations.AcademicosUemDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "SCOTT.Area",
                c => new
                    {
                        Id = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Nome = c.String(),
                        CursoID = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("SCOTT.Curso", t => t.CursoID, cascadeDelete: true)
                .Index(t => t.CursoID);
            
            CreateTable(
                "SCOTT.Curso",
                c => new
                    {
                        Id = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Nome = c.String(),
                        Area_conhecimento = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "SCOTT.Autor",
                c => new
                    {
                        Id = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Nome = c.String(maxLength: 250),
                        Telefone = c.String(maxLength: 50),
                        Email = c.String(maxLength: 50),
                        CursoID = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("SCOTT.Curso", t => t.CursoID, cascadeDelete: true)
                .Index(t => t.CursoID);
            
            CreateTable(
                "SCOTT.Temas",
                c => new
                    {
                        Id = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Titulo = c.String(),
                        Descricao = c.String(),
                        AutorID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        AreaID = c.Decimal(precision: 10, scale: 0),
                        Data_Publicacao = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("SCOTT.Area", t => t.AreaID)
                .ForeignKey("SCOTT.Autor", t => t.AutorID, cascadeDelete: true)
                .Index(t => t.AutorID)
                .Index(t => t.AreaID);
            
            CreateTable(
                "SCOTT.Trabalho",
                c => new
                    {
                        Id = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Titulo = c.String(),
                        Descricao = c.String(),
                        Data_Publicacao = c.String(),
                        Grau_Academico = c.String(),
                        Estado = c.String(),
                        DirectorioDoc = c.String(),
                        userId = c.String(),
                        AreaID = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("SCOTT.Area", t => t.AreaID, cascadeDelete: true)
                .Index(t => t.AreaID);
            
            CreateTable(
                "SCOTT.TrabalhoAutors",
                c => new
                    {
                        Autor_Id = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Trabalho_Id = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => new { t.Autor_Id, t.Trabalho_Id })
                .ForeignKey("SCOTT.Autor", t => t.Autor_Id, cascadeDelete: true)
                .ForeignKey("SCOTT.Trabalho", t => t.Trabalho_Id, cascadeDelete: true)
                .Index(t => t.Autor_Id)
                .Index(t => t.Trabalho_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("SCOTT.TrabalhoAutors", "Trabalho_Id", "SCOTT.Trabalho");
            DropForeignKey("SCOTT.TrabalhoAutors", "Autor_Id", "SCOTT.Autor");
            DropForeignKey("SCOTT.Trabalho", "AreaID", "SCOTT.Area");
            DropForeignKey("SCOTT.Temas", "AutorID", "SCOTT.Autor");
            DropForeignKey("SCOTT.Temas", "AreaID", "SCOTT.Area");
            DropForeignKey("SCOTT.Autor", "CursoID", "SCOTT.Curso");
            DropForeignKey("SCOTT.Area", "CursoID", "SCOTT.Curso");
            DropIndex("SCOTT.TrabalhoAutors", new[] { "Trabalho_Id" });
            DropIndex("SCOTT.TrabalhoAutors", new[] { "Autor_Id" });
            DropIndex("SCOTT.Trabalho", new[] { "AreaID" });
            DropIndex("SCOTT.Temas", new[] { "AreaID" });
            DropIndex("SCOTT.Temas", new[] { "AutorID" });
            DropIndex("SCOTT.Autor", new[] { "CursoID" });
            DropIndex("SCOTT.Area", new[] { "CursoID" });
            DropTable("SCOTT.TrabalhoAutors");
            DropTable("SCOTT.Trabalho");
            DropTable("SCOTT.Temas");
            DropTable("SCOTT.Autor");
            DropTable("SCOTT.Curso");
            DropTable("SCOTT.Area");
        }
    }
}
