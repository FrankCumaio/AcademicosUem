namespace AcademicosUem.Migrations
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
                        Nome = c.String(maxLength: 255),
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
                        Nome = c.String(maxLength: 255),
                        Area_conhecimento = c.String(maxLength: 255),
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
                "SCOTT.Temas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Titulo = c.String(maxLength: 255),
                        Descricao = c.String(maxLength: 255),
                        AutorID = c.Int(nullable: false),
                        AreaID = c.Int(),
                        Data_Publicacao = c.String(),
                        userId = c.String(),
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
                        Titulo = c.String(maxLength: 255),
                        Descricao = c.String(maxLength: 255),
                        Data_Publicacao = c.String(maxLength: 255),
                        Grau_Academico = c.String(maxLength: 255),
                        Estado = c.String(maxLength: 255),
                        DirectorioDoc = c.String(),
                        userId = c.String(),
                        AreaID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Area", t => t.AreaID, cascadeDelete: true)
                .Index(t => t.AreaID);
            
            CreateTable(
                "dbo.IdentityRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                        IdentityRole_Id = c.String(maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.IdentityRoles", t => t.IdentityRole_Id)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .Index(t => t.IdentityRole_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.ApplicationUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.TrabalhoAutors",
                c => new
                    {
                        Autor_Id = c.Int(nullable: false),
                        Trabalho_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Autor_Id, t.Trabalho_Id })
                .ForeignKey("dbo.Autor", t => t.Autor_Id, cascadeDelete: false)
                .ForeignKey("dbo.Trabalho", t => t.Trabalho_Id, cascadeDelete: false)
                .Index(t => t.Autor_Id)
                .Index(t => t.Trabalho_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.AspNetUserLogins", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.IdentityUserClaims", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.AspNetUserRoles", "IdentityRole_Id", "dbo.IdentityRoles");
            DropForeignKey("dbo.TrabalhoAutors", "Trabalho_Id", "dbo.Trabalho");
            DropForeignKey("dbo.TrabalhoAutors", "Autor_Id", "dbo.Autor");
            DropForeignKey("dbo.Trabalho", "AreaID", "dbo.Area");
            DropForeignKey("SCOTT.Temas", "AutorID", "dbo.Autor");
            DropForeignKey("SCOTT.Temas", "AreaID", "dbo.Area");
            DropForeignKey("dbo.Autor", "CursoID", "dbo.Curso");
            DropForeignKey("dbo.Area", "CursoID", "dbo.Curso");
            DropIndex("dbo.TrabalhoAutors", new[] { "Trabalho_Id" });
            DropIndex("dbo.TrabalhoAutors", new[] { "Autor_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserClaims", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "IdentityRole_Id" });
            DropIndex("dbo.Trabalho", new[] { "AreaID" });
            DropIndex("SCOTT.Temas", new[] { "AreaID" });
            DropIndex("SCOTT.Temas", new[] { "AutorID" });
            DropIndex("dbo.Autor", new[] { "CursoID" });
            DropIndex("dbo.Area", new[] { "CursoID" });
            DropTable("dbo.TrabalhoAutors");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.IdentityUserClaims");
            DropTable("dbo.ApplicationUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.IdentityRoles");
            DropTable("dbo.Trabalho");
            DropTable("SCOTT.Temas");
            DropTable("dbo.Autor");
            DropTable("dbo.Curso");
            DropTable("dbo.Area");
        }
    }
}
