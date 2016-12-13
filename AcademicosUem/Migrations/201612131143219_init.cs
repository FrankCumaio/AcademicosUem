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
                        Nome = c.String(),
                        Area_conhecimento = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Autor",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Numero = c.Int(nullable: false),
                        Telefone = c.String(),
                        Email = c.String(),
                        CursoID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Curso", t => t.CursoID, cascadeDelete: true)
                .ForeignKey("dbo.ApplicationUsers", t => t.User_Id)
                .Index(t => t.CursoID)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Temas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Titulo = c.String(),
                        Descricao = c.String(),
                        PerfilID = c.Int(nullable: false),
                        AreaID = c.Int(),
                        Data_Publicacao = c.String(),
                        userId = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Area", t => t.AreaID)
                .ForeignKey("dbo.Autor", t => t.PerfilID, cascadeDelete: true)
                .Index(t => t.PerfilID)
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
                        userId = c.String(),
                        AreaID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Area", t => t.AreaID, cascadeDelete: true)
                .Index(t => t.AreaID);
            
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
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        IdentityRole_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.IdentityRoles", t => t.IdentityRole_Id)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.IdentityRole_Id);
            
            CreateTable(
                "dbo.Eventoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Titulo = c.String(),
                        StartDateTime = c.DateTime(nullable: false),
                        EndDateTime = c.DateTime(nullable: false),
                        Local = c.String(),
                        Agenda = c.String(),
                        Telefone = c.Int(nullable: false),
                        Email = c.String(),
                        Website = c.String(),
                        IsPublished = c.Boolean(nullable: false),
                        IsPublic = c.Boolean(nullable: false),
                        publishDate = c.DateTime(nullable: false),
                        TrabalhoID = c.Int(nullable: false),
                        EventoCategoriaID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EventoCategorias", t => t.EventoCategoriaID, cascadeDelete: true)
                .ForeignKey("dbo.Trabalho", t => t.TrabalhoID, cascadeDelete: true)
                .Index(t => t.TrabalhoID)
                .Index(t => t.EventoCategoriaID);
            
            CreateTable(
                "dbo.EventoCategorias",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        descricao = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Papels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        descricao = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Participacaos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsNotified = c.Boolean(nullable: false),
                        IsConfirmed = c.Boolean(nullable: false),
                        PerfilID = c.Int(nullable: false),
                        EventoID = c.Int(nullable: false),
                        PapelID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Eventoes", t => t.EventoID, cascadeDelete: false)
                .ForeignKey("dbo.Papels", t => t.PapelID, cascadeDelete: false)
                .ForeignKey("dbo.Autor", t => t.PerfilID, cascadeDelete: false)
                .Index(t => t.PerfilID)
                .Index(t => t.EventoID)
                .Index(t => t.PapelID);
            
            CreateTable(
                "dbo.IdentityRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Supervisaos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PerfilID = c.Int(nullable: false),
                        TrabalhoID = c.Int(nullable: false),
                        PapelID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Papels", t => t.PapelID, cascadeDelete: false)
                .ForeignKey("dbo.Autor", t => t.PerfilID, cascadeDelete: false)
                .ForeignKey("dbo.Trabalho", t => t.TrabalhoID, cascadeDelete: false)
                .Index(t => t.PerfilID)
                .Index(t => t.TrabalhoID)
                .Index(t => t.PapelID);
            
            CreateTable(
                "dbo.TrabalhoFiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Path = c.String(),
                        Tipo = c.String(),
                        TrabalhoID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Trabalho", t => t.TrabalhoID, cascadeDelete: true)
                .Index(t => t.TrabalhoID);
            
            CreateTable(
                "dbo.TrabalhoPerfils",
                c => new
                    {
                        Trabalho_Id = c.Int(nullable: false),
                        Perfil_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Trabalho_Id, t.Perfil_Id })
                .ForeignKey("dbo.Trabalho", t => t.Trabalho_Id, cascadeDelete: false)
                .ForeignKey("dbo.Autor", t => t.Perfil_Id, cascadeDelete: false)
                .Index(t => t.Trabalho_Id)
                .Index(t => t.Perfil_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TrabalhoFiles", "TrabalhoID", "dbo.Trabalho");
            DropForeignKey("dbo.Supervisaos", "TrabalhoID", "dbo.Trabalho");
            DropForeignKey("dbo.Supervisaos", "PerfilID", "dbo.Autor");
            DropForeignKey("dbo.Supervisaos", "PapelID", "dbo.Papels");
            DropForeignKey("dbo.AspNetUserRoles", "IdentityRole_Id", "dbo.IdentityRoles");
            DropForeignKey("dbo.Participacaos", "PerfilID", "dbo.Autor");
            DropForeignKey("dbo.Participacaos", "PapelID", "dbo.Papels");
            DropForeignKey("dbo.Participacaos", "EventoID", "dbo.Eventoes");
            DropForeignKey("dbo.Eventoes", "TrabalhoID", "dbo.Trabalho");
            DropForeignKey("dbo.Eventoes", "EventoCategoriaID", "dbo.EventoCategorias");
            DropForeignKey("dbo.Autor", "User_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.AspNetUserRoles", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.AspNetUserLogins", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.IdentityUserClaims", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.TrabalhoPerfils", "Perfil_Id", "dbo.Autor");
            DropForeignKey("dbo.TrabalhoPerfils", "Trabalho_Id", "dbo.Trabalho");
            DropForeignKey("dbo.Trabalho", "AreaID", "dbo.Area");
            DropForeignKey("dbo.Temas", "PerfilID", "dbo.Autor");
            DropForeignKey("dbo.Temas", "AreaID", "dbo.Area");
            DropForeignKey("dbo.Autor", "CursoID", "dbo.Curso");
            DropForeignKey("dbo.Area", "CursoID", "dbo.Curso");
            DropIndex("dbo.TrabalhoPerfils", new[] { "Perfil_Id" });
            DropIndex("dbo.TrabalhoPerfils", new[] { "Trabalho_Id" });
            DropIndex("dbo.TrabalhoFiles", new[] { "TrabalhoID" });
            DropIndex("dbo.Supervisaos", new[] { "PapelID" });
            DropIndex("dbo.Supervisaos", new[] { "TrabalhoID" });
            DropIndex("dbo.Supervisaos", new[] { "PerfilID" });
            DropIndex("dbo.Participacaos", new[] { "PapelID" });
            DropIndex("dbo.Participacaos", new[] { "EventoID" });
            DropIndex("dbo.Participacaos", new[] { "PerfilID" });
            DropIndex("dbo.Eventoes", new[] { "EventoCategoriaID" });
            DropIndex("dbo.Eventoes", new[] { "TrabalhoID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "IdentityRole_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserClaims", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Trabalho", new[] { "AreaID" });
            DropIndex("dbo.Temas", new[] { "AreaID" });
            DropIndex("dbo.Temas", new[] { "PerfilID" });
            DropIndex("dbo.Autor", new[] { "User_Id" });
            DropIndex("dbo.Autor", new[] { "CursoID" });
            DropIndex("dbo.Area", new[] { "CursoID" });
            DropTable("dbo.TrabalhoPerfils");
            DropTable("dbo.TrabalhoFiles");
            DropTable("dbo.Supervisaos");
            DropTable("dbo.IdentityRoles");
            DropTable("dbo.Participacaos");
            DropTable("dbo.Papels");
            DropTable("dbo.EventoCategorias");
            DropTable("dbo.Eventoes");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.IdentityUserClaims");
            DropTable("dbo.ApplicationUsers");
            DropTable("dbo.Trabalho");
            DropTable("dbo.Temas");
            DropTable("dbo.Autor");
            DropTable("dbo.Curso");
            DropTable("dbo.Area");
        }
    }
}
