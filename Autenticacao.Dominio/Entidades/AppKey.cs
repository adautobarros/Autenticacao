using System;
using Autenticacao.Dominio.Entidades.Base;

namespace Autenticacao.Dominio.Entidades
{
    public class AppKey : Entidade<int>
    {
        public string Nome { get; set; }
        public Guid Chave { get; set; }
        public bool Ativo { get; set; }
    }
}