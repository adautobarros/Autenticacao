namespace Autenticacao.Dominio.Commands.Usuario
{
    public class AlterarSenhaUsuarioCommand
    {
        public long Login { get; set; }
        public string SenhaAtual { get; set; }
        public string NovaSenha { get; set; }
        public string ConfirmaSenha { get; set; }

        public AlterarSenhaUsuarioCommand(long login, string senhaAtual, string novaSenha, string confirmaNovaSenha)
        {
            Login = login;
            SenhaAtual = senhaAtual;
            NovaSenha = novaSenha;
            ConfirmaSenha = confirmaNovaSenha;
        }
    }
}
