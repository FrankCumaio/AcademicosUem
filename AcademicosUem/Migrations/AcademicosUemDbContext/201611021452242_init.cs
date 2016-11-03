namespace AcademicosUem.Migrations.AcademicosUemDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {

            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TrabalhoAutors", "Autor_Id", "SCOTT.Autor");
            DropForeignKey("dbo.TrabalhoAutors", "Trabalho_Id", "SCOTT.Trabalho");
            DropForeignKey("SCOTT.Trabalho", "AreaID", "SCOTT.Area");
            DropForeignKey("SCOTT.Temas", "AutorID", "SCOTT.Autor");
            DropForeignKey("SCOTT.Temas", "AreaID", "SCOTT.Area");
            DropForeignKey("SCOTT.Autor", "CursoID", "SCOTT.Curso");
            DropForeignKey("SCOTT.Area", "CursoID", "SCOTT.Curso");
            DropIndex("dbo.TrabalhoAutors", new[] { "Autor_Id" });
            DropIndex("dbo.TrabalhoAutors", new[] { "Trabalho_Id" });
            DropIndex("SCOTT.Trabalho", new[] { "AreaID" });
            DropIndex("SCOTT.Temas", new[] { "AreaID" });
            DropIndex("SCOTT.Temas", new[] { "AutorID" });
            DropIndex("SCOTT.Autor", new[] { "CursoID" });
            DropIndex("SCOTT.Area", new[] { "CursoID" });
            DropTable("dbo.TrabalhoAutors");
            DropTable("SCOTT.Trabalho");
            DropTable("SCOTT.Temas");
            DropTable("SCOTT.Autor");
            DropTable("SCOTT.Curso");
            DropTable("SCOTT.Area");
        }
    }
}
