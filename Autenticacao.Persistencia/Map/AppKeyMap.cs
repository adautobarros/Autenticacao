using System.Data.Entity.ModelConfiguration;
using Autenticacao.Dominio.Entidades;

namespace Autenticacao.Persistencia.Map
{
    internal class AppKeyMap : EntityTypeConfiguration<AppKey>
    {
        public AppKeyMap()
        {
            ToTable("tab_APPKEY");
            HasKey(x => x.Codigo);

            Property(x => x.Codigo).HasColumnName("Id");
            Property(x => x.Nome).HasColumnName("Nome").HasColumnType("varchar");
            Property(x => x.Chave).HasColumnName("chave");
            Property(x => x.Ativo).HasColumnName("ativo").HasColumnType("bit");
        }
    }
}