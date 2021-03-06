﻿using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AcademicosUem.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        public virtual DbSet<catFiles> catFiles { get; set; }
        public virtual DbSet<Docente> Docente { get; set; }
        public virtual DbSet<DocenteAssociado> DocenteAssociado { get; set; }
        public virtual DbSet<EstadoEvento> EstadoEvento { get; set; }
        public virtual DbSet<EstadosDoEvento> EstadosDoEvento { get; set; }
        public virtual DbSet<EstadosDoTrabalho> EstadosDoTrabalho { get; set; }
        public virtual DbSet<EstadoTrabalho> EstadoTrabalho { get; set; }
        public virtual DbSet<EstadoTrabalhoFile> EstadoTrabalhoFile { get; set; }
        public virtual DbSet<EstudanteDetails> Estudante { get; set; }
        public virtual DbSet<Evento> Evento { get; set; }
        public virtual DbSet<EventoCategoria> EventoCategoria { get; set; }
        public virtual DbSet<Funcao> Funcao { get; set; }
        public virtual DbSet<Trabalho> Trabalho { get; set; }
        public virtual DbSet<TrabalhoFiles> TrabalhoFiles { get; set; }
      
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<IdentityUserRole>()
            .HasKey(r => new { r.UserId, r.RoleId })
            .ToTable("AspNetUserRoles");

            modelBuilder.Entity<IdentityUserLogin>()
                .HasKey(l => new { l.LoginProvider, l.ProviderKey, l.UserId })
                .ToTable("AspNetUserLogins");
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

            }
}