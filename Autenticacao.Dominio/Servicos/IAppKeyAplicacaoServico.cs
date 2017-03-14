using System;
using Autenticacao.Dominio.Entidades;

namespace Autenticacao.Dominio.Servicos
{
    public interface IAppKeyAplicacaoServico
    {
        AppKey Obter(Guid chave);
    }
}