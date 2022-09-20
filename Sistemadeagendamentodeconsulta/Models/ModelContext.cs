using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace Sistemadeagendamentodeconsulta.Models
{
    public partial class ModelContext : DbContext
    {
        public ModelContext()
        {
        }

        public ModelContext(DbContextOptions<ModelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Agendamento> Agendamento { get; set; }
        public virtual DbSet<StatusEmail> StatusEmail { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                          .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                          .AddJsonFile("appsettings.json")
                          .Build();
                optionsBuilder.UseOracle(configuration.GetConnectionString("FiapSistemaAgendamento"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:DefaultSchema", "RM92195");

            modelBuilder.Entity<Agendamento>(entity =>
            {
                entity.ToTable("AGENDAMENTO");

                entity.HasIndex(e => e.Id)
                    .HasName("SYS_C002627157")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("NUMBER(38)");

                entity.Property(e => e.Data)
                    .HasColumnName("DATA")
                    .HasColumnType("DATE");

                entity.Property(e => e.Horario)
                    .HasColumnName("HORARIO")
                    .HasColumnType("TIMESTAMP(6)");

                entity.Property(e => e.UsuarioId)
                    .HasColumnName("USUARIO_ID")
                    .HasColumnType("NUMBER(38)");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Agendamento)
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SYS_C002627158");
            });

            modelBuilder.Entity<StatusEmail>(entity =>
            {
                entity.ToTable("STATUS_EMAIL");

                entity.HasIndex(e => e.Id)
                    .HasName("SYS_C002627260")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("NUMBER(38)");

                entity.Property(e => e.EmailEnviado)
                    .HasColumnName("EMAIL_ENVIADO")
                    .HasColumnType("CHAR(1)");

                entity.Property(e => e.UsuarioId)
                    .HasColumnName("USUARIO_ID")
                    .HasColumnType("NUMBER(38)");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.StatusEmail)
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SYS_C002627261");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("USUARIO");

                entity.HasIndex(e => e.Id)
                    .HasName("SYS_C002627153")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("NUMBER(38)");

                entity.Property(e => e.Cpf)
                    .HasColumnName("CPF")
                    .HasColumnType("VARCHAR2(14)");

                entity.Property(e => e.DataNasc)
                    .HasColumnName("DATA_NASC")
                    .HasColumnType("VARCHAR2(10)");

                entity.Property(e => e.Email)
                    .HasColumnName("EMAIL")
                    .HasColumnType("VARCHAR2(200)");

                entity.Property(e => e.Nome)
                    .HasColumnName("NOME")
                    .HasColumnType("VARCHAR2(200)");

                entity.Property(e => e.RegistroProfissional)
                    .HasColumnName("REGISTRO_PROFISSIONAL")
                    .HasColumnType("VARCHAR2(20)");

                entity.Property(e => e.Senha)
                    .HasColumnName("SENHA")
                    .HasColumnType("VARCHAR2(50)");
            });

            modelBuilder.HasSequence("ISEQ$$_1636658");

            modelBuilder.HasSequence("ISEQ$$_1636660");

            modelBuilder.HasSequence("SQ_TBL_ESTABELECIMENTO");
        }
    }
}
