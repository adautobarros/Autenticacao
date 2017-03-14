using System;
using System.Data.Entity;
using Autenticacao.Dominio.Entidades;
using Autenticacao.Persistencia.Map;

namespace Autenticacao.Persistencia.DataContexts
{

    public class DataContext : DbContext
    {
        static DataContext()
        {
            Database.SetInitializer<DataContext>(null);
        }
        public DataContext() :
            base("DbConnectionString")
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<AppKey> AppKeys { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            MapGlobal(modelBuilder);
            MappTables(modelBuilder);

        }

        private static void MapGlobal(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<string>()
                          .Configure(p => p.HasColumnType("varchar"));

            modelBuilder.Properties<int>()
              .Configure(p => p.HasColumnType("int"));

            modelBuilder.Properties<DateTime>()
                .Configure(p => p.HasColumnType("datetime"));

            modelBuilder.Properties<long>()
               .Configure(p => p.HasColumnType("bigint"));

            modelBuilder.Properties<decimal>()
               .Configure(p => p.HasColumnType("decimal"));

            modelBuilder.Properties<DateTime>()
                .Configure(p => p.HasColumnType("date"));
        }

        private static void MappTables(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UsuarioMap());
            modelBuilder.Configurations.Add(new AppKeyMap());
        }
    }
}
