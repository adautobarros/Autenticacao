using System.Data.Entity.ModelConfiguration;
using Autenticacao.Dominio.Entidades;

namespace Autenticacao.Persistencia.Map
{
    public class UsuarioMap : EntityTypeConfiguration<Usuario>
    {
        //public UsuarioMap()
        //{
        //    ToTable("tab_usuarios");

        //    HasKey(c => c.Codigo)
        //        .Property(c => c.Codigo)
        //        .HasColumnName("CodigoUsuario");

        //    Property(c => c.Login)
        //        .HasColumnName("Login");

        //    Property(c => c.Senha)
        //       .HasColumnName("Senha")
        //       .HasMaxLength(50);

        //    Property(c => c.Email)
        //       .HasColumnName("Email")
        //       .HasMaxLength(60);

        //    Property(c => c.Nome)
        //       .HasColumnName("Nome")
        //       .HasMaxLength(150);

        //    Property(x => x.Status)
        //        .HasColumnName("Status_usuario")
        //        .IsRequired();
        //}

        public UsuarioMap()
        {
            ToTable("tab_usuarios");

            HasKey(c => c.Codigo)
                .Property(c => c.Codigo)
                .HasColumnName("cod_user");

            Property(c => c.Login)
                .HasColumnName("chapa_inteligente");

            Property(c => c.Senha)
               .HasColumnName("senha")
               .HasMaxLength(50);

            Property(c => c.Email)
               .HasColumnName("email")
               .HasMaxLength(60);

            Property(c => c.Nome)
               .HasColumnName("nome")
               .HasMaxLength(50);


            Property(x => x.Status)
                .HasColumnName("status_usuario")
                .IsRequired();
            
        }
    }
}
