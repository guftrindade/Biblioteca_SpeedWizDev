namespace ProjetoFinal.Services.Auth.JWT.Interfaces
{
    public interface IJwtAuthGerenciador
    {
        JwtAuthModelo GerarToken(JwtCredenciais credenciais);
    }
}
