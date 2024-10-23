using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace SysCoco._0.Models
{
    public class syscocoContext : DbContext
    {
        public syscocoContext(DbContextOptions<syscocoContext> options) : base(options)
        {
        }

        public DbSet<Roles> Roles { get; set; }
        public DbSet<Actividad> Actividad { get; set; }
        public DbSet<Asistencia> Asistencia { get; set; }
        public DbSet<Buzon> Buzon { get; set; }
        public DbSet<Comunicado> Comunicado { get; set; }
        public DbSet<Cursos> Cursos { get; set; }
        public DbSet<Encuesta> Encuesta { get; set; }
        public DbSet<Materia> Materia { get; set; }
        public DbSet<Mensaje> Mensaje { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Votacion> Votacion { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Actividad>()
                      .HasOne(ac => ac.cursos)
                      .WithMany()
                      .HasForeignKey(ac => ac.cursoid)
                      .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Actividad>()
                      .HasOne(ac => ac.ActividadUsuarios)
                      .WithMany()
                      .HasForeignKey(ac => ac.creador)
                      .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Asistencia>()
                      .HasOne(a => a.FkUsuarios)
                      .WithMany()
                      .HasForeignKey(a => a.usuariosId)
                      .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Asistencia>()
                      .HasOne(a => a.Curso)
                      .WithMany()
                      .HasForeignKey(a => a.CursoId)
                      .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Buzon>()
                      .HasOne(b => b.buzonUsuario)
                      .WithMany()
                      .HasForeignKey(b => b.usuariosId)
                      .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comunicado>()
                      .HasOne(c => c.comunicadoRemitente)
                      .WithMany()
                      .HasForeignKey(c => c.remitente)
                      .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comunicado>()
                      .HasOne(c => c.comunicadoDestinatario)
                      .WithMany()
                      .HasForeignKey(c => c.destinatario)
                      .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Cursos>()
                      .HasOne(cu => cu.materia)
                      .WithMany()
                      .HasForeignKey(cu => cu.materiaid)
                      .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Encuesta>()
                      .HasOne(e => e.EncuestaUsuario)
                      .WithMany()
                      .HasForeignKey(e => e.usuarioId)
                      .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Materia>()
                      .HasOne(m => m.materiaUsuarios)
                      .WithMany()
                      .HasForeignKey(m => m.usuariosid)
                      .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Mensaje>()
                      .HasOne(me => me.mensajeRemitente)
                      .WithMany()
                      .HasForeignKey(me => me.remitente)
                      .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Mensaje>()
                      .HasOne(me => me.mensajeDestinatario)
                      .WithMany()
                      .HasForeignKey(me => me.destinatario)
                      .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Usuarios>()
                    .HasOne(u => u.roles)
                    .WithMany()
                    .HasForeignKey(u => u.rolesid)
                    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Votacion>()
                    .HasOne(v => v.votacionUsuario)
                    .WithMany()
                    .HasForeignKey(v => v.usuarioId)
                    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Votacion>()
                    .HasOne(v => v.Encuesta)
                    .WithMany()
                    .HasForeignKey(v => v.encuestaId)
                    .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
