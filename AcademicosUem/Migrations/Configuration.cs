namespace AcademicosUem.Migrations
{
    using AcademicosUem.Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AcademicosUem.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AcademicosUem.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Roles.AddOrUpdate(
              p => p.Name,
              new IdentityRole { Name = "Admin" },
              new IdentityRole { Name = "RA" },
              new IdentityRole { Name = "CC" },
              new IdentityRole { Name = "MI" }
            );
            context.catFiles.AddOrUpdate(c => c.Designacao,
  new catFiles { Designacao = "Protocolo" },
  new catFiles { Designacao = "Tese" }
);
            context.EventoCategoria.AddOrUpdate(e => e.descricao,
new EventoCategoria { descricao = "Defesa" }
);
            context.EstadoEvento.AddOrUpdate(e => e.Designacao,
new EstadoEvento { Designacao = "Agendado" },
new EstadoEvento { Designacao = "Cancelado" },
new EstadoEvento { Designacao = "Realizado" }
);
            context.EstadoTrabalho.AddOrUpdate(e => e.Designacao,
new EstadoTrabalho { Designacao = "Pendente" },
new EstadoTrabalho { Designacao = "Protocolo Submetido" },
new EstadoTrabalho { Designacao = "Tese Submetida" },
new EstadoTrabalho { Designacao = "Defesa Marcada" },
new EstadoTrabalho { Designacao = "Aguarda Retificacao" },
new EstadoTrabalho { Designacao = "Publicado" }
);
            context.Funcao.AddOrUpdate(c => c.Designacao,
  new Funcao { Designacao = "Juri" },
  new Funcao { Designacao = "Oponente" },
  new Funcao { Designacao = "Presidente" },
  new Funcao { Designacao = "Supervisor" },
  new Funcao { Designacao = "Co-supervisor" }



);
        }
    }
}
