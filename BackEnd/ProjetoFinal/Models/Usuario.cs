using System;

namespace ProjetoFinal.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public Role Role { get; set; }
        public int CodigoRole { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public DateTime CriadoEm { get; set; }
    }
}
