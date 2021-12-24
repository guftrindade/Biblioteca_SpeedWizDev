using System.ComponentModel.DataAnnotations;

namespace ProjetoFinal.Services.Auth.JWT
{
    public class JwtUsuarioCredenciais
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Senha { get; set; }
    }
}
