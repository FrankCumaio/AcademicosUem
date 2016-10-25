namespace AcademicosUem.Migrations.ApplicationDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "SCOTT.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "SCOTT.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("SCOTT.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("SCOTT.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "SCOTT.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "SCOTT.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("SCOTT.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "SCOTT.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("SCOTT.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("SCOTT.AspNetUserRoles", "UserId", "SCOTT.AspNetUsers");
            DropForeignKey("SCOTT.AspNetUserLogins", "UserId", "SCOTT.AspNetUsers");
            DropForeignKey("SCOTT.AspNetUserClaims", "UserId", "SCOTT.AspNetUsers");
            DropForeignKey("SCOTT.AspNetUserRoles", "RoleId", "SCOTT.AspNetRoles");
            DropIndex("SCOTT.AspNetUserLogins", new[] { "UserId" });
            DropIndex("SCOTT.AspNetUserClaims", new[] { "UserId" });
            DropIndex("SCOTT.AspNetUsers", "UserNameIndex");
            DropIndex("SCOTT.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("SCOTT.AspNetUserRoles", new[] { "UserId" });
            DropIndex("SCOTT.AspNetRoles", "RoleNameIndex");
            DropTable("SCOTT.AspNetUserLogins");
            DropTable("SCOTT.AspNetUserClaims");
            DropTable("SCOTT.AspNetUsers");
            DropTable("SCOTT.AspNetUserRoles");
            DropTable("SCOTT.AspNetRoles");
        }
    }
}
