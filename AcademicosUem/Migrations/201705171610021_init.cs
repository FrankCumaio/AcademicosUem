namespace AcademicosUem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.catFiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Designacao = c.String(),
                        Desc = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Docentes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        apelido = c.String(),
                        nome = c.String(),
                        seccao = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DocenteAssociadoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TrabalhoID = c.Int(nullable: false),
                        DocenteID = c.Int(nullable: false),
                        FuncaoID = c.Int(nullable: false),
                        Data = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Docentes", t => t.DocenteID, cascadeDelete: true)
                .ForeignKey("dbo.Funcaos", t => t.FuncaoID, cascadeDelete: true)
                .ForeignKey("dbo.Trabalho", t => t.TrabalhoID, cascadeDelete: true)
                .Index(t => t.TrabalhoID)
                .Index(t => t.DocenteID)
                .Index(t => t.FuncaoID);
            
            CreateTable(
                "dbo.Funcaos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Designacao = c.String(),
                        Desc = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Trabalho",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Titulo = c.String(),
                        Descricao = c.String(),
                        Grau_Academico = c.String(),
                        EstudanteID = c.String(),
                        Data = c.DateTime(nullable: false),
                        Estudante_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Estudantes", t => t.Estudante_Id)
                .Index(t => t.Estudante_Id);
            
            CreateTable(
                "dbo.Estudantes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        apelido = c.String(),
                        nome = c.String(),
                        dataNasc = c.DateTime(nullable: false),
                        morada = c.String(),
                        seccao = c.String(),
                        curso = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EstadoEventoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Designacao = c.String(),
                        Desc = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EstadosDoEventoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EventoID = c.Int(nullable: false),
                        EstadoEventoID = c.Int(nullable: false),
                        Data = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EstadoEventoes", t => t.EstadoEventoID, cascadeDelete: true)
                .ForeignKey("dbo.Eventoes", t => t.EventoID, cascadeDelete: true)
                .Index(t => t.EventoID)
                .Index(t => t.EstadoEventoID);
            
            CreateTable(
                "dbo.Eventoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
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
                        Id = c.Int(nullable: false, identity: true),
                        descricao = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EstadosDoTrabalhoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isActual = c.Boolean(nullable: false),
                        TrabalhoID = c.Int(nullable: false),
                        EstadoTrabalhoID = c.Int(nullable: false),
                        Data = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EstadoTrabalhoes", t => t.EstadoTrabalhoID, cascadeDelete: true)
                .ForeignKey("dbo.Trabalho", t => t.TrabalhoID, cascadeDelete: true)
                .Index(t => t.TrabalhoID)
                .Index(t => t.EstadoTrabalhoID);
            
            CreateTable(
                "dbo.EstadoTrabalhoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Designacao = c.String(),
                        Desc = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
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
                "dbo.TrabalhoFiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Path = c.String(),
                        TrabalhoID = c.Int(nullable: false),
                        CatFilesID = c.Int(nullable: false),
                        Data = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.catFiles", t => t.CatFilesID, cascadeDelete: true)
                .ForeignKey("dbo.Trabalho", t => t.TrabalhoID, cascadeDelete: true)
                .Index(t => t.TrabalhoID)
                .Index(t => t.CatFilesID);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.AspNetUserLogins", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.IdentityUserClaims", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.TrabalhoFiles", "TrabalhoID", "dbo.Trabalho");
            DropForeignKey("dbo.TrabalhoFiles", "CatFilesID", "dbo.catFiles");
            DropForeignKey("dbo.AspNetUserRoles", "IdentityRole_Id", "dbo.IdentityRoles");
            DropForeignKey("dbo.EstadosDoTrabalhoes", "TrabalhoID", "dbo.Trabalho");
            DropForeignKey("dbo.EstadosDoTrabalhoes", "EstadoTrabalhoID", "dbo.EstadoTrabalhoes");
            DropForeignKey("dbo.EstadosDoEventoes", "EventoID", "dbo.Eventoes");
            DropForeignKey("dbo.Eventoes", "TrabalhoID", "dbo.Trabalho");
            DropForeignKey("dbo.Eventoes", "EventoCategoriaID", "dbo.EventoCategorias");
            DropForeignKey("dbo.EstadosDoEventoes", "EstadoEventoID", "dbo.EstadoEventoes");
            DropForeignKey("dbo.DocenteAssociadoes", "TrabalhoID", "dbo.Trabalho");
            DropForeignKey("dbo.Trabalho", "Estudante_Id", "dbo.Estudantes");
            DropForeignKey("dbo.DocenteAssociadoes", "FuncaoID", "dbo.Funcaos");
            DropForeignKey("dbo.DocenteAssociadoes", "DocenteID", "dbo.Docentes");
            DropIndex("dbo.AspNetUserLogins", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserClaims", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.TrabalhoFiles", new[] { "CatFilesID" });
            DropIndex("dbo.TrabalhoFiles", new[] { "TrabalhoID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "IdentityRole_Id" });
            DropIndex("dbo.EstadosDoTrabalhoes", new[] { "EstadoTrabalhoID" });
            DropIndex("dbo.EstadosDoTrabalhoes", new[] { "TrabalhoID" });
            DropIndex("dbo.Eventoes", new[] { "EventoCategoriaID" });
            DropIndex("dbo.Eventoes", new[] { "TrabalhoID" });
            DropIndex("dbo.EstadosDoEventoes", new[] { "EstadoEventoID" });
            DropIndex("dbo.EstadosDoEventoes", new[] { "EventoID" });
            DropIndex("dbo.Trabalho", new[] { "Estudante_Id" });
            DropIndex("dbo.DocenteAssociadoes", new[] { "FuncaoID" });
            DropIndex("dbo.DocenteAssociadoes", new[] { "DocenteID" });
            DropIndex("dbo.DocenteAssociadoes", new[] { "TrabalhoID" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.IdentityUserClaims");
            DropTable("dbo.ApplicationUsers");
            DropTable("dbo.TrabalhoFiles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.IdentityRoles");
            DropTable("dbo.EstadoTrabalhoes");
            DropTable("dbo.EstadosDoTrabalhoes");
            DropTable("dbo.EventoCategorias");
            DropTable("dbo.Eventoes");
            DropTable("dbo.EstadosDoEventoes");
            DropTable("dbo.EstadoEventoes");
            DropTable("dbo.Estudantes");
            DropTable("dbo.Trabalho");
            DropTable("dbo.Funcaos");
            DropTable("dbo.DocenteAssociadoes");
            DropTable("dbo.Docentes");
            DropTable("dbo.catFiles");
        }
    }
}
