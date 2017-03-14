using System;
using Autenticacao.Dominio.Entidades;

namespace Autenticacao.Dominio.Repositorios
{
    public interface IAppKeyRepositorio
    {
        AppKey Obter(Guid chave);
    }
}