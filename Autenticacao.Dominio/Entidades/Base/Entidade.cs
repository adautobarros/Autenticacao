namespace Autenticacao.Dominio.Entidades.Base
{
    public abstract class Entidade<T> where T : struct
    {
        public T Codigo { get; protected set; }
    }
}
